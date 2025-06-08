using System;

namespace TiendaBarrio
{
    class Program
    {
        static string[] ventasUsuariosIds = new string[1000];
        static string[] ventasUsuariosNombres = new string[1000];
        static string[] ventasUsuariosApellidos = new string[1000];
        static int[][] ventasArticulosIds = new int[1000][];
        static string[][] ventasArticulosNombres = new string[1000][];
        static double[][] ventasArticulosValores = new double[1000][];
        static int[][] ventasArticulosCantidades = new int[1000][];
        static double[][] ventasArticulosSubtotales = new double[1000][];
        static int[] ventasNumeroArticulos = new int[1000];
        static double[] ventasTotales = new double[1000];
        static DateTime[] ventasFechas = new DateTime[1000];
        static int cantidadVentas = 0;

        static string[] usuariosIds = new string[15];
        static string[] usuariosNombres = new string[15];
        static string[] usuariosApellidos = new string[15];
        static string[] usuariosTelefonos = new string[15];
        static string[] usuariosDirecciones = new string[15];
        static int cantidadUsuarios = 0;

        static int[] articulosIds = new int[15];
        static string[] articulosNombres = new string[15];
        static double[] articulosValorUnitario = new double[15];
        static int[] articulosCantidadStock = new int[15];
        static int cantidadArticulos = 0;
        static int siguienteIdArticulo = 1;

        static int siguienteIdFactura = 1;

        static void Main(string[] args)
        {
            bool continuarPrograma = true;
            
            Console.WriteLine("=== SISTEMA DE GESTIÓN - TIENDA DE BARRIO ===\n");
            
            while (continuarPrograma)
            {
                bool autenticado = AutenticarUsuario();
                
                if (autenticado)
                {
                    MostrarMenuPrincipal();
                    
                    Console.Write("\n¿Desea salir completamente del programa? (s/n): ");
                    string respuesta = Console.ReadLine()?.ToLower() ?? "";
                    
                    if (respuesta == "s" || respuesta == "si" || respuesta == "sí")
                    {
                        continuarPrograma = false;
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                else
                {
                    continuarPrograma = false;
                }
            }
            
            Console.WriteLine("\nGracias por usar el sistema. ¡Hasta pronto!");
        }

        static bool AutenticarUsuario()
        {
            string usuarioValido = "admin";
            string passwordValida = "123456";
            int intentosMaximos = 3;
            
            for (int intento = 1; intento <= intentosMaximos; intento++)
            {
                Console.WriteLine($"Inicio de Sesión (Intento {intento}/{intentosMaximos})");
                Console.Write("Usuario: ");
                string usuario = Console.ReadLine() ?? "";
                
                Console.Write("Contraseña: ");
                string password = Console.ReadLine() ?? "";
                
                if (usuario == usuarioValido && password == passwordValida)
                {
                    Console.WriteLine("¡Acceso concedido!\n");
                    return true;
                }
                
                Console.WriteLine("Credenciales incorrectas.\n");
            }
            
            Console.WriteLine("Máximo número de intentos alcanzado. Acceso denegado.");
            return false;
        }

        static void MostrarMenuPrincipal()
        {
            bool volverAlMenu = true;
            
            while (volverAlMenu)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Gestión de usuarios");
                Console.WriteLine("2. Gestión de artículos");
                Console.WriteLine("3. Gestión de ventas");
                Console.WriteLine("4. Salir del programa");
                Console.Write("Seleccione una opción: ");
                
                string opcion = Console.ReadLine() ?? "";
                
                switch (opcion)
                {
                    case "1":
                        GestionarUsuarios();
                        break;
                    case "2":
                        GestionarArticulos();
                        break;
                    case "3":
                        GestionarVentas();
                        break;
                    case "4":
                        volverAlMenu = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void GestionarUsuarios()
        {
            bool volverAlMenuAnterior = true;
            
            while (volverAlMenuAnterior)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ GESTIÓN USUARIOS ===");
                Console.WriteLine("1. Ver lista de usuarios");
                Console.WriteLine("2. Nuevo usuario");
                Console.WriteLine("3. Editar información de usuario");
                Console.WriteLine("4. Salir de Gestión de usuarios");
                Console.Write("Seleccione una opción: ");
                
                string opcion = Console.ReadLine() ?? "";
                
                switch (opcion)
                {
                    case "1":
                        VerListaUsuarios();
                        break;
                    case "2":
                        NuevoUsuario();
                        break;
                    case "3":
                        EditarUsuario();
                        break;
                    case "4":
                        volverAlMenuAnterior = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        
        static void VerListaUsuarios()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE USUARIOS ===");
            
            if (cantidadUsuarios == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                Console.WriteLine("ID\tIdentificación\tNombres\t\t\tApellidos\t\tTeléfono\t\tDirección");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                
                for (int i = 0; i < cantidadUsuarios; i++)
                {
                    Console.WriteLine($"{i + 1}\t{usuariosIds[i],-12}\t{usuariosNombres[i],-20}\t{usuariosApellidos[i],-15}\t{usuariosTelefonos[i],-15}\t{usuariosDirecciones[i]}");
                }
            }
            
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        static void NuevoUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== NUEVO USUARIO ===");
            
            if (cantidadUsuarios >= 15)
            {
                Console.WriteLine("No se permiten crear usuarios nuevos. Límite de 15 usuarios alcanzado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Número de identificación: ");
            string identificacion = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(identificacion))
            {
                Console.WriteLine("El número de identificación no puede estar vacío.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            for (int i = 0; i < cantidadUsuarios; i++)
            {
                if (usuariosIds[i] == identificacion)
                {
                    Console.WriteLine("Ya existe un usuario con esa identificación.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
            }
            
            Console.Write("Nombres: ");
            string nombres = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(nombres))
            {
                Console.WriteLine("Los nombres no pueden estar vacíos.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(apellidos))
            {
                Console.WriteLine("Los apellidos no pueden estar vacíos.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(telefono))
            {
                Console.WriteLine("El teléfono no puede estar vacío.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Dirección: ");
            string direccion = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(direccion))
            {
                Console.WriteLine("La dirección no puede estar vacía.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            usuariosIds[cantidadUsuarios] = identificacion;
            usuariosNombres[cantidadUsuarios] = nombres;
            usuariosApellidos[cantidadUsuarios] = apellidos;
            usuariosTelefonos[cantidadUsuarios] = telefono;
            usuariosDirecciones[cantidadUsuarios] = direccion;
            cantidadUsuarios++;
            
            Console.WriteLine($"Usuario '{nombres} {apellidos}' creado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        static void EditarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== EDITAR INFORMACIÓN DE USUARIO ===");
            
            if (cantidadUsuarios == 0)
            {
                Console.WriteLine("No hay usuarios registrados para editar.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Ingrese el número de identificación del usuario a editar: ");
            string identificacionBuscar = Console.ReadLine() ?? "";
            
            int indiceUsuario = -1;
            for (int i = 0; i < cantidadUsuarios; i++)
            {
                if (usuariosIds[i] == identificacionBuscar)
                {
                    indiceUsuario = i;
                    break;
                }
            }
            
            if (indiceUsuario == -1)
            {
                Console.WriteLine("Usuario no encontrado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("\n=== USUARIO ENCONTRADO ===");
            Console.WriteLine($"Identificación: {usuariosIds[indiceUsuario]}");
            Console.WriteLine($"Nombres: {usuariosNombres[indiceUsuario]}");
            Console.WriteLine($"Apellidos: {usuariosApellidos[indiceUsuario]}");
            Console.WriteLine($"Teléfono: {usuariosTelefonos[indiceUsuario]}");
            Console.WriteLine($"Dirección: {usuariosDirecciones[indiceUsuario]}");
            
            Console.WriteLine("\n¿Qué información desea editar?");
            Console.WriteLine("1. Nombres");
            Console.WriteLine("2. Apellidos");
            Console.WriteLine("3. Teléfono");
            Console.WriteLine("4. Dirección");
            Console.WriteLine("5. Volcer al menú anterior");
            Console.Write("Seleccione una opción: ");
            
            string opcionEditar = Console.ReadLine() ?? "";
            
            switch (opcionEditar)
            {
                case "1":
                    Console.Write("Ingrese los nuevos nombres: ");
                    string nuevosNombres = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(nuevosNombres))
                    {
                        usuariosNombres[indiceUsuario] = nuevosNombres;
                        Console.WriteLine("Nombres actualizados exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Los nombres no pueden estar vacíos. No se realizaron cambios.");
                    }
                    break;
                    
                case "2":
                    Console.Write("Ingrese los nuevos apellidos: ");
                    string nuevosApellidos = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(nuevosApellidos))
                    {
                        usuariosApellidos[indiceUsuario] = nuevosApellidos;
                        Console.WriteLine("Apellidos actualizados exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Los apellidos no pueden estar vacíos. No se realizaron cambios.");
                    }
                    break;
                    
                case "3":
                    Console.Write("Ingrese el nuevo teléfono: ");
                    string nuevoTelefono = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(nuevoTelefono))
                    {
                        usuariosTelefonos[indiceUsuario] = nuevoTelefono;
                        Console.WriteLine("Teléfono actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("El teléfono no puede estar vacío. No se realizaron cambios.");
                    }
                    break;
                    
                case "4":
                    Console.Write("Ingrese la nueva dirección: ");
                    string nuevaDireccion = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(nuevaDireccion))
                    {
                        usuariosDirecciones[indiceUsuario] = nuevaDireccion;
                        Console.WriteLine("Dirección actualizada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("La dirección no puede estar vacía. No se realizaron cambios.");
                    }
                    break;
                    
                case "5":
                    Console.WriteLine("Edición cancelada.");
                    break;
                    
                default:
                    Console.WriteLine("Opción inválida. No se realizaron cambios.");
                    break;
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void GestionarVentas()
        {
            bool volverAlMenuAnterior = true;
            
            while (volverAlMenuAnterior)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ GESTIÓN VENTAS ===");
                Console.WriteLine("1. Generar factura");
                Console.WriteLine("2. Ver facturas registradas");
                Console.WriteLine("3. Salir de Gestión Ventas");
                Console.Write("Seleccione una opción: ");
                
                string opcion = Console.ReadLine() ?? "";
                
                switch (opcion)
                {
                    case "1":
                        GenerarFactura();
                        break;
                    case "2":
                        VerFacturasRegistradas();
                        break;
                    case "3":
                        volverAlMenuAnterior = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void VerFacturasRegistradas()
        {
            Console.Clear();
            Console.WriteLine("=== FACTURAS REGISTRADAS ===");
            
            if (cantidadVentas == 0)
            {
                Console.WriteLine("No hay facturas registradas.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("ID\tFecha y Hora\t\t\tCliente\t\t\t\tArtículos\tTotal");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            
            for (int i = 0; i < cantidadVentas; i++)
            {
                string cliente = $"{ventasUsuariosIds[i]} - {ventasUsuariosNombres[i]} {ventasUsuariosApellidos[i]}";
                Console.WriteLine($"{i + 1}\t{ventasFechas[i]:dd/MM/yyyy HH:mm}\t\t{cliente,-30}\t{ventasNumeroArticulos[i]}\t\t${ventasTotales[i]:F2}");
            }
            
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine($"Total de facturas: {cantidadVentas}");
            
            Console.Write("\n¿Desea ver el detalle de alguna factura? (s/n): ");
            string respuesta = Console.ReadLine()?.ToLower() ?? "";
            
            if (respuesta == "s" || respuesta == "si" || respuesta == "sí")
            {
                Console.Write("Ingrese el ID de la factura: ");
                if (int.TryParse(Console.ReadLine(), out int idFactura) && idFactura >= 1 && idFactura <= cantidadVentas)
                {
                    VerDetalleFactura(idFactura - 1);
                }
                else
                {
                    Console.WriteLine("ID de factura inválido.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        static void VerDetalleFactura(int indiceFactura)
        {
            Console.Clear();
            Console.WriteLine($"=== DETALLE DE FACTURA #{indiceFactura + 1} ===");
            Console.WriteLine($"Fecha: {ventasFechas[indiceFactura]:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Cliente: {ventasUsuariosIds[indiceFactura]} - {ventasUsuariosNombres[indiceFactura]} {ventasUsuariosApellidos[indiceFactura]}");
            Console.WriteLine();
            Console.WriteLine("DETALLE DE PRODUCTOS:");
            Console.WriteLine("ID\tNombre\t\t\tValor Unit.\tCantidad\tSubtotal");
            Console.WriteLine("--------------------------------------------------------------------");
            
            double totalFactura = 0;
            
            for (int i = 0; i < ventasNumeroArticulos[indiceFactura]; i++)
            {
                Console.WriteLine($"{ventasArticulosIds[indiceFactura][i]}\t{ventasArticulosNombres[indiceFactura][i],-20}\t${ventasArticulosValores[indiceFactura][i],-10:F2}\t{ventasArticulosCantidades[indiceFactura][i]}\t\t${ventasArticulosSubtotales[indiceFactura][i]:F2}");
                totalFactura += ventasArticulosSubtotales[indiceFactura][i];
            }
            
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"TOTAL: ${totalFactura:F2}");
            Console.WriteLine();
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        static void GenerarFactura()
        {
            Console.Clear();
            Console.WriteLine("=== GENERAR FACTURA ===");
            
            if (cantidadUsuarios == 0)
            {
                Console.WriteLine("No hay usuarios registrados. No se puede generar una factura.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            if (cantidadArticulos == 0)
            {
                Console.WriteLine("No hay artículos registrados. No se puede generar una factura.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            string usuarioId, usuarioNombre, usuarioApellido;
            if (!BuscarYElegirUsuario(out usuarioId, out usuarioNombre, out usuarioApellido))
            {
                return; 
            }
            
            int[] articulosSeleccionados = new int[10];
            string[] nombresSeleccionados = new string[10];
            double[] valoresSeleccionados = new double[10];
            int[] cantidadesSeleccionadas = new int[10];
            double[] subtotalesSeleccionados = new double[10];
            int numeroArticulosSeleccionados = 0;
            
            if (!ElegirArticulos(articulosSeleccionados, nombresSeleccionados, valoresSeleccionados, 
                               cantidadesSeleccionadas, subtotalesSeleccionados, out numeroArticulosSeleccionados))
            {
                return;
            }
            
            MostrarFactura(usuarioId, usuarioNombre, usuarioApellido, 
                          articulosSeleccionados, nombresSeleccionados, valoresSeleccionados,
                          cantidadesSeleccionadas, subtotalesSeleccionados, numeroArticulosSeleccionados);
            
            GuardarVenta(usuarioId, usuarioNombre, usuarioApellido,
                        articulosSeleccionados, nombresSeleccionados, valoresSeleccionados,
                        cantidadesSeleccionadas, subtotalesSeleccionados, numeroArticulosSeleccionados);
        }
        
        static bool BuscarYElegirUsuario(out string usuarioId, out string usuarioNombre, out string usuarioApellido)
        {
            usuarioId = "";
            usuarioNombre = "";
            usuarioApellido = "";
            
            Console.WriteLine("\n=== BUSCAR USUARIO ===");
            Console.Write("Ingrese la identificación del usuario: ");
            string identificacionBuscar = Console.ReadLine() ?? "";
            
            int indiceUsuario = -1;
            for (int i = 0; i < cantidadUsuarios; i++)
            {
                if (usuariosIds[i] == identificacionBuscar)
                {
                    indiceUsuario = i;
                    break;
                }
            }
            
            if (indiceUsuario == -1)
            {
                Console.WriteLine("Usuario no encontrado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return false;
            }
            
            Console.WriteLine("\n=== USUARIO ENCONTRADO ===");
            Console.WriteLine($"Identificación: {usuariosIds[indiceUsuario]}");
            Console.WriteLine($"Nombres: {usuariosNombres[indiceUsuario]}");
            Console.WriteLine($"Apellidos: {usuariosApellidos[indiceUsuario]}");
            Console.WriteLine($"Teléfono: {usuariosTelefonos[indiceUsuario]}");
            Console.WriteLine($"Dirección: {usuariosDirecciones[indiceUsuario]}");
            
            Console.Write("\n¿Confirma que este es el usuario correcto? (s/n): ");
            string confirmacion = Console.ReadLine()?.ToLower() ?? "";
            
            if (confirmacion == "s" || confirmacion == "si" || confirmacion == "sí")
            {
                usuarioId = usuariosIds[indiceUsuario];
                usuarioNombre = usuariosNombres[indiceUsuario];
                usuarioApellido = usuariosApellidos[indiceUsuario];
                return true;
            }
            
            return false;
        }
        
        static bool ElegirArticulos(int[] articulosSeleccionados, string[] nombresSeleccionados, 
                                   double[] valoresSeleccionados, int[] cantidadesSeleccionadas,
                                   double[] subtotalesSeleccionados, out int numeroArticulosSeleccionados)
        {
            numeroArticulosSeleccionados = 0;
            bool continuarSeleccionando = true;
            
            while (continuarSeleccionando && numeroArticulosSeleccionados < 10)
            {
                Console.Clear();
                Console.WriteLine("=== ELEGIR ARTÍCULOS ===");
                Console.WriteLine($"Artículos seleccionados: {numeroArticulosSeleccionados}/10");
                
                if (cantidadArticulos == 0)
                {
                    Console.WriteLine("No hay artículos disponibles.");
                    return false;
                }
                
                Console.WriteLine("\nArtículos disponibles:");
                Console.WriteLine("ID\tNombre\t\t\tValor Unitario\t\tStock");
                Console.WriteLine("--------------------------------------------------------");
                
                for (int i = 0; i < cantidadArticulos; i++)
                {
                    Console.WriteLine($"{articulosIds[i]}\t{articulosNombres[i],-20}\t${articulosValorUnitario[i],-15:F2}\t{articulosCantidadStock[i]}");
                }
                
                Console.Write("\nIngrese el ID del artículo a agregar: ");
                if (!int.TryParse(Console.ReadLine(), out int idArticulo))
                {
                    Console.WriteLine("ID inválido.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                
                if (idArticulo == 0)
                {
                    if (numeroArticulosSeleccionados > 0)
                    {
                        continuarSeleccionando = false;
                    }
                    else
                    {
                        Console.WriteLine("Debe seleccionar al menos un artículo.");
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                    continue;
                }
                
                int indiceArticulo = -1;
                for (int i = 0; i < cantidadArticulos; i++)
                {
                    if (articulosIds[i] == idArticulo)
                    {
                        indiceArticulo = i;
                        break;
                    }
                }
                
                if (indiceArticulo == -1)
                {
                    Console.WriteLine("Artículo no encontrado.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                
                bool yaSeleccionado = false;
                for (int i = 0; i < numeroArticulosSeleccionados; i++)
                {
                    if (articulosSeleccionados[i] == idArticulo)
                    {
                        yaSeleccionado = true;
                        break;
                    }
                }
                
                if (yaSeleccionado)
                {
                    Console.WriteLine("Este artículo ya fue seleccionado.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                
                if (articulosCantidadStock[indiceArticulo] == 0)
                {
                    Console.WriteLine("Este artículo no tiene stock disponible.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                
                Console.WriteLine($"\nArtículo seleccionado: {articulosNombres[indiceArticulo]}");
                Console.WriteLine($"Stock disponible: {articulosCantidadStock[indiceArticulo]}");
                Console.WriteLine($"Valor unitario: ${articulosValorUnitario[indiceArticulo]:F2}");
                
                Console.Write("Ingrese la cantidad a comprar: ");
                if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
                {
                    Console.WriteLine("Cantidad inválida.");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                
                if (cantidad > articulosCantidadStock[indiceArticulo])
                {
                    Console.WriteLine($"Stock insuficiente. Disponible: {articulosCantidadStock[indiceArticulo]}");
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }
                
                articulosSeleccionados[numeroArticulosSeleccionados] = articulosIds[indiceArticulo];
                nombresSeleccionados[numeroArticulosSeleccionados] = articulosNombres[indiceArticulo];
                valoresSeleccionados[numeroArticulosSeleccionados] = articulosValorUnitario[indiceArticulo];
                cantidadesSeleccionadas[numeroArticulosSeleccionados] = cantidad;
                subtotalesSeleccionados[numeroArticulosSeleccionados] = cantidad * articulosValorUnitario[indiceArticulo];
                numeroArticulosSeleccionados++;
                
                articulosCantidadStock[indiceArticulo] -= cantidad;
                
                Console.WriteLine($"Artículo agregado exitosamente. Subtotal: ${subtotalesSeleccionados[numeroArticulosSeleccionados - 1]:F2}");
                
                if (numeroArticulosSeleccionados < 10)
                {
                    Console.Write("¿Desea agregar otro artículo? (s/n): ");
                    string respuesta = Console.ReadLine()?.ToLower() ?? "";
                    if (respuesta != "s" && respuesta != "si" && respuesta != "sí")
                    {
                        continuarSeleccionando = false;
                    }
                }
                else
                {
                    Console.WriteLine("Ha alcanzado el límite máximo de 10 artículos por factura.");
                    continuarSeleccionando = false;
                }
                
                if (continuarSeleccionando)
                {
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            
            return numeroArticulosSeleccionados > 0;
        }
        
        static void MostrarFactura(string usuarioId, string usuarioNombre, string usuarioApellido,
                                  int[] articulosSeleccionados, string[] nombresSeleccionados, 
                                  double[] valoresSeleccionados, int[] cantidadesSeleccionadas,
                                  double[] subtotalesSeleccionados, int numeroArticulosSeleccionados)
        {
            Console.Clear();
            Console.WriteLine("=== FACTURA ===");
            Console.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Cliente: {usuarioId} - {usuarioNombre} {usuarioApellido}");
            Console.WriteLine();
            Console.WriteLine("DETALLE DE PRODUCTOS:");
            Console.WriteLine("ID\tNombre\t\t\tValor Unit.\tCantidad\tSubtotal");
            Console.WriteLine("--------------------------------------------------------------------");
            
            double totalFactura = 0;
            
            for (int i = 0; i < numeroArticulosSeleccionados; i++)
            {
                Console.WriteLine($"{articulosSeleccionados[i]}\t{nombresSeleccionados[i],-20}\t${valoresSeleccionados[i],-10:F2}\t{cantidadesSeleccionadas[i]}\t\t${subtotalesSeleccionados[i]:F2}");
                totalFactura += subtotalesSeleccionados[i];
            }
            
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"TOTAL: ${totalFactura:F2}");
            Console.WriteLine();
            
            Console.WriteLine("¡Factura generada exitosamente!");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        static void GuardarVenta(string usuarioId, string usuarioNombre, string usuarioApellido,
                                int[] articulosSeleccionados, string[] nombresSeleccionados,
                                double[] valoresSeleccionados, int[] cantidadesSeleccionadas,
                                double[] subtotalesSeleccionados, int numeroArticulosSeleccionados)
        {
            if (cantidadVentas < 1000)
            {
                ventasArticulosIds[cantidadVentas] = new int[numeroArticulosSeleccionados];
                ventasArticulosNombres[cantidadVentas] = new string[numeroArticulosSeleccionados];
                ventasArticulosValores[cantidadVentas] = new double[numeroArticulosSeleccionados];
                ventasArticulosCantidades[cantidadVentas] = new int[numeroArticulosSeleccionados];
                ventasArticulosSubtotales[cantidadVentas] = new double[numeroArticulosSeleccionados];
                
                ventasUsuariosIds[cantidadVentas] = usuarioId;
                ventasUsuariosNombres[cantidadVentas] = usuarioNombre;
                ventasUsuariosApellidos[cantidadVentas] = usuarioApellido;
                
                double totalVenta = 0;
                for (int i = 0; i < numeroArticulosSeleccionados; i++)
                {
                    ventasArticulosIds[cantidadVentas][i] = articulosSeleccionados[i];
                    ventasArticulosNombres[cantidadVentas][i] = nombresSeleccionados[i];
                    ventasArticulosValores[cantidadVentas][i] = valoresSeleccionados[i];
                    ventasArticulosCantidades[cantidadVentas][i] = cantidadesSeleccionadas[i];
                    ventasArticulosSubtotales[cantidadVentas][i] = subtotalesSeleccionados[i];
                    totalVenta += subtotalesSeleccionados[i];
                }
                
                ventasNumeroArticulos[cantidadVentas] = numeroArticulosSeleccionados;
                ventasTotales[cantidadVentas] = totalVenta;
                ventasFechas[cantidadVentas] = DateTime.Now;
                
                cantidadVentas++;
                siguienteIdFactura++;
                
                Console.WriteLine($"\nFactura #{cantidadVentas} guardada exitosamente.");
            }
        }
        
        static void GestionarArticulos()
        {
            bool volverAlMenuAnterior = true;
            
            while (volverAlMenuAnterior)
            {
                Console.Clear();
                Console.WriteLine("=== MENÚ GESTIÓN DE ARTÍCULOS ===");
                Console.WriteLine("1. Ver lista de artículos");
                Console.WriteLine("2. Nuevo artículo");
                Console.WriteLine("3. Editar información del artículo (buscar por id)");
                Console.WriteLine("4. Salir de Gestión de Artículos (Menú Principal)");
                Console.Write("Seleccione una opción: ");
                
                string opcion = Console.ReadLine() ?? "";
                
                switch (opcion)
                {
                    case "1":
                        VerListaArticulos();
                        break;
                    case "2":
                        NuevoArticulo();
                        break;
                    case "3":
                        EditarArticulo();
                        break;
                    case "4":
                        volverAlMenuAnterior = false;
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        
        static void VerListaArticulos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE ARTÍCULOS ===");
            
            if (cantidadArticulos == 0)
            {
                Console.WriteLine("No hay artículos registrados.");
            }
            else
            {
                Console.WriteLine("ID\tNombre\t\t\tValor Unitario\t\tStock");
                Console.WriteLine("--------------------------------------------------------");
                
                for (int i = 0; i < cantidadArticulos; i++)
                {
                    Console.WriteLine($"{articulosIds[i]}\t{articulosNombres[i],-20}\t${articulosValorUnitario[i],-15:F2}\t{articulosCantidadStock[i]}");
                }
            }
            
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        static void NuevoArticulo()
        {
            Console.Clear();
            Console.WriteLine("=== NUEVO ARTÍCULO ===");
            
            if (cantidadArticulos >= 15)
            {
                Console.WriteLine("No se permiten crear artículos nuevos. Límite de 15 artículos alcanzado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Nombre del artículo: ");
            string nombre = Console.ReadLine() ?? "";
            
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre del artículo no puede estar vacío.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Valor unitario: $");
            if (!double.TryParse(Console.ReadLine(), out double valorUnitario) || valorUnitario <= 0)
            {
                Console.WriteLine("Valor unitario inválido. Debe ser un número mayor a 0.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Cantidad de stock: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidadStock) || cantidadStock < 0)
            {
                Console.WriteLine("Cantidad de stock inválida. Debe ser un número mayor o igual a 0.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            articulosIds[cantidadArticulos] = siguienteIdArticulo;
            articulosNombres[cantidadArticulos] = nombre;
            articulosValorUnitario[cantidadArticulos] = valorUnitario;
            articulosCantidadStock[cantidadArticulos] = cantidadStock;
            cantidadArticulos++;
            
            Console.WriteLine($"Artículo '{nombre}' creado exitosamente con ID: {siguienteIdArticulo}");
            siguienteIdArticulo++;
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        static void EditarArticulo()
        {
            Console.Clear();
            Console.WriteLine("=== EDITAR INFORMACIÓN DEL ARTÍCULO ===");
            
            if (cantidadArticulos == 0)
            {
                Console.WriteLine("No hay artículos registrados para editar.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Ingrese el ID del artículo a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int idBuscar))
            {
                Console.WriteLine("ID inválido.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int indiceArticulo = -1;
            for (int i = 0; i < cantidadArticulos; i++)
            {
                if (articulosIds[i] == idBuscar)
                {
                    indiceArticulo = i;
                    break;
                }
            }
            
            if (indiceArticulo == -1)
            {
                Console.WriteLine("Artículo no encontrado.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("\n=== ARTÍCULO ENCONTRADO ===");
            Console.WriteLine($"ID: {articulosIds[indiceArticulo]}");
            Console.WriteLine($"Nombre: {articulosNombres[indiceArticulo]}");
            Console.WriteLine($"Valor Unitario: ${articulosValorUnitario[indiceArticulo]:F2}");
            Console.WriteLine($"Stock: {articulosCantidadStock[indiceArticulo]}");
            
            Console.WriteLine("\n¿Qué información desea editar?");
            Console.WriteLine("1. Nombre");
            Console.WriteLine("2. Valor Unitario");
            Console.WriteLine("3. Cantidad de Stock");
            Console.WriteLine("4. Volver al menú anterior");
            Console.Write("Seleccione una opción: ");
            
            string opcionEditar = Console.ReadLine() ?? "";
            
            switch (opcionEditar)
            {
                case "1":
                    Console.Write("Ingrese el nuevo nombre: ");
                    string nuevoNombre = Console.ReadLine() ?? "";
                    if (!string.IsNullOrEmpty(nuevoNombre))
                    {
                        articulosNombres[indiceArticulo] = nuevoNombre;
                        Console.WriteLine("Nombre actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("El nombre no puede estar vacío. No se realizaron cambios.");
                    }
                    break;
                    
                case "2":
                    Console.Write("Ingrese el nuevo valor unitario: $");
                    if (double.TryParse(Console.ReadLine(), out double nuevoValor) && nuevoValor > 0)
                    {
                        articulosValorUnitario[indiceArticulo] = nuevoValor;
                        Console.WriteLine("Valor unitario actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Valor inválido. Debe ser un número mayor a 0. No se realizaron cambios.");
                    }
                    break;
                    
                case "3":
                    Console.Write("Ingrese la nueva cantidad de stock: ");
                    if (int.TryParse(Console.ReadLine(), out int nuevoStock) && nuevoStock >= 0)
                    {
                        articulosCantidadStock[indiceArticulo] = nuevoStock;
                        Console.WriteLine("Stock actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Stock inválido. Debe ser un número mayor o igual a 0. No se realizaron cambios.");
                    }
                    break;
                    
                case "4":
                    Console.WriteLine("Edición cancelada.");
                    break;
                    
                default:
                    Console.WriteLine("Opción inválida. No se realizaron cambios.");
                    break;
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}