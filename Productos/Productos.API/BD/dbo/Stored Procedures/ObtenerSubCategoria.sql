CREATE   PROCEDURE [dbo].[ObtenerSubCategoria]
 @IdCategoria UNIQUEIDENTIFIER
 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id AS IdSubCategoria,
        IdCategoria,
        Nombre
    FROM SubCategorias
     WHERE (IdCategoria = @IdCategoria)
END