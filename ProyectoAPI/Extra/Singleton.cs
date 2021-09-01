using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoAPI;
using ArbolB;
using ProyectoAPI.Models;

namespace ProyectoAPI.Extra
{
    public sealed class Singleton
    {
        private readonly static Singleton instance = new Singleton();
        int grado = 3;
        public ArbolB<Pelicula> ABPeliculas;
        ListaDobleEnlace.ListaDoble<Pelicula> ListaPeliculas;

        private Singleton()
        {
           
        }
      
        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
