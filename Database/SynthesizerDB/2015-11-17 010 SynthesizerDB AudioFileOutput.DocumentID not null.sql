DROP INDEX [IX_AudioFileOutput_DocumentID] ON [dbo].[AudioFileOutput]

alter table AudioFileOutput alter column DocumentID int not null;

CREATE NONCLUSTERED INDEX [IX_AudioFileOutput_DocumentID] ON [dbo].[AudioFileOutput]
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
