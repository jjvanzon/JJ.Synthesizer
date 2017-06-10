ALTER TABLE [dbo].[Operator]  WITH CHECK ADD  CONSTRAINT [FK_Operator_UnderlyingPatchID] FOREIGN KEY([UnderlyingPatchID])
REFERENCES [dbo].[Patch] ([ID])

ALTER TABLE [dbo].[Operator] CHECK CONSTRAINT [FK_Operator_UnderlyingPatchID]


