using System;
using ListaDobleEnlace;
using System.Collections.Generic;

namespace ArbolB
{
    public class ArbolB<L> where L: IComparable
    {
        private static int grado; //grado del arbol, se enviará dentro del constructor
        private NodoArbol<L> raiz; 

        private class NodoArbol<T> where T : IComparable {
            public bool hoja = true;
            public int Cant_valores = 0; //Registro de la cantidad de valores que tiene cierto nodo, va a incrementar

            public ListaDoble<T> Listahoja = new ListaDoble<T>();

            public NodoArbol<T>[] hijo = new NodoArbol<T>[grado+1]; // referencia a los hijos de un nodo, n 

            public NodoArbol<T> padre; // referencia hacia el nodo padre

            public NodoArbol(NodoArbol<T> padre) {
                this.padre = padre;
            }

            public void InsertarValor(T valor) //inserta dentro de nodo hoja
            {
                bool insertar = true;
                for (int i = 0; i <= this.Listahoja.contador; i++)
                {
                    if (valor.CompareTo(Listahoja.ObtenerValor(i)) == 0)
                    {
                        insertar = false;
                        break;
                    }
                }

                if (insertar)
                {
                    if (Cant_valores <= grado)
                    {
                        this.Listahoja.InsertarInicio(valor);
                        Cant_valores++;
                    }
                    if (Cant_valores > 1)
                        Ordenar();
                }
            }

            private void Ordenar()
            {
                T temporal;
                for (int i = 0; i < Listahoja.contador-1; i++)
                {
                    for (int j = i+1; j < Listahoja.contador; j++)
                    {
                        if (Listahoja.ObtenerValor(i).CompareTo(Listahoja.ObtenerValor(j)) > 0)
                        {
                            temporal = Listahoja.ExtraerEnPosicion(i).Valor;
                            Listahoja.InsertarEnPosicion(Listahoja.ExtraerEnPosicion(j-1).Valor, i);
                            Listahoja.InsertarEnPosicion(temporal, j);
                        }
                    }
                }
            }

            public void ImprimirNodo()
            {
                Console.Write("[ ");
                for (int i = 0; i < Cant_valores; i++)
                {
                    Console.Write(Listahoja.ObtenerValor(i) + " ");
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }

        public ArbolB(int Grado)
        {
            grado = Grado;
            raiz = new NodoArbol<L>(null); //raiz no tiene padre 
        }

        public void Insertar(L valor) 
        {
            raiz = InsertarNodo(valor, raiz); // se modifica la referencia del objeto
        }

        private NodoArbol<L> InsertarNodo(L valor, NodoArbol<L> temporal) //inserta o encuentra la ruta correcta
        {
            //inserta si el nodo es hoja
            if (temporal.hoja)
            {
                temporal.InsertarValor(valor);
            }
            else
            {
                // busca la posicion correcta para insertar el valor en el hijo correcto
                bool posicion_valida = false;
                for (int i = 0; i < temporal.Cant_valores; i++)
                {
                    if (valor.CompareTo(temporal.Listahoja.ObtenerValor(i)) < 0)
                    {
                        posicion_valida = true;
                        InsertarNodo(valor, temporal.hijo[i]);
                        break;
                    }
                }
                if (!posicion_valida)
                {
                    InsertarNodo(valor, temporal.hijo[temporal.Cant_valores]);
                }
            }

            //verificar la cantidad de valores en el nodo hoja, 
            if (temporal.Cant_valores == grado)
            {
                // nodo raíz lleno
                if (temporal.padre == null)
                {
                    temporal = SepararPadre(temporal);
                }
                else
                {
                    // toma el valor medio y lo sube al padre
                    L valor_medio = temporal.Listahoja.ObtenerValor(grado / 2);
                    temporal.padre.InsertarValor(valor_medio); 
                    int posicion = 0;
                    //encuentra la posición desde donde debe separar
                    for (int i = 0; i < temporal.padre.Cant_valores; i++)
                    {
                        if (temporal.padre.Listahoja.ObtenerValor(i).CompareTo(valor_medio) == 0)
                        {
                            posicion = i;
                        }
                    }
                    //entra si el nodo padre es el que se debe dividir 
                    if (temporal.padre.Cant_valores == grado) 
                    {
                        for (int i = temporal.padre.Cant_valores; i > posicion; i--)
                        {
                            temporal.padre.hijo[i] = temporal.padre.hijo[i - 1];
                        }

                        NodoArbol<L> pivote = temporal;

                        // se crea el nuevo hijo con referencia al nodo padre 
                        temporal.padre.hijo[posicion + 1] = new NodoArbol<L>(temporal.padre);
                        for (int i = (grado / 2) + 1; i < grado; i++)
                        {
                            temporal.padre.hijo[posicion + 1].InsertarValor(temporal.Listahoja.ObtenerValor(i));
                            if (!pivote.hoja)
                            {
                                for (int j = (grado / 2) + 1, k = 0; j <= grado; j++, k++)
                                {
                                    temporal.padre.hijo[posicion + 1].hijo[k] = pivote.hijo[j];
                                    if (pivote.hijo[j] != null)
                                    {
                                        temporal.padre.hijo[posicion + 1].hoja = false;
                                        pivote.hijo[j].padre = temporal.padre.hijo[posicion + 1];
                                    }
                                }
                            }
                        }

                        NodoArbol<L> aux = temporal;
                        temporal.padre.hijo[posicion] = new NodoArbol<L>(temporal.padre);
                        for (int i = 0; i < grado / 2; i++)
                        {
                            temporal.padre.hijo[posicion].InsertarValor(aux.Listahoja.ObtenerValor(i));
                            if (!pivote.hoja)
                            {
                                for (int j = 0, k =0 ; j < grado/2 + 1; j++, k++)
                                {
                                    temporal.padre.hijo[posicion].hijo[k] = pivote.hijo[j];
                                    if (pivote.hijo[j] != null)
                                    {
                                        temporal.padre.hijo[posicion].hoja = false;
                                        pivote.hijo[j].padre = temporal.padre.hijo[posicion];
                                    }
                                }
                            }
                        }

                        //actualiza el padre de temporal para continuar con la recursividad
                        temporal.padre = SepararPadre(temporal.padre);
                    }
                    else
                    {
                        // se duplica un nodo para actualizar cant. de hijos 
                        for (int i = temporal.padre.Cant_valores; i > posicion; i--)
                        {
                            temporal.padre.hijo[i] = temporal.padre.hijo[i - 1];
                        }

                        // se crea el nuevo hijo con referencia al nodo padre 
                        temporal.padre.hijo[posicion + 1] = new NodoArbol<L>(temporal.padre);
                        for (int i = (grado / 2) + 1; i < grado; i++)
                        {
                            temporal.padre.hijo[posicion + 1].InsertarValor(temporal.Listahoja.ObtenerValor(i));
                        }
                        NodoArbol<L> aux = temporal;
                        temporal.padre.hijo[posicion] = new NodoArbol<L>(temporal.padre);
                        for (int i = 0; i < grado / 2; i++)
                        {
                            temporal.padre.hijo[posicion].InsertarValor(aux.Listahoja.ObtenerValor(i));
                        }

                        // verificar si el nodo es nodo hoja
                        if (!temporal.hoja)
                        {
                            NodoArbol<L> pivote = temporal;

                            for (int i = 0; i < (grado / 2) + 1; i++)
                            {
                                for (int j = 0; j < pivote.hijo[i].Cant_valores; j++)
                                {
                                    temporal.padre.hijo[posicion].hijo[i] = new NodoArbol<L>(temporal.padre.hijo[posicion]);
                                    temporal.padre.hijo[posicion].hijo[i].InsertarValor(pivote.hijo[i].Listahoja.ExtraerEnPosicion(j).Valor);
                                }
                            }

                            for (int i = (grado/2) + 1, k = 0; i < grado + 1; i++, k++)
                            {
                                for (int j = 0; j < pivote.hijo[i].Cant_valores; j++)
                                {
                                    temporal.padre.hijo[posicion + 1].hijo[k] = new NodoArbol<L>(temporal.padre.hijo[posicion + 1]);
                                    temporal.padre.hijo[posicion + 1].hijo[k].InsertarValor(pivote.hijo[i].Listahoja.ExtraerEnPosicion(j).Valor);
                                }
                            }

                            temporal.padre.hijo[posicion].hoja = false;
                            temporal.padre.hijo[posicion+1].hoja = false;
                        }  
                    }
                }
            }
            return temporal;
        }

        NodoArbol<L> SepararPadre(NodoArbol<L> temporal)
        {
            NodoArbol<L> pivote = temporal;
            temporal = new NodoArbol<L>(null);
            temporal.InsertarValor(pivote.Listahoja.ObtenerValor(grado / 2)); //valor medio en el padre
            temporal.hijo[0] = new NodoArbol<L>(temporal);
            temporal.hijo[1] = new NodoArbol<L>(temporal);

            //hijo izq.
            for (int i = 0; i < grado / 2; i++)
            {
                temporal.hijo[0].InsertarValor(pivote.Listahoja.ObtenerValor(i));
            }
            //hijo der.
            for (int i = (grado / 2) + 1; i < grado; i++)
            {
                temporal.hijo[1].InsertarValor(pivote.Listahoja.ObtenerValor(i));
            }
            temporal.hoja = false;

            if (!pivote.hoja)
            {
                int contador = 0;
                for (int i = 0, j = 0 - ((grado / 2) + 1); i <= grado; i++, j++)
                {
                    if (i <= grado/2)
                    {
                        temporal.hijo[0].hijo[i] = new NodoArbol<L>(temporal.hijo[0]);
                        temporal.hijo[0].hijo[i] = pivote.hijo[contador];
                        pivote.hijo[contador].padre = temporal.hijo[0];
                    }
                    else
                    {
                        temporal.hijo[1].hijo[j] = new NodoArbol<L>(temporal.hijo[1]);
                        temporal.hijo[1].hijo[j] = pivote.hijo[contador];
                        pivote.hijo[contador].padre = temporal.hijo[1];
                    }
                    contador++;
                }

                temporal.hijo[0].hoja = false;
                temporal.hijo[1].hoja = false;
            }
            return temporal;
        }





        public void Imprimir()
        {
            Imprimir(raiz, 0);
        }

        void Imprimir(NodoArbol<L> nodo, int nivel)
        {
            Console.Write("Nivel " + nivel + ": ");
            nodo.ImprimirNodo();
            for (int i = 0; i < grado; i++)
            {
                if (nodo.hijo[i] != null)
                {
                    Imprimir(nodo.hijo[i], nivel + 1);
                }
            }
        }
    }
}
