-- =============================================
	-- Author:		<Author,,Name>
	-- Create date: <Create Date,,>
	-- Description:	<Description,,>
	-- =============================================
	CREATE PROCEDURE [dbo].[SP_actualizaSubApartados]

	@ApartadoId as int,
    @ConceptoId as int,
	@Concepto as varchar(50),
	@normal as bit,
	@Concredito  as bit,
	@Unifamiliar  as bit, 
	@Redensificacion  as bit

	   	 	AS
	BEGIN

	UPDATE Cat_ConceptoLB SET ApartadoId =@apartadoId, 
	Concepto=@Concepto,Normal = @normal, 
	Concredito = @Concredito, 
	Unifamiliar=@Unifamiliar, 
	Redensificacion=@Redensificacion 
	WHERE ConceptoID= @ConceptoId

	END


	
	-- Procedimientos Conceptos				------------------------------------------------------------


	/****** Object:  StoredProcedure [dbo].[SP_VISUALIZA_CATAPARTADOS]    Script Date: 06/07/2020 04:06:17 a. m. ******/
	SET ANSI_NULLS ON