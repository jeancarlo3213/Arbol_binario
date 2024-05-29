namespace Arbol_binario.Pages
{
 
    public class ArbolBinario
    {
        public NodoArbol? Raiz { get; private set; }

        public ArbolBinario()
        {
            Raiz = null;
        }

        public void Insertar(object informacion)
        {
            Raiz = InsertarRecursivo(Raiz, informacion);
        }

        private NodoArbol InsertarRecursivo(NodoArbol? nodo, object informacion)
        {
            if (nodo == null)
            {
                return new NodoArbol(informacion);
            }

            if ((int)informacion < (int)nodo.Informacion)
            {
                nodo.RamaIzquierda = InsertarRecursivo(nodo.RamaIzquierda, informacion);
            }
            else if ((int)informacion > (int)nodo.Informacion)
            {
                nodo.RamaDerecha = InsertarRecursivo(nodo.RamaDerecha, informacion);
            }
            else
            {
                // Si el valor ya existe, no se inserta. Puedes ajustar esto según la política de duplicados que prefieras.
                return nodo;
            }

            return nodo;
        }

        public bool Buscar(object informacion)
        {
            return BuscarRecursivo(Raiz, informacion);
        }

        private bool BuscarRecursivo(NodoArbol? nodo, object informacion)
        {
            if (nodo == null) return false;

            if ((int)informacion == (int)nodo.Informacion) return true;

            if ((int)informacion < (int)nodo.Informacion)
            {
                return BuscarRecursivo(nodo.RamaIzquierda, informacion);
            }
            else
            {
                return BuscarRecursivo(nodo.RamaDerecha, informacion);
            }
        }

        public void Eliminar(object informacion)
        {
            Raiz = EliminarRecursivo(Raiz, informacion);
        }

        private NodoArbol? EliminarRecursivo(NodoArbol? nodo, object informacion)
        {
            if (nodo == null) return nodo;

            if ((int)informacion < (int)nodo.Informacion)
            {
                nodo.RamaIzquierda = EliminarRecursivo(nodo.RamaIzquierda, informacion);
            }
            else if ((int)informacion > (int)nodo.Informacion)
            {
                nodo.RamaDerecha = EliminarRecursivo(nodo.RamaDerecha, informacion);
            }
            else
            {
                if (nodo.RamaIzquierda == null)
                {
                    return nodo.RamaDerecha;
                }
                else if (nodo.RamaDerecha == null)
                {
                    return nodo.RamaIzquierda;
                }

                nodo.Informacion = MinValor(nodo.RamaDerecha);
                nodo.RamaDerecha = EliminarRecursivo(nodo.RamaDerecha, nodo.Informacion);
            }

            return nodo;
        }

        private object MinValor(NodoArbol nodo)
        {
            object minVal = nodo.Informacion;
            while (nodo.RamaIzquierda != null)
            {
                minVal = nodo.RamaIzquierda.Informacion;
                nodo = nodo.RamaIzquierda;
            }
            return minVal;
        }

        // Métodos para los recorridos en preorden, inorden y postorden, tanto recursivo como iterativo
        public NodoArbol? RecorridoPreOrdenRecursivo()
        {
            var resultado = new NodoArbol();
            var current = resultado;
            RecorridoPreOrdenRecursivo(Raiz, ref current);
            return resultado.Siguiente;
        }

        private void RecorridoPreOrdenRecursivo(NodoArbol? nodo, ref NodoArbol current)
        {
            if (nodo == null) return;

            current.Siguiente = new NodoArbol(nodo.Informacion);
            current = current.Siguiente;

            RecorridoPreOrdenRecursivo(nodo.RamaIzquierda, ref current);
            RecorridoPreOrdenRecursivo(nodo.RamaDerecha, ref current);
        }

        public NodoArbol? RecorridoInOrdenRecursivo()
        {
            var resultado = new NodoArbol();
            var current = resultado;
            RecorridoInOrdenRecursivo(Raiz, ref current);
            return resultado.Siguiente;
        }

        private void RecorridoInOrdenRecursivo(NodoArbol? nodo, ref NodoArbol current)
        {
            if (nodo == null) return;

            RecorridoInOrdenRecursivo(nodo.RamaIzquierda, ref current);

            current.Siguiente = new NodoArbol(nodo.Informacion);
            current = current.Siguiente;

            RecorridoInOrdenRecursivo(nodo.RamaDerecha, ref current);
        }

        public NodoArbol? RecorridoPostOrdenRecursivo()
        {
            var resultado = new NodoArbol();
            var current = resultado;
            RecorridoPostOrdenRecursivo(Raiz, ref current);
            return resultado.Siguiente;
        }

        private void RecorridoPostOrdenRecursivo(NodoArbol? nodo, ref NodoArbol current)
        {
            if (nodo == null) return;

            RecorridoPostOrdenRecursivo(nodo.RamaIzquierda, ref current);
            RecorridoPostOrdenRecursivo(nodo.RamaDerecha, ref current);

            current.Siguiente = new NodoArbol(nodo.Informacion);
            current = current.Siguiente;
        }

        public NodoArbol? RecorridoPreOrdenIterativo()
        {
            if (Raiz == null) return null;

            NodoArbol resultado = new NodoArbol();
            NodoArbol actualResultado = resultado;

            NodoArbol[] pila = new NodoArbol[100];
            int top = 0;

            pila[top++] = Raiz;

            while (top > 0)
            {
                var nodo = pila[--top];

                actualResultado.Siguiente = new NodoArbol(nodo.Informacion);
                actualResultado = actualResultado.Siguiente;

                if (nodo.RamaDerecha != null)
                {
                    pila[top++] = nodo.RamaDerecha;
                }
                if (nodo.RamaIzquierda != null)
                {
                    pila[top++] = nodo.RamaIzquierda;
                }
            }

            return resultado.Siguiente;
        }

        public NodoArbol? RecorridoInOrdenIterativo()
        {
            if (Raiz == null) return null;

            NodoArbol resultado = new NodoArbol();
            NodoArbol actualResultado = resultado;

            NodoArbol? actual = Raiz;
            NodoArbol[] pila = new NodoArbol[100];
            int top = 0;

            while (actual != null || top > 0)
            {
                while (actual != null)
                {
                    pila[top++] = actual;
                    actual = actual.RamaIzquierda;
                }

                actual = pila[--top];

                actualResultado.Siguiente = new NodoArbol(actual.Informacion);
                actualResultado = actualResultado.Siguiente;

                actual = actual.RamaDerecha;
            }

            return resultado.Siguiente;
        }

        public NodoArbol? RecorridoPostOrdenIterativo()
        {
            if (Raiz == null) return null;

            NodoArbol resultado = new NodoArbol();
            NodoArbol actualResultado = resultado;

            NodoArbol? actual = Raiz;
            NodoArbol[] pila = new NodoArbol[100];
            NodoArbol[] visitados = new NodoArbol[100];
            int top = 0, topVisitados = 0;

            pila[top++] = actual;

            while (top > 0)
            {
                actual = pila[--top];

                if (actual.RamaIzquierda != null && !Array.Exists(visitados, n => n == actual.RamaIzquierda))
                {
                    pila[top++] = actual;
                    pila[top++] = actual.RamaIzquierda;
                }
                else if (actual.RamaDerecha != null && !Array.Exists(visitados, n => n == actual.RamaDerecha))
                {
                    pila[top++] = actual;
                    pila[top++] = actual.RamaDerecha;
                }
                else
                {
                    visitados[topVisitados++] = actual;
                    actualResultado.Siguiente = new NodoArbol(actual.Informacion);
                    actualResultado = actualResultado.Siguiente;
                }
            }

            return resultado.Siguiente;
        }

        // Método para verificar si el árbol es un ABB
        public bool EsArbolBinarioDeBusqueda(NodoArbol? nodo, object? min, object? max)
        {
            if (nodo == null)
                return true;

            if ((min != null && (int)nodo.Informacion <= (int)min) || (max != null && (int)nodo.Informacion >= (int)max))
                return false;

            return EsArbolBinarioDeBusqueda(nodo.RamaIzquierda, min, nodo.Informacion) &&
                   EsArbolBinarioDeBusqueda(nodo.RamaDerecha, nodo.Informacion, max);
        }
    }
}
