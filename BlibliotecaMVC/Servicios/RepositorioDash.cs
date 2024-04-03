using BlibliotecaMVC.Models;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;


namespace BlibliotecaMVC.Servicios
{

    public interface IRepositorioDash
    {
        Task<IEnumerable<ViewModelDash>> ObtenerDatosParaGrafica();
    }
    public class RepositorioDash : IRepositorioDash
    {
        // Creamos un método provado para conecciones de datos
        private readonly string connectionString;
        public RepositorioDash(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ViewModelDash>> ObtenerDatosParaGrafica()
        {

            using var connection = new SqlConnection(connectionString);
            var datos = await connection.QueryAsync<ViewModelDash>("SP_DASHBOARD", commandType: CommandType.StoredProcedure);
            return datos;
        }

    }

}
