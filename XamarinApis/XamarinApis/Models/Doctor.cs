using System;
using System.Collections.Generic;
using System.Text;
using XamarinApis.Helpers;

namespace XamarinApis.Models
{
    public class Doctor
    {
        public int IdDoctor { get; set; }
        public int IdHospital { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public int Salario { get; set; }

        private bool _IsFavorite;
        public bool IsFavorite
        {
            get { return this._IsFavorite; }
            set { 
                HelperUtilities helper =
                App.ServiceLocator.HelperUtilities;
                this._IsFavorite =
                    helper.IsFavoriteDoctor(this.IdDoctor);
            }
        }
    }
}
