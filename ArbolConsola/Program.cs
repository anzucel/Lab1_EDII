using System;

namespace ArbolConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolB.ArbolB<int> arbol = new ArbolB.ArbolB<int>(3);

            arbol.Insertar(18);
            arbol.Insertar(19);
            arbol.Insertar(20);
            arbol.Insertar(24);
            arbol.Insertar(40);
            arbol.Insertar(42);
            //arbol.Insertar(44);
            //arbol.Insertar(45);
            //arbol.Insertar(50);

            //arbol.eliminar(44);
            arbol.eliminar(18);
            //arbol.eliminar(45);
            arbol.eliminar(40);
            //arbol.eliminar(24);
            arbol.eliminar(42);
            arbol.eliminar(24);
            //arbol.eliminar(20);


            // arbol.Insertar(12);
            // arbol.Insertar(12);
            // arbol.Insertar(63);
            // arbol.Insertar(40);
            // arbol.Insertar(73);
            // arbol.Insertar(78);
            // arbol.Insertar(59);
            // arbol.Insertar(57);
            // arbol.Insertar(57);
            // arbol.Insertar(29);
            // arbol.Insertar(51);
            // arbol.Insertar(71);
            // arbol.Insertar(98);
            // arbol.Insertar(19);
            // arbol.Insertar(44);
            // arbol.Insertar(24);
            // arbol.Insertar(72);
            // arbol.Insertar(80);
            // arbol.Insertar(41);
            // arbol.Insertar(25);
            // arbol.Insertar(42);
            // arbol.Insertar(26);
            // arbol.Insertar(70);
            // arbol.Insertar(69);

            // arbol.eliminar(98);
            // arbol.eliminar(78);
            //arbol.eliminar(80);
            //arbol.eliminar(41);
            //arbol.eliminar(29);


            //arbol.Insertar(20);
            //arbol.Insertar(43);
            //arbol.Insertar(68);
            //arbol.Insertar(64);
            //arbol.Insertar(45);

            //arbol.eliminar(25);
            //arbol.eliminar(26);
            //arbol.eliminar(69);
            //arbol.eliminar(73);

            //arbol.Insertar(88);

            //arbol.eliminar(64);


            /*int opcion, grado, cant_datos;

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
