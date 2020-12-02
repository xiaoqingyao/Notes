ALTER TABLE [NotesPage] ADD [ClassId] nvarchar(100) NULL;

GO

CREATE INDEX [IX_NotesPage_ClassId] ON [NotesPage] ([ClassId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728033457_PageClassId', N'3.1.5');

GO

ALTER TABLE [NotesPage] ADD [Creator] nvarchar(100) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728055114_PageCreatorId', N'3.1.5');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728073819_PageCreator', N'3.1.5');

GO

