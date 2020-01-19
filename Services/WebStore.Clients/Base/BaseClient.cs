using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;
        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration config, String ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;
            _Client = new HttpClient 
            {
                BaseAddress = new Uri(config["ClientAddress"]) 
            };
            var headers = _Client.DefaultRequestHeaders.Accept;
            headers.Clear();
            headers.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default) =>
            await _Client.DeleteAsync(url, Cancel);

        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;

        protected async Task<T> GetAsync<T>(string url, CancellationToken Cancel = default) where T : new()
        {
            var response = await _Client.GetAsync(url, Cancel);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>(Cancel);

            return new T();
        }

        protected HttpResponseMessage Post<T>(string url, T value) => PostAsync<T>(url, value).Result;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException">Если получен некорректный ответ</exception>
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken Cancel = default)
        {
            var response = await _Client.PostAsJsonAsync<T>(url, value, Cancel);
            return response.EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException">Если получен некорректный ответ</exception>
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken Cancel = default)
        {
            var response = await _Client.PutAsJsonAsync<T>(url, value, Cancel);
            return response.EnsureSuccessStatusCode();
        }
        #region Dispose


        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _Client.Dispose();
            }
            _disposed = true;
        }
        #endregion
    }
}
