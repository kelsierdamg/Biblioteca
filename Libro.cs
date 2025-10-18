using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    internal class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Portada { get; set; }

        public Libro(string titulo, string autor, string editorial, string portada)
        {
            Titulo = titulo;
            Autor = autor;
            Editorial = editorial;
            Portada = portada;
        }
    }
}
