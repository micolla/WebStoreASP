using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;
        private readonly string _ServiceAddress;

        public BaseClient(IConfiguration config, String ServiceAddress)
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
    }
}
