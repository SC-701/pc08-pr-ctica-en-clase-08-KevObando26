using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase, ISubCategoriaController
    {
        private ISubCategoriaFlujo _subcategoriaFlujo;
        private ILogger<SubCategoriaController> _logger;

        public SubCategoriaController(ISubCategoriaFlujo subcategoriaFlujo, ILogger<SubCategoriaController> logger)
        {
            _subcategoriaFlujo = subcategoriaFlujo;
            _logger = logger;
        }


        [HttpGet("{IdCategoria}")]
        public async Task<IActionResult> Obtener(Guid IdCategoria)
        {
            var resultado = await _subcategoriaFlujo.Obtener(IdCategoria);
            return Ok(resultado);
        }


    }
}
