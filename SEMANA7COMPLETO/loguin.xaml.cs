﻿using SEMANA7COMPLETO.modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEMANA7COMPLETO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loguin : ContentPage
    {
        private SQLiteAsyncConnection con;

        public loguin()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
        }
        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario,string contrasena)
        {
            return db.Query<Estudiante>("SELECT * FROM Estudiante where usuario=? and contrasena=?",usuario,contrasena);
        }
        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try
           {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtContrasena.Text);
                if (resultado.Count()>0)
                {
                    Navigation.PushAsync(new ConsultaRegistro());
                }
                else
                {
                    DisplayAlert("Alerta", "No existe el Usuario", "Cerrar");
                }

           }
            catch(Exception ex)
            {
                DisplayAlert("Alerta",ex.Message,"ok");
            }

        }

        private async void btnRegrisgtar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Regristro());

        }
    }
}