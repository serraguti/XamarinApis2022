using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinApis.Models;
using XamarinApis.Services;

namespace XamarinApis.Helpers
{
    public class HelperUtilities
    {
        private SessionService sessionService;
        //NECESITAMOS BUSCAR EL DOCTOR EN SESSION DE FAVORITOS
        //Y DEVOLVER SI ES FAVORITO O NO
        public HelperUtilities(SessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        public bool IsFavoriteDoctor(int idDoctor)
        {
            Doctor doctor =
                this.sessionService.Favoritos.FirstOrDefault
                (x => x.IdDoctor == idDoctor);
            if (doctor == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
