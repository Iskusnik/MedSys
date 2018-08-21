
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/21/2018 16:13:41
-- Generated from EDMX file: C:\Users\IskusnikXD\Documents\Visual Studio 2015\Projects\MedSys\MedSys\ModelMed.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MedDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet] DROP CONSTRAINT [FK_PersonDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_JobDoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Doctor] DROP CONSTRAINT [FK_JobDoctor];
GO
IF OBJECT_ID(N'[dbo].[FK_DoctorTimeForVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeForVisitSet] DROP CONSTRAINT [FK_DoctorTimeForVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_RecordDoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Doctor] DROP CONSTRAINT [FK_RecordDoctor];
GO
IF OBJECT_ID(N'[dbo].[FK_MedCardRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RecordSet] DROP CONSTRAINT [FK_MedCardRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_IllnessMedCard_Illness]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IllnessMedCard] DROP CONSTRAINT [FK_IllnessMedCard_Illness];
GO
IF OBJECT_ID(N'[dbo].[FK_IllnessMedCard_MedCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IllnessMedCard] DROP CONSTRAINT [FK_IllnessMedCard_MedCard];
GO
IF OBJECT_ID(N'[dbo].[FK_MedCardPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Patient] DROP CONSTRAINT [FK_MedCardPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_CabinetTimeForVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeForVisitSet] DROP CONSTRAINT [FK_CabinetTimeForVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_TimeForVisitPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TimeForVisitSet] DROP CONSTRAINT [FK_TimeForVisitPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Doctor] DROP CONSTRAINT [FK_Doctor_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonSet_Patient] DROP CONSTRAINT [FK_Patient_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PersonSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet];
GO
IF OBJECT_ID(N'[dbo].[MedCardSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedCardSet];
GO
IF OBJECT_ID(N'[dbo].[RecordSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RecordSet];
GO
IF OBJECT_ID(N'[dbo].[TimeForVisitSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeForVisitSet];
GO
IF OBJECT_ID(N'[dbo].[IllnessSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IllnessSet];
GO
IF OBJECT_ID(N'[dbo].[JobSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobSet];
GO
IF OBJECT_ID(N'[dbo].[DocumentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentSet];
GO
IF OBJECT_ID(N'[dbo].[CabinetSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CabinetSet];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Doctor];
GO
IF OBJECT_ID(N'[dbo].[PersonSet_Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonSet_Patient];
GO
IF OBJECT_ID(N'[dbo].[IllnessMedCard]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IllnessMedCard];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [InsuranceNum] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Document_Id] int  NOT NULL
);
GO

-- Creating table 'MedCardSet'
CREATE TABLE [dbo].[MedCardSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Shelf] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RecordSet'
CREATE TABLE [dbo].[RecordSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Info] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [MedCard_Id] int  NOT NULL
);
GO

-- Creating table 'TimeForVisitSet'
CREATE TABLE [dbo].[TimeForVisitSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VisitTime] datetime  NOT NULL,
    [Doctor_Id] int  NOT NULL,
    [Cabinet_Id] int  NOT NULL,
    [Patient_Id] int  NULL
);
GO

-- Creating table 'IllnessSet'
CREATE TABLE [dbo].[IllnessSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'JobSet'
CREATE TABLE [dbo].[JobSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DocumentSet'
CREATE TABLE [dbo].[DocumentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Num] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CabinetSet'
CREATE TABLE [dbo].[CabinetSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Num] int  NOT NULL,
    [Floor] int  NOT NULL,
    [Corpus] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PersonSet_Doctor'
CREATE TABLE [dbo].[PersonSet_Doctor] (
    [Education] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [Job_Id] int  NOT NULL,
    [Record_Id] int  NOT NULL
);
GO

-- Creating table 'PersonSet_Patient'
CREATE TABLE [dbo].[PersonSet_Patient] (
    [BloodType] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [MedCard_Id] int  NOT NULL
);
GO

-- Creating table 'IllnessMedCard'
CREATE TABLE [dbo].[IllnessMedCard] (
    [Illness_Id] int  NOT NULL,
    [MedCard_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedCardSet'
ALTER TABLE [dbo].[MedCardSet]
ADD CONSTRAINT [PK_MedCardSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [PK_RecordSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TimeForVisitSet'
ALTER TABLE [dbo].[TimeForVisitSet]
ADD CONSTRAINT [PK_TimeForVisitSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IllnessSet'
ALTER TABLE [dbo].[IllnessSet]
ADD CONSTRAINT [PK_IllnessSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobSet'
ALTER TABLE [dbo].[JobSet]
ADD CONSTRAINT [PK_JobSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentSet'
ALTER TABLE [dbo].[DocumentSet]
ADD CONSTRAINT [PK_DocumentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CabinetSet'
ALTER TABLE [dbo].[CabinetSet]
ADD CONSTRAINT [PK_CabinetSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Doctor'
ALTER TABLE [dbo].[PersonSet_Doctor]
ADD CONSTRAINT [PK_PersonSet_Doctor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonSet_Patient'
ALTER TABLE [dbo].[PersonSet_Patient]
ADD CONSTRAINT [PK_PersonSet_Patient]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Illness_Id], [MedCard_Id] in table 'IllnessMedCard'
ALTER TABLE [dbo].[IllnessMedCard]
ADD CONSTRAINT [PK_IllnessMedCard]
    PRIMARY KEY CLUSTERED ([Illness_Id], [MedCard_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Document_Id] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [FK_PersonDocument]
    FOREIGN KEY ([Document_Id])
    REFERENCES [dbo].[DocumentSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonDocument'
CREATE INDEX [IX_FK_PersonDocument]
ON [dbo].[PersonSet]
    ([Document_Id]);
GO

-- Creating foreign key on [Job_Id] in table 'PersonSet_Doctor'
ALTER TABLE [dbo].[PersonSet_Doctor]
ADD CONSTRAINT [FK_JobDoctor]
    FOREIGN KEY ([Job_Id])
    REFERENCES [dbo].[JobSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobDoctor'
CREATE INDEX [IX_FK_JobDoctor]
ON [dbo].[PersonSet_Doctor]
    ([Job_Id]);
GO

-- Creating foreign key on [Doctor_Id] in table 'TimeForVisitSet'
ALTER TABLE [dbo].[TimeForVisitSet]
ADD CONSTRAINT [FK_DoctorTimeForVisit]
    FOREIGN KEY ([Doctor_Id])
    REFERENCES [dbo].[PersonSet_Doctor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DoctorTimeForVisit'
CREATE INDEX [IX_FK_DoctorTimeForVisit]
ON [dbo].[TimeForVisitSet]
    ([Doctor_Id]);
GO

-- Creating foreign key on [Record_Id] in table 'PersonSet_Doctor'
ALTER TABLE [dbo].[PersonSet_Doctor]
ADD CONSTRAINT [FK_RecordDoctor]
    FOREIGN KEY ([Record_Id])
    REFERENCES [dbo].[RecordSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecordDoctor'
CREATE INDEX [IX_FK_RecordDoctor]
ON [dbo].[PersonSet_Doctor]
    ([Record_Id]);
GO

-- Creating foreign key on [MedCard_Id] in table 'RecordSet'
ALTER TABLE [dbo].[RecordSet]
ADD CONSTRAINT [FK_MedCardRecord]
    FOREIGN KEY ([MedCard_Id])
    REFERENCES [dbo].[MedCardSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MedCardRecord'
CREATE INDEX [IX_FK_MedCardRecord]
ON [dbo].[RecordSet]
    ([MedCard_Id]);
GO

-- Creating foreign key on [Illness_Id] in table 'IllnessMedCard'
ALTER TABLE [dbo].[IllnessMedCard]
ADD CONSTRAINT [FK_IllnessMedCard_Illness]
    FOREIGN KEY ([Illness_Id])
    REFERENCES [dbo].[IllnessSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MedCard_Id] in table 'IllnessMedCard'
ALTER TABLE [dbo].[IllnessMedCard]
ADD CONSTRAINT [FK_IllnessMedCard_MedCard]
    FOREIGN KEY ([MedCard_Id])
    REFERENCES [dbo].[MedCardSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IllnessMedCard_MedCard'
CREATE INDEX [IX_FK_IllnessMedCard_MedCard]
ON [dbo].[IllnessMedCard]
    ([MedCard_Id]);
GO

-- Creating foreign key on [MedCard_Id] in table 'PersonSet_Patient'
ALTER TABLE [dbo].[PersonSet_Patient]
ADD CONSTRAINT [FK_MedCardPatient]
    FOREIGN KEY ([MedCard_Id])
    REFERENCES [dbo].[MedCardSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MedCardPatient'
CREATE INDEX [IX_FK_MedCardPatient]
ON [dbo].[PersonSet_Patient]
    ([MedCard_Id]);
GO

-- Creating foreign key on [Cabinet_Id] in table 'TimeForVisitSet'
ALTER TABLE [dbo].[TimeForVisitSet]
ADD CONSTRAINT [FK_CabinetTimeForVisit]
    FOREIGN KEY ([Cabinet_Id])
    REFERENCES [dbo].[CabinetSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CabinetTimeForVisit'
CREATE INDEX [IX_FK_CabinetTimeForVisit]
ON [dbo].[TimeForVisitSet]
    ([Cabinet_Id]);
GO

-- Creating foreign key on [Patient_Id] in table 'TimeForVisitSet'
ALTER TABLE [dbo].[TimeForVisitSet]
ADD CONSTRAINT [FK_TimeForVisitPatient]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[PersonSet_Patient]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TimeForVisitPatient'
CREATE INDEX [IX_FK_TimeForVisitPatient]
ON [dbo].[TimeForVisitSet]
    ([Patient_Id]);
GO

-- Creating foreign key on [Id] in table 'PersonSet_Doctor'
ALTER TABLE [dbo].[PersonSet_Doctor]
ADD CONSTRAINT [FK_Doctor_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PersonSet_Patient'
ALTER TABLE [dbo].[PersonSet_Patient]
ADD CONSTRAINT [FK_Patient_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PersonSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------