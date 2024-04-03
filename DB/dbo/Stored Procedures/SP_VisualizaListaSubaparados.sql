-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_VisualizaListaSubaparados]
@filtro int 
AS
BEGIN
IF @filtro = 0

SELECT ConceptoId,Concepto,c.ApartadoId,Apartado,Unifamiliar,Normal, Redensificacion,Concredito 
,CASE C.Normal
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloNormal
			,CASE C.Normal
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RNormal
			,CASE C.Concredito
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloConcredito
			,CASE C.Concredito
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RConcredito
			,CASE C.Redensificacion
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloRedensificacion
			,CASE C.Redensificacion
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RRedensificacion
			,CASE C.Unifamiliar
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloUnifamiliar
			,CASE C.Unifamiliar
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RUnifamiliar
FROM  
dbo.Cat_ConceptoLB C INNER JOIN 
Cat_apartados A ON  c.ApartadoID =a.ApartadoID
order by apartado
ELSE

SELECT ConceptoId,Concepto,c.ApartadoId,Apartado,Unifamiliar,Normal, Redensificacion,Concredito 
,CASE C.Normal
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloNormal
			,CASE C.Normal
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RNormal
			,CASE C.Concredito
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloConcredito
			,CASE C.Concredito
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RConcredito
			,CASE C.Redensificacion
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloRedensificacion
			,CASE C.Redensificacion
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RRedensificacion
			,CASE C.Unifamiliar
				WHEN 0 THEN 'btn btn-danger'
				WHEN 1 THEN 'btn btn-primary'
				ELSE  'btn btn-danger'
				END AS EstiloUnifamiliar
			,CASE C.Unifamiliar
				WHEN 0 THEN 'NO'
				WHEN 1 THEN 'SI'
				ELSE  'NO'
				END AS RUnifamiliar
FROM  
dbo.Cat_ConceptoLB C INNER JOIN 
Cat_apartados A ON  c.ApartadoID =a.ApartadoID
where c.ApartadoID=@filtro
order by apartado
END
