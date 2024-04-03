CREATE TABLE [dbo].[Cat_apartados] (
    [ApartadoID]               INT            IDENTITY (1, 1) NOT NULL,
    [ApartadoNo]               INT            NULL,
    [Apartado]                 NVARCHAR (100) NOT NULL,
    [ExpedienteID]             INT            NULL,
    [IdentificacionDirectorio] VARCHAR (50)   NULL,
    [AreaID]                   INT            NULL,
    CONSTRAINT [PK_Cat_apartados] PRIMARY KEY CLUSTERED ([ApartadoID] ASC)
);

