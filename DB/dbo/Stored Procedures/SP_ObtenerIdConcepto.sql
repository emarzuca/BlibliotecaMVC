-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ObtenerIdConcepto]
	@ConceptoId int
AS
BEGIN

Select ConceptoID from Cat_ConceptoLB where ConceptoID = @ConceptoId
END
