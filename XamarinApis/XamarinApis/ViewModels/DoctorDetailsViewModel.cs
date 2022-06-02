using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class DoctorDetailsViewModel: ViewModelBase
    {
        private ServiceApiDoctores service;

        public DoctorDetailsViewModel(ServiceApiDoctores service)
        {
            this.service = service;
        }

        public DoctorDetailsViewModel()
        {
            this.service = new ServiceApiDoctores();
        }

        public Command DeleteDoctor
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.DeleteDoctorAsync(Doctor.IdDoctor);
                    await
                    Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }

        private Doctor _Doctor;

        public Doctor Doctor
        {
            get { return this._Doctor; }
            set {
                this._Doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
    }
}
