using System;

namespace ArbolConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolB.ArbolB<int> arbol = new ArbolB.ArbolB<int>(5);

            arbol.Insertar(1);
            arbol.Insertar(2);
            arbol.Insertar(3);
            arbol.Insertar(4);
            arbol.Insertar(5);
            arbol.Insertar(6);
            arbol.Insertar(7);
            arbol.Insertar(8);
            arbol.Insertar(9);

            arbol.eliminar(6);
            arbol.eliminar(7);

            //arbol.Insertar(7);
            //arbol.Insertar(8);
            //arbol.Insertar(9);
            //arbol.Insertar(10);
            //arbol.Insertar(11);
            //arbol.Insertar(12);
            //arbol.Insertar(13);
            //arbol.Insertar(14);
            //arbol.Insertar(15);
            //arbol.Insertar(16);
            //arbol.Insertar(17);
            //arbol.Insertar(18);
            //ListaDobleEnlace.ListaDoble<int> In_Order = arbol.Recorrido("In_Order");
            //ListaDobleEnlace.ListaDoble<int> Pre_Order= arbol.Recorrido("Pre_Order");
            //ListaDobleEnlace.ListaDoble<int> Post_Order = arbol.Recorrido("Post_Order");
            //arbol.eliminar(3);

            //arbol.Insertar(10);
            //arbol.Insertar(54);
            //arbol.Insertar(25);
            //arbol.Insertar(81);
            //arbol.Insertar(86);
            //arbol.Insertar(87);
            //arbol.Insertar(9);
            //arbol.Insertar(74);
            //arbol.Insertar(51);
            //arbol.Insertar(47);
            //arbol.Insertar(46);
            //arbol.Insertar(12);
            //arbol.Insertar(16);

            //arbol.Insertar(34);
            //arbol.Insertar(36);
            //arbol.Insertar(96);
            //arbol.Insertar(44);
            //arbol.Insertar(6);
            //arbol.Insertar(19); 
            //arbol.Insertar(81);
            //arbol.Insertar(64);
            //arbol.Insertar(81);
            //arbol.Insertar(21);
            //arbol.Insertar(60);
            //arbol.Insertar(50);
            //arbol.Insertar(90);

            //arbol.Recorrido("In_Order");
            // arbol.Insertar(44);
            /*arbol.Insertar(45);
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
            arbol.Insertar(50);
            arbol.Insertar(1);
            arbol.Insertar(3);
            arbol.Insertar(4);
            arbol.Insertar(6);
            arbol.Insertar(7);
            arbol.Insertar(9);
            arbol.Insertar(10);//
            arbol.Insertar(12);
            arbol.Insertar(13);
            arbol.Insertar(14);
            arbol.Insertar(16);
            arbol.Insertar(17);
            arbol.Insertar(23);
            arbol.Insertar(25);
            arbol.Insertar(26);
            arbol.Insertar(29);
            arbol.Insertar(32);
            arbol.Insertar(33);
            arbol.Insertar(34);*/
            //arbol.Imprimir();

            //arbol.eliminar(44);
            //arbol.eliminar(92);
            //arbol.eliminar(20);
            //arbol.eliminar(18);
            //arbol.eliminar(40);
            //arbol.eliminar(99);
            //arbol.eliminar(8);

            //arbol.eliminar(11);

            //arbol.eliminar(18);
            //arbol.eliminar(19);
            //arbol.eliminar(99);


            //    arbol.eliminar(100);
            //arbol.eliminar(99);
            //arbol.eliminar(102);
            //arbol.eliminar(18);
            //arbol.eliminar(19);
            ////arbol.eliminar(24);

            arbol.Imprimir();
            Console.ReadKey();
        }
    }
}
