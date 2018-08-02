ALTER TABLE [dbo].[Node]  WITH CHECK ADD  CONSTRAINT [FK_Node_InterpolationType] FOREIGN KEY([InterpolationTypeID])
REFERENCES [dbo].[InterpolationType] ([ID])

ALTER TABLE [dbo].[Node] CHECK CONSTRAINT [FK_Node_InterpolationType]

