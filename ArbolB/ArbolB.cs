using System;
using ListaDobleEnlace;
using System.Collections.Generic;


namespace ArbolB
{
    public class ArbolB<L> where L : IComparable
    {
        private static int grado; //grado del arbol, se enviará dentro del constructor
        private NodoArbol<L> raiz;

        private class NodoArbol<T> where T : IComparable
        {
            public bool hoja = true;
            public int Cant_valores = 0; //Registro de la cantidad de valores que tiene cierto nodo, va a incrementar

            public ListaDoble<T> Listahoja = new ListaDoble<T>();


            public NodoArbol<T>[] hijo = new NodoArbol<T>[grado + 1]; // referencia a los hijos de un nodo, n 

            public NodoArbol<T> padre; // referencia hacia el nodo padre

            public NodoArbol(NodoArbol<T> padre)
            {
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
                for (int i = 0; i < Listahoja.contador - 1; i++)
                {
                    for (int j = i + 1; j < Listahoja.contador; j++)
                    {
                        if (Listahoja.ObtenerValor(i).CompareTo(Listahoja.ObtenerValor(j)) > 0)
                        {
                            temporal = Listahoja.ExtraerEnPosicion(i).Valor;
                            Listahoja.InsertarEnPosicion(Listahoja.ExtraerEnPosicion(j - 1).Valor, i);
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
                                for (int j = 0, k = 0; j < grado / 2 + 1; j++, k++)
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
                                temporal.padre.hijo[posicion].hijo[i] = new NodoArbol<L>(temporal.padre.hijo[posicion]);
                                for (int j = 0; j < pivote.hijo[i].Cant_valores; j++)
                                {
                                    temporal.padre.hijo[posicion].hijo[i].InsertarValor(pivote.hijo[i].Listahoja.ExtraerEnPosicion(j).Valor);

                                    if (pivote.hijo[i].hijo[j] != null)
                                    {
                                        if (!pivote.hijo[i].hoja)
                                        {
                                            for (int k = 0; k < grado; k++)
                                            {
                                                temporal.padre.hijo[posicion].hijo[i].hijo[k] = pivote.hijo[i].hijo[k];
                                                if (pivote.hijo[i].hijo[k] != null)
                                                {
                                                    temporal.padre.hijo[posicion].hijo[i].hoja = false;
                                                    pivote.hijo[i].hijo[k].padre = temporal.padre.hijo[posicion].hijo[i];
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            for (int i = (grado / 2) + 1, k = 0; i < grado + 1; i++, k++)
                            {
                                temporal.padre.hijo[posicion + 1].hijo[k] = new NodoArbol<L>(temporal.padre.hijo[posicion + 1]);
                                for (int j = 0; j < pivote.hijo[i].Cant_valores; j++)
                                {
                                    temporal.padre.hijo[posicion + 1].hijo[k].InsertarValor(pivote.hijo[i].Listahoja.ExtraerEnPosicion(j).Valor);

                                    if (pivote.hijo[i].hijo[j] != null)//
                                    {
                                        if (!pivote.hijo[i].hoja)
                                        {
                                            for (int l = 0; l < grado; l++)//
                                            {
                                                temporal.padre.hijo[posicion + 1].hijo[k].hijo[l] = pivote.hijo[i].hijo[l];
                                                if (pivote.hijo[i].hijo[l] != null)
                                                {
                                                    temporal.padre.hijo[posicion + 1].hijo[k].hoja = false;
                                                    pivote.hijo[i].hijo[l].padre = temporal.padre.hijo[posicion + 1].hijo[k];
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            temporal.padre.hijo[posicion].hoja = false;
                            temporal.padre.hijo[posicion + 1].hoja = false;
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
                    if (i <= grado / 2)
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
        //Empieza la parte de eliminación


        public void eliminar(L Valor)
        {
            NodoArbol<L> vervalor = raiz;
            NodoArbol<L> NODO_A = EliminarNodo(Valor, raiz);


            while(NODO_A.padre !=null)
            {
                if (NODO_A.padre != null)
                {
                    NODO_A = Verificacion_CantMin(NODO_A.padre);
                }
            }
                  
             return;
        }


        private NodoArbol<L> EliminarNodo(L valor, NodoArbol<L> Raiz_P)
        {
           
            //Paso 1 verificar si estamos en una hoja
            if (Raiz_P.hoja)
            {

                if (raiz == Raiz_P)//verifica si la hoja es la raiz de nuestro árbol
                {
                    Raiz_P = EliminarEnSecuencia(Raiz_P, valor);//si si es elimina en la lista
                }


                else //en este caso solo es una hoja 
                {

                    for (int i = 0; i < Raiz_P.Cant_valores; i++) //busca en la hoja el valor
                    {
                        if (Raiz_P.Listahoja.ObtenerValor(i).CompareTo(valor) == 0)
                        {
                            if (Raiz_P.Cant_valores > ((grado - 1) / 2))//Verifica si cumple con la cantidad mínima
                            {
                                Raiz_P.Listahoja.ExtraerEnPosicion(i);
                                Raiz_P.Cant_valores--;
                                return Raiz_P;
                            }


                            else //Sino cumple con la cantidad mínima  
                            {
                                //Primero verificamos si nos pueden prestar

                                int padre_R = PosicionPadre(Raiz_P.padre, valor); //obtenemos en que posición se encuentra el padre


                                //Verificación en que lado del nivel está el nodo

                                if (padre_R > 0 && padre_R <= (Raiz_P.padre.Cant_valores - 1))//Esta en el medio 
                                {
                                    if (Raiz_P.padre.hijo[padre_R - 1].Cant_valores > (grado - 1) / 2)
                                    {
                                        Raiz_P = PrestamoMedio(padre_R, Raiz_P, i, valor, "izquierdo");
                                        return Raiz_P;
                                    }
                                    else
                                    {
                                        if (Raiz_P.padre.hijo[padre_R + 1].Cant_valores > (grado - 1) / 2)
                                        {
                                            Raiz_P = PrestamoMedio(padre_R, Raiz_P, i, valor, "derecho");
                                            return Raiz_P;
                                        }
                                        else
                                        {
                                            if(Raiz_P.padre.hijo[padre_R - 1].Cant_valores <= (grado - 1) / 2 && Raiz_P.padre.hijo[padre_R + 1].Cant_valores <= (grado - 1) / 2)
                                            {
                                                Raiz_P= UnionMedio(padre_R, Raiz_P, i, valor);// si ninguno nos puede prestar hacemos unión
                                                return Raiz_P;

                                            }
                                        }
                                    }
                                }
                                

                                //esta en la orilla izquierda
                                if (padre_R == 0 && Raiz_P.padre.hijo[padre_R + 1].Cant_valores > (grado - 1) / 2)//Verifica si el valor es el primero
                                {
                                    Raiz_P = PrestamoInicio(padre_R, Raiz_P, i, valor);
                                    return Raiz_P;
                                }
                                else
                                {
                                    if (padre_R == 0 && Raiz_P.padre.hijo[padre_R + 1].Cant_valores <= (grado - 1) / 2)//Verifica si el valor es el primero
                                    {
                                        Raiz_P = UnionOrillaiz(padre_R, Raiz_P, i, valor);
                                        return Raiz_P;
                                    }
                                }



                                //esta en la orilla derecha
                                if (padre_R == (Raiz_P.padre.Cant_valores) && Raiz_P.padre.hijo[padre_R - 1].Cant_valores > (grado - 1) / 2) //Verifica si el valor es el ultimo de la lista
                                {
                                    Raiz_P = PrestamoFinal(padre_R - 1, Raiz_P, i, valor);
                                    return Raiz_P;
                                }
                                else
                                {
                                    if (padre_R == (Raiz_P.padre.Cant_valores) && Raiz_P.padre.hijo[padre_R - 1].Cant_valores <= (grado - 1) / 2)//Verifica si el valor es el primero
                                    {
                                        Raiz_P = UnionOrillader(padre_R, Raiz_P, i, valor);
                                        return Raiz_P;
                                    }
                                }
                            }            
                        }

                    }
                }


            }

            else //eliminacion de padres
            {
                bool encontrado = false;
                bool mayor = true;
                if (Raiz_P.hoja == false) //Verifica si no es hoja
                {
                    for (int i = 0; i < Raiz_P.Cant_valores; i++)//Busca si existe en la listahoja
                    {
                        if (Raiz_P.Listahoja.ObtenerValor(i).CompareTo(valor) == 0)
                        {

                            
                            if(Raiz_P.hijo[i].Cant_valores > (grado-1)/2)//Caso en el que hermano izquierdo va a prestar
                            {
                               
                                L temporal = Raiz_P.hijo[i].Listahoja.ObtenerValor(Raiz_P.hijo[i].Cant_valores-1);
                                Raiz_P.Listahoja.ObtenerValor(i, temporal);
                                Raiz_P.hijo[i].Listahoja.ExtraerFinal();
                                Raiz_P.hijo[i].Cant_valores = Raiz_P.hijo[i].Listahoja.contador;
                                
                            }
                            else
                            {
                                if(Raiz_P.hijo[i+1].Cant_valores >(grado-1)/2) //Caso en el que hermano derecho va a prestar
                                {
                                    L temporal = Raiz_P.hijo[i+1].Listahoja.ObtenerValor(0);
                                    Raiz_P.Listahoja.ObtenerValor(i, temporal);
                                    Raiz_P.hijo[i+1].Listahoja.ExtraerInicio();
                                    Raiz_P.hijo[i+1].Cant_valores = Raiz_P.hijo[i+1].Listahoja.contador;
                                    
                                }
                                else//caso que no este en la orilla
                                {
                                    L Temporal = Raiz_P.hijo[i].Listahoja.ObtenerValor(Raiz_P.hijo[i].Cant_valores-1); //extrae el ultimo valor de la lista
                                    Raiz_P.Listahoja.ObtenerValor(i, Temporal);

                                    Raiz_P = Raiz_P.hijo[i];
                                    valor = Temporal;

                                    if(i== 0)
                                    {
                                        Raiz_P =  UnionOrillaiz(i, Raiz_P, i, valor);
                                        return Raiz_P;
                                    }
                                    else
                                    {

                                        Raiz_P = UnionMedio(i, Raiz_P, i, valor);
                                        return Raiz_P;
                                        //PosicionPadre(Raiz_P.padre, Temporal);
                                        //Raiz_P = Raiz_P.padre;
                                        //if (i !=0 && Raiz_P.hijo[i-1] != null)
                                        //{

                                        //    if (Raiz_P.hijo[i - 1].Cant_valores > (grado - 1) / 2)
                                        //    {


                                        //        L Reemplazo_P = Raiz_P.Listahoja.ObtenerValor(Raiz_P.Cant_valores - 1);
                                        //        Raiz_P.Listahoja.ObtenerValor(i, Reemplazo_P);

                                        //        PrestamoFinal(i, Raiz_P, 99999, valor);
                                        //    }
                                        //    else
                                        //    {
                                        //        Raiz_P = UnionMedio(i, Raiz_P, i, valor);
                                        //        return Raiz_P;
                                        //    }

                                        //}
                                        //else
                                        //{
                                        //    Raiz_P = UnionMedio(i, Raiz_P, i, valor);
                                        //    return Raiz_P;




                                    }                                 
                                }
                            }
                            encontrado = true;
                            return Raiz_P;
                        }

                    }

                    if (encontrado != true) // Define el camino a tomar si aún no se ha encontrado
                    {
                        for (int i = 0; i < Raiz_P.Cant_valores; i++)
                        {
                            if (Raiz_P.Listahoja.ObtenerValor(i).CompareTo(valor) > 0)
                            {
                                Raiz_P = Raiz_P.hijo[i];
                                mayor = false;
                                Raiz_P = EliminarNodo(valor, Raiz_P);

                                return Raiz_P;
                            }

                        }
                        if (mayor = true)// si en el caso es el ultimo nodo 
                        {
                            Raiz_P = Raiz_P.hijo[Raiz_P.Cant_valores];
                            Raiz_P = EliminarNodo(valor, Raiz_P);
                        }
                    }
                }
                //else
                //{
                //}
            }
         
            return Raiz_P;
        }







        //metodos utilizados para distintos procesos como uniones prestamos, busquedas de padres y más...

       private NodoArbol<L> Verificacion_CantMin(NodoArbol<L> Raiz_P)
        {
            if (Raiz_P.padre != null)
            {
                if (Raiz_P.Cant_valores >= (grado - 1) / 2)
                {
                    return Raiz_P;
                }
                else
                {
                    L valor = Raiz_P.Listahoja.ObtenerValor(0);
                    int padre_R = PosicionPadre(Raiz_P.padre, valor);
                    Raiz_P=UnionPadres(padre_R, Raiz_P);

                    return Raiz_P;
                    // UnionMedio()
                    //equilibrar
                }
            }

            return Raiz_P;
        }



        private NodoArbol<L> UnionPadres(int padre_R, NodoArbol<L> Raiz_P)
        {
            
                if (padre_R == 0)
                {



                ////inseta el padre en la unión

                L ValorPadre = Raiz_P.padre.Listahoja.ObtenerValor(padre_R);
                Raiz_P.padre.hijo[padre_R].Listahoja.InsertarFinal(ValorPadre);
                Raiz_P.padre.hijo[padre_R].Cant_valores++;
                EliminarEnSecuencia(Raiz_P.padre, ValorPadre);
                int cont2 = 0;
                ////Inserta los valores del nodo a unir
                for (int i = 0; i <= Raiz_P.padre.hijo[padre_R+1].Cant_valores; i++)
                {
                    L ValorNodo = Raiz_P.padre.hijo[padre_R+1].Listahoja.ObtenerValor(0);
                    EliminarEnSecuencia(Raiz_P.padre.hijo[padre_R + 1], ValorNodo);
                    Raiz_P.padre.hijo[padre_R].Listahoja.InsertarFinal(ValorNodo);
                    Raiz_P.padre.hijo[padre_R].Cant_valores++;

                    cont2++;
                }


                //Agrega los hijos al nuevo padre unido
                NodoArbol<L> Raiz_Nueva = Raiz_P.padre.hijo[padre_R+1]; // Resultado de la Unión

                int cont = 0;
                while(Raiz_Nueva.hijo[cont] != null)
                {
                    
                    Raiz_P.hijo[(Raiz_P.Cant_valores - cont2) + cont] = Raiz_Nueva.hijo[cont];
                    cont = cont + 1;
                }

                Raiz_Nueva.padre = null;


                //if (Raiz_Nueva.padre.padre == null)
                //{
                //    raiz = Raiz_Nueva;
                //}

                //Raiz_P = Raiz_Nueva;
                //Raiz_P.padre = Raiz_P.padre.padre;
            }
                else
                  {
                  

                //inseta el padre en la unión
                    padre_R--;
                    L ValorPadre = Raiz_P.padre.Listahoja.ObtenerValor(padre_R);
                    Raiz_P.padre.hijo[padre_R].Listahoja.InsertarFinal(ValorPadre);
                    Raiz_P.padre.hijo[padre_R].Cant_valores++;
                    EliminarEnSecuencia(Raiz_P.padre, ValorPadre);

                    //Inserta los valores del nodo a unir
                    for (int i = 0; i < Raiz_P.Cant_valores; i++)
                    {
                        L ValorNodo = Raiz_P.Listahoja.ObtenerValor(0);
                        Raiz_P.padre.hijo[padre_R].Listahoja.InsertarFinal(ValorNodo);
                        Raiz_P.padre.hijo[padre_R].Cant_valores++;


                    }


                    //Agrega los hijos al nuevo padre unido
                    NodoArbol<L> Raiz_Nueva = Raiz_P.padre.hijo[padre_R]; // Resultado de la Unión
                    for (int i = 0; i <= Raiz_P.Cant_valores; i++)
                    {
                        Raiz_Nueva.hijo[(Raiz_Nueva.Cant_valores-1) + i] = Raiz_P.hijo[i];
                    }

               
                for (int i = 0; i <= Raiz_P.padre.Cant_valores; i++)
                {
                    Raiz_P.padre.hijo[padre_R + i] = Raiz_P.padre.hijo[padre_R + (i+1)];
                }


                if (Raiz_Nueva.padre.padre == null && Raiz_Nueva.padre.Cant_valores<1)
                    {
                        raiz = Raiz_Nueva;
                    }
                    
                Raiz_P = Raiz_Nueva;
                Raiz_P.padre = Raiz_P.padre.padre;
                
            }
            return Raiz_P;
        }
        int PosicionPadreNodo(NodoArbol<L> Padre, L valor) //Busca en la lista padre la posición del padre de un nodo
        {
            for (int i = 0; i < Padre.Cant_valores; i++)
            {
                if (Padre.Listahoja.ObtenerValor(i).CompareTo(valor) > 0)
                {
                    return i;
                }
            }
            return Padre.Cant_valores;
        }



        int PosicionPadre(NodoArbol<L> Padre, L valor) //Busca en la lista padre la posición del padre de un nodo
        {
            for (int i = 0; i < Padre.Cant_valores; i++)
            {
                if (Padre.Listahoja.ObtenerValor(i).CompareTo(valor) > 0)
                {
                    return i;
                }
            }
            
            return Padre.Cant_valores;
        }



        private NodoArbol<L> EliminarEnSecuencia(NodoArbol<L> Raiz_P, L valor) //Elimina un valor en hojalista
        {
            for (int i = 0; i < Raiz_P.Cant_valores; i++)
            {
                if (Raiz_P.Listahoja.ObtenerValor(i).CompareTo(valor) == 0)
                {

                    Raiz_P.Listahoja.ExtraerEnPosicion(i);
                    Raiz_P.Cant_valores--;
                    return Raiz_P;
                }
            }
            return Raiz_P;
        }



        private NodoArbol<L> PrestamoInicio(int padre_R, NodoArbol<L> Raiz_P, int i, L valor)//Presta un nodo al estar desequilibrado en la esquina izquierda
        {

            EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato
            L Temporal_P = Raiz_P.padre.Listahoja.ObtenerValor(padre_R);
            Raiz_P.padre.Listahoja.ObtenerValor(padre_R, Raiz_P.padre.hijo[padre_R + 1].Listahoja.ObtenerValor(0)); //Hace el cambio de raíz          
            Raiz_P.padre.hijo[padre_R + 1].Listahoja.ExtraerEnPosicion(0); //Elimina el dato prestado del hijo anterior
            Raiz_P.padre.hijo[padre_R + 1].Cant_valores--; //como se toma un valor del hermano se debe restar  
            Insertar(Temporal_P);//inserta el prestamo
            return Raiz_P;

        }


        private NodoArbol<L> PrestamoFinal(int padre_R, NodoArbol<L> Raiz_P, int i, L valor)//Presta un nodo al estar desequilibrado en la esquina derecha
        {
            if(i!= 99999)
            {
                EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato
            }
            
            L Temporal_P = Raiz_P.padre.Listahoja.ObtenerValor(padre_R);
            L insertar_Cambio = Raiz_P.padre.hijo[padre_R].Listahoja.ObtenerValor(Raiz_P.padre.hijo[padre_R].Cant_valores - 1);
            Raiz_P.padre.Listahoja.ObtenerValor(padre_R, insertar_Cambio); //Hace el cambio de raíz          
            Raiz_P.padre.hijo[padre_R].Listahoja.ExtraerEnPosicion(Raiz_P.padre.hijo[padre_R].Cant_valores); //Elimina el dato prestado del hijo anterior
            Raiz_P.padre.hijo[padre_R].Cant_valores--; //como se toma un valor del hermano se debe restar                     
            Insertar(Temporal_P);//inserta el prestamo
            return Raiz_P;
        }

        private NodoArbol<L> PrestamoMedio(int padre_R, NodoArbol<L> Raiz_P, int i, L valor, string hermano) //Presta un nodo al estar desequilibrado dentro del arbol, no en ninguna esquina
        {
            if (hermano == "izquierdo")
            {
                padre_R--;
                EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato
                L Temporal_P = Raiz_P.padre.Listahoja.ObtenerValor(padre_R);
                Raiz_P.padre.Listahoja.ObtenerValor(padre_R, Raiz_P.padre.hijo[padre_R].Listahoja.ObtenerValor(Raiz_P.padre.hijo[padre_R].Cant_valores - 1)); //Hace el cambio de raíz          
                Raiz_P.padre.hijo[padre_R].Listahoja.ExtraerEnPosicion(Raiz_P.padre.hijo[padre_R].Cant_valores - 1); //Elimina el dato prestado del hijo anterior
                Raiz_P.padre.hijo[padre_R].Cant_valores--; //como se toma un valor del hermano se debe restar  
                Insertar(Temporal_P);//inserta el prestamo
                return Raiz_P;
            }
            else
            {
                EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato
                L Temporal_P = Raiz_P.padre.Listahoja.ObtenerValor(padre_R);
                Raiz_P.padre.Listahoja.ObtenerValor(padre_R, Raiz_P.padre.hijo[padre_R + 1].Listahoja.ObtenerValor(0)); //Hace el cambio de raíz          
                Raiz_P.padre.hijo[padre_R + 1].Listahoja.ExtraerEnPosicion(0); //Elimina el dato prestado del hijo anterior
                Raiz_P.padre.hijo[padre_R + 1].Cant_valores--; //como se toma un valor del hermano se debe restar  
                Insertar(Temporal_P);//inserta el prestamo
                return Raiz_P;
            }

            return Raiz_P;
        }


        private NodoArbol<L> UnionOrillaiz(int padre_R, NodoArbol<L> Raiz_P, int i, L valor)//Unión de nodos en la orilla izquierda de nuestro árbol
        {
            EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato

            Raiz_P.padre.hijo[padre_R + 1].Listahoja.InsertarInicio(Raiz_P.padre.Listahoja.ObtenerValor(0));

            for (int j = 0; j < Raiz_P.Cant_valores; j++)
            {
                Raiz_P.padre.hijo[padre_R + 1].Listahoja.InsertarInicio(Raiz_P.Listahoja.ObtenerValor((Raiz_P.Cant_valores - 1) - j));
                Raiz_P.padre.hijo[padre_R + 1].Cant_valores++;
                Raiz_P.Listahoja.ExtraerFinal();

            }
            EliminarEnSecuencia(Raiz_P.padre, Raiz_P.padre.Listahoja.ObtenerValor(0));//Elimina el padre
            Raiz_P.padre.hijo[padre_R + 1].Cant_valores = Raiz_P.padre.hijo[padre_R + 1].Listahoja.contador;
            //Raiz_P.padre.Cant_valores--;

            Raiz_P = Raiz_P.padre.hijo[padre_R + 1];

            int cont = 0;
            for (int j = 0; j < (Raiz_P.padre.Cant_valores + 1); j++)
            {
                Raiz_P.padre.hijo[j] = Raiz_P.padre.hijo[j + 1];
                cont++;
            }

            Raiz_P.padre.hijo[cont] = null;

            return Raiz_P;
        }

        



        private NodoArbol<L> UnionOrillader(int padre_R, NodoArbol<L> Raiz_P, int i, L valor) //Union de nodos en la orilla derecha
        {
            EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato

            
            
            Raiz_P.padre.hijo[padre_R-1].Listahoja.InsertarFinal(Raiz_P.padre.Listahoja.ObtenerValor(Raiz_P.padre.Cant_valores-1));

            for (int j = 0; j < Raiz_P.Cant_valores; j++)
            {
                Raiz_P.padre.hijo[padre_R - 1].Listahoja.InsertarFinal(Raiz_P.Listahoja.ObtenerValor(0));
                Raiz_P.padre.hijo[padre_R - 1].Cant_valores++;
                Raiz_P.Listahoja.ExtraerInicio();

            }

            EliminarEnSecuencia(Raiz_P.padre, Raiz_P.padre.Listahoja.ObtenerValor(Raiz_P.padre.Cant_valores - 1));//Elimina el padre
            Raiz_P.padre.hijo[padre_R - 1].Cant_valores = Raiz_P.padre.hijo[padre_R - 1].Listahoja.contador;
            

            Raiz_P = Raiz_P.padre.hijo[padre_R - 1];
           
            Raiz_P.padre.hijo[Raiz_P.padre.Cant_valores+1] = null;
        
            if(Raiz_P.padre.Cant_valores==0)
            {
                raiz = Raiz_P;
            }
            
            return Raiz_P;
        }


       




        private NodoArbol<L> UnionMedio(int padre_R, NodoArbol<L> Raiz_P, int i, L valor)//Unión de nodos en el medio del árbol
        {
            EliminarEnSecuencia(Raiz_P, valor);//Elimina el dato
           

            Raiz_P.padre.hijo[padre_R - 1].Listahoja.InsertarFinal(Raiz_P.padre.Listahoja.ObtenerValor(padre_R-1));

            for (int j = 0; j < Raiz_P.Cant_valores; j++)
            {
                Raiz_P.padre.hijo[padre_R - 1].Listahoja.InsertarFinal(Raiz_P.Listahoja.ObtenerValor(0));
                Raiz_P.padre.hijo[padre_R - 1].Cant_valores++;
                Raiz_P.Listahoja.ExtraerInicio();

            }

            EliminarEnSecuencia(Raiz_P.padre, Raiz_P.padre.Listahoja.ObtenerValor(padre_R - 1));//Elimina el padre
            Raiz_P.padre.hijo[padre_R - 1].Cant_valores = Raiz_P.padre.hijo[padre_R - 1].Listahoja.contador;


            Raiz_P = Raiz_P.padre.hijo[padre_R - 1];
            int cont = padre_R;
            for (int j = padre_R; j < (Raiz_P.padre.Cant_valores + 1); j++)
            {
                Raiz_P.padre.hijo[j] = Raiz_P.padre.hijo[j + 1];
                cont++;
            }

            Raiz_P.padre.hijo[cont] = null;

            Raiz_P.padre.hijo[Raiz_P.padre.Cant_valores + 1] = null;

            if (Raiz_P.padre.Cant_valores == 0)
            {
                raiz = Raiz_P;
            }
            return Raiz_P;
        }


        
      }

    }


  