using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApis.Code;
using XamarinApis.Views;

namespace XamarinApis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainDoctores : MasterDetailPage
    {
        public MainDoctores()
        {
            InitializeComponent();
        }
    }
}