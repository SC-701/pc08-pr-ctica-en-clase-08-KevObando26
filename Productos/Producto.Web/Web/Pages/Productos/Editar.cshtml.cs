using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;

        public ProductoRequest productoRequest { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; } = new();

        [BindProperty]
        public Guid categoriaSeleccionada { get; set; } = default!;

        [BindProperty]
        public Guid subCategoriaSeleccionada { get; set; } = default!;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<ActionResult> OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategoriasAsync();

                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

                if (producto != null)
                {
                    var categoriaActual = categorias.FirstOrDefault(c => c.Text == producto.Categoria);
                    if (categoriaActual != null)
                    {
                        categoriaSeleccionada = Guid.Parse(categoriaActual.Value);

                        subcategorias = (await ObtenerSubCategoriasAsync(categoriaSeleccionada))
                            .Select(s => new SelectListItem
                            {
                                Value = s.IdSubCategoria.ToString(),
                                Text = s.Nombre,
                                Selected = s.Nombre == producto.SubCategoria
                            }).ToList();

                        var subCategoriaActual = subcategorias.FirstOrDefault(s => s.Text == producto.SubCategoria);
                        if (subCategoriaActual != null)
                        {
                            subCategoriaSeleccionada = Guid.Parse(subCategoriaActual.Value);
                        }
                    }
                }
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (producto.Id == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await ObtenerCategoriasAsync();

                if (categoriaSeleccionada != Guid.Empty)
                {
                    subcategorias = (await ObtenerSubCategoriasAsync(categoriaSeleccionada))
                        .Select(s => new SelectListItem
                        {
                            Value = s.IdSubCategoria.ToString(),
                            Text = s.Nombre,
                            Selected = s.IdSubCategoria == subCategoriaSeleccionada
                        }).ToList();
                }

                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = new HttpClient();

            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(
                string.Format(endpoint, producto.Id.ToString()),
                new ProductoRequest
                {
                    IdSubCategoria = subCategoriaSeleccionada,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CodigoBarras = producto.CodigoBarras
                });

            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategoriasAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultadoDeserializado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);

                categorias = resultadoDeserializado.Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.Nombre
                }).ToList();
            }
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid idCategoria)
        {
            var listaSubCategorias = await ObtenerSubCategoriasAsync(idCategoria);
            return new JsonResult(listaSubCategorias);
        }

        private async Task<List<SubCategoria>> ObtenerSubCategoriasAsync(Guid idCategoria)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, idCategoria));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }

            return new List<SubCategoria>();
        }
    }
}