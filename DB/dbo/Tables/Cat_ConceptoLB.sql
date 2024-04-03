CREATE TABLE [dbo].[Cat_ConceptoLB] (
    [ConceptoID]      INT            IDENTITY (1, 1) NOT NULL,
    [Concepto]        NVARCHAR (250) NULL,
    [ProyectoID]      INT            CONSTRAINT [DF_Cat_ConceptoLB_ProyectoID] DEFAULT ((0)) NULL,
    [DamnificadoId]   INT            CONSTRAINT [DF_Cat_ConceptoLB_DamnificadoId] DEFAULT ((0)) NULL,
    [ApartadoID]      INT            NULL,
    [AreaID]          INT            NULL,
    [Unifamiliar]     BIT            NULL,
    [Normal]          BIT            NULL,
    [Redensificacion] BIT            NULL,
    [Concredito]      BIT            NULL,
    CONSTRAINT [PK_Cat_ConceptoLB] PRIMARY KEY CLUSTERED ([ConceptoID] ASC),
    CONSTRAINT [FK_Cat_ConceptoLB_Cat_apartados1] FOREIGN KEY ([ApartadoID]) REFERENCES [dbo].[Cat_apartados] ([ApartadoID])
);

