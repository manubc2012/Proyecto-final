namespace TiendaBarrio
{
    public class Usuario
    {
        public string NumeroIdentificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        
        public Usuario(string numeroIdentificacion, string nombres, string apellidos, string telefono, string direccion)
        {
            NumeroIdentificacion = numeroIdentificacion;
            Nombres = nombres;
            Apellidos = apellidos;
            Telefono = telefono;
            Direccion = direccion;
        }
        
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        
        public string Cedula => NumeroIdentificacion;
        public string Nombre => NombreCompleto;
        
        public override string ToString()
        {
            return $"ID: {NumeroIdentificacion}, Nombre: {NombreCompleto}, Tel: {Telefono}";
        }
    }
}