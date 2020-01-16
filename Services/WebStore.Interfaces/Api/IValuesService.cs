using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebStore.Interfaces.Api
{
    public interface IValuesService
    {
        IEnumerable<String> Get();
        Task<IEnumerable<String>> GetAsync();
        string Get(int id);
        Task<string> GetAsync(int id);
        Uri Post(string value);
        Task<Uri> PostAsync(string value);
        HttpStatusCode Put(int id, string value);
        Task<HttpStatusCode> PutAsync(int id, string value);
        HttpStatusCode Delete(int id);
        Task<HttpStatusCode> DeleteAsync(int id);
    }
}
