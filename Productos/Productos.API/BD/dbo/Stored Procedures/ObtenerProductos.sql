-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerProductos
	
AS
BEGIN

	SET NOCOUNT ON;

    SELECT
        Producto.Id,
        Producto.IdSubCategoria,
        Producto.Nombre,
        Producto.Descripcion,
        Producto.Precio,
        Producto.Stock,
        Producto.CodigoBarras,
        Categorias.Nombre AS Categoria, SubCategorias.Nombre AS SubCategoria
    FROM dbo.Producto INNER JOIN 
      dbo.SubCategorias ON dbo.Producto.IdSubCategoria = dbo.SubCategorias.Id
        INNER JOIN dbo.Categorias ON dbo.SubCategorias.IdCategoria = dbo.Categorias.Id

END