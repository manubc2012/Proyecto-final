using System;

namespace TiendaBarrio
{
    public class Menu
    {
        public static void MostrarMenuPrincipal()
        {
            int opcion;
            
            do
            {
                MostrarOpcionesMenu();
                opcion = LeerYValidarOpcion();
                ProcesarOpcion(opcion);
                
            } while (opcion != 4);
        }
        
        private static void MostrarOpcionesMenu()
        {
            Console.WriteLine("=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Gestión de usuarios");
            Console.WriteLine("2. Gestión de artículos");
            Console.WriteLine("3. Gestión de ventas");
            Console.WriteLine("4. Salir del programa");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }
        
        private static int LeerYValidarOpcion()
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
        
        private static void ProcesarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    GestionUsuarios.MostrarMenuGestionUsuarios();
                    break;
                case 2:
                    GestionArticulos.MostrarMenuGestionArticulos();
                    break;
                case 3:
                    GestionVentas.MostrarMenuGestionVentas();
                    break;
                case 4:
                    MostrarMensajeCierreSesion();
                    break;
            }
        }
        
        private static void MostrarMensajeCierreSesion()
        {
            Console.WriteLine("\nCerrando sesión...");
            Console.WriteLine("Regresando a la pantalla de autenticación.");
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}