ALTER TABLE [dbo].[Operator]  WITH CHECK ADD  CONSTRAINT [FK_Operator_EntityPosition] FOREIGN KEY([EntityPositionID])
REFERENCES [dbo].[EntityPosition] ([ID])

ALTER TABLE [dbo].[Operator] CHECK CONSTRAINT [FK_Operator_EntityPosition]
