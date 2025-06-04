namespace TiendaBarrio
{
    public class Venta
    {
        public string NombreComprador { get; set; }
        public string CedulaComprador { get; set; }
        public List<ArticuloVenta> ArticulosVendidos { get; set; }
        public decimal TotalVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        
        public Venta(string nombreComprador, string cedulaComprador)
        {
            NombreComprador = nombreComprador;
            CedulaComprador = cedulaComprador;
            ArticulosVendidos = new List<ArticuloVenta>();
            TotalVenta = 0;
            FechaVenta = DateTime.Now;
        }
        
        public void AgregarArticulo(Articulo articulo, int cantidad)
        {
            decimal subtotal = articulo.Precio * cantidad;
            ArticulosVendidos.Add(new ArticuloVenta(articulo.Nombre, articulo.Precio, cantidad, subtotal));
            TotalVenta += subtotal;
        }
        
        public override string ToString()
        {
            return $"Comprador: {NombreComprador}, Total: ${TotalVenta}, Fecha: {FechaVenta:dd/MM/yyyy HH:mm}";
        }
    }
    
    public class ArticuloVenta
    {
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        
        public ArticuloVenta(string nombre, decimal precioUnitario, int cantidad, decimal subtotal)
        {
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
            Subtotal = subtotal;
        }
    }
}