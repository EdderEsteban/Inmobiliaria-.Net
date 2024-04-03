using MySql.Data.MySqlClient;
using Inmobiliaria_.Net.Models;
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.Design;
using Mysqlx.Connection;



namespace Inmobiliaria_.Net.Repositorios
{
    public class RepositorioInmueble
    {
        readonly string ConnectionString = "Server=localhost;Database=inmobiliaria_edder_matias;User=root;Password=;";
        public RepositorioInmueble()
        {

        }

        //[Listar]
       public IList<Inmueble> ListarInmuebles()
{
    var inmuebles = new List<Inmueble>();
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @" SELECT Id_inmueble, Direccion, Uso, Id_tipo, ti.Nombre AS TipoInmueble, Cantidad_Ambientes,
                Precio_Alquiler, Latitud, Longitud, Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario";

        using (var command = new MySqlCommand(sql, connection))
        {
            connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usoString = reader.GetString("Uso");
                            var usoEnum = (UsoInmueble)Enum.Parse(typeof(UsoInmueble), usoString);
                            inmuebles.Add(new Inmueble
                            {
                                Id_inmueble = reader.GetInt32(nameof(Inmueble.Id_inmueble)),
                                Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                Uso = usoEnum,
                                Tipo = new InmuebleTipo
                                {
                                    Nombre = reader.GetString(nameof(InmuebleTipo.Nombre)),
                                },
                                Cantidad_Ambientes = reader.GetInt32(nameof(Inmueble.Cantidad_Ambientes)),
                                Precio_Alquiler = reader.GetDecimal(nameof(Inmueble.Precio_Alquiler)),
                                Latitud = reader.GetDecimal(nameof(Inmueble.Latitud)),
                                Longitud = reader.GetDecimal(nameof(Inmueble.Longitud)),
                                Propietario = new Propietario
                                {
                                    Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                    Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                }
                            });
                        }
                        connection.Close();
                    }  
        }
    }
    return inmuebles;
}

        // [Guardar]
        /*public int GuardarNuevo(Inquilino inquilino)
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

        // [Eliminar Inquilino]
        public int EliminarInquilino(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @$"DELETE FROM inquilino 
                WHERE {nameof(Inquilino.Id_inquilino)} = @{nameof(Inquilino.Id_inquilino)}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue($"@{nameof(Inquilino.Id_inquilino)}", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return 0;
        }*/
    }
}
