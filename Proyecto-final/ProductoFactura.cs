namespace TiendaBarrio
{
    public class ProductoFactura
    {
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        
        public ProductoFactura(Articulo articulo, int cantidad)
        {
            Articulo = articulo;
            Cantidad = cantidad;
        }
        
        public decimal Subtotal => Articulo.ValorUnitario * Cantidad;
        
        public override string ToString()
        {
            return $"ID: {Articulo.IdArticulo}, {Articulo.Nombre}, Cantidad: {Cantidad}, Subtotal: ${Subtotal}";
        }
    }
}