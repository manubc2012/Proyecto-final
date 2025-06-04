using System;

namespace TiendaBarrio
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuarPrograma = true;
            
            Console.WriteLine("=== SISTEMA DE GESTIÓN - TIENDA DE BARRIO ===\n");
            
            while (continuarPrograma)
            {
                bool autenticado = Autenticacion.AutenticarUsuario();
                
                if (autenticado)
                {
                    Menu.MostrarMenuPrincipal();
                    
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
    }
}