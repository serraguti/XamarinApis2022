using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinApis.Base;
using XamarinApis.Code;
using XamarinApis.Views;

namespace XamarinApis.ViewModels
{
    public class MainDoctoresViewModel: ViewModelBase
    {
        public MainDoctoresViewModel()
        {
            ObservableCollection<MasterPageItem> menu =
                new ObservableCollection<MasterPageItem>
                {
                    new MasterPageItem
                    {
                     Titulo = "Doctores",
                     Tipo = typeof(DoctoresView),
                     Icono = "sable2.png"
                    },
                    new MasterPageItem
                    {
                        Titulo = "Favoritos",
                        Tipo = typeof(DoctoresFavoritosView),
                        Icono = "bb8.png"
                    },
                    new MasterPageItem
                    {
                        Titulo = "Nuevo doctor",
                        Tipo = typeof(NuevoDoctorView),
                        Icono = "clone.png"
                    }
                };
            this.MenuPrincipal = menu;
        }

        private ObservableCollection<MasterPageItem> _MenuPrincipal;

        public ObservableCollection<MasterPageItem> MenuPrincipal
        {
            get { return this._MenuPrincipal; }
            set {
                this._MenuPrincipal = value;
                OnPropertyChanged("MenuPrincipal");
            }
        }

        public Command PaginaSeleccionada
        {
            get
            {
                return new Command(async() =>
                {
                    await
                    Application.Current.MainPage.DisplayAlert
                    ("Alert", "Estoy aqui", "Ok");
                    MainDoctores masterPage = App.ServiceLocator.MainDoctoresView;
                    ListView lsv = (ListView)masterPage.FindByName("lsvMenu");
                    //var item = (MasterPageItem)
                    //  lsv.SelectedItem;
                    //var tipo = item.Tipo;
                    //masterPage.Detail =
                    //    new NavigationPage((Page)Activator.CreateInstance(tipo));
                    //masterPage.IsPresented = false;
                });
            }
        }
    }
}
