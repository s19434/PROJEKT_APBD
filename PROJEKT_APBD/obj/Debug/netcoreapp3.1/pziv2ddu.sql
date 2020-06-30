IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Buildings] (
    [IdBuilding] int NOT NULL IDENTITY,
    [Street] nvarchar(100) NOT NULL,
    [StreetNumber] int NOT NULL,
    [City] nvarchar(100) NOT NULL,
    [Height] decimal(6, 2) NOT NULL,
    CONSTRAINT [PK_Buildings] PRIMARY KEY ([IdBuilding])
);

GO

CREATE TABLE [Clients] (
    [IdClient] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [Phone] nvarchar(100) NOT NULL,
    [Login] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([IdClient])
);

GO

CREATE TABLE [Campaigns] (
    [IdCampaign] int NOT NULL IDENTITY,
    [StartDate] Date NOT NULL,
    [EndDate] Date NOT NULL,
    [PricePerSquareMeter] decimal(6, 2) NOT NULL,
    [FromIdBuilding] int NULL,
    [ToIdBuilding] int NULL,
    [IdClient] int NULL,
    CONSTRAINT [PK_Campaigns] PRIMARY KEY ([IdCampaign]),
    CONSTRAINT [FK_Campaigns_Buildings_FromIdBuilding] FOREIGN KEY ([FromIdBuilding]) REFERENCES [Buildings] ([IdBuilding]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Campaigns_Clients_IdClient] FOREIGN KEY ([IdClient]) REFERENCES [Clients] ([IdClient]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Campaigns_Buildings_ToIdBuilding] FOREIGN KEY ([ToIdBuilding]) REFERENCES [Buildings] ([IdBuilding]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Banners] (
    [IdAdvertisement] int NOT NULL IDENTITY,
    [Name] int NOT NULL,
    [Price] decimal(6, 2) NOT NULL,
    [IdCampaign] int NULL,
    [Area] decimal(6, 2) NOT NULL,
    CONSTRAINT [PK_Banners] PRIMARY KEY ([IdAdvertisement]),
    CONSTRAINT [FK_Banners_Campaigns_IdCampaign] FOREIGN KEY ([IdCampaign]) REFERENCES [Campaigns] ([IdCampaign]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Banners_IdCampaign] ON [Banners] ([IdCampaign]);

GO

CREATE INDEX [IX_Campaigns_FromIdBuilding] ON [Campaigns] ([FromIdBuilding]);

GO

CREATE INDEX [IX_Campaigns_IdClient] ON [Campaigns] ([IdClient]);

GO

CREATE INDEX [IX_Campaigns_ToIdBuilding] ON [Campaigns] ([ToIdBuilding]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200629203759_InitialMigration', N'3.1.5');

GO

