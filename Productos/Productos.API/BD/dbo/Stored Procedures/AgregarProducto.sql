
CREATE PROCEDURE AgregarProducto 

   @Id AS uniqueidentifier
          ,@IdSubCategoria  AS uniqueidentifier
          ,@Nombre         AS VARCHAR (MAX)
          ,@Descripcion    AS  VARCHAR (MAX)
          ,@Precio         AS  DECIMAL (18, 2) 
          ,@Stock          AS INT    
          ,@CodigoBarras  AS VARCHAR (MAX)
AS
BEGIN
SET NOCOUNT ON;
BEGIN TRANSACTION
	INSERT INTO [dbo].[Producto]
           ([Id]
           ,[IdSubCategoria]
           ,[Nombre]
           ,[Descripcion]
           ,[Precio]
           ,[Stock]
           ,[CodigoBarras])
     VALUES
           (@Id, 
           @IdSubCategoria, 
           @Nombre,@Descripcion,
           @Precio,@Stock,
           @CodigoBarras)
SELECT @Id
COMMIT TRANSACTION
END