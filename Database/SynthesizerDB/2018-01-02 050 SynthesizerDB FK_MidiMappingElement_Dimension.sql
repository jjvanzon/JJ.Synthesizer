ALTER TABLE [dbo].[MidiMappingElement]  WITH CHECK ADD  CONSTRAINT [FK_MidiMappingElement_Dimension] FOREIGN KEY([StandardDimensionID])
REFERENCES [dbo].[Dimension] ([ID])

ALTER TABLE [dbo].[MidiMappingElement] CHECK CONSTRAINT [FK_MidiMappingElement_Dimension]
