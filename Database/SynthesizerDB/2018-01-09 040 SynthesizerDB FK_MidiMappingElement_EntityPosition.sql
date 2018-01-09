ALTER TABLE [dbo].[MidiMappingElement]  WITH CHECK ADD  CONSTRAINT [FK_MidiMappingElement_EntityPosition] FOREIGN KEY([EntityPositionID])
REFERENCES [dbo].[EntityPosition] ([ID])

ALTER TABLE [dbo].[MidiMappingElement] CHECK CONSTRAINT [FK_MidiMappingElement_EntityPosition]
