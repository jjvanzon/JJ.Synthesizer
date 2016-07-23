ALTER TABLE [dbo].[Operator]  WITH CHECK ADD  CONSTRAINT [FK_Operator_Dimension] FOREIGN KEY([DimensionID])
REFERENCES [dbo].[Dimension] ([ID])

ALTER TABLE [dbo].[Operator] CHECK CONSTRAINT [FK_Operator_Dimension]
