﻿using System;

namespace ArbolConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolB.ArbolB<int> arbol = new ArbolB.ArbolB<int>(5);

            arbol.Insertar(18);
            arbol.Insertar(19);
            arbol.Insertar(20);
            arbol.Insertar(24);
            arbol.Insertar(40);
            arbol.Insertar(42);
            arbol.Insertar(44);
            arbol.Insertar(45);
            arbol.Insertar(80);
            arbol.Insertar(92);
            arbol.Insertar(99);
            arbol.Insertar(11);
            arbol.Insertar(8);
            arbol.Insertar(2);
            arbol.Insertar(27);
            arbol.Insertar(30);
            arbol.Insertar(31);
            arbol.Insertar(15);
            arbol.Insertar(5);
            arbol.Insertar(21);
            arbol.Insertar(22);
            arbol.Insertar(28);
            //arbol.Insertar(50);

            //arbol.eliminar(24);
            //arbol.eliminar(44);
            //arbol.eliminar(92);
            //arbol.eliminar(20);
            //arbol.eliminar(18);
            ////arbol.eliminar(40);
            //arbol.eliminar(99);
            arbol.eliminar(8);

            arbol.eliminar(11);
        arbol.eliminar(20);

            arbol.eliminar(18);
            arbol.eliminar(19);
            arbol.eliminar(42);

            //arbol.Insertar(100);
            //arbol.Insertar(101);
            //arbol.Insertar(102);

            //    arbol.eliminar(100);
            //arbol.eliminar(99);
            //arbol.eliminar(102);
            //arbol.eliminar(18);
            //arbol.eliminar(19);
            ////arbol.eliminar(24);


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
