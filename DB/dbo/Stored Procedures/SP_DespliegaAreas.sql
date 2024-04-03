-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_DespliegaAreas
	
AS
BEGIN
SELECT 0 AS AreaID, '<-Seleccione el Área->' as Area
UNION ALL
SELECT AreaID,Area from Cat_Area order by area
END
