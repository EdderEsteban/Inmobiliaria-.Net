using MySql.Data.MySqlClient;
using Inmobiliaria_.Net.Models;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.Design;
using Mysqlx.Connection;



namespace Inmobiliaria_.Net.Repositorios
{
    public class RepositorioInquilino
    {
        readonly string ConnectionString = "Server=localhost;Database=inmobiliaria_edder_matias;User=root;Password=;";
        public RepositorioInquilino()
        {

        }

        //[Listar]
        public IList<Inquilino> Listar()
        {
            var inquilinos = new List<Inquilino>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"Select {nameof(Inquilino.Id_inquilino)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, 
                {nameof(Inquilino.Dni)}, {nameof(Inquilino.Direccion)}, {nameof(Inquilino.Telefono)},{nameof(Inquilino.Correo)} FROM inquilino";

                using (var comand = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = comand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inquilinos.Add(new Inquilino
                            {
                                Id_inquilino = reader.GetInt32(nameof(Inquilino.Id_inquilino)),
                                Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                                Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                                Dni = reader.GetInt32(nameof(Inquilino.Dni)),
                                Direccion = reader.GetString(nameof(Inquilino.Direccion)),
                                Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                                Correo = reader.GetString(nameof(Inquilino.Correo))
                            });
                        }
                        connection.Close();
                    }
                }
            }
            return inquilinos;
        }
        // [Guardar]
        public int GuardarNuevo(Inquilino inquilino)
        {
            int Id = 0;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"INSERT INTO inquilino ({nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, 
                {nameof(Inquilino.Dni)}, {nameof(Inquilino.Direccion)}, {nameof(Inquilino.Telefono)}, {nameof(Inquilino.Correo)})
                VALUES (@{nameof(Inquilino.Nombre)}, @{nameof(Inquilino.Apellido)}, @{nameof(Inquilino.Dni)},
                @{nameof(Inquilino.Direccion)}, @{nameof(Inquilino.Telefono)}, @{nameof(Inquilino.Correo)});
                SELECT LAST_INSERT_ID();";


                using (var comand = new MySqlCommand(sql, connection))
                {
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}", inquilino.Apellido);
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Dni)}", inquilino.Dni);
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Direccion)}", inquilino.Direccion);
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Telefono)}", inquilino.Telefono);
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Correo)}", inquilino.Correo);

                    connection.Open();

                    Id = Convert.ToInt32(comand.ExecuteScalar());
                    inquilino.Id_inquilino = Id;
                    connection.Close();
                }
            }
            return Id;
        }

        // [Obtener Inquilino]
        public Inquilino? ObtenerInquilino(int id) 
        {
            Inquilino? inquilino = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"Select {nameof(Inquilino.Id_inquilino)}, {nameof(Inquilino.Nombre)}, {nameof(Inquilino.Apellido)}, 
                {nameof(Inquilino.Dni)}, {nameof(Inquilino.Direccion)}, {nameof(Inquilino.Telefono)},{nameof(Inquilino.Correo)} 
                FROM inquilino
                WHERE {nameof(Inquilino.Id_inquilino)} = @{nameof(Inquilino.Id_inquilino)}";

                using (var comand = new MySqlCommand(sql, connection))
                {
                    comand.Parameters.AddWithValue($"@{nameof(Inquilino.Id_inquilino)}", id);
                    connection.Open();
                    using (var reader = comand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inquilino = new Inquilino
                            {
                                Id_inquilino = reader.GetInt32(nameof(Inquilino.Id_inquilino)),
                                Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                                Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                                Dni = reader.GetInt32(nameof(Inquilino.Dni)),
                                Direccion = reader.GetString(nameof(Inquilino.Direccion)),
                                Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                                Correo = reader.GetString(nameof(Inquilino.Correo))

                            };
                        }
                        connection.Close();
                    }
                }
            }
            return inquilino;
        }
        
        // [Actualizar Inquilino]
        public void ActualizarInquilino(Inquilino inquilino)
{
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @$"UPDATE inquilino SET
                    {nameof(Inquilino.Nombre)} = @{nameof(Inquilino.Nombre)},
                    {nameof(Inquilino.Apellido)} = @{nameof(Inquilino.Apellido)},
                    {nameof(Inquilino.Dni)} = @{nameof(Inquilino.Dni)},
                    {nameof(Inquilino.Direccion)} = @{nameof(Inquilino.Direccion)},
                    {nameof(Inquilino.Telefono)} = @{nameof(Inquilino.Telefono)},
                    {nameof(Inquilino.Correo)} = @{nameof(Inquilino.Correo)}
                WHERE {nameof(Inquilino.Id_inquilino)} = @{nameof(Inquilino.Id_inquilino)}";

        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Nombre)}", inquilino.Nombre);
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Apellido)}", inquilino.Apellido);
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Dni)}", inquilino.Dni);
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Direccion)}", inquilino.Direccion);
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Telefono)}", inquilino.Telefono);
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Correo)}", inquilino.Correo);
            command.Parameters.AddWithValue($"@{nameof(Inquilino.Id_inquilino)}", inquilino.Id_inquilino);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

    }
}
