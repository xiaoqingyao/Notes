ALTER TABLE [NotesPage] ADD [NotesId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

GO

ALTER TABLE [NotesContent] ADD [NotesId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

GO

ALTER TABLE [NotesContent] ADD [SectionId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

GO

ALTER TABLE [Notes] ADD [Grade] nvarchar(50) NULL;

GO

ALTER TABLE [Notes] ADD [Subject] nvarchar(50) NULL;

GO

CREATE INDEX [IX_NotesPage_NotesId] ON [NotesPage] ([NotesId]);

GO

CREATE INDEX [IX_NotesContent_NotesId] ON [NotesContent] ([NotesId]);

GO

CREATE INDEX [IX_NotesContent_SectionId] ON [NotesContent] ([SectionId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200630054252_extentionAll', N'3.1.5');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesSection]') AND [c].[name] = N'UpdateTime');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [NotesSection] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [NotesSection] ALTER COLUMN [UpdateTime] datetime2 NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesSection]') AND [c].[name] = N'CreationTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [NotesSection] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [NotesSection] ALTER COLUMN [CreationTime] datetime2 NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesPage]') AND [c].[name] = N'UpdateTime');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [NotesPage] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [NotesPage] ALTER COLUMN [UpdateTime] datetime2 NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesPage]') AND [c].[name] = N'CreationTime');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [NotesPage] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [NotesPage] ALTER COLUMN [CreationTime] datetime2 NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesContent]') AND [c].[name] = N'UpdateTime');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [NotesContent] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [NotesContent] ALTER COLUMN [UpdateTime] datetime2 NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesContent]') AND [c].[name] = N'CreationTime');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [NotesContent] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [NotesContent] ALTER COLUMN [CreationTime] datetime2 NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'UpdateTime');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Notes] ALTER COLUMN [UpdateTime] datetime2 NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'CreationTime');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Notes] ALTER COLUMN [CreationTime] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200707054534_cap', N'3.1.5');

GO

EXEC sp_rename N'[Notes].[Subject]', N'GradeCode', N'COLUMN';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200710095629_newfiled', N'3.1.5');

GO

ALTER TABLE [NotesSection] DROP CONSTRAINT [PK_NotesSection];

GO

ALTER TABLE [NotesPage] DROP CONSTRAINT [PK_NotesPage];

GO

ALTER TABLE [NotesContent] DROP CONSTRAINT [PK_NotesContent];

GO

ALTER TABLE [Notes] DROP CONSTRAINT [PK_Notes];

GO

ALTER TABLE [NotesSection] ADD [IndentityId] int NOT NULL IDENTITY;

GO

ALTER TABLE [NotesPage] ADD [IndentityId] int NOT NULL IDENTITY;

GO

ALTER TABLE [NotesContent] ADD [IndentityId] int NOT NULL IDENTITY;

GO

ALTER TABLE [Notes] ADD [IndentityId] int NOT NULL IDENTITY;

GO

ALTER TABLE [NotesSection] ADD CONSTRAINT [PK_NotesSection] PRIMARY KEY ([IndentityId]);

GO

ALTER TABLE [NotesPage] ADD CONSTRAINT [PK_NotesPage] PRIMARY KEY ([IndentityId]);

GO

ALTER TABLE [NotesContent] ADD CONSTRAINT [PK_NotesContent] PRIMARY KEY ([IndentityId]);

GO

ALTER TABLE [Notes] ADD CONSTRAINT [PK_Notes] PRIMARY KEY ([IndentityId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200715082845_IndentityId', N'3.1.5');

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesSection]') AND [c].[name] = N'Name');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [NotesSection] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [NotesSection] ALTER COLUMN [Name] nvarchar(max) NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotesPage]') AND [c].[name] = N'Name');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [NotesPage] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [NotesPage] ALTER COLUMN [Name] nvarchar(max) NULL;

GO

CREATE INDEX [IX_NotesSection_NotesId] ON [NotesSection] ([NotesId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200722035652_addCatalog', N'3.1.5');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200722052553_addCatalog2', N'3.1.5');

GO

CREATE TABLE [Catalogs] (
    [IndentityId] int NOT NULL IDENTITY,
    [Id] uniqueidentifier NOT NULL,
    [Deleted] int NOT NULL,
    [CreationTime] datetime2 NULL,
    [UpdateTime] datetime2 NULL,
    [Creator] nvarchar(100) NULL,
    [Name] nvarchar(100) NULL,
    [ParentCode] nvarchar(450) NULL,
    [Code] nvarchar(100) NULL,
    [Count] int NOT NULL,
    CONSTRAINT [PK_Catalogs] PRIMARY KEY ([IndentityId])
);

GO

CREATE INDEX [IX_Catalogs_Deleted] ON [Catalogs] ([Deleted]);

GO

CREATE INDEX [IX_Catalogs_Id] ON [Catalogs] ([Id]);

GO

CREATE INDEX [IX_Catalogs_ParentCode] ON [Catalogs] ([ParentCode]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200722052755_addCatalog3', N'3.1.5');

GO

CREATE TABLE [NotesForCourse] (
    [IndentityId] int NOT NULL IDENTITY,
    [Id] uniqueidentifier NOT NULL,
    [Deleted] int NOT NULL,
    [CreationTime] datetime2 NULL,
    [UpdateTime] datetime2 NULL,
    [DsId] uniqueidentifier NOT NULL,
    [TaskId] uniqueidentifier NOT NULL,
    [ClassId] nvarchar(10) NULL,
    [SectionId] uniqueidentifier NOT NULL,
    [PageId] uniqueidentifier NOT NULL,
    [Creator] nvarchar(10) NULL,
    CONSTRAINT [PK_NotesForCourse] PRIMARY KEY ([IndentityId])
);

GO

CREATE INDEX [IX_NotesForCourse_Deleted] ON [NotesForCourse] ([Deleted]);

GO

CREATE INDEX [IX_NotesForCourse_DsId] ON [NotesForCourse] ([DsId]);

GO

CREATE INDEX [IX_NotesForCourse_Id] ON [NotesForCourse] ([Id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200724004132_Course', N'3.1.5');

GO

