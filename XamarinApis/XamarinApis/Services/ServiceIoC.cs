using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using XamarinApis.Helpers;
using XamarinApis.ViewModels;

namespace XamarinApis.Services
{
    public class ServiceIoC
    {
        private IContainer container;

        public ServiceIoC()
        {
            this.RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //REGISTRAMOS TODO LO QUE VAYAMOS A INYECTAR
            builder.RegisterType<ServiceApiDoctores>();
            builder.RegisterType<DoctoresListViewModel>();
            builder.RegisterType<DoctorDetailsViewModel>();
            builder.RegisterType<SessionService>().SingleInstance();
            builder.RegisterType<DoctoresFavoritosViewModel>();
            builder.RegisterType<HelperUtilities>();
            //BUSCAMOS EL FICHERO DE SETTINGS
            string resourceName = "XamarinApis.appsettings.json";
            Stream stream =
                GetType().GetTypeInfo().Assembly
                .GetManifestResourceStream(resourceName);
            //CREAMOS EL OBJETO ICONFIGURATION
            IConfiguration configuration =
                new ConfigurationBuilder().AddJsonStream(stream)
                .Build();
            //INCLUIMOS EL OBJETO CONFIGURATION DENTRO DE LA 
            //INYECCION DE DEPENDENCIAS
            builder.Register<IConfiguration>(x => configuration);
            this.container = builder.Build();
        }

        public HelperUtilities HelperUtilities
        {
            get
            {
                return this.container.Resolve<HelperUtilities>();
            }
        }

        public DoctoresFavoritosViewModel DoctoresFavoritosViewModel
        {
            get
            {
                return this.container.Resolve<DoctoresFavoritosViewModel>();
            }
        }

        //NECESITAMOS UNA PROPIEDAD PARA RECUPERAR LA SESSION
        //DESDE CUALQUIER LUGAR
        public SessionService SessionService
        {
            get
            {
                return this.container.Resolve<SessionService>();
            }
        }

        //CREAMOS LAS PROPIEDADES PARA RECUPERAR LOS VIEWMODELS 
        //DENTRO DE LAS VISTAS
        public DoctoresListViewModel DoctoresListViewModel
        {
            get
            {
                return this.container.Resolve<DoctoresListViewModel>();
            }
        }

        public DoctorDetailsViewModel DoctorDetailsViewModel
        {
            get
            {
                return this.container.Resolve<DoctorDetailsViewModel>();
            }
        }
    }
}
