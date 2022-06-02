using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Helpers;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class DoctorDetailsViewModel: ViewModelBase
    {
        private ServiceApiDoctores service;
        private HelperUtilities helperUtilities;

        public DoctorDetailsViewModel(ServiceApiDoctores service
            , HelperUtilities helperUtilities)
        {
            this.service = service;
            this.helperUtilities = helperUtilities;
        }

        public Command DeleteDoctor
        {
            get
            {
                return new Command(async () =>
                {
                    await this.service.DeleteDoctorAsync(Doctor.IdDoctor);
                    //ENVIAMOS UN MENSAJE PARA RECARGAR LOS DATOS EN EL METODO
                    MessagingCenter.Send<DoctoresListViewModel>
                    (App.ServiceLocator.DoctoresListViewModel, "RELOADDOCTORES");
                    await Application.Current.MainPage
                    .DisplayAlert("Alert",
                    "Doctor " + Doctor.Apellido + " eliminado", "OK");
                    await
                    Application.Current.MainPage.Navigation.PopModalAsync();
                });
            }
        }

        public Command SeleccionarFavorito
        {
            get
            {
                return new Command(async() =>
                {
                    //BUSCAMOS LA CLASE SESSION
                    SessionService session =
                    App.ServiceLocator.SessionService;
                    session.Favoritos.Add(this.Doctor);
                    await Application.Current.MainPage
                    .DisplayAlert("Alert", "Doctor almacenado", "OK");
                });
            }
        }

        private Doctor _Doctor;

        public Doctor Doctor
        {
            get { return this._Doctor; }
            set {
                this._Doctor = value;
                //this._Doctor.IsFavorite 
                //    = this.helperUtilities.IsFavoriteDoctor(_Doctor.IdDoctor);
                OnPropertyChanged("Doctor");
            }
        }
    }
}
