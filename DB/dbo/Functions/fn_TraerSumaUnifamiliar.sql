-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_TraerSumaUnifamiliar]
(
    @ApartadoId as integer
)
RETURNS int AS
BEGIN
	-- Declare the return variable here
	DECLARE @Suma as INT

   SET @Suma = (SELECT count(MVCDigital.dbo.Cat_ConceptoLB.Unifamiliar)
		FROM MVCDigital.dbo.Cat_ConceptoLB
		WHERE Unifamiliar = 1 and ApartadoID=@ApartadoId)

	-- Return the result of the function
	RETURN ISNULL(@suma,0)

END
