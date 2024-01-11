
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Service.Services;
using System.Net.Http;

namespace GameMasterEnterprise.Service.Services
{
    public class CassinoService : BaseService, ICassinoService
    {
        private readonly ICassinoRepository _cassinoRepository;

        public CassinoService(ICassinoRepository cassinoRepository, INotificador notificador, HttpClient httpClient)
            : base(notificador, httpClient)
        {
            _cassinoRepository = cassinoRepository;
        }

        public async Task<Cassino> ObterCassino(Guid cassinoId)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorId(cassinoId);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }
            return cassinoPorId;
        }

        public async Task<Cassino> ObterCassinoPorNome(string Nome)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorNome(Nome);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }
            return cassinoPorId;
        }
        public async Task<Cassino> ObterCassinoPorToken(string Token)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorToken(Token);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }
            return cassinoPorId;
        }
        public async Task<Guid?> ObterCassinoIdPorToken(string Token)
        {
            var cassinoPorId = await _cassinoRepository.ObterPorToken(Token);

            if (cassinoPorId == null)
            {
                Notificar("Cassino não encontrado.");
                return null;
            }

            return cassinoPorId.Id;
        }
        public async Task<float> AtualizarSaldoCassino(Guid cassinoId,float valor, bool situacao)
        {
            var cassino = await _cassinoRepository.ObterPorId(cassinoId);

            var saldoAtual = cassino.Banco;

            if(situacao==true)
            {
                saldoAtual = saldoAtual + valor;
            }if(situacao == false)
            {
                saldoAtual = saldoAtual - valor;
                if(saldoAtual < 0)
                {
                    //Codigo caso cassino não tenha dinheiro suficiente
                }
            }

            cassino.Banco = saldoAtual;

            await _cassinoRepository.Atualizar(cassino);


            return saldoAtual;
        }
        public async Task CriarCassino(Cassino cassino)
        {

            var consultaCassino = await _cassinoRepository.ObterPorId(cassino.Id);
            var consultaCassino_2 = await _cassinoRepository.ObterPorNome(cassino.Nome);
            var consultaCassino_3 = await _cassinoRepository.ObterPorToken(cassino.Token);

            if (consultaCassino == null || consultaCassino_2 == null || consultaCassino_3 == null )
            {
                 await _cassinoRepository.Adicionar(cassino);
                return;
            }
            else
            {

                throw new InvalidOperationException("Cassino já cadastrado");
            }
        }
        public async Task<IEnumerable<Cassino>> ObterTodosCassinos()
        {
            return await _cassinoRepository.ObterTodos();
        }
        public async Task AtualizarCassino(Guid cassinoId, Cassino cassinoNovo)
        {
            var cassino = await _cassinoRepository.ObterPorId(cassinoId);

            if (cassino == null)
            {
                Notificar("Cassino não encontrado.");
                return;
            }


            cassino.Url = cassinoNovo.Url;
            cassino.Nome= cassinoNovo.Nome;
            cassino.Banco = cassinoNovo.Banco;
            await _cassinoRepository.Atualizar(cassino);
        }


        public async Task RemoverCassino(Guid cassinoId)
        {
            await _cassinoRepository.Remover(cassinoId);
        }

        public async Task Dispose()
        {
            _cassinoRepository?.Dispose();
        }
    }
}
