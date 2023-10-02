using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Proyecto_Venta_Productos_Lacteos.Models.Interfaces;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;
using static Mysqlx.Crud.Order.Types;

namespace Proyecto_Venta_Productos_Lacteos.Models.CRUDs
{
    public class CRUDClientes : ICRUDConexion<ClienteViewModel, Cliente>
    {
        public bool create(ClienteViewModel model)
        {
            string sql = "INSERT INTO clientes (cod_clientes, nombre, apellido, cedula, email, telefono, direccion) " +
                "VALUES (@CodigoCliente, @Nombre ,@Apellido ,@Cedula, @Email, @Telefono, @Direccion)";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            if (int.TryParse(model.cedula, out _) && int.TryParse(model.telefono, out _))
            {
                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                    comando.Parameters.AddWithValue("@CodigoCliente", model.cod_clientes);
                    comando.Parameters.AddWithValue("@Nombre", model.nombre);
                    comando.Parameters.AddWithValue("@Apellido", model.apellido);
                    comando.Parameters.AddWithValue("@Cedula", model.cedula);
                    comando.Parameters.AddWithValue("@Email", model.email);
                    comando.Parameters.AddWithValue("@Telefono", model.telefono);
                    comando.Parameters.AddWithValue("@Direccion", model.direccion);
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
            }

            return respuesta;
        }

        public bool delete(int cod)
        {
            string sql = "DELETE FROM clientes WHERE cod_clientes = @Codigo";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@Codigo", cod);
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

        public List<Cliente> read()
        {
            string sql = "SELECT cod_clientes, nombre, apellido, cedula, email, telefono, direccion FROM clientes ORDER BY cod_clientes";

            List<Cliente> listaClientes = new List<Cliente>();

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
                        Cliente cliente = new Cliente();

                        cliente.cod_clientes = reader.GetInt32("cod_clientes");
                        cliente.nombre = reader.GetString("nombre");
                        cliente.apellido = reader.GetString("apellido");
                        cliente.cedula = reader.GetString("cedula");
                        cliente.email = reader.GetString("email");
                        cliente.telefono = reader.GetString("telefono");
                        cliente.direccion = reader.GetString("direccion");

                        listaClientes.Add(cliente);
                    }
                }
            }
            catch (MySqlException ex)
            {
                listaClientes = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return listaClientes;
        }
        
        public Cliente read_by_code(int cod)
        {
            string sql = "SELECT cod_clientes, nombre, apellido, cedula, email, telefono, direccion " +
                "FROM clientes WHERE cod_clientes = @Codigo";

            Cliente cliente = new Cliente();

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
                        cliente.cod_clientes = reader.GetInt32("cod_clientes");
                        cliente.nombre = reader.GetString("nombre");
                        cliente.apellido = reader.GetString("apellido");
                        cliente.cedula = reader.GetString("cedula");
                        cliente.email = reader.GetString("email");
                        cliente.telefono = reader.GetString("telefono");
                        cliente.direccion = reader.GetString("direccion");
                    }
                }
                else
                {
                    cliente = null;
                }
            }
            catch (MySqlException ex)
            {
                cliente = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return cliente;
        }

        public bool update(ClienteViewModel model)
        {
            string sql = "UPDATE clientes SET nombre = @Nombre, apellido = @Apellido, cedula = @Cedula, " +
                "email = @Email, telefono = @Telefono, direccion = @Direccion WHERE cod_clientes = @CodigoCliente";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoCliente", model.cod_clientes);
                comando.Parameters.AddWithValue("@Nombre", model.nombre);
                comando.Parameters.AddWithValue("@Apellido", model.apellido);
                comando.Parameters.AddWithValue("@Cedula", model.cedula);
                comando.Parameters.AddWithValue("@Email", model.email);
                comando.Parameters.AddWithValue("@Telefono", model.telefono);
                comando.Parameters.AddWithValue("@Direccion", model.direccion);
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
