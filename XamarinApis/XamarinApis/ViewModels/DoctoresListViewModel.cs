using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Helpers;
using XamarinApis.Models;
using XamarinApis.Services;
using XamarinApis.Views;

namespace XamarinApis.ViewModels
{
    public class DoctoresListViewModel: ViewModelBase
    {
        private ServiceApiDoctores service;
        private HelperUtilities helperUtilities;

        public DoctoresListViewModel(ServiceApiDoctores service
           , HelperUtilities helperUtilities )
        {
            this.service = service;
            this.helperUtilities = helperUtilities;
            //METODO ASINCRONO DENTRO DE CONSTRUCTOR
            Task.Run(async () =>
            {
                await this.LoadDoctoresAsync();
            });
            MessagingCenter.Subscribe<DoctoresListViewModel>
                (this, "RELOADDOCTORES", async (sender) =>
                {
                    await this.LoadDoctoresAsync();
                });
        }

        private ObservableCollection<Doctor> _Doctores;

        public ObservableCollection<Doctor> Doctores
        {
            get { return this._Doctores; }
            set
            {
                this._Doctores = value;
                OnPropertyChanged("Doctores");
            }
        }

        public Command MostrarDoctores
        {
            get
            {
                return new Command(async () =>
                {
                    await this.LoadDoctoresAsync();
                });
            }
        }

        public Command ShowDoctor
        {
            get
            {
                return new Command(async(idDoctor) =>
                {
                    //BUSCAMOS EL DOCTOR EN EL SERVICIO
                    Doctor doctor =
                    await this.service.FindDoctorAsync((int)idDoctor);
                    //CREAMOS LA VISTA DETALLES
                    DoctorDetailsView view = new DoctorDetailsView();
                    //BUSCAMOS EL VIEWMODEL DENTRO DE IoC
                    DoctorDetailsViewModel viewmodel =
                    App.ServiceLocator.DoctorDetailsViewModel;
                    //PASAMOS LOS DATOS AL VIEWMODEL
                    viewmodel.Doctor = doctor;
                    //ENLAZAMOS LA VISTA CON EL VIEWMODEL
                    view.BindingContext = viewmodel;
                    //MOSTRAMOS LA VISTA
                    await
                    Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }

        public Command MostrarFavoritos
        {
            get
            {
                return new Command(async() =>
                {
                    DoctoresFavoritosView view =
                    new DoctoresFavoritosView();
                    await
                    Application.Current.MainPage.Navigation
                    .PushModalAsync(view);
                });
            }
        }

        public Command SeleccionarFavorito
        {
            get
            {
                return new Command(async(doctor) =>
                {
                    SessionService session =
                    App.ServiceLocator.SessionService;
                    session.Favoritos.Add(doctor as Doctor);
                    await Application.Current.MainPage
                    .DisplayAlert("Alert", "Doctor almacenado", "OK");
                });
            }
        }

        public Command DeleteDoctor
        {
            get
            {
                return new Command(async (iddoctor) =>
                {
                    int id = (int)iddoctor;
                    await this.service.DeleteDoctorAsync(id);
                    await this.LoadDoctoresAsync();
                });
            }
        }

        private async Task LoadDoctoresAsync()
        {
            List<Doctor> data =
                await this.service.GetDoctoressAsync();
            //BUSCAR SI LOS DOCTORES QUE HEMOS TRAIDO SON FAVORITOS
            //foreach (Doctor doc in data)
            //{
            //    doc.IsFavorite =
            //        this.helperUtilities.IsFavoriteDoctor(doc.IdDoctor);
            //}
            this.Doctores =
            new ObservableCollection<Doctor>(data);
        }
    }
}
