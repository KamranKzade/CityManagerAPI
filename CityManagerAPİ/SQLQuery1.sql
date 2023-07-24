﻿﻿CREATE DATABASE CITYMANAGERDB

GO

USE CITYMANAGERDB

GO

CREATE TABLE [dbo].[Users] (

    [Id] INT PRIMARY KEY  IDENTITY (1, 1) NOT NULL,

    [PasswordHash] VARBINARY (MAX) NULL,

    [PasswordSalt] VARBINARY (MAX) NULL,

    [Username]     NVARCHAR (MAX)  NULL

);

GO

CREATE TABLE [dbo].[Cities] (

    [Id]          INT   PRIMARY KEY  IDENTITY (1, 1) NOT NULL,

    [Description] NVARCHAR (MAX) NULL,

    [Name]        NVARCHAR (MAX) NULL,

    [UserId]      INT   NOT NULL DEFAULT(1)

);


GO
CREATE TABLE [dbo].[CityImages] (

    [Id]          INT   PRIMARY KEY IDENTITY (1, 1) NOT NULL,

    [CityId]      INT      FOREIGN KEY REFERENCES Cities(Id)       NOT NULL,

    [DateAdded]   DATETIME2 (7)  NOT NULL,

    [Description] NVARCHAR (MAX) NULL,

    [IsMain]      BIT    NOT NULL,

    [Url]         NVARCHAR (MAX) NULL,

    [PublicId]    NVARCHAR (250) NULL DEFAULT('none')
)
