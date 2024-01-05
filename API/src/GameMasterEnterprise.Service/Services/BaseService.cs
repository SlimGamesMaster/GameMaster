using FluentValidation;
using FluentValidation.Results;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Domain.Notificacoes;

namespace GameMasterEnterprise.Service.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        public string GerarToken()
        {
            try
            {
                const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                const int comprimentoToken = 50;

                char[] token = new char[comprimentoToken];

                using (var gerador = new System.Security.Cryptography.RNGCryptoServiceProvider())
                {
                    byte[] bytes = new byte[comprimentoToken];

                    gerador.GetBytes(bytes);

                    for (int i = 0; i < comprimentoToken; i++)
                    {
                        token[i] = caracteresPermitidos[bytes[i] % caracteresPermitidos.Length];
                    }
                }

                return new string(token);
            }
            catch (Exception ex)
            {
                Notificar("Erro ao gerar o Token " + ex.Message);
                return null;
            }
        }
    }
}