using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Models;
using XamarinApis.Services;
using XamarinApis.Views;

namespace XamarinApis.ViewModels
{
    public class DoctoresListViewModel: ViewModelBase
    {
        private ServiceApiDoctores service;

        public DoctoresListViewModel(ServiceApiDoctores service)
        {
            this.service = service;
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
                    List<Doctor> data =
                    await this.service.GetDoctoressAsync();
                    this.Doctores =
                    new ObservableCollection<Doctor>(data);
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
            this.Doctores =
            new ObservableCollection<Doctor>(data);
        }
    }
}
