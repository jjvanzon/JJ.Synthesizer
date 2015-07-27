ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_ParentDocument] FOREIGN KEY([ParentDocumentID])
REFERENCES [dbo].[Document] ([ID])

ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_ParentDocument]
