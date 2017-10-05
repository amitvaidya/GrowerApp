using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GrowerApp.Droid.Impl;
using GrowerApp.Interfaces;
using SQLite;
using Path = System.IO.Path;

[assembly: Xamarin.Forms.Dependency(typeof(DbOperations))]
namespace GrowerApp.Droid.Impl
{
    public class DbOperations : IDbOperations
    {
        public SQLiteConnection GetDbConnection()
        {
            var path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, "GrowerApp.db3");
            //var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "GrowerApp.db3");
            //            if(!File.Exists(path))

            return new SQLiteConnection(path);
        }
    }
}