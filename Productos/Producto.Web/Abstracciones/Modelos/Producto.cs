using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
       
        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9 ]{2,50}$", ErrorMessage = "El nombre solo puede contener letras, números y espacios, y tener entre 2 y 50 caracteres.")]
        public string Nombre { get; set; }


        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres.")]
        [Required(ErrorMessage = "La  descripcion es requerida")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 100000000, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "El stock debe ser un número entero mayor o igual a 0.")]
        [Required(ErrorMessage = "La propiedad stock es requerida")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La propiedad codigo de barras es requerida")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "El código de barras debe tener exactamente 12 dígitos.")]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string? SubCategoria { get; set; }
        public string? Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse
    {
        public decimal PrecioUSD { get; set; }
    }

}
