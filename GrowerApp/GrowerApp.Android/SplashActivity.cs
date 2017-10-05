using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Ioc;
using GrowerApp.Interfaces;
using GrowerApp.ViewModel;

namespace GrowerApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            System.Threading.Thread.Sleep(1000);

            //create DB
            IocContainer.CreateContainer();
            var dbSchemaService = IocContainer.Container.GetInstance<IDbSchemaService>();
            dbSchemaService.CreateDatabaseAndTables();

            this.StartActivity(typeof(MainActivity));
        }
    }
}