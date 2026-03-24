using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DA
{
    public class SubCategoriaDA : ISubCategoriaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor
        public SubCategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
      

        public async Task<IEnumerable<SubCategoria>> Obtener(Guid IdCategoria)
        {
            string query = @"ObtenerSubCategoria";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoria>(query, new { IdCategoria = IdCategoria });
            return resultadoConsulta;
        }
        #endregion


    }
}
