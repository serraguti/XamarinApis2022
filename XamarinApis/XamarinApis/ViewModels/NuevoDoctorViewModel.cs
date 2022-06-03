using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class NuevoDoctorViewModel: ViewModelBase
    {
        private ServiceApiDoctores service;

        public NuevoDoctorViewModel(ServiceApiDoctores service)
        {
            this.service = service;
            this.Doctor = new Doctor();
        }

        public Command InsertarDoctor
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.CreateDoctor
                    (this.Doctor.Apellido, this.Doctor.Especialidad
                    , this.Doctor.IdHospital, this.Doctor.Salario);
                    await
                    Application.Current.MainPage
                    .DisplayAlert("Alert", "Doctor insertado", "Ok");
                });
            }
        }

        private Doctor _Doctor;

        public Doctor Doctor
        {
            get { return this._Doctor; }
            set
            {
                this._Doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
    }
}
