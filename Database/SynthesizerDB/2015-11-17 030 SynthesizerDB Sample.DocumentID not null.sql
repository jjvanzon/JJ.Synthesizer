DROP INDEX [IX_Sample_DocumentID] ON [dbo].[Sample]

alter table Sample alter column DocumentID int not null;

CREATE NONCLUSTERED INDEX [IX_Sample_DocumentID] ON [dbo].[Sample]
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
