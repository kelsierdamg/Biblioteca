namespace Biblioteca;

public partial class Alta : ContentPage
{
	private string portadaPath = string.Empty;
    public Alta()
	{
		InitializeComponent();
	}

	private async void OnGuardarButtonClicked(object sender, EventArgs e)
	{
		string titulo = tituloEntry.Text;
        string autor = autorEntry.Text;
        string editorial = editorialEntry.Text;
		string portada = portadaPath;
        Libro nuevoLibro = new Libro(titulo, autor, editorial, portada);

		((App)Application.Current).Libros.Add(nuevoLibro);

		OnLimpiarButtonClicked(sender, e);
        await DisplayAlert("�xito", "Libro guardado correctamente", "OK");
    }
	private void OnLimpiarButtonClicked(object sender, EventArgs e)
	{
		tituloEntry.Text = string.Empty;
		autorEntry.Text = string.Empty;
		editorialEntry.Text = string.Empty;
		portadaPath = string.Empty;
		portadaImagen.Source = null;
    }
	private async void OnImagenButtonClicked(object sender, EventArgs e)
	{ 	
		try
		{
			var result = await FilePicker.PickAsync(new PickOptions
			{
				PickerTitle = "Seleccione una imagen de portada",
				FileTypes = FilePickerFileType.Images
			});
			if (result != null)
			{
				portadaPath = result.FullPath;
				portadaImagen.Source = ImageSource.FromFile(portadaPath);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"No se pudo seleccionar la imagen: {ex.Message}", "OK");
		}
    }
}