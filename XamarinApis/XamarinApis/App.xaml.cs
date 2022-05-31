using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinApis.Views;

namespace XamarinApis
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new CochesView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
