ALTER TABLE [dbo].[Operator]  WITH CHECK ADD  CONSTRAINT [FK_Operator_PatchID] FOREIGN KEY([PatchID])
REFERENCES [dbo].[Patch] ([ID])

ALTER TABLE [dbo].[Operator] CHECK CONSTRAINT [FK_Operator_PatchID]


