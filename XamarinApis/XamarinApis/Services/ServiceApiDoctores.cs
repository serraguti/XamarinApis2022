using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XamarinApis.Models;
using System.Net.Http;

namespace XamarinApis.Services
{
    public class ServiceApiDoctores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDoctores()
        {
            this.UrlApi = "https://apicruddoctores.azurewebsites.net/";
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Doctor>> GetDoctoressAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores";
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    List<Doctor> data =
                        await response.Content.ReadAsAsync<List<Doctor>>();
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
