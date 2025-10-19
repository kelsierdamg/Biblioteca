using System.Collections.ObjectModel;

namespace Biblioteca;

public partial class Consulta : ContentPage
{
    ObservableCollection<Libro> libros;
    List<string> filtroActual;
    List<Libro> titulosFiltrados;

    public Consulta()
    {
        InitializeComponent();
        libros = ((App)Application.Current).Libros;
        autorRadioButton.IsChecked = true;
        ActualizarFiltro();
    }

    private void ActualizarFiltro()
    {
        if (autorRadioButton.IsChecked)
            filtroActual = libros.Select(l => l.Autor).Distinct().OrderBy(a => a).ToList();
        else
            filtroActual = libros.Select(l => l.Editorial).Distinct().OrderBy(e => e).ToList();

        filtroCollectionView.ItemsSource = filtroActual;
    }

    private void OnRadioChanged(object sender, CheckedChangedEventArgs e)
    {
        ActualizarFiltro();
        titulosCollectionView.ItemsSource = null;
        portadaImage.Source = null;
    }

    private void OnFiltroSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var seleccionado = e.CurrentSelection.FirstOrDefault() as string;
        if (seleccionado == null) return;

        if (autorRadioButton.IsChecked)
            titulosFiltrados = libros.Where(l => l.Autor == seleccionado).ToList();
        else
            titulosFiltrados = libros.Where(l => l.Editorial == seleccionado).ToList();

        titulosCollectionView.ItemsSource = titulosFiltrados;
        portadaImage.Source = null;
    }

    private void OnTituloSeleccionado(object sender, SelectionChangedEventArgs e)
    {
        var seleccionado = e.CurrentSelection.FirstOrDefault() as Libro;
        if (seleccionado == null) return;

        portadaImage.Source = seleccionado.ImagenPortada;
    }
}