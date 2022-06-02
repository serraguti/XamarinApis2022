using System;
using System.Collections.Generic;
using System.Text;
using XamarinApis.Models;

namespace XamarinApis.Services
{
    public class SessionService
    {
        //AQUI TENDREMOS TODAS LAS PROPIEDADES 
        //DE LOS OBJETOS QUE DESEEMOS ALMACENAR
        public List<Doctor> Favoritos { get; set; }
        public SessionService()
        {
            this.Favoritos = new List<Doctor>();
        }
    }
}
