ALTER TABLE [NotesPage] ADD [Creator] nvarchar(100) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728055114_PageCreatorId', N'3.1.5');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728073819_PageCreator', N'3.1.5');

GO

