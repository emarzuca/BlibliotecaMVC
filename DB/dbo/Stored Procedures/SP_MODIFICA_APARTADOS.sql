-- =============================================
	-- Author:		<Author,,Name>
	-- Create date: <Create Date,,>
	-- Description:	<Description,,>
	-- =============================================
	CREATE PROCEDURE [dbo].[SP_MODIFICA_APARTADOS]

	@ApartadoID as int,
	@ApartadoDir as varchar(50),
	@apartado as varchar(100),
	@AreaID as int

	AS
	BEGIN

	UPDATE Cat_apartados SET IdentificacionDirectorio =@ApartadoDir, Apartado =@apartado, AreaID=@AreaID
	WHERE ApartadoID= @ApartadoID

	END


	
	-- Procedimientos Conceptos				------------------------------------------------------------


	/****** Object:  StoredProcedure [dbo].[SP_VISUALIZA_CATAPARTADOS]    Script Date: 06/07/2020 04:06:17 a. m. ******/
	SET ANSI_NULLS ON
