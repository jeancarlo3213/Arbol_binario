namespace Arbol_binario.Pages
{
    public class NodoArbol
    {
        public NodoArbol? RamaIzquierda { get; set; }
        public object? Informacion { get; set; }
        public NodoArbol? RamaDerecha { get; set; }
        public NodoArbol? Siguiente { get; set; }

        public NodoArbol()
        {
            RamaIzquierda = null;
            Informacion = null;
            RamaDerecha = null;
            Siguiente = null;
        }

        public NodoArbol(object? informacion)
        {
            RamaIzquierda = null;
            Informacion = informacion;
            RamaDerecha = null;
            Siguiente = null;
        }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
        }
    }
}

