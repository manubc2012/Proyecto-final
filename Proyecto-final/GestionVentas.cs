using System;
using System.Collections.Generic;

namespace TiendaBarrio
{
    public class GestionVentas
    {
        private static Venta?[] matrizVentas = new Venta?[15];
        
        private static bool ventasInicializadas = false;
        
        public static void MostrarMenuGestionVentas()
        {
            if (!ventasInicializadas)
            {
                InicializarMatrizVentas();
                ventasInicializadas = true;
            }
            
            int opcion;
            
            do
            {
                MostrarOpcionesMenuVentas();
                opcion = LeerYValidarOpcionVentas();
                ProcesarOpcionVentas(opcion);
                
            } while (opcion != 2);
        }
        
        private static void InicializarMatrizVentas()
        {
            for (int i = 0; i < 15; i++)
            {
                matrizVentas[i] = null;
            }
        }
        
        private static void MostrarOpcionesMenuVentas()
        {
            Console.WriteLine("\n=== MENÚ GESTIÓN VENTAS ===");
            Console.WriteLine("1. Generar factura");
            Console.WriteLine("2. Salir de Gestión Ventas");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }
        
        private static int LeerYValidarOpcionVentas()
        {
            int opcion;
            
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    if (opcion >= 1 && opcion <= 2)
                    {
                        return opcion;
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una opción del menú válida");
                        Console.Write("Seleccione una opción: ");
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida");
                    Console.Write("Seleccione una opción: ");
                }
            }
        }
        
        private static void ProcesarOpcionVentas(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    GenerarFactura();
                    break;
                case 2:
                    Console.WriteLine("Regresando al Menú Principal...\n");
                    break;
            }
        }
        
        private static void GenerarFactura()
        {
            Console.WriteLine("\n=== GENERAR FACTURA ===");
            
            if (!GestionUsuarios.HayUsuariosRegistrados())
            {
                Console.WriteLine("No hay usuarios registrados en el sistema.");
                Console.WriteLine("Por favor, vaya a 'Gestión de usuarios' y registre algunos usuarios primero.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            if (!GestionArticulos.HayArticulosRegistrados())
            {
                Console.WriteLine("No hay artículos registrados en el sistema.");
                Console.WriteLine("Por favor, vaya a 'Gestión de artículos' y agregue algunos artículos primero.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Usuario? comprador = BuscarYElegirComprador();
            if (comprador == null)
            {
                return;
            }
            
            List<ProductoFactura> productosFactura = ElegirProductosCompra();
            if (productosFactura.Count == 0)
            {
                Console.WriteLine("No se seleccionaron productos. Factura cancelada.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Venta nuevaVenta = new Venta(comprador.NombreCompleto, comprador.NumeroIdentificacion);
            
            foreach (var producto in productosFactura)
            {
                nuevaVenta.AgregarArticulo(producto.Articulo, producto.Cantidad);
            }
            
            GuardarVenta(nuevaVenta);
            
            MostrarFactura(comprador, productosFactura, nuevaVenta.TotalVenta);
        }
        
        private static Usuario? BuscarYElegirComprador()
        {
            Console.WriteLine("\n--- BUSCAR Y ELEGIR COMPRADOR ---");
            Console.Write("Ingrese el número de identificación del comprador: ");
            string numeroIdentificacion = Console.ReadLine() ?? "";
            
            var usuario = GestionUsuarios.BuscarUsuarioPorCedula(numeroIdentificacion);
            
            if (usuario == null)
            {
                Console.WriteLine("\nEl usuario no está registrado en el sistema.");
                Console.WriteLine("Por favor, registre al usuario en 'Gestión de usuarios' antes de generar la factura.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return null;
            }
            
            Console.WriteLine("\n--- COMPRADOR ENCONTRADO ---");
            Console.WriteLine($"Identificación: {usuario.NumeroIdentificacion}");
            Console.WriteLine($"Nombres: {usuario.Nombres}");
            Console.WriteLine($"Apellidos: {usuario.Apellidos}");
            Console.WriteLine($"Teléfono: {usuario.Telefono}");
            Console.WriteLine($"Dirección: {usuario.Direccion}");
            
            return usuario;
        }
        
        private static List<ProductoFactura> ElegirProductosCompra()
        {
            Console.WriteLine("\n--- ELEGIR PRODUCTOS PARA COMPRAR ---");
            List<ProductoFactura> productosFactura = new List<ProductoFactura>();
            
            string continuar = "s";
            int productosAgregados = 0;
            
            do
            {
                if (productosAgregados >= 10)
                {
                    Console.WriteLine("\nNo se pueden agregar más productos. Máximo 10 productos por factura.");
                    break;
                }
                
                var articulosDisponibles = ObtenerArticulosConStock();
                if (articulosDisponibles.Count == 0)
                {
                    Console.WriteLine("No hay artículos con stock disponible para la venta.");
                    break;
                }
                
                Console.WriteLine($"\nProductos agregados: {productosAgregados}/10");
                MostrarListaArticulosConStock(articulosDisponibles);
                
                Console.Write($"Seleccione un producto (1-{articulosDisponibles.Count}): ");
                int opcionArticulo = LeerYValidarOpcionArticulo(1, articulosDisponibles.Count);
                
                Articulo articuloSeleccionado = articulosDisponibles[opcionArticulo - 1];
                
                var productoExistente = productosFactura.Find(p => p.Articulo.IdArticulo == articuloSeleccionado.IdArticulo);
                if (productoExistente != null)
                {
                    Console.WriteLine("Este producto ya fue agregado a la factura. Elija otro producto.");
                    continue;
                }
                
                int cantidad = PedirCantidadConValidacionStock(articuloSeleccionado);
                if (cantidad > 0)
                {
                    productosFactura.Add(new ProductoFactura(articuloSeleccionado, cantidad));
                    productosAgregados++;
                    
                    decimal subtotal = cantidad * articuloSeleccionado.ValorUnitario;
                    Console.WriteLine($"\nProducto agregado exitosamente:");
                    Console.WriteLine($"- {articuloSeleccionado.Nombre}");
                    Console.WriteLine($"- Cantidad: {cantidad}");
                    Console.WriteLine($"- Subtotal: ${subtotal}");
                }
                
                if (productosAgregados < 10)
                {
                    Console.Write("\n¿Desea agregar otro producto a la factura? (s/n): ");
                    continuar = Console.ReadLine()?.ToLower() ?? "n";
                }
                else
                {
                    continuar = "n";
                }
                
            } while (continuar == "s" || continuar == "si" || continuar == "sí");
            
            return productosFactura;
        }
        
        private static List<Articulo> ObtenerArticulosConStock()
        {
            return GestionArticulos.ObtenerArticulosDisponibles();
        }
        
        private static void MostrarListaArticulosConStock(List<Articulo> articulos)
        {
            Console.WriteLine("\n--- PRODUCTOS DISPONIBLES ---");
            
            for (int i = 0; i < articulos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. ID: {articulos[i].IdArticulo} | {articulos[i].Nombre} | ${articulos[i].ValorUnitario} | Stock: {articulos[i].CantidadStock}");
            }
        }
        
        private static int PedirCantidadConValidacionStock(Articulo articulo)
        {
            Console.WriteLine($"\n--- PRODUCTO SELECCIONADO ---");
            Console.WriteLine($"ID: {articulo.IdArticulo}");
            Console.WriteLine($"Nombre: {articulo.Nombre}");
            Console.WriteLine($"Valor unitario: ${articulo.ValorUnitario}");
            Console.WriteLine($"Stock disponible: {articulo.CantidadStock}");
            
            int cantidad;
            Console.Write($"Ingrese la cantidad que desea comprar (máximo {articulo.CantidadStock}): ");
            
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out cantidad))
                {
                    if (cantidad <= 0)
                    {
                        Console.WriteLine("La cantidad debe ser mayor que 0.");
                        Console.Write($"Ingrese la cantidad que desea comprar (máximo {articulo.CantidadStock}): ");
                    }
                    else if (cantidad > articulo.CantidadStock)
                    {
                        Console.WriteLine($"No hay cantidad suficiente en stock. Stock disponible: {articulo.CantidadStock}");
                        Console.Write($"Ingrese la cantidad que desea comprar (máximo {articulo.CantidadStock}): ");
                    }
                    else
                    {
                        ActualizarStockArticulo(articulo, cantidad);
                        return cantidad;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    Console.Write($"Ingrese la cantidad que desea comprar (máximo {articulo.CantidadStock}): ");
                }
            }
        }
        
        private static void ActualizarStockArticulo(Articulo articulo, int cantidadVendida)
        {
            articulo.CantidadStock -= cantidadVendida;
        }
        
        private static void GuardarVenta(Venta venta)
        {
            for (int i = 0; i < 15; i++)
            {
                if (matrizVentas[i] == null)
                {
                    matrizVentas[i] = venta;
                    break;
                }
            }
        }
        
        private static void MostrarFactura(Usuario comprador, List<ProductoFactura> productos, decimal totalVenta)
        {
            Console.Clear();
            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("                           FACTURA DE VENTA");
            Console.WriteLine(new string('=', 70));
            
            Console.WriteLine($"Identificación: {comprador.NumeroIdentificacion}");
            Console.WriteLine($"Nombres: {comprador.Nombres}");
            Console.WriteLine($"Apellidos: {comprador.Apellidos}");
            Console.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
            Console.WriteLine(new string('-', 70));
            
            Console.WriteLine($"{"ID",-5} {"Producto",-20} {"Valor Unit.",-12} {"Cantidad",-10} {"Subtotal",-12}");
            Console.WriteLine(new string('-', 70));
            
            foreach (var producto in productos)
            {
                decimal subtotal = producto.Articulo.ValorUnitario * producto.Cantidad;
                Console.WriteLine($"{producto.Articulo.IdArticulo,-5} {producto.Articulo.Nombre,-20} ${producto.Articulo.ValorUnitario,-11} {producto.Cantidad,-10} ${subtotal,-11}");
            }
            
            Console.WriteLine(new string('-', 70));
            
            Console.WriteLine($"{"TOTAL A PAGAR:",-57} ${totalVenta}");
            Console.WriteLine(new string('=', 70));
            
            Console.WriteLine("\n¡Factura generada exitosamente!");
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static int LeerYValidarOpcionArticulo(int min, int max)
        {
            int opcion;
            
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    if (opcion >= min && opcion <= max)
                    {
                        return opcion;
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una opción del menú válida");
                        Console.Write($"Seleccione una opción ({min}-{max}): ");
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida");
                    Console.Write($"Seleccione una opción ({min}-{max}): ");
                }
            }
        }
    }
    
}