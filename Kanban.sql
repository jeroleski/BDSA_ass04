USE [master]
GO

IF EXISTS(SELECT * FROM sys.databases WHERE [name] = 'Kanban')
    DROP DATABASE Kanban
GO

CREATE DATABASE Kanban
GO

USE Kanban
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Task (
    Id int IDENTITY(1,1),
    [Title] nvarchar(100) NOT NULL,
    [AssignedTo] int FOREIGN KEY REFERENCES User,
    [Description] nvarchar(max),
    [State] int FOREIGN KEY REFERENCES State NOT NULL,
    [Tag] nvarchar(50),
    CONSTRAINT PK_Task PRIMARY KEY CLUSTERED (Id)
)
CREATE TABLE User (
    Id int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [Tasks] 
    CONSTRAINT PK_User PRIMARY KEY CLUSTERED (Id)
)
CREATE TABLE Tag (
    Id int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    
    CONSTRAINT PK_Tag PRIMARY KEY CLUSTERED (Id)
)
CREATE TABLE State (
    Id int IDENTITY(1,1) NOT NULL,
    [State] nvarchar(50) NOT NULL,
    CONSTRAINT PK_Tag PRIMARY KEY CLUSTERED (Id)
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Characters(
    Id int IDENTITY(1,1) NOT NULL,
    ActorId int NULL,
    [Name] nvarchar(50) NOT NULL,
    Species nvarchar(50) NOT NULL,
    Planet nvarchar(50) NULL,
    CONSTRAINT PK_Characters PRIMARY KEY CLUSTERED (Id)
)
GO

INSERT State (Id, [State]) VALUES (1, N'New')
INSERT State (Id, [State]) VALUES (2, N'Active')
INSERT State (Id, [State]) VALUES (3, N'Resolved')
INSERT State (Id, [State]) VALUES (4, N'Closed')
INSERT State (Id, [State]) VALUES (5, N'Removed')


INSERT User ([Name], [Email], [Tasks]) VALUES (N'Billy West')
INSERT Actors ([Name]) VALUES (N'Katey Sagal')
INSERT Actors ([Name]) VALUES (N'John DiMaggio')
INSERT Actors ([Name]) VALUES (N'Lauren Tom')
INSERT Actors ([Name]) VALUES (N'Phil LaMarr')

INSERT Characters (ActorId, [Name], Species, Planet) VALUES (1, N'Philip J. Fry', N'Human', N'Earth')
INSERT Characters (ActorId, [Name], Species, Planet) VALUES (2, N'Turanga Leela', N'Mutant, Human', N'Earth')
INSERT Characters (ActorId, [Name], Species, Planet) VALUES (3, N'Bender Bending Rodriquez', N'Robot', N'Tijuana, Baja California')
INSERT Characters (ActorId, [Name], Species, Planet) VALUES (1, N'John A. Zoidberg', N'Decapodian', N'Decapod 10')
INSERT Characters (ActorId, [Name], Species, Planet) VALUES (4, N'Amy Wong', N'Human', N'Mars')
INSERT Characters (ActorId, [Name], Species, Planet) VALUES (5, N'Hermes Conrad', N'Human', N'Earth')
INSERT Characters (ActorId, [Name], Species, Planet) VALUES (1, N'Hubert J. Farnsworth', N'Human', N'Earth')

ALTER TABLE Characters  WITH CHECK ADD CONSTRAINT FK_Characters_Actors FOREIGN KEY(ActorId)
REFERENCES Actors (Id)
GO
ALTER TABLE Characters CHECK CONSTRAINT FK_Characters_Actors
GO