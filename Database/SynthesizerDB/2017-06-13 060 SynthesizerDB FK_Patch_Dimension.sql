ALTER TABLE [dbo].[Patch]  WITH CHECK ADD  CONSTRAINT [FK_Patch_Dimension] FOREIGN KEY([StandardDimensionID])
REFERENCES [dbo].[Dimension] ([ID])
ALTER TABLE [dbo].[Patch] CHECK CONSTRAINT [FK_Patch_Dimension]



