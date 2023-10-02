using MySql.Data.MySqlClient;

namespace Proyecto_Venta_Productos_Lacteos.Models.ViewModels
{
    public class ConexionViewModel
    {
        public static MySqlConnection conectar()
        {
            const string SERVIDOR = "localhost";
            const string BD = "tienda_lacteos";
            const string USUARIO = "root";
            const string PASSWORD = "12345";

            string cadenaConexion = "Database = " + BD + "; Data Source = " +
                SERVIDOR + "; User Id = " + USUARIO + "; Password = " + PASSWORD + "";

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);

                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("ERROR AL CONECTARSE A LA BASE DE DATOS: " + ex.Message);
                return null;
            }
        }
    }
}