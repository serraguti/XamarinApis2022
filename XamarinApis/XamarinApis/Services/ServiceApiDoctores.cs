using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XamarinApis.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace XamarinApis.Services
{
    public class ServiceApiDoctores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDoctores(IConfiguration configuration)
        {
            this.UrlApi = configuration["ApiUrls:ApiDoctores"];
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Doctor>> GetDoctoressAsync()
        {
            string request = "/api/doctores";
            List<Doctor> doctors =
                await this.CallApiAsync<List<Doctor>>(request);
            return doctors;
        }

        public async Task<Doctor> FindDoctorAsync(int id)
        {
            string request = "/api/doctores/" + id;
            Doctor doctor = await
                this.CallApiAsync<Doctor>(request);
            return doctor;
        }

        public async Task DeleteDoctorAsync(int idDoctor)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores/" + idDoctor;
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(uri);
            }
        }

        public async Task CreateDoctor(string apellido, 
            string especialidad, int idHospital, int salario)
        {
            Doctor doctor = new Doctor()
            {
                 IdDoctor = 0,
                 Apellido = apellido,
                 Especialidad = especialidad,
                 IdHospital = idHospital,
                 Salario = salario
            };
            String json = JsonConvert.SerializeObject(doctor);
            StringContent content = new StringContent
                (json, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/doctores";
                Uri uri = new Uri(this.UrlApi + request);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PostAsync(uri, content);
            }
        }

        public async Task UpdateDoctor(int idDoctor
            , string apellido, string especialidad, int idhospital
            , int salario)
        {
            Doctor doctor = new Doctor
            {
                IdDoctor = idDoctor,
                Apellido = apellido,
                Especialidad = especialidad,
                IdHospital = idhospital,
                Salario = salario
            };
            String json = JsonConvert.SerializeObject(doctor);
            StringContent content = new StringContent
                (json, Encoding.UTF8, "application/json");
            string request = "/api/doctores";
            Uri uri = new Uri(this.UrlApi + request);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.PutAsync(uri, content);
            }
        }
    }
}
