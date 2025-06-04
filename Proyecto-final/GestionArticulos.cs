using System;
using System.Collections.Generic;

namespace TiendaBarrio
{
    public class GestionArticulos
    {
        private static Articulo?[] matrizArticulos = new Articulo?[15];
        
        private static bool articulosInicializados = false;
        private static int contadorIdArticulo = 1;
        
        public static bool HayArticulosRegistrados()
        {
            if (!articulosInicializados)
            {
                InicializarMatrizVacia();
                articulosInicializados = true;
            }
            
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] != null)
                {
                    return true;
                }
            }
            return false;
        }
        
        public static List<Articulo> ObtenerArticulosDisponibles()
        {
            if (!articulosInicializados)
            {
                InicializarMatrizVacia();
                articulosInicializados = true;
            }
            
            List<Articulo> articulos = new List<Articulo>();
            
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] != null && matrizArticulos[i]!.CantidadStock > 0)
                {
                    articulos.Add(matrizArticulos[i]!);
                }
            }
            
            return articulos;
        }
        
        public static void MostrarMenuGestionArticulos()
        {
            if (!articulosInicializados)
            {
                InicializarMatrizVacia();
                articulosInicializados = true;
            }
            
            int opcion;
            
            do
            {
                MostrarOpcionesMenuArticulos();
                opcion = LeerYValidarOpcionArticulos();
                ProcesarOpcionArticulos(opcion);
                
            } while (opcion != 4);
        }
        
        private static void InicializarMatrizVacia()
        {
            for (int i = 0; i < 15; i++)
            {
                matrizArticulos[i] = null;
            }
        }
        
        private static void MostrarOpcionesMenuArticulos()
        {
            Console.WriteLine("\n=== MENÚ GESTIÓN DE ARTÍCULOS ===");
            Console.WriteLine("1. Ver lista de artículos");
            Console.WriteLine("2. Nuevo artículo");
            Console.WriteLine("3. Editar información del artículo (buscar por id)");
            Console.WriteLine("4. Salir de Gestión de Artículos (Menú Principal)");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }
        
        private static int LeerYValidarOpcionArticulos()
        {
            int opcion;
            
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    if (opcion >= 1 && opcion <= 4)
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
        
        private static void ProcesarOpcionArticulos(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    VerListaArticulos();
                    break;
                case 2:
                    NuevoArticulo();
                    break;
                case 3:
                    EditarInformacionArticulo();
                    break;
                case 4:
                    Console.WriteLine("Regresando al Menú Principal...\n");
                    break;
            }
        }
        
        private static void VerListaArticulos()
        {
            Console.WriteLine("\n=== LISTA DE ARTÍCULOS ===");
            
            bool hayArticulos = false;
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] != null)
                {
                    hayArticulos = true;
                    break;
                }
            }
            
            if (!hayArticulos)
            {
                Console.WriteLine("No hay artículos registrados.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int contador = 1;
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] != null)
                {
                    Console.WriteLine($"\n--- Artículo {contador} ---");
                    Console.WriteLine($"ID Artículo: {matrizArticulos[i]!.IdArticulo}");
                    Console.WriteLine($"Nombre: {matrizArticulos[i]!.Nombre}");
                    Console.WriteLine($"Valor Unitario: ${matrizArticulos[i]!.ValorUnitario}");
                    Console.WriteLine($"Cantidad en Stock: {matrizArticulos[i]!.CantidadStock}");
                    contador++;
                }
            }
            
            Console.WriteLine($"\nTotal de artículos registrados: {contador - 1}/15");
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static void NuevoArticulo()
        {
            Console.WriteLine("\n=== CREAR NUEVO ARTÍCULO ===");
            
            int posicionDisponible = -1;
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] == null)
                {
                    posicionDisponible = i;
                    break;
                }
            }
            
            if (posicionDisponible == -1)
            {
                Console.WriteLine("No se permiten crear artículos nuevos. Se ha alcanzado el límite de 15 artículos.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine($"Artículos registrados: {ContarArticulosRegistrados()}/15");
            
            int idArticulo = contadorIdArticulo;
            contadorIdArticulo++;
            
            Console.WriteLine($"ID del nuevo artículo: {idArticulo} (generado automáticamente)");
            
            Console.Write("\nIngrese el nombre del artículo: ");
            string nombre = Console.ReadLine() ?? "";
            
            decimal valorUnitario;
            Console.Write("Ingrese el valor unitario: $");
            while (!decimal.TryParse(Console.ReadLine(), out valorUnitario) || valorUnitario <= 0)
            {
                Console.WriteLine("Por favor, ingrese un valor numérico válido mayor que 0.");
                Console.Write("Ingrese el valor unitario: $");
            }
            
            int cantidadStock;
            Console.Write("Ingrese la cantidad en stock: ");
            while (!int.TryParse(Console.ReadLine(), out cantidadStock) || cantidadStock < 0)
            {
                Console.WriteLine("Por favor, ingrese una cantidad válida (número entero mayor o igual a 0).");
                Console.Write("Ingrese la cantidad en stock: ");
            }
            
            matrizArticulos[posicionDisponible] = new Articulo(idArticulo, nombre, valorUnitario, cantidadStock);
            
            Console.WriteLine("\n¡Artículo creado exitosamente!");
            Console.WriteLine($"Posición en matriz: {posicionDisponible + 1}");
            Console.WriteLine($"ID Artículo: {idArticulo}");
            Console.WriteLine($"Nombre: {nombre}");
            Console.WriteLine($"Valor Unitario: ${valorUnitario}");
            Console.WriteLine($"Cantidad en Stock: {cantidadStock}");
            
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static int ContarArticulosRegistrados()
        {
            int contador = 0;
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] != null)
                {
                    contador++;
                }
            }
            return contador;
        }
        
        private static void EditarInformacionArticulo()
        {
            Console.WriteLine("\n=== EDITAR INFORMACIÓN DE ARTÍCULO ===");
            Console.Write("Ingrese el ID del artículo a buscar: ");
            
            int idBuscado;
            while (!int.TryParse(Console.ReadLine(), out idBuscado))
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
                Console.Write("Ingrese el ID del artículo a buscar: ");
            }
            
            bool articuloEncontrado = false;
            int indiceArticulo = -1;
            
            for (int i = 0; i < 15; i++)
            {
                if (matrizArticulos[i] != null && matrizArticulos[i]!.IdArticulo == idBuscado)
                {
                    articuloEncontrado = true;
                    indiceArticulo = i;
                    Console.WriteLine("\n¡Artículo encontrado!");
                    MostrarInformacionArticulo(matrizArticulos[i]!);
                    break;
                }
            }
            
            if (!articuloEncontrado)
            {
                Console.WriteLine("\nArtículo no encontrado");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int opcion;
            bool seRealizaronCambios = false;
            
            do
            {
                Console.WriteLine("\n--- OPCIONES DE EDICIÓN ---");
                Console.WriteLine("1. Editar nombre");
                Console.WriteLine("2. Editar valor unitario");
                Console.WriteLine("3. Editar cantidad en stock");
                Console.WriteLine("4. Eliminar artículo");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                
                opcion = LeerYValidarOpcionArticulo(1, 5);
                
                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el nuevo nombre: ");
                        string nuevoNombre = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(nuevoNombre))
                        {
                            matrizArticulos[indiceArticulo]!.Nombre = nuevoNombre;
                            Console.WriteLine("Nombre actualizado exitosamente.");
                            seRealizaronCambios = true;
                        }
                        break;
                        
                    case 2:
                        Console.Write("Ingrese el nuevo valor unitario: $");
                        decimal nuevoValor;
                        while (!decimal.TryParse(Console.ReadLine(), out nuevoValor) || nuevoValor <= 0)
                        {
                            Console.WriteLine("Por favor, ingrese un valor numérico válido mayor que 0.");
                            Console.Write("Ingrese el nuevo valor unitario: $");
                        }
                        matrizArticulos[indiceArticulo]!.ValorUnitario = nuevoValor;
                        Console.WriteLine("Valor unitario actualizado exitosamente.");
                        seRealizaronCambios = true;
                        break;
                        
                    case 3:
                        Console.Write("Ingrese la nueva cantidad en stock: ");
                        int nuevaCantidad;
                        while (!int.TryParse(Console.ReadLine(), out nuevaCantidad) || nuevaCantidad < 0)
                        {
                            Console.WriteLine("Por favor, ingrese una cantidad válida (número entero mayor o igual a 0).");
                            Console.Write("Ingrese la nueva cantidad en stock: ");
                        }
                        matrizArticulos[indiceArticulo]!.CantidadStock = nuevaCantidad;
                        Console.WriteLine("Cantidad en stock actualizada exitosamente.");
                        seRealizaronCambios = true;
                        break;
                        
                    case 4:
                        Console.Write("¿Está seguro que desea eliminar este artículo? (s/n): ");
                        string confirmacion = Console.ReadLine()?.ToLower() ?? "";
                        if (confirmacion == "s" || confirmacion == "si" || confirmacion == "sí")
                        {
                            matrizArticulos[indiceArticulo] = null;
                            Console.WriteLine("Artículo eliminado exitosamente.");
                            seRealizaronCambios = true;
                            opcion = 5;
                        }
                        break;
                        
                    case 5:
                        if (seRealizaronCambios)
                        {
                            Console.WriteLine("Saliendo del menú de edición. Los cambios han sido guardados.");
                        }
                        else
                        {
                            Console.WriteLine("Saliendo del menú de edición sin realizar cambios.");
                        }
                        break;
                }
                
                if (opcion != 4 && opcion != 5 && matrizArticulos[indiceArticulo] != null)
                {
                    Console.WriteLine("\n--- INFORMACIÓN ACTUALIZADA ---");
                    MostrarInformacionArticulo(matrizArticulos[indiceArticulo]!);
                }
                
            } while (opcion != 5 && opcion != 4);
            
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static void MostrarInformacionArticulo(Articulo articulo)
        {
            Console.WriteLine($"ID Artículo: {articulo.IdArticulo}");
            Console.WriteLine($"Nombre: {articulo.Nombre}");
            Console.WriteLine($"Valor Unitario: ${articulo.ValorUnitario}");
            Console.WriteLine($"Cantidad en Stock: {articulo.CantidadStock}");
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