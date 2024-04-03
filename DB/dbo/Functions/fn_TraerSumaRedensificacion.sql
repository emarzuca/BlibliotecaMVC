-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_TraerSumaRedensificacion]
(
   @ApartadoId as integer
)
RETURNS int AS
BEGIN
	-- Declare the return variable here
	DECLARE @Suma as INT


   SET @Suma = (SELECT count(MVCDigital.dbo.Cat_ConceptoLB.Redensificacion)
		FROM MVCDigital.dbo.Cat_ConceptoLB
		WHERE Redensificacion = 1 and ApartadoID=@ApartadoID)

	-- Return the result of the function
	RETURN ISNULL(@suma,0)

END
