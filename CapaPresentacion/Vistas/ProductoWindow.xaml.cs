using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CapaEntidad;
using CapaNegocio;
namespace CapaPresentacion.Vistas
{
    // Solo conoce a Negocio. Aquí NO hay SQL.
    public partial class ProductoWindow : Window
    {
        private readonly ProductoBL _bl = new ProductoBL();
        public ProductoWindow()
        {
            InitializeComponent();
        }
        // Se ejecuta al abrir la ventana
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Listar();
            txtNombre.Focus();
        }
        private void Listar()
        {
            try
            {
                List<Producto> lista = _bl.Buscar(txtBuscar.Text);
                dgProductos.ItemsSource = lista;
                lblTotal.Text = "Total: " + lista.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message, "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // 1) Leer los controles y armar la entidad
            Producto p = new Producto();
            p.Nombre = txtNombre.Text.Trim();
            p.Categoria = (cboCategoria.SelectedItem as
            ComboBoxItem)?.Content.ToString();
            decimal.TryParse(txtPrecio.Text, out decimal precio);
            p.Precio = precio;
            p.Stock = (int)sldStock.Value;
            p.Activo = chkActivo.IsChecked == true;
            // 2) Mandar a Negocio
            try
            {
                string error = _bl.Registrar(p);
                if (error != "")
                {
                    MessageBox.Show(error, "Validación",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                    return;
                }
                MessageBox.Show("Producto guardado", "OK",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Se ejecuta cada vez que se escribe o borra una letra en el buscador
private void TxtBuscar_TextChanged(object sender,
TextChangedEventArgs e)
        {
            if (IsLoaded) // evita que se ejecute antes de abrir la ventana
Listar();
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            txtNombre.Clear();
            cboCategoria.SelectedIndex = -1;
            txtPrecio.Clear();
            sldStock.Value = 0;
            chkActivo.IsChecked = true;
            dgProductos.SelectedItem = null;
            txtNombre.Focus();
        }
        // Al hacer clic en una fila, se copian sus datos al formulario
        private void DgProductos_SelectionChanged(object sender,
        SelectionChangedEventArgs e)
        {
            if (dgProductos.SelectedItem is Producto p)
            {
                txtNombre.Text = p.Nombre;
                txtPrecio.Text = p.Precio.ToString("0.00");
                sldStock.Value = p.Stock;
                chkActivo.IsChecked = p.Activo;
                foreach (ComboBoxItem item in cboCategoria.Items)
                {
                    if (item.Content.ToString() == p.Categoria)
                        cboCategoria.SelectedItem = item;
                }
            }
        }
        // Solo deja escribir números y un punto decimal
        private void SoloDecimales(object sender, TextCompositionEventArgs e)
        {
            bool esNumero = char.IsDigit(e.Text, 0);
            bool esPunto = e.Text == "." && !txtPrecio.Text.Contains(".");
            e.Handled = !(esNumero || esPunto); // true = se bloquea la tecla
        }
    }
}
