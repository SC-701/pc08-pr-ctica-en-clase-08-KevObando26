
CREATE PROCEDURE EliminarProducto 

   @Id AS uniqueidentifier
AS
BEGIN

SET NOCOUNT ON;

BEGIN TRANSACTION
	DELETE FROM [dbo].[Producto]
         WHERE (Id = @Id)
SELECT @Id
COMMIT TRANSACTION
END

