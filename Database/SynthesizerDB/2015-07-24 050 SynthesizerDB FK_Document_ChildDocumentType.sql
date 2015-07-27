ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_ChildDocumentType] FOREIGN KEY([ChildDocumentTypeID])
REFERENCES [dbo].[ChildDocumentType] ([ID])

ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_ChildDocumentType]
