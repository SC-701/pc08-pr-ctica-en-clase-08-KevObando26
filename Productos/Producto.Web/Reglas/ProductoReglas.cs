using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TipoCambio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;
        private readonly IConfiguracion _configuracion;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio, IConfiguracion configuracion)
        {
            _tipoCambioServicio = tipoCambioServicio;
            _configuracion = configuracion;
        }

        public async Task<decimal> CalcularPrecioUSD(decimal precioCRC)
        {
            var resultado = await _tipoCambioServicio.ObtenerTipoCambioVenta(DateTime.Now);

            var tipoCambioVenta = ObtenerTipoCambio(resultado);

            return precioCRC / tipoCambioVenta;
        }
        private static decimal ObtenerTipoCambio(TipoCambio.TipoDeCambio resultado)
        {
            return resultado.datos.First().indicadores.First().series.First().valorDatoPorPeriodo;
        }
    }
}
