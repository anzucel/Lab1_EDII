using System;

namespace ArbolConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolB.ArbolB<int> arbol = new ArbolB.ArbolB<int>(5);
            arbol.Insertar(12);
            arbol.Insertar(12);
            arbol.Insertar(63);
            arbol.Insertar(40);
            arbol.Insertar(73);
            arbol.Insertar(78);
            arbol.Insertar(59);
            arbol.Insertar(57);
            arbol.Insertar(57);
            arbol.Insertar(29);
            arbol.Insertar(51);
            arbol.Insertar(71);
            arbol.Insertar(98);
            arbol.Insertar(19);
            arbol.Insertar(44);
            arbol.Insertar(24);
            arbol.Insertar(72);
            arbol.Insertar(80);
            arbol.Insertar(41);
            arbol.Insertar(25);

            arbol.Imprimir();
            /*
            int opcion, grado, cant_datos;

            Console.WriteLine("Árbol B");
            Console.WriteLine("Seleccione una opción");
            Console.WriteLine("1. Insertar");
            Console.WriteLine("2. Buscar");
            Console.WriteLine("3. Eliminar");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    int numero, contador = 1;
                    Console.WriteLine("Ingrese grado del árbol");
                    grado = int.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese cantidad de datos que contendrá el árbol");
                    cant_datos = int.Parse(Console.ReadLine());
                    arbol = new ArbolB.ArbolB<int>(grado);
                    while (contador <= cant_datos)
                    {
                        Console.WriteLine("Dato " + contador + ":");
                        numero = int.Parse(Console.ReadLine());
                        arbol.Insertar(numero);
                        contador++;
                    }
                    arbol.Imprimir();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            */

            //arbol.Imprimir();

            Console.ReadKey();
        }
    }
}
