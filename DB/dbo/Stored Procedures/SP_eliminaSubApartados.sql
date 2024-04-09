-- =============================================
	-- Author:		<Author,,Name>
	-- Create date: <Create Date,,>
	-- Description:	<Description,,>
	-- =============================================
	CREATE PROCEDURE [dbo].[SP_eliminaSubApartados]

	@Id as int

	AS
	BEGIN

	DELETE FROM Cat_ConceptoLB where ConceptoID= @Id

	END


	/****** Object:  StoredProcedure [dbo].[SP_ALTA_APARTADOS]    Script Date: 06/07/2020 04:01:52 a. m. ******/
	SET ANSI_NULLS ON