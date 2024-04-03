-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_InsertarConcepto
	@ApartadoId int,
	@Concepto nvarchar(250),
	@Unifamiliar bit,
	@Normal bit,
	@Redensificacion bit,
	@Concredito bit
AS
BEGIN

INSERT INTO Cat_ConceptoLB (ApartadoID,Concepto,Unifamiliar,Normal,Redensificacion,Concredito) values (@ApartadoId,@Concepto,@Unifamiliar,@Normal,@Redensificacion,@Concredito)
SELECT SCOPE_IDENTITY()
END
