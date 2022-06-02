using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinApis.Base;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.ViewModels
{
    public class DoctoresFavoritosViewModel: ViewModelBase
    {
        public DoctoresFavoritosViewModel
            (SessionService sessionService)
        {
            this.DoctoresFavoritos =
                new ObservableCollection<Doctor>(sessionService.Favoritos);
        }
        private ObservableCollection<Doctor> _DoctoresFavoritos;

        public ObservableCollection<Doctor> DoctoresFavoritos
        {
            get { return this._DoctoresFavoritos; }
            set {
                this._DoctoresFavoritos = value;
                OnPropertyChanged("DoctoresFavoritos");
            }
        }
    }
}
