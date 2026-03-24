using Abstracciones.Modelos.Servicios.TipoCambio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Servicios.TipoCambio.TipoCambio;

namespace Abstracciones.Interfaces.Servicios
{
    public interface ITipoCambioServicio
    {
        Task<TipoDeCambio> ObtenerTipoCambioVenta(DateTime Fecha);
    }
}
