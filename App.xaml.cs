using System.Collections.ObjectModel;

namespace Biblioteca
{
    public partial class App : Application
    {
        public ObservableCollection<Libro> Libros { get; set; } = new();
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}