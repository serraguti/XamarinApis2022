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
    public class DoctoresListViewModel: ViewModelBase
    {
        private ServiceApiDoctores service;

        public DoctoresListViewModel(ServiceApiDoctores service)
        {
            this.service = service;
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
    }
}
