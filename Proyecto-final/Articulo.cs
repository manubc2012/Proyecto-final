namespace TiendaBarrio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public decimal ValorUnitario { get; set; }
        public int CantidadStock { get; set; }
        
        public Articulo(int idArticulo, string nombre, decimal valorUnitario, int cantidadStock)
        {
            IdArticulo = idArticulo;
            Nombre = nombre;
            ValorUnitario = valorUnitario;
            CantidadStock = cantidadStock;
        }
        
        public decimal Precio => ValorUnitario;
        
        public override string ToString()
        {
            return $"ID: {IdArticulo}, Nombre: {Nombre}, Precio: ${ValorUnitario}, Stock: {CantidadStock}";
        }
    }
}