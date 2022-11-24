using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using SEMANA7COMPLETO.modelos;
using System.Collections.ObjectModel;

namespace SEMANA7COMPLETO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private ObservableCollection<Estudiante> _TablaEstudiante;
        public ConsultaRegistro()
        {
            InitializeComponent();
            _conn = DependencyService.Get<Database>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
            Datos();
        }

        public async void Datos()
        {
            var Resultado = await _conn.Table<Estudiante>().ToListAsync();
            _TablaEstudiante = new ObservableCollection<Estudiante>(Resultado);
            ListadoEstudiante.ItemsSource = _TablaEstudiante;





        }

        private void ListadoEstudiante_ItemSelected(object sender,SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem;
            var item = obj.Id.ToString();
            var IdSeleccionado = Convert.ToInt32(item);




            try
            {
                Navigation.PushAsync(new elemento(IdSeleccionado));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}