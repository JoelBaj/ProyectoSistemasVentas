using MySql.Data.MySqlClient;
using Proyecto_Venta_Productos_Lacteos.Models.Interfaces;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;

namespace Proyecto_Venta_Productos_Lacteos.Models.CRUDs
{
    public class CRUDProductos : ICRUDConexion<ProductoViewModel, Producto>
    {
        public bool create(ProductoViewModel model)
        {
            string sql = "INSERT INTO productos (cod_producto, nombre, stock, precio_unitario, cod_categoria) " +
                "VALUES (@CodigoProducto, @Nombre, @Stock, @PrecioUnitario, @CodigoCategoria)";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();
                
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoProducto", model.cod_producto);
                comando.Parameters.AddWithValue("@Nombre", model.nombre);
                comando.Parameters.AddWithValue("@Stock", model.stock);
                comando.Parameters.AddWithValue("@PrecioUnitario", model.precio_unitario);
                comando.Parameters.AddWithValue("@CodigoCategoria", model.cod_categoria);
                comando.ExecuteNonQuery();

                respuesta = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return respuesta;
        }

        public bool delete(int cod)
        {
            string sql = "DELETE FROM productos WHERE cod_producto = @codigo";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@codigo", cod);
                comando.ExecuteNonQuery();

                respuesta = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return respuesta;
        }

        public List<Producto> read()
        {
            string sql = "SELECT productos.cod_producto, productos.nombre, productos.stock, " +
                "productos.precio_unitario, productos.cod_categoria, categorias_productos.nombre as nombre_categoria " +
                "FROM productos INNER JOIN categorias_productos " +
                "ON productos.cod_categoria = categorias_productos.cod_categoria ORDER BY cod_producto";

            List<Producto> listaProductos = new List<Producto>();

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
                        Producto producto = new Producto();
                        CategoriaProducto categoria = new CategoriaProducto();

                        producto.cod_producto = reader.GetInt32("cod_producto");
                        producto.nombre = reader.GetString("nombre");
                        producto.stock = reader.GetInt32("stock");
                        producto.precio_unitario = reader.GetDouble("precio_unitario");

                        categoria.cod_categoria = reader.GetInt32("cod_categoria");
                        categoria.nombre = reader.GetString("nombre_categoria");

                        producto.categoria = categoria;

                        listaProductos.Add(producto);
                    }
                }
                else
                {
                    listaProductos = null;
                }
            }
            catch (MySqlException ex)
            {
                listaProductos = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return listaProductos;
        }

        public Producto read_by_code(int cod)
        {
            string sql = "SELECT productos.cod_producto, productos.nombre, productos.stock, " +
                "productos.precio_unitario, productos.cod_categoria, categorias_productos.nombre as nombre_categoria " +
                "FROM productos INNER JOIN categorias_productos " +
                "ON productos.cod_categoria = categorias_productos.cod_categoria WHERE cod_producto = @Codigo";

            Producto producto = new Producto();
            CategoriaProducto categoria = new CategoriaProducto();

            MySqlDataReader reader = null;
            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@Codigo", cod);
                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        producto.cod_producto = reader.GetInt32("cod_producto");
                        producto.nombre = reader.GetString("nombre");
                        producto.stock = reader.GetInt32("stock");
                        producto.precio_unitario = reader.GetDouble("precio_unitario");

                        categoria.cod_categoria = reader.GetInt32("cod_categoria");
                        categoria.nombre = reader.GetString("nombre_categoria");

                        producto.categoria = categoria;
                    }
                }
                else
                {
                    producto = null;
                }
            }
            catch (MySqlException ex)
            {
                producto = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return producto;
        }

        public bool update(ProductoViewModel model)
        {
            string sql = "UPDATE productos SET nombre = @Nombre, stock = @Stock, precio_unitario = @Precio, " +
                "cod_categoria = @Categoria WHERE cod_producto = @CodigoProducto";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoProducto", model.cod_producto);
                comando.Parameters.AddWithValue("@Nombre", model.nombre);
                comando.Parameters.AddWithValue("@Stock", model.stock);
                comando.Parameters.AddWithValue("@Precio", model.precio_unitario);
                comando.Parameters.AddWithValue("@Categoria", model.cod_categoria);
                comando.ExecuteNonQuery();

                respuesta = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return respuesta;
        }
    }
}
