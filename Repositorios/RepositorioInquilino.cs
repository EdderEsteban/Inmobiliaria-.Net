using MySql.Data.MySqlClient;
using Inmobiliaria_.Net.Models;



namespace Inmobiliaria_.Net.Repositorios
{
    public class RepositorioInquilino
    {
        readonly string ConnectionString = "Server=localhost;Database=inmobiliaria_edder;User=root;Password=;";
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
                    }
                }
            }
            return inquilinos;
        }
    }
}
