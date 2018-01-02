ALTER TABLE [dbo].[MidiMappingElement]  WITH CHECK ADD  CONSTRAINT [FK_MidiMappingElement_MidiMapping] FOREIGN KEY([MidiMappingID])
REFERENCES [dbo].[MidiMapping] ([ID])

ALTER TABLE [dbo].[MidiMappingElement] CHECK CONSTRAINT [FK_MidiMappingElement_MidiMapping]
