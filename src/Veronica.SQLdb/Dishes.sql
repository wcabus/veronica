﻿CREATE TABLE [dbo].[Dishes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Score] NUMERIC(2, 1) NULL, 
    [Added] DATETIMEOFFSET NOT NULL, 
    [LastInMenu] DATETIMEOFFSET NULL, 
    CONSTRAINT [FK_Dishes_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)