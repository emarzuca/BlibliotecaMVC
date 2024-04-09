using BlibliotecaMVC.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using BlibliotecaMVC.Migrations;
using static BlibliotecaMVC.Models.Apartado;

namespace BlibliotecaMVC.Servicios
{

    public interface IRepositorioSubApartados
    {
        Task Borrar(int Id);
        Task Crear(ViewModelListadoApartado modelo);
        Task Guardar(ViewModelListadoApartado SubConceptos);
        Task<VMDetalleSubApartado> ObtenerConceptoId(int Id);
        Task<IEnumerable<VMDetalleSubApartado>> ObtenerDetalleSubApartados(int filtro);
        Task<IEnumerable<ViewModelSubAparadosEstilos>> ObtenerListaSubApartados(int filtro);  
    }
    public class RepositorioSubApartados: IRepositorioSubApartados
    {
        private readonly string connectionString;

        public RepositorioSubApartados(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ViewModelSubAparadosEstilos>> ObtenerListaSubApartados(int filtro)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<ViewModelSubAparadosEstilos>("SP_VisualizaListaSubaparados"
            , new { filtro }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<VMDetalleSubApartado>> ObtenerDetalleSubApartados(int filtro)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<VMDetalleSubApartado>("SP_VisualizaListaSubaparados"
            , new { filtro }, commandType: CommandType.StoredProcedure);
        }

        public async Task<VMDetalleSubApartado> ObtenerConceptoId(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<VMDetalleSubApartado>("SP_ObtenerIdConcepto"
            , new { Id }, commandType: CommandType.StoredProcedure);
        }

        public async Task Crear(ViewModelListadoApartado modelo)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Cat_ConceptoLB 
                    (ApartadoID,Concepto,Unifamiliar,Normal,Redensificacion,Concredito) 
                values 
                (@ApartadoId,@Concepto,@Unifamiliar,@Normal,@Redensificacion,@Concredito);
                SELECT SCOPE_IDENTITY();", modelo);
            modelo.ConceptoID = id;
        }


        public async Task Guardar(ViewModelListadoApartado SubConceptos)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("SP_actualizaSubApartados",
                new { SubConceptos.ApartadoId, SubConceptos.ConceptoID,
                    SubConceptos.Concepto,
                    SubConceptos.Normal,
                    SubConceptos.ConCredito,
                    SubConceptos.Unifamiliar,
                    SubConceptos.Redensificacion}, commandType: CommandType.StoredProcedure);
        }


        public async Task Borrar(int Id)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("SP_eliminaSubApartados", new { Id }, commandType: CommandType.StoredProcedure);

        }

    }
}
