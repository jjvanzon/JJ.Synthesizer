ALTER TABLE [dbo].[MidiMappingElement]  WITH CHECK ADD  CONSTRAINT [FK_MidiMappingElement_Scale] FOREIGN KEY([ScaleID])
REFERENCES [dbo].[Scale] ([ID])

ALTER TABLE [dbo].[MidiMappingElement] CHECK CONSTRAINT [FK_MidiMappingElement_Scale]