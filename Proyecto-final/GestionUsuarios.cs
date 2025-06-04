using System;

namespace TiendaBarrio
{
    public class GestionUsuarios
    {
        private static Usuario?[] matrizUsuarios = new Usuario?[15];
        
        private static bool usuariosInicializados = false;

        public static bool HayUsuariosRegistrados()
        {
            if (!usuariosInicializados)
            {
                InicializarMatrizVacia();
                usuariosInicializados = true;
            }

            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static Usuario? BuscarUsuarioPorCedula(string numeroIdentificacion)
        {
            if (!usuariosInicializados)
            {
                InicializarMatrizVacia();
                usuariosInicializados = true;
            }

            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] != null && matrizUsuarios[i]!.NumeroIdentificacion == numeroIdentificacion)
                {
                    return matrizUsuarios[i];
                }
            }

            return null;
        }

        public static void MostrarMenuGestionUsuarios()
        {
            if (!usuariosInicializados)
            {
                InicializarMatrizVacia();
                usuariosInicializados = true;
            }

            int opcion;

            do
            {
                MostrarOpcionesMenuUsuarios();
                opcion = LeerYValidarOpcionUsuarios();
                ProcesarOpcionUsuarios(opcion);

            } while (opcion != 4);
        }
        
        private static void InicializarMatrizVacia()
        {
            for (int i = 0; i < 15; i++)
            {
                matrizUsuarios[i] = null;
            }
        }
        
        private static void MostrarOpcionesMenuUsuarios()
        {
            Console.WriteLine("\n=== MENÚ GESTIÓN USUARIOS ===");
            Console.WriteLine("1. Ver lista de usuarios");
            Console.WriteLine("2. Nuevo usuario");
            Console.WriteLine("3. Editar información de usuario");
            Console.WriteLine("4. Salir de Gestión de usuarios");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }
        
        private static int LeerYValidarOpcionUsuarios()
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
        
        private static void ProcesarOpcionUsuarios(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    VerListaUsuarios();
                    break;
                case 2:
                    NuevoUsuario();
                    break;
                case 3:
                    EditarInformacionUsuario();
                    break;
                case 4:
                    Console.WriteLine("Regresando al Menú Principal...\n");
                    break;
            }
        }
        
        private static void VerListaUsuarios()
        {
            Console.WriteLine("\n=== LISTA DE USUARIOS ===");
            
            bool hayUsuarios = false;
            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] != null)
                {
                    hayUsuarios = true;
                    break;
                }
            }
            
            if (!hayUsuarios)
            {
                Console.WriteLine("No hay usuarios registrados.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int contador = 1;
            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] != null)
                {
                    Console.WriteLine($"\n--- Usuario {contador} ---");
                    Console.WriteLine($"Número de identificación: {matrizUsuarios[i]!.NumeroIdentificacion}");
                    Console.WriteLine($"Nombres: {matrizUsuarios[i]!.Nombres}");
                    Console.WriteLine($"Apellidos: {matrizUsuarios[i]!.Apellidos}");
                    Console.WriteLine($"Teléfono: {matrizUsuarios[i]!.Telefono}");
                    Console.WriteLine($"Dirección: {matrizUsuarios[i]!.Direccion}");
                    contador++;
                }
            }
            
            Console.WriteLine($"\nTotal de usuarios registrados: {contador - 1}/15");
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static void NuevoUsuario()
        {
            Console.WriteLine("\n=== CREAR NUEVO USUARIO ===");
            
            int posicionDisponible = -1;
            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] == null)
                {
                    posicionDisponible = i;
                    break;
                }
            }
            
            if (posicionDisponible == -1)
            {
                Console.WriteLine("No se permiten crear usuarios nuevos. Se ha alcanzado el límite de 15 usuarios.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine($"Usuarios registrados: {ContarUsuariosRegistrados()}/15");
            
            Console.Write("\nIngrese el número de identificación: ");
            string numeroIdentificacion = Console.ReadLine() ?? "";
            
            if (BuscarUsuarioPorCedula(numeroIdentificacion) != null)
            {
                Console.WriteLine("Ya existe un usuario con ese número de identificación.");
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            Console.Write("Ingrese los nombres: ");
            string nombres = Console.ReadLine() ?? "";
            
            Console.Write("Ingrese los apellidos: ");
            string apellidos = Console.ReadLine() ?? "";
            
            Console.Write("Ingrese el teléfono: ");
            string telefono = Console.ReadLine() ?? "";
            
            Console.Write("Ingrese la dirección: ");
            string direccion = Console.ReadLine() ?? "";
            
            matrizUsuarios[posicionDisponible] = new Usuario(numeroIdentificacion, nombres, apellidos, telefono, direccion);
            
            Console.WriteLine("\n¡Usuario creado exitosamente!");
            Console.WriteLine($"Posición en matriz: {posicionDisponible + 1}");
            Console.WriteLine($"Número de identificación: {numeroIdentificacion}");
            Console.WriteLine($"Nombre completo: {nombres} {apellidos}");
            
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static int ContarUsuariosRegistrados()
        {
            int contador = 0;
            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] != null)
                {
                    contador++;
                }
            }
            return contador;
        }
        
        private static void EditarInformacionUsuario()
        {
            Console.WriteLine("\n=== EDITAR INFORMACIÓN DE USUARIO ===");
            Console.Write("Ingrese el número de identificación del usuario a buscar: ");
            string numeroIdentificacion = Console.ReadLine() ?? "";
            
            bool usuarioEncontrado = false;
            int indiceUsuario = -1;
            
            for (int i = 0; i < 15; i++)
            {
                if (matrizUsuarios[i] != null && matrizUsuarios[i]!.NumeroIdentificacion == numeroIdentificacion)
                {
                    usuarioEncontrado = true;
                    indiceUsuario = i;
                    Console.WriteLine("\n¡Usuario encontrado!");
                    MostrarInformacionUsuario(matrizUsuarios[i]!);
                    break;
                }
            }
            
            if (!usuarioEncontrado)
            {
                Console.WriteLine("\nUsuario no encontrado");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            int opcion;
            bool seRealizaronCambios = false;
            
            do
            {
                Console.WriteLine("\n--- OPCIONES DE EDICIÓN ---");
                Console.WriteLine("1. Editar nombres");
                Console.WriteLine("2. Editar apellidos");
                Console.WriteLine("3. Editar teléfono");
                Console.WriteLine("4. Editar dirección");
                Console.WriteLine("5. Eliminar usuario");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");
                
                opcion = LeerYValidarOpcionUsuario(1, 6);
                
                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese los nuevos nombres: ");
                        string nuevosNombres = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(nuevosNombres))
                        {
                            matrizUsuarios[indiceUsuario]!.Nombres = nuevosNombres;
                            Console.WriteLine("Nombres actualizados exitosamente.");
                            seRealizaronCambios = true;
                        }
                        break;
                        
                    case 2:
                        Console.Write("Ingrese los nuevos apellidos: ");
                        string nuevosApellidos = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(nuevosApellidos))
                        {
                            matrizUsuarios[indiceUsuario]!.Apellidos = nuevosApellidos;
                            Console.WriteLine("Apellidos actualizados exitosamente.");
                            seRealizaronCambios = true;
                        }
                        break;
                        
                    case 3:
                        Console.Write("Ingrese el nuevo teléfono: ");
                        string nuevoTelefono = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(nuevoTelefono))
                        {
                            matrizUsuarios[indiceUsuario]!.Telefono = nuevoTelefono;
                            Console.WriteLine("Teléfono actualizado exitosamente.");
                            seRealizaronCambios = true;
                        }
                        break;
                        
                    case 4:
                        Console.Write("Ingrese la nueva dirección: ");
                        string nuevaDireccion = Console.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(nuevaDireccion))
                        {
                            matrizUsuarios[indiceUsuario]!.Direccion = nuevaDireccion;
                            Console.WriteLine("Dirección actualizada exitosamente.");
                            seRealizaronCambios = true;
                        }
                        break;
                        
                    case 5:
                        Console.Write("¿Está seguro que desea eliminar este usuario? (s/n): ");
                        string confirmacion = Console.ReadLine()?.ToLower() ?? "";
                        if (confirmacion == "s" || confirmacion == "si" || confirmacion == "sí")
                        {
                            matrizUsuarios[indiceUsuario] = null;
                            Console.WriteLine("Usuario eliminado exitosamente.");
                            seRealizaronCambios = true;
                            opcion = 6;
                        }
                        break;
                        
                    case 6:
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
                
                if (opcion != 5 && opcion != 6 && matrizUsuarios[indiceUsuario] != null)
                {
                    Console.WriteLine("\n--- INFORMACIÓN ACTUALIZADA ---");
                    MostrarInformacionUsuario(matrizUsuarios[indiceUsuario]!);
                }
                
            } while (opcion != 6 && opcion != 5);
            
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private static void MostrarInformacionUsuario(Usuario usuario)
        {
            Console.WriteLine($"Número de identificación: {usuario.NumeroIdentificacion}");
            Console.WriteLine($"Nombres: {usuario.Nombres}");
            Console.WriteLine($"Apellidos: {usuario.Apellidos}");
            Console.WriteLine($"Teléfono: {usuario.Telefono}");
            Console.WriteLine($"Dirección: {usuario.Direccion}");
        }
        
        private static int LeerYValidarOpcionUsuario(int min, int max)
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