using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Proyecto_Venta_Productos_Lacteos.Models.Interfaces;
using Proyecto_Venta_Productos_Lacteos.Models.ViewModels;
using System.Data;

namespace Proyecto_Venta_Productos_Lacteos.Models.CRUDs
{
    public class CRUDUsuarios : ICRUDConexion<UsuarioViewModel, Usuario>
    {
        public bool create(UsuarioViewModel model)
        {
            string sql = "INSERT INTO usuario (cod_usuario, nombre, usuario, contrasena, cod_rol) " +
                "VALUES (@CodigoUsuario, @Nombre, @Usuario, @Contrasena, @CodigoRol)";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoUsuario", model.cod_usuario);
                comando.Parameters.AddWithValue("@Nombre", model.nombre);
                comando.Parameters.AddWithValue("@Usuario", model.usuario);
                comando.Parameters.AddWithValue("@Contrasena", model.contrasena);
                comando.Parameters.AddWithValue("@CodigoRol", model.cod_rol);
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
            string sql = "DELETE FROM usuario WHERE cod_usuario = @CodigoUsuario";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoUsuario", cod);
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

        public List<Usuario> read()
        {
            string sql = "SELECT usuario.cod_usuario, usuario.nombre, usuario.usuario, usuario.contrasena, " +
                "usuario.cod_rol, roles.nombre as nombre_rol FROM usuario " +
                "INNER JOIN roles ON usuario.cod_rol = roles.cod_rol ORDER BY cod_usuario";

            List<Usuario> listaUsuarios = new List<Usuario>();

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
                        Usuario usuario = new Usuario();
                        Rol rol = new Rol();

                        usuario.cod_usuario = reader.GetInt32("cod_usuario");
                        usuario.nombre = reader.GetString("nombre");
                        usuario.usuario = reader.GetString("usuario");
                        usuario.contrasena = reader.GetString("contrasena");

                        rol.cod_rol = reader.GetInt32("cod_rol");
                        rol.nombre = reader.GetString("nombre_rol");

                        usuario.rol = rol;

                        listaUsuarios.Add(usuario);
                    }
                }
                else
                {
                    listaUsuarios = null;
                }
            }
            catch (MySqlException ex)
            {
                listaUsuarios = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return listaUsuarios;
        }

        public bool update(UsuarioViewModel model)
        {

            string sql = "UPDATE usuario SET nombre = @Nombre, usuario = @Usuario, contrasena = @Contrasena, " +
                "cod_rol = @CodigoRol WHERE cod_usuario = @CodigoUsuario";
            bool respuesta = false;

            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoUsuario", model.cod_usuario);
                comando.Parameters.AddWithValue("@Nombre", model.nombre);
                comando.Parameters.AddWithValue("@Usuario", model.usuario);
                comando.Parameters.AddWithValue("@Contrasena", model.contrasena);
                comando.Parameters.AddWithValue("@CodigoRol", model.cod_rol);
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

        public Usuario ValidarUsuario(string usuario, string contrasena)
        {
            string sql = "SELECT usuario.cod_usuario, usuario.nombre, usuario.usuario, usuario.contrasena," +
                " usuario.cod_rol, roles.nombre as nombre_rol FROM usuario INNER JOIN roles" +
                " ON usuario.cod_rol = roles.cod_rol WHERE usuario.usuario = @UsuarioIngresado" +
                " and usuario.contrasena = @ContrasenaIngresada LIMIT 1";

            Usuario usuarioEncontrado = new Usuario();

            MySqlDataReader reader = null;
            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@UsuarioIngresado", usuario);
                comando.Parameters.AddWithValue("@ContrasenaIngresada", contrasena);
                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Usuario _usuario = new Usuario();
                        Rol nuevo_rol = new Rol();

                        _usuario.cod_usuario = reader.GetInt32("cod_usuario");
                        _usuario.nombre = reader.GetString("nombre");
                        _usuario.usuario = reader.GetString("usuario");
                        _usuario.contrasena = reader.GetString("contrasena");

                        nuevo_rol.cod_rol = reader.GetInt32("cod_rol");
                        nuevo_rol.nombre = reader.GetString("nombre_rol");

                        _usuario.rol = nuevo_rol;

                        usuarioEncontrado = _usuario;
                    }
                }
                else{
                    usuarioEncontrado = null;
                }
            }
            catch (MySqlException ex)
            {
                usuarioEncontrado = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return usuarioEncontrado;
        }

        public Usuario read_by_code(int cod)
        {
            string sql = "SELECT usuario.cod_usuario, usuario.nombre, usuario.usuario, usuario.contrasena, " +
                "usuario.cod_rol, roles.nombre as nombre_rol FROM usuario " +
                "INNER JOIN roles ON usuario.cod_rol = roles.cod_rol WHERE cod_usuario = @CodigoUsuario LIMIT 1";

            Usuario usuario = new Usuario();

            MySqlDataReader reader = null;
            MySqlConnection conexionBD = ConexionViewModel.conectar();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.Parameters.AddWithValue("@CodigoUsuario", cod);
                reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Rol rol = new Rol();

                        usuario.cod_usuario = reader.GetInt32("cod_usuario");
                        usuario.nombre = reader.GetString("nombre");
                        usuario.usuario = reader.GetString("usuario");
                        usuario.contrasena = reader.GetString("contrasena");

                        rol.cod_rol = reader.GetInt32("cod_rol");
                        rol.nombre = reader.GetString("nombre_rol");

                        usuario.rol = rol;
                    }
                }
                else
                {
                    usuario = null;
                }
            }
            catch (MySqlException ex)
            {
                usuario = null;
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }

            return usuario;
        }
    }
}
