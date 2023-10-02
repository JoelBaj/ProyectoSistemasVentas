using MySql.Data.MySqlClient;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;

namespace Proyecto_Venta_Productos_Lacteos.Models.CRUDs
{
    public class CRUDCategoriasProducto
    {
        public List<CategoriaProducto> read()
        {
            string sql = "SELECT cod_categoria, nombre FROM categorias_productos ORDER BY cod_categoria";
            List<CategoriaProducto> listaCategorias = new List<CategoriaProducto>();
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
                        CategoriaProducto categoria = new CategoriaProducto();

                        categoria.cod_categoria = reader.GetInt32("cod_categoria");
                        categoria.nombre = reader.GetString("nombre");

                        listaCategorias.Add(categoria);
                    }
                }
            }
            catch (MySqlException ex)
            {
                listaCategorias = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return listaCategorias;
        }
    }
}
