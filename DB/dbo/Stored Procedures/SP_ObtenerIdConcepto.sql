-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ObtenerIdConcepto]
	@Id int
AS
BEGIN

Select apartadoId, ConceptoID, Concepto, normal,Concredito,Unifamiliar, Redensificacion from Cat_ConceptoLB where ConceptoID =@Id
END
