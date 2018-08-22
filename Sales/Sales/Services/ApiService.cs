using System;
using System.Threading.Tasks;
using Sales.Common.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// sirve para consumir cualquie lista de objetos es generica
/// 
/// </summary>

namespace Sales.Services
{
    class ApiService
    {
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                if(response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,

                    };
                }


                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,

                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };

                throw;
            }
        }
    }
}
