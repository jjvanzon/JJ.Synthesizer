ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_AudioOutput] FOREIGN KEY([AudioOutputID])
REFERENCES [dbo].[AudioOutput] ([ID])

ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_AudioOutput]
