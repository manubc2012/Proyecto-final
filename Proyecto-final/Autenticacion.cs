using System;

namespace TiendaBarrio
{
    public class Autenticacion
    {
        private static string[] credencialesDefecto = { "admin", "123456" };
        
        public static bool AutenticarUsuario()
        {
            string usuarioIngresado;
            string contraseñaIngresada;
            bool autenticado = false;
            
            Console.WriteLine("=== AUTENTICACIÓN ===");
            
            while (!autenticado)
            {
                Console.Write("Usuario: ");
                usuarioIngresado = Console.ReadLine() ?? "";
                
                Console.Write("Contraseña: ");
                contraseñaIngresada = Console.ReadLine() ?? "";
                
                if (ValidarCredenciales(usuarioIngresado, contraseñaIngresada))
                {
                    autenticado = true;
                    MostrarMensajeBienvenida();
                }
                else
                {
                    MostrarMensajeError();
                }
            }
            
            return autenticado;
        }
        
        private static bool ValidarCredenciales(string usuario, string contraseña)
        {
            return usuario == credencialesDefecto[0] && contraseña == credencialesDefecto[1];
        }
        
        private static void MostrarMensajeBienvenida()
        {
            Console.WriteLine("\n¡BIENVENIDO AL SISTEMA!");
            Console.WriteLine("Autenticación exitosa.\n");
        }
        
        private static void MostrarMensajeError()
        {
            Console.WriteLine("\nERROR: Usuario o contraseña incorrectos.");
            Console.WriteLine("Por favor, intente nuevamente.\n");
        }
    }
}