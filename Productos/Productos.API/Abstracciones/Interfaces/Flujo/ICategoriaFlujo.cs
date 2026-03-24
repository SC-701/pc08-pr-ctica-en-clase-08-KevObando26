using Abstracciones.Modelos;
using System.Text.RegularExpressions;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICategoriaFlujo
    {

        Task<IEnumerable<Categoria>> Obtener();

    }
}
