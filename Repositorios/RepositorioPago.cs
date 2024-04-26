using Inmobiliaria_.Net.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria_.Net.Repositorios
{
    public class RepositorioPago
    {
        readonly string ConnectionString =
            "Server=localhost;Database=inmobiliaria_edder_matias;User=root;Password=;";

        public RepositorioPago() { }

        public IList<Pago> ListarPagos()
        {
            var pagos = new List<Pago>();
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @"SELECT id_pago, id_contrato, fecha_pago, monto
                FROM pago";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pagos.Add(
                                new Pago
                                {
                                    Id_Pago = reader.GetInt32("id_pago"),
                                    Id_Contrato = reader.GetInt32("id_contrato"),
                                    Fecha_Pago = reader.GetDateTime("fecha_pago"),
                                    Monto = reader.GetInt32("monto"),
                                }
                            );
                        }
                    }
                    connection.Close();
                }
            }
            return pagos;
        }

        public int GuardarNuevo(Pago pago)
        {
            int id = 0;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @"INSERT INTO pago (id_contrato, fecha_pago, monto)
                            VALUES (@id_contrato, @fecha_pago, @monto);
                            SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_contrato", pago.Id_Contrato);
                    command.Parameters.AddWithValue("@fecha_pago", pago.Fecha_Pago);
                    command.Parameters.AddWithValue("@monto", pago.Monto);

                    connection.Open();
                    id = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            return id;
        }

        public Pago ObtenerPago(int id)
        {
            Pago? pago = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    @"SELECT id_pago, id_contrato, fecha_pago, monto
                            FROM pago
                            WHERE id_pago = @id_pago";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_pago", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pago = new Pago
                            {
                                Id_Pago = reader.GetInt32("id_pago"),
                                Id_Contrato = reader.GetInt32("id_contrato"),
                                Fecha_Pago = reader.GetDateTime("fecha_pago"),
                                Monto = reader.GetInt32("monto"),
                            };
                        }
                    }
                    connection.Close();
                }
            }
            return pago;
        }

        /*public Contrato ObtenerContratoInmueble(int id)
        {
            Contrato? contrato = null;
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin, vigencia
                            FROM contrato
                            WHERE id_inmueble = @id_inmueble";
                            
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_inmueble", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            contrato = new Contrato
                            {
                                Id_contrato = reader.GetInt32("id_contrato"),
                                Id_inquilino = reader.GetInt32("id_inquilino"),
                                Id_inmueble = reader.GetInt32("id_inmueble"),
                                Monto = reader.GetInt32("monto"),
                                Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                                Fecha_fin = reader.GetDateTime("fecha_fin"),
                                Vigencia = reader.GetBoolean("vigencia")
                            };
                        }
                    }
                    connection.Close();
                }
            }
            return contrato;
        }*/

        /*public void ActualizarContrato(Contrato contrato)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = @"UPDATE contrato
                            SET id_inquilino = @id_inquilino,
                                id_inmueble = @id_inmueble,
                                monto = @monto,
                                fecha_inicio = @fecha_inicio,
                                fecha_fin = @fecha_fin
                            WHERE id_contrato = @id_contrato";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_inquilino", contrato.Id_inquilino);
                    command.Parameters.AddWithValue("@id_inmueble", contrato.Id_inmueble);
                    command.Parameters.AddWithValue("@monto", contrato.Monto);
                    command.Parameters.AddWithValue("@fecha_inicio", contrato.Fecha_inicio);
                    command.Parameters.AddWithValue("@fecha_fin", contrato.Fecha_fin);
                    command.Parameters.AddWithValue("@id_contrato", contrato.Id_contrato);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }*/

        public void EliminarPago(int id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql = "DELETE FROM pago WHERE id_pago = @id_pago";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_pago", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        /* public IList<Contrato> ListarContratosPorInmueble(int idInmueble)
 {
     var contratos = new List<Contrato>();
 
     using (var connection = new MySqlConnection(ConnectionString))
     {
         var sql = "SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin " +
                   "FROM contrato " +
                   "WHERE id_inmueble = @IdInmueble";
 
         using (var command = new MySqlCommand(sql, connection))
         {
             command.Parameters.AddWithValue("@IdInmueble", idInmueble);
 
             connection.Open();
             using (var reader = command.ExecuteReader())
             {
                 while (reader.Read())
                 {
                     contratos.Add(new Contrato
                     {
                         Id_contrato = reader.GetInt32("id_contrato"),
                         Id_inquilino = reader.GetInt32("id_inquilino"),
                         Id_inmueble = reader.GetInt32("id_inmueble"),
                         Monto = reader.GetInt32("monto"),
                         Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                         Fecha_fin = reader.GetDateTime("fecha_fin")
                     });
                 }
             }
             connection.Close();
         }
     }
 
     return contratos;
 }*/
    }
}
