using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoAPI.Models 
{
    public class Pelicula
    {
        public string Director { get; set; }
        public double IMDbRating { get; set; }
        public string Genero { get; set; }
        public string Estreno { get; set; }
        public int RTM { get; set; }
        public string Titulo { get; set; }
    }
}
