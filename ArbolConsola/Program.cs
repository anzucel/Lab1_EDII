using System;

namespace ArbolConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolB.ArbolB<int> arbol = new ArbolB.ArbolB<int>(3);

            arbol.Insertar(1);
            arbol.Insertar(2);
            arbol.Insertar(3);
            arbol.Insertar(4);
            arbol.Insertar(5);
            arbol.Insertar(6);
        }
    }
}
