ALTER TABLE [dbo].[MidiMapping]  WITH CHECK ADD  CONSTRAINT [FK_MidiMapping_MidiMappingType] FOREIGN KEY([MidiMappingTypeID])
REFERENCES [dbo].[MidiMappingType] ([ID])

ALTER TABLE [dbo].[MidiMapping] CHECK CONSTRAINT [FK_MidiMapping_MidiMappingType]
