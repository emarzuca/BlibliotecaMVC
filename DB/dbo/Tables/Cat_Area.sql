CREATE TABLE [dbo].[Cat_Area] (
    [AreaId]  INT           IDENTITY (1, 1) NOT NULL,
    [Area]    NVARCHAR (70) NULL,
    [NivelID] INT           NULL,
    [Padre]   INT           NULL,
    [Orden]   INT           NULL,
    [Correo]  VARCHAR (100) NULL,
    [Llave]   VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Cat_Area] PRIMARY KEY CLUSTERED ([AreaId] ASC)
);

