﻿using FluentValidation;
using FluentValidation.Results;
using GameMasterEnterprise.Domain.Intefaces;
using GameMasterEnterprise.Domain.Models;
using GameMasterEnterprise.Domain.Notificacoes;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace GameMasterEnterprise.Service.Services
{
    public abstract class BaseService
    {
        private readonly HttpClient _httpClient;
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador, HttpClient httpClient)
        {
            _notificador = notificador;
            _httpClient = httpClient;
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

       public async Task<T> GetAsync<T>(string url)
       {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                return await HandleResponse<T>(response);
       }

       public async Task<T> PostAsync<T>(string url, object body)
            {
                string jsonBody = JsonSerializer.Serialize(body);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                return await HandleResponse<T>(response);
            }

       public async Task<T> PutAsync<T>(string url, object body)
            {
                string jsonBody = JsonSerializer.Serialize(body);
                HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                return await HandleResponse<T>(response);
            }

       public async Task<T> DeleteAsync<T>(string url)
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                return await HandleResponse<T>(response);
            }

       private async Task<T> HandleResponse<T>(HttpResponseMessage response)
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(jsonResponse);
                }
                else
                {
                    // Lidar com erros, lançar exceção ou retornar um objeto com informações de erro
                    throw new HttpRequestException($"Erro na requisição. Código de status: {response.StatusCode}");
                }
            }
        
    }
}