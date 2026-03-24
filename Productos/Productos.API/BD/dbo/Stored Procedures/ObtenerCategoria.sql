CREATE PROCEDURE [dbo].[ObtenerCategoria]

AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id AS IdCategoria,
        Nombre
    FROM Categorias
   
END