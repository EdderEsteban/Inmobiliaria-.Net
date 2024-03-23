using MySql.Data.MySqlClient;
using Inmobiliaria_.Net.Models;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.Design;
using Mysqlx.Connection;



namespace Inmobiliaria_.Net.Repositorios
{
    public class RepositorioPropietario
    {
        readonly string ConnectionString = "Server=localhost;Database=inmobiliaria_edder_matias;User=root;Password=;";
        public RepositorioPropietario()
        {

        }

        //[Listar]
        public IList<Propietario> ListarPropietarios()
        {
            var propietarios = new List<Propietario>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"Select {nameof(Propietario.Id_Propietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, 
                {nameof(Propietario.Dni)}, {nameof(Propietario.Direccion)}, {nameof(Propietario.Telefono)},{nameof(Propietario.Correo)} 
                FROM propietario";

                using (var comand = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = comand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            propietarios.Add(new Propietario
                            {
                                Id_Propietario = reader.GetInt32(nameof(Propietario.Id_Propietario)),
                                Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetInt32(nameof(Propietario.Dni)),
                                Direccion = reader.GetString(nameof(Propietario.Direccion)),
                                Telefono = reader.GetString(nameof(Propietario.Telefono)),
                                Correo = reader.GetString(nameof(Propietario.Correo))
                            });
                        }
                        connection.Close();
                    }
                }
            }
            return propietarios;
        }
        // [Guardar]
        public int GuardarNuevo(Propietario propietario)
        {
            int Id = 0;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"INSERT INTO propietario ({nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, 
                {nameof(Propietario.Dni)}, {nameof(Propietario.Direccion)}, {nameof(Propietario.Telefono)}, {nameof(Propietario.Correo)})
                VALUES (@{nameof(Propietario.Nombre)}, @{nameof(Propietario.Apellido)}, @{nameof(Propietario.Dni)},
                @{nameof(Propietario.Direccion)}, @{nameof(Propietario.Telefono)}, @{nameof(Propietario.Correo)});
                SELECT LAST_INSERT_ID();";


                using (var comand = new MySqlCommand(sql, connection))
                {
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Dni)}", propietario.Dni);
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Direccion)}", propietario.Direccion);
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Telefono)}", propietario.Telefono);
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Correo)}", propietario.Correo);

                    connection.Open();

                    Id = Convert.ToInt32(comand.ExecuteScalar());
                    propietario.Id_Propietario = Id;
                    connection.Close();
                }
            }
            return Id;
        }

        // [Obtener Propietario]
        public Propietario? ObtenerPropietario(int id) 
        {
            Propietario? propietario = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"Select {nameof(Propietario.Id_Propietario)}, {nameof(Propietario.Nombre)}, {nameof(Propietario.Apellido)}, 
                {nameof(Propietario.Dni)}, {nameof(Propietario.Direccion)}, {nameof(Propietario.Telefono)},{nameof(Propietario.Correo)} 
                FROM propietario
                WHERE {nameof(Propietario.Id_Propietario)} = @{nameof(Propietario.Id_Propietario)}";

                using (var comand = new MySqlCommand(sql, connection))
                {
                    comand.Parameters.AddWithValue($"@{nameof(Propietario.Id_Propietario)}", id);
                    connection.Open();
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            propietario = new Propietario
                            {
                                Id_Propietario = reader.GetInt32(nameof(Propietario.Id_Propietario)),
                                Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetInt32(nameof(Propietario.Dni)),
                                Direccion = reader.GetString(nameof(Propietario.Direccion)),
                                Telefono = reader.GetString(nameof(Propietario.Telefono)),
                                Correo = reader.GetString(nameof(Propietario.Correo))

                            };
                        }
                        connection.Close();
                    }
                }
            }
            return propietario;
        }
        
        // [Actualizar Propietario]
        public void ActualizarPropietario(Propietario propietario)
{
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @$"UPDATE propietario SET
                    {nameof(Propietario.Nombre)} = @{nameof(Propietario.Nombre)},
                    {nameof(Propietario.Apellido)} = @{nameof(Propietario.Apellido)},
                    {nameof(Propietario.Dni)} = @{nameof(Propietario.Dni)},
                    {nameof(Propietario.Direccion)} = @{nameof(Propietario.Direccion)},
                    {nameof(Propietario.Telefono)} = @{nameof(Propietario.Telefono)},
                    {nameof(Propietario.Correo)} = @{nameof(Propietario.Correo)}
                WHERE {nameof(Propietario.Id_Propietario)} = @{nameof(Propietario.Id_Propietario)}";

        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue($"@{nameof(Propietario.Nombre)}", propietario.Nombre);
            command.Parameters.AddWithValue($"@{nameof(Propietario.Apellido)}", propietario.Apellido);
            command.Parameters.AddWithValue($"@{nameof(Propietario.Dni)}", propietario.Dni);
            command.Parameters.AddWithValue($"@{nameof(Propietario.Direccion)}", propietario.Direccion);
            command.Parameters.AddWithValue($"@{nameof(Propietario.Telefono)}", propietario.Telefono);
            command.Parameters.AddWithValue($"@{nameof(Propietario.Correo)}", propietario.Correo);
            command.Parameters.AddWithValue($"@{nameof(Propietario.Id_Propietario)}", propietario.Id_Propietario);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

        // [Eliminar Propietario]
        public int EliminarPropietario(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"DELETE FROM propietario 
                WHERE {nameof(Propietario.Id_Propietario)} = @{nameof(Propietario.Id_Propietario)}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue($"@{nameof(Propietario.Id_Propietario)}", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return 0;
        }
    }
}
