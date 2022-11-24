using SEMANA7COMPLETO.modelos;
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
    public partial class elemento : ContentPage
    {
        public int idSel;
        private SQLiteAsyncConnection con;
        IEnumerable<Estudiante> borrar;
        IEnumerable<Estudiante> actulizar;
        public elemento(int id)
        {
            InitializeComponent();
            idSel = id;
            con = DependencyService.Get<Database>().GetConnection();

        }
        public static IEnumerable<Estudiante> borrar1(SQLiteConnection db, int id)
        {

            return db.Query<Estudiante>("Delete from Estudiante where id = ?", id);
        }

        public static IEnumerable<Estudiante> actulizar1(SQLiteConnection db, int id, string nombre, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("Update Estudiante set Nombre=?, Usuario=?,contrsena=? where id = ?", nombre, usuario, contrasena, id);
        }
        //
        private void btnActualizar_Clicked(object sender, EventArgs e)
        {

            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                actulizar = actulizar1(db, idSel, txtNombre.Text, txtUsuario.Text, txtContrasena.Text);
                DisplayAlert("Mensaje", "Actualizar", "Ok");

                Navigation.PushAsync(new ConsultaRegistro());

            }
            catch (Exception)
            {

                throw;
            }


        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(databasePath);
            borrar = borrar1(db, idSel);
            DisplayAlert("Mensaje", "Eliminar", "Ok");

            Navigation.PushAsync(new ConsultaRegistro());
        }
    }
}