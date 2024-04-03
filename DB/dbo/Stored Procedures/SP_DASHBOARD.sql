-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_DASHBOARD]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT
dbo.Cat_apartados.Apartado,
dbo.fn_TraerSumaUnifamiliar(dbo.Cat_ConceptoLB.ApartadoID) as Unifamiliar,
dbo.fn_TraerSumaNormal(dbo.Cat_ConceptoLB.ApartadoID) as Normal,
dbo.fn_TraerSumaRedensificacion(dbo.Cat_ConceptoLB.ApartadoID) as Redensificacion,
dbo.fn_TraerSumaConcredito(dbo.Cat_ConceptoLB.ApartadoID) as Concredito
FROM            dbo.Cat_ConceptoLB INNER JOIN
                         dbo.Cat_apartados ON dbo.Cat_ConceptoLB.ApartadoID = dbo.Cat_apartados.ApartadoID
GROUP BY  dbo.Cat_apartados.Apartado,dbo.Cat_ConceptoLB.ApartadoID
END
