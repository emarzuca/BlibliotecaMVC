using BlibliotecaMVC.Models;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BlibliotecaMVC.Servicios
{
    //Implementamos la Interfase para despues darla de altra en servicios

    public interface IRepositorioApartados
    {
        Task Actualizar(ApartadosVierModel apartado);
        Task Borrar(int id);
        Task Crear(Apartado apartado);
        Task<IEnumerable<Apartado>> FiltrarArea();
        Task<IEnumerable<Apartado>> ListadeApartados();
        Task<IEnumerable<ViewModelCatApartados>> ListadeApartados2();
        Task<ApartadosVierModel> ObtenerApartadoId(int Id);
        Task<Apartado> ObtenerPorID(int id);
    }

    public class RepositorioApartados : IRepositorioApartados
    {
        // Creamos un método privado para conecciones de datos
        private readonly string connectionString;


        public RepositorioApartados(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task Crear(Apartado apartado)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Cat_apartados 
                (IdentificacionDirectorio, Apartado, AreaID) 
                values (@Directorio,@apartado,@AreaID);
                SELECT SCOPE_IDENTITY();", apartado);
            apartado.AreaId= id;
        }

        public async Task<IEnumerable<Apartado>> ListadeApartados()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Apartado>(@"SELECT Cat_apartados.ApartadoID, Cat_Area.Area, Cat_apartados.Apartado
                FROM 
                Cat_apartados INNER JOIN Cat_Area ON Cat_apartados.AreaID = Cat_Area.AreaId");
        }


        public async Task<IEnumerable<ViewModelCatApartados>> ListadeApartados2()
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<ViewModelCatApartados>
                ("SP_VisualizaApartados", new {}, commandType: CommandType.StoredProcedure);

           
        }

        //Obtener áreas
        public async Task<IEnumerable<Apartado>> FiltrarArea()
        {
            using var connection = new SqlConnection(connectionString);


            var cadena = await connection.QueryAsync<Apartado>
                  ("SP_DespliegaAreas", new { }, commandType: CommandType.StoredProcedure);
            return cadena;

            //return await connection.QueryAsync<Apartado>("SELECT AreaID,Area from " +
            //    "Cat_Area order by area ");

            //return await connection.QuerySingleAsync<Apartado>(@"SP_DespliegaExpedientes",
            //commandType: CommandType.StoredProcedure);
        }

        public async Task<Apartado> ObtenerPorID(int id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<ApartadosVierModel>(
                @"select apartadoId, Apartado, IdentificacionDirectorio, Cat_Area.AreaId, Area from Cat_apartados INNER JOIN Cat_Area on
                  Cat_Area.AreaId = Cat_apartados.AreaId
                 WHERE ApartadoId = @Id ", new { id });
        }


        public async Task Borrar(int ApartadoId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("SP_ELIMINAR_APARTADOS", new { ApartadoId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<ApartadosVierModel> ObtenerApartadoId(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<ApartadosVierModel>(
                @"select apartadoId, Apartado, IdentificacionDirectorio, Cat_Area.AreaId as AreaId, Area 
                  from Cat_apartados INNER JOIN Cat_Area on
                 Cat_Area.AreaId = Cat_apartados.AreaID
                 WHERE ApartadoId = @Id ", new { Id });
        }

        public async Task Actualizar(ApartadosVierModel apartado)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                @"UPDATE Cat_apartados set apartado=@apartado, 
                  IdentificacionDirectorio=@identificacionDirectorio,
                  areaId = @AreaId
                  WHERE ApartadoId = @ApartadoId", apartado);
        }
    }
}
