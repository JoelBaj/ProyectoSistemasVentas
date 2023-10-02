using MySql.Data.MySqlClient;
using Proyecto_Venta_Productos_Lacteos.Models.Interfaces;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;

namespace Proyecto_Venta_Productos_Lacteos.Models.CRUDs
{
    public class CRUDVentas : ICRUDConexion<VentaViewModel, Venta>
    {
        public bool create(VentaViewModel model)
        {
            string sql = "INSERT INTO ventas (cod_venta, cod_cliente, cod_producto, cantidad, fecha, forma_pago, total) " +
                "VALUES (@CodigoVenta, @CodigoCliente, @CodigoProducto, @Cantidad, @Fecha, @FormaPago, @Total)";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoVenta", model.cod_venta);
                comando.Parameters.AddWithValue("@CodigoCliente", model.cod_cliente);
                comando.Parameters.AddWithValue("@CodigoProducto", model.cod_producto);
                comando.Parameters.AddWithValue("@Cantidad", model.cantidad);
                comando.Parameters.AddWithValue("@Fecha", model.fecha);
                comando.Parameters.AddWithValue("@FormaPago", model.forma_pago);
                comando.Parameters.AddWithValue("@Total", model.total);
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
            string sql = "DELETE FROM ventas WHERE cod_venta = @CodigoVenta";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoVenta", cod);
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

        public List<Venta> read()
        {
            string sql = "SELECT ventas.cod_venta, ventas.cod_cliente, clientes.nombre as nombre_cliente, clientes.apellido as apellido_cliente," +
                "clientes.cedula as cedula_cliente, clientes.email as email_cliente, clientes.telefono as telefono_cliente, " +
                "clientes.direccion as direccion_cliente, ventas.cod_producto, productos.nombre as nombre_producto, " +
                "productos.stock as stock_producto, productos.precio_unitario as precio_producto, productos.cod_categoria, " +
                "categorias_productos.nombre as nombre_categoria, ventas.cantidad, ventas.fecha, ventas.forma_pago, " +
                "ventas.total FROM (((ventas INNER JOIN productos ON ventas.cod_producto = productos.cod_producto) " +
                "INNER JOIN clientes ON ventas.cod_cliente = clientes.cod_clientes) " +
                "INNER JOIN categorias_productos ON productos.cod_categoria = categorias_productos.cod_categoria) ORDER BY cod_venta";

            List<Venta> listaVentas = new List<Venta>();

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
                        Venta venta = new Venta();
                        Cliente cliente = new Cliente();
                        Producto producto = new Producto();
                        CategoriaProducto categoria = new CategoriaProducto();

                        venta.cod_venta = reader.GetInt32("cod_venta");

                        cliente.cod_clientes = reader.GetInt32("cod_cliente");
                        cliente.nombre = reader.GetString("nombre_cliente");
                        cliente.apellido = reader.GetString("apellido_cliente");
                        cliente.cedula = reader.GetString("cedula_cliente");
                        cliente.email = reader.GetString("email_cliente");
                        cliente.telefono = reader.GetString("telefono_cliente");
                        cliente.direccion = reader.GetString("direccion_cliente");

                        venta.cliente = cliente;

                        producto.cod_producto = reader.GetInt32("cod_producto");
                        producto.nombre = reader.GetString("nombre_producto");
                        producto.stock = reader.GetInt32("stock_producto");
                        producto.precio_unitario = reader.GetDouble("precio_producto");

                        categoria.cod_categoria = reader.GetInt32("cod_categoria");
                        categoria.nombre = reader.GetString("nombre_categoria");

                        producto.categoria = categoria;

                        venta.producto = producto;

                        venta.cantidad = reader.GetInt32("cantidad");
                        venta.fecha = reader.GetDateTime("fecha");
                        venta.total = reader.GetDouble("total");

                        listaVentas.Add(venta);
                    }
                }
                else
                {
                    listaVentas = null;
                }
            }
            catch (MySqlException ex)
            {
                listaVentas = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return listaVentas;
        }

        public Venta read_by_code(int cod)
        {
            string sql = "SELECT ventas.cod_venta, ventas.cod_cliente, clientes.nombre as nombre_cliente, clientes.apellido as apellido_cliente," +
                "clientes.cedula as cedula_cliente, clientes.email as email_cliente, clientes.telefono as telefono_cliente, " +
                "clientes.direccion as direccion_cliente, ventas.cod_producto, productos.nombre as nombre_producto, " +
                "productos.stock as stock_producto, productos.precio_unitario as precio_producto, productos.cod_categoria, " +
                "categorias_productos.nombre as nombre_categoria, ventas.cantidad, ventas.fecha, ventas.forma_pago, " +
                "ventas.total FROM (((ventas INNER JOIN productos ON ventas.cod_producto = productos.cod_producto) " +
                "INNER JOIN clientes ON ventas.cod_cliente = clientes.cod_clientes) " +
                "INNER JOIN categorias_productos ON productos.cod_categoria = categorias_productos.cod_categoria) " +
                "WHERE cod_venta = @CodigoVenta LIMIT 1";

            Venta venta = new Venta();

            MySqlDataReader reader = null;
            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoVenta", cod);
                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente();
                        Producto producto = new Producto();
                        CategoriaProducto categoria = new CategoriaProducto();

                        venta.cod_venta = reader.GetInt32("cod_venta");

                        cliente.cod_clientes = reader.GetInt32("cod_cliente");
                        cliente.nombre = reader.GetString("nombre_cliente");
                        cliente.apellido = reader.GetString("apellido_cliente");
                        cliente.cedula = reader.GetString("cedula_cliente");
                        cliente.email = reader.GetString("email_cliente");
                        cliente.telefono = reader.GetString("telefono_cliente");
                        cliente.direccion = reader.GetString("direccion_cliente");

                        venta.cliente = cliente;

                        producto.cod_producto = reader.GetInt32("cod_producto");
                        producto.nombre = reader.GetString("nombre_producto");
                        producto.stock = reader.GetInt32("stock_producto");
                        producto.precio_unitario = reader.GetDouble("precio_producto");

                        categoria.cod_categoria = reader.GetInt32("cod_categoria");
                        categoria.nombre = reader.GetString("nombre_categoria");

                        producto.categoria = categoria;

                        venta.producto = producto;

                        venta.cantidad = reader.GetInt32("cantidad");
                        venta.fecha = reader.GetDateTime("fecha");
                        venta.total = reader.GetDouble("total");
                    }
                }
                else
                {
                    venta = null;
                }
            }
            catch (MySqlException ex)
            {
                venta = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return venta;
        }

        public bool update(VentaViewModel model)
        {
            string sql = "UPDATE ventas SET cod_cliente = @CodigoCliente, cod_producto = @CodigoProducto, " +
                "cantidad = @Cantidad, fecha = @FechaVenta, forma_pago = @FormaPago, total = @Total " +
                "WHERE cod_venta = @CodigoVenta";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoVenta", model.cod_venta);
                comando.Parameters.AddWithValue("@CodigoCliente", model.cod_cliente);
                comando.Parameters.AddWithValue("@CodigoProducto", model.cod_producto);
                comando.Parameters.AddWithValue("@Cantidad", model.cantidad);
                comando.Parameters.AddWithValue("@FechaVenta", model.fecha);
                comando.Parameters.AddWithValue("@FormaPago", model.forma_pago);
                comando.Parameters.AddWithValue("@Total", model.total);
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
