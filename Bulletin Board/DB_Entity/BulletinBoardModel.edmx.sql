
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/10/2020 16:29:47
-- Generated from EDMX file: D:\VB.Net\Bulletin Board\Bulletin Board\DB_Entity\BulletinBoardModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BulletinBoard];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_usersroles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_usersroles];
GO
IF OBJECT_ID(N'[dbo].[FK_createduser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_createduser];
GO
IF OBJECT_ID(N'[dbo].[FK_updateduser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_updateduser];
GO
IF OBJECT_ID(N'[dbo].[FK_usersposts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_usersposts];
GO
IF OBJECT_ID(N'[dbo].[FK_usersposts1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_usersposts1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[ImageFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageFiles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] uniqueidentifier   NOT NULL DEFAULT newsequentialid(),
    
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Created_At] datetime  NOT NULL,
    [Updated_User_Id] uniqueidentifier  NOT NULL,
    [Created_User_Id] uniqueidentifier  NOT NULL,
    [Updated_At] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] uniqueidentifier   NOT NULL DEFAULT newsequentialid(),
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Profile] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Dob] datetime  NULL,
    [Deleted_User_Id] uniqueidentifier  NULL,
    [Created_At] datetime  NOT NULL,
    [Updated_At] datetime  NOT NULL,
    [Deleted_At] datetime  NULL,
    [Is_Deleted] nvarchar(max)  NOT NULL,
    [Roles_Id] uniqueidentifier  NOT NULL,
    [Created_User_Id] uniqueidentifier  NOT NULL,
    [Updated_User_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] uniqueidentifier   NOT NULL DEFAULT newsequentialid(),
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ImageFiles'
CREATE TABLE [dbo].[ImageFiles] (
    [Id] uniqueidentifier   NOT NULL DEFAULT newsequentialid(),
    [Name] nvarchar(50)  NULL,
    [FileSize] int  NULL,
    [FilePath] nvarchar(100)  NULL,
    [Uploaded_User_Id] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ImageFiles'
ALTER TABLE [dbo].[ImageFiles]
ADD CONSTRAINT [PK_ImageFiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Roles_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_usersroles]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersroles'
CREATE INDEX [IX_FK_usersroles]
ON [dbo].[Users]
    ([Roles_Id]);
GO

-- Creating foreign key on [Created_User_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_createduser]
    FOREIGN KEY ([Created_User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_createduser'
CREATE INDEX [IX_FK_createduser]
ON [dbo].[Users]
    ([Created_User_Id]);
GO

-- Creating foreign key on [Updated_User_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_updateduser]
    FOREIGN KEY ([Updated_User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_updateduser'
CREATE INDEX [IX_FK_updateduser]
ON [dbo].[Users]
    ([Updated_User_Id]);
GO

-- Creating foreign key on [Updated_User_Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_usersposts]
    FOREIGN KEY ([Updated_User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersposts'
CREATE INDEX [IX_FK_usersposts]
ON [dbo].[Posts]
    ([Updated_User_Id]);
GO

-- Creating foreign key on [Created_User_Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_usersposts1]
    FOREIGN KEY ([Created_User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_usersposts1'
CREATE INDEX [IX_FK_usersposts1]
ON [dbo].[Posts]
    ([Created_User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

