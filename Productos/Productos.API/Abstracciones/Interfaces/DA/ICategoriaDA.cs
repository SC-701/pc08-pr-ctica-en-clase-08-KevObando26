using Abstracciones.Modelos;
using System.Text.RegularExpressions;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaDA
    {
        Task<IEnumerable<Categoria>> Obtener();
    }
}
