using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using Inmobiliaria_.Net.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace Inmobiliaria_.Net.Repositorios
{
    public class RepositorioInmueble
    {
        readonly string ConnectionString =
            "Server=localhost;Database=inmobiliaria_edder_matias;User=root;Password=;";

        public RepositorioInmueble() { }

        //[Listar Todos Inmuebles]
        public IList<Inmueble> ListarTodosInmuebles()
        {
            var inmuebles = new List<Inmueble>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes,
                i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
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
                            //Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmueble.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            //Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmueble
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmueble.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmueble.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmueble.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                                    Propietario = new Propietario
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },
                                    Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                                }
                            );
                        }
                        connection.Close();
                    }
                }
            }
            return inmuebles;
        }

        //[Listar Inmuebles Activos]
        public IList<Inmueble> ListarInmueblesActivos()
        {
            var inmuebles = new List<Inmueble>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes,
                i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
            WHERE i.activo = 1";

                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmueble.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            //Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmueble
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmueble.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmueble.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmueble.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                                    Propietario = new Propietario
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },
                                    Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                                }
                            );
                        }
                        connection.Close();
                    }
                }
            }
            return inmuebles;
        }

        //[Listar Inmuebles Disponibles]
        public IList<Inmueble> ListarInmueblesDisponibles()
        {
            var inmuebles = new List<Inmueble>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes,
                i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
            WHERE i.disponible = 1 AND i.disponible = 1";

                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmueble.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            //Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmueble
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmueble.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmueble.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmueble.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                                    Propietario = new Propietario
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },
                                    Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                                }
                            );
                        }
                        connection.Close();
                    }
                }
            }
            return inmuebles;
        }

        public IList<Inmueble> ListarInmueblesAlquilados()
        {
            var inmuebles = new List<Inmueble>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes,
                i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
            WHERE i.disponible = 0 ";

                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmueble.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            //Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmueble
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmueble.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmueble.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmueble.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                                    Propietario = new Propietario
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },
                                    Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                                }
                            );
                        }
                        connection.Close();
                    }
                }
            }
            return inmuebles;
        }

        // [Listar Tipos Inmueble]
        public IList<InmuebleTipo> ListarTiposInmueble()
        {
            var listado = new List<InmuebleTipo>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = "SELECT * FROM tipo_inmueble;";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listado.Add(
                                new InmuebleTipo
                                {
                                    Id_tipo = reader.GetInt32("Id_Tipo"),
                                    Tipo = reader.GetString("Tipo")
                                }
                            );
                        }
                    }
                    connection.Close();
                }
            }
            return listado;
        }

        // [Guardar]
        public int GuardarNuevo(Inmueble inmueble)
        {
            int Id = 0;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @$"INSERT INTO inmueble ({nameof(Inmueble.Direccion)}, {nameof(Inmueble.Uso)}, 
                {nameof(Inmueble.Id_tipo)}, {nameof(Inmueble.Cantidad_Ambientes)}, {nameof(Inmueble.Precio_Alquiler)}, 
                {nameof(Inmueble.Latitud)}, {nameof(Inmueble.Longitud)}, {nameof(Inmueble.Activo)}, {nameof(Inmueble.Disponible)},
                {nameof(Inmueble.Id_propietario)})
                VALUES (@{nameof(Inmueble.Direccion)}, @{nameof(Inmueble.Uso)}, @{nameof(Inmueble.Id_tipo)},
                 @{nameof(Inmueble.Cantidad_Ambientes)}, @{nameof(Inmueble.Precio_Alquiler)},
                @{nameof(Inmueble.Latitud)}, @{nameof(Inmueble.Longitud)}, @{nameof(Inmueble.Activo)}, 
                @{nameof(Inmueble.Disponible)}, @{nameof(Inmueble.Id_propietario)});
                SELECT LAST_INSERT_ID();";

                using (var comand = new MySqlCommand(sql, connection))
                {
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Direccion)}",
                        inmueble.Direccion
                    );
                    comand.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_tipo)}",
                        inmueble.Id_tipo
                    );
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Cantidad_Ambientes)}",
                        inmueble.Cantidad_Ambientes
                    );
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Precio_Alquiler)}",
                        inmueble.Precio_Alquiler
                    );
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Latitud)}",
                        inmueble.Latitud
                    );
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Longitud)}",
                        inmueble.Longitud
                    );
                    comand.Parameters.AddWithValue($"@{nameof(Inmueble.Activo)}", inmueble.Activo);
                    comand.Parameters.AddWithValue($"@{nameof(Inmueble.Disponible)}",inmueble.Disponible);
                    comand.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_propietario)}",
                        inmueble.Id_propietario
                    );

                    connection.Open();

                    Id = Convert.ToInt32(comand.ExecuteScalar());
                    inmueble.Id_inmueble = Id;
                    connection.Close();
                }
            }
            return Id;
        }

        // [Obtener Inmueble]
        public Inmueble? ObtenerInmueble(int id)
        {
            Inmueble? inmueble = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes,
                i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
            WHERE i.id_inmueble = @id;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmueble.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            //Fin Manejo de los Enum en C#

                            inmueble = new Inmueble
                            {
                                Id_inmueble = reader.GetInt32(nameof(Inmueble.Id_inmueble)),
                                Direccion = reader.GetString(nameof(Inmueble.Direccion)),
                                Uso = usoEnum,
                                Tipo = new InmuebleTipo
                                {
                                    Tipo = reader.GetString("TipoInmueble"),
                                },
                                Cantidad_Ambientes = reader.GetInt32(
                                    nameof(Inmueble.Cantidad_Ambientes)
                                ),
                                Precio_Alquiler = reader.GetDecimal(
                                    nameof(Inmueble.Precio_Alquiler)
                                ),
                                Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                                Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                                Id_propietario = reader.GetInt32(nameof(Inmueble.Id_propietario)),
                                Propietario = new Propietario
                                {
                                    Nombre = reader.GetString("NombrePropietario"),
                                    Apellido = reader.GetString("ApellidoPropietario"),
                                },
                                Activo = reader.GetBoolean(nameof(Inmueble.Activo)),
                                Disponible = reader.GetBoolean(nameof(Inmueble.Disponible)),
                            };
                        }
                        connection.Close();
                    }
                }
            }
            return inmueble;
        }

        // [Actualizar Inmueble]
        public void ActualizarInmueble(Inmueble inmueble)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @$"UPDATE Inmueble SET
                            {nameof(Inmueble.Direccion)} = @{nameof(Inmueble.Direccion)},
                            {nameof(Inmueble.Uso)} = @{nameof(Inmueble.Uso)},
                            {nameof(Inmueble.Id_tipo)} = @{nameof(Inmueble.Id_tipo)},
                            {nameof(Inmueble.Cantidad_Ambientes)} = @{nameof(Inmueble.Cantidad_Ambientes)},
                            {nameof(Inmueble.Precio_Alquiler)} = @{nameof(Inmueble.Precio_Alquiler)},
                            {nameof(Inmueble.Latitud)} = @{nameof(Inmueble.Latitud)},
                            {nameof(Inmueble.Longitud)} = @{nameof(Inmueble.Longitud)},
                            {nameof(Inmueble.Activo)} = @{nameof(Inmueble.Activo)},
                            {nameof(Inmueble.Disponible)} = @{nameof(Inmueble.Disponible)},
                            {nameof(Inmueble.Id_propietario)} = @{nameof(Inmueble.Id_propietario)}
                        WHERE {nameof(Inmueble.Id_inmueble)} = @{nameof(Inmueble.Id_inmueble)}";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_inmueble)}",
                        inmueble.Id_inmueble
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Direccion)}",
                        inmueble.Direccion
                    );
                    command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_tipo)}",
                        inmueble.Id_tipo
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Cantidad_Ambientes)}",
                        inmueble.Cantidad_Ambientes
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Precio_Alquiler)}",
                        inmueble.Precio_Alquiler
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Latitud)}",
                        inmueble.Latitud
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Longitud)}",
                        inmueble.Longitud
                    );
                    command.Parameters.AddWithValue($"@{nameof(Inmueble.Activo)}", inmueble.Activo);
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Disponible)}",
                        inmueble.Disponible
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_propietario)}",
                        inmueble.Id_propietario
                    );

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void ActualizarInmuebleExceptoDisponible(Inmueble inmueble)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @$"UPDATE Inmueble SET
                            {nameof(Inmueble.Direccion)} = @{nameof(Inmueble.Direccion)},
                            {nameof(Inmueble.Uso)} = @{nameof(Inmueble.Uso)},
                            {nameof(Inmueble.Id_tipo)} = @{nameof(Inmueble.Id_tipo)},
                            {nameof(Inmueble.Cantidad_Ambientes)} = @{nameof(Inmueble.Cantidad_Ambientes)},
                            {nameof(Inmueble.Precio_Alquiler)} = @{nameof(Inmueble.Precio_Alquiler)},
                            {nameof(Inmueble.Latitud)} = @{nameof(Inmueble.Latitud)},
                            {nameof(Inmueble.Longitud)} = @{nameof(Inmueble.Longitud)},
                            {nameof(Inmueble.Activo)} = @{nameof(Inmueble.Activo)},
                            {nameof(Inmueble.Disponible)} = @{nameof(Inmueble.Disponible)},
                            {nameof(Inmueble.Id_propietario)} = @{nameof(Inmueble.Id_propietario)}
                        WHERE {nameof(Inmueble.Id_inmueble)} = @{nameof(Inmueble.Id_inmueble)}";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_inmueble)}",
                        inmueble.Id_inmueble
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Direccion)}",
                        inmueble.Direccion
                    );
                    command.Parameters.AddWithValue($"@{nameof(Inmueble.Uso)}", inmueble.Uso);
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_tipo)}",
                        inmueble.Id_tipo
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Cantidad_Ambientes)}",
                        inmueble.Cantidad_Ambientes
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Precio_Alquiler)}",
                        inmueble.Precio_Alquiler
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Latitud)}",
                        inmueble.Latitud
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Longitud)}",
                        inmueble.Longitud
                    );
                    command.Parameters.AddWithValue($"@{nameof(Inmueble.Activo)}", inmueble.Activo);
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Disponible)}",
                        inmueble.Disponible
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmueble.Id_propietario)}",
                        inmueble.Id_propietario
                    );

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        // [Cambiar Disponibilidad de Inmueble]
        public int CambiarEstadoInmueble(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    $"UPDATE inmueble SET {nameof(Inmueble.Disponible)} = 0 WHERE {nameof(Inmueble.Id_inmueble)} = @{nameof(Inmueble.Id_inmueble)}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue($"@{nameof(Inmueble.Id_inmueble)}", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return 0;
        }

        // [Eliminar de BD Inmueble]
        public int EliminarInmueble(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @$"DELETE FROM inmueble 
                WHERE {nameof(Inmueble.Id_inmueble)} = @{nameof(Inmueble.Id_inmueble)}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue($"@{nameof(Inmueble.Id_inmueble)}", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return 0;
        }
    }
}
