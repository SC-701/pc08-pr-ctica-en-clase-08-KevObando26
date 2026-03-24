using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abstracciones.Interfaces.API;
using Abstracciones.Modelos;
using Abstracciones.Interfaces.Flujo;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoController
    {
        private IProductoFlujo _productoFlujo;
        private ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo productoFlujo, ILogger<ProductoController> logger)
        {
            _productoFlujo = productoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProductoRequest producto)
        {
            var resultado = await _productoFlujo.Agregar(producto);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, resultado);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, ProductoRequest producto)
        {
            var resultado = await _productoFlujo.Editar(Id, producto);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _productoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _productoFlujo.Obtener();
            if (!resultado.Any())

                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("ObtenerPorId/{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _productoFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
