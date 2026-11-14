using CapaPresentacion.Vistas;
using System.Windows;
using System.Windows.Controls;
namespace CapaPresentacion
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Productos_Click(object sender, RoutedEventArgs e)
        {
            txtEstado.Text = "Mantenimiento de productos abierto";
            ProductoWindow ventana = new ProductoWindow();
            ventana.Owner = this; // se centra sobre esta ventana
            ventana.ShowDialog(); // modal: bloquea el menú hasta cerrarla
            txtEstado.Text = "Listo";
        }
        private void AcercaDe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sistema de Ventas\nWPF - N-Capas - Singleton",
            "Acerca de", MessageBoxButton.OK,
            MessageBoxImage.Information);
        }
        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}