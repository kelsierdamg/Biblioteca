using System.Collections.ObjectModel;

namespace Biblioteca;

public partial class Consulta : ContentPage
{
    ObservableCollection<Libro> libros;
    List<string> filtroActual;
    List<LibroViewModel> titulosFiltrados;

    public Consulta()
    {
        InitializeComponent();
        libros = ((App)Application.Current).Libros;
        autorRadioButton.IsChecked = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Actualizar la lista cada vez que aparece la página
        ActualizarFiltro();
    }

    private void ActualizarFiltro()
    {
        if (autorRadioButton.IsChecked)
            filtroActual = libros.Select(l => l.Autor).Distinct().OrderBy(a => a).ToList();
        else
            filtroActual = libros.Select(l => l.Editorial).Distinct().OrderBy(e => e).ToList();

        filtroCollectionView.ItemsSource = filtroActual;
        
        // Limpiar las selecciones cuando cambia el filtro
        filtroCollectionView.SelectedItem = null;
    }

    private void OnRadioChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value) // Solo actualizar cuando el RadioButton se marca (no cuando se desmarca)
        {
            ActualizarFiltro();
            titulosCollectionView.ItemsSource = null;
            portadaImage.Source = null;
        }
    }

    private void OnFiltroSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var seleccionado = e.CurrentSelection.FirstOrDefault() as string;
        if (seleccionado == null)
        {
            titulosCollectionView.ItemsSource = null;
            portadaImage.Source = null;
            return;
        }

        List<Libro> librosFiltrados;
        if (autorRadioButton.IsChecked)
            librosFiltrados = libros.Where(l => l.Autor == seleccionado).ToList();
        else
            librosFiltrados = libros.Where(l => l.Editorial == seleccionado).ToList();

        // Convertir a ViewModels para mostrar Autor o Editorial según el filtro
        titulosFiltrados = librosFiltrados.Select(l => new LibroViewModel
        {
            Libro = l,
            AutorOEditorial = autorRadioButton.IsChecked ? l.Autor : l.Editorial
        }).ToList();

        titulosCollectionView.ItemsSource = titulosFiltrados;
        titulosCollectionView.SelectedItem = null;
        portadaImage.Source = null;
    }

    private void OnTituloSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var seleccionado = e.CurrentSelection.FirstOrDefault() as LibroViewModel;
        if (seleccionado == null)
        {
            portadaImage.Source = null;
            return;
        }

        portadaImage.Source = seleccionado.Libro.ImagenPortada;
    }

    // Clase auxiliar para el binding en la vista
    public class LibroViewModel
    {
        public Libro Libro { get; set; }
        public string Titulo => Libro?.Titulo ?? string.Empty;
        public string AutorOEditorial { get; set; }
        public ImageSource Portada => Libro?.ImagenPortada;
    }
}