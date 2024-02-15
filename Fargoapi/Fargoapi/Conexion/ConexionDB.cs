namespace Fargoapi.Conexion
{
    public class ConexionDB
    {
        private String Conexion = String.Empty;
        public ConexionDB() { 
        var builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").
                Build();
            Conexion = builder.GetSection
                    ("ConnectionStrings:conexion").Value;
        }
        public String cadena() {
            return Conexion;
        }
        
        
    }
}
