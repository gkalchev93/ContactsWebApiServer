CREATE TABLE [dbo].[Contacts] (
    [Id]        INT            NOT NULL,
    [Name]      NVARCHAR (255) NOT NULL,
    [Egn]       NVARCHAR (10)  NOT NULL,
    [Address]   NVARCHAR (255) NOT NULL,
    [Telephone] NCHAR (32)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);