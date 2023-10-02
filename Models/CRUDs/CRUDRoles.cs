using MySql.Data.MySqlClient;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;

namespace Proyecto_Venta_Productos_Lacteos.Models.CRUDs
{
    public class CRUDRoles
    {
        public List<Rol> read()
        {
            string sql = "SELECT cod_rol, nombre FROM roles ORDER BY cod_rol";
            List<Rol> listaRoles = new List<Rol>();
            MySqlDataReader reader = null;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rol rol = new Rol();

                        rol.cod_rol = reader.GetInt32("cod_rol");
                        rol.nombre = reader.GetString("nombre");

                        listaRoles.Add(rol);
                    }
                }
            }
            catch (MySqlException ex)
            {
                listaRoles = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return listaRoles;
        }
    }
}
