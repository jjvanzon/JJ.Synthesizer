ALTER TABLE [dbo].[MidiMapping]  WITH CHECK ADD  CONSTRAINT [FK_MidiMapping_Document] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Document] ([ID])

ALTER TABLE [dbo].[MidiMapping] CHECK CONSTRAINT [FK_MidiMapping_Document]
