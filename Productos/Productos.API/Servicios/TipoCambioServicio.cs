using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TipoCambio;
using System.Numerics;
using System.Text.Json;
using static Abstracciones.Modelos.Servicios.TipoCambio.TipoCambio;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public TipoCambioServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<TipoDeCambio> ObtenerTipoCambioVenta(DateTime Fecha)
        {

            var EndPoint = _configuracion.ObtenerMetodo("BancoCentralCR", "ObtenerTipoCambio");
            var FechaInicio = Fecha.ToString("yyyy/MM/dd");
            var FechaFin = Fecha.ToString("yyyy/MM/dd");
            var Idioma = "ES";
            var Token = _configuracion.ObtenerValor("BearerToken");


            var ServicioTipoCambio = _httpClient.CreateClient("ServicioTipoCambio");

            ServicioTipoCambio.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            var respuesta = await ServicioTipoCambio.GetAsync(string.Format(EndPoint, FechaInicio, FechaFin, Idioma));
            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultadoDeserializado =
                JsonSerializer.Deserialize<TipoDeCambio>(resultado, opciones);

            return resultadoDeserializado;
        }
    }
}
