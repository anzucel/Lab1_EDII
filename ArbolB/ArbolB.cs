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

            public NodoArbol<T>[] hijo = new NodoArbol<T>[grado]; // referencia a los hijos de un nodo, n 

            public NodoArbol<T> padre; // referencia hacia el nodo padre


            public NodoArbol(NodoArbol<T> padre) {
                this.padre = padre;
                //hijo = new ListaDoble<NodoArbol<T>>();
            }

            public void InsertarValor(T valor) //inserta dentro de nodo hoja
            {
                if(Cant_valores <= grado)
                {
                    this.Listahoja.InsertarInicio(valor);
                    Cant_valores++;
                }
                if (Cant_valores > 1)
                    Ordenar();
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
                            /*temporal = Listahoja.ObtenerValor(i);
                            Listahoja.InsertarEnPosicion(Listahoja.ObtenerValor(j), i);
                            Listahoja.InsertarEnPosicion(temporal, j);*/
                            temporal = Listahoja.ExtraerEnPosicion(i).Valor;
                            Listahoja.InsertarEnPosicion(Listahoja.ExtraerEnPosicion(j-1).Valor, i);
                            Listahoja.InsertarEnPosicion(temporal, j);
                        }
                    }
                }
            }

            /*public bool Mayor<Y>(Y valor1, Y valor2) where Y : IComparable
            {
                if (valor1.CompareTo(valor2) > 0) return true;
                return false;
            }*/
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
            if(temporal.hoja)
            {
                temporal.InsertarValor(valor);

                //verificar la cantidad de valores en el nodo hoja
                if (temporal.Cant_valores == grado)
                {
                    // nodo raíz lleno
                    if (temporal.padre == null)
                    {
                        NodoArbol<L> pivote = temporal;
                        temporal = new NodoArbol<L>(null);
                        temporal.InsertarValor(pivote.Listahoja.ObtenerValor(grado / 2)); //valor medio en el padre
                        temporal.hijo[0] = new NodoArbol<L>(temporal);
                        temporal.hijo[1] = new NodoArbol<L>(temporal);
                        //hijo izq.
                        for (int i = 0; i < grado/2; i++)
                        {
                            temporal.hijo[0].InsertarValor(pivote.Listahoja.ObtenerValor(i));
                        }
                        //hijo der.
                        for (int i = (grado/2)+1; i < grado; i++)
                        {
                            temporal.hijo[1].InsertarValor(pivote.Listahoja.ObtenerValor(i));
                        }
                        temporal.hoja = false; 
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
                        for (int i = 0; i < grado/2; i++)
                        {
                            temporal.padre.hijo[posicion].InsertarValor(aux.Listahoja.ObtenerValor(i));
                        }
                    }
                }
            }
            else
            {
                bool posicion_valida = false;
                for (int i = 0; i < temporal.Cant_valores - 1; i++)
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
            return temporal;
        }
    }
}
