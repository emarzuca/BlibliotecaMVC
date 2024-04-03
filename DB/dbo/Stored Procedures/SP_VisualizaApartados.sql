-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_VisualizaApartados
	


AS
BEGIN

Select 0 as ApartadoID, '<- Seleccione una Opción->' as Apartado
UNION ALL
select ApartadoID, Apartado from Cat_apartados

END
