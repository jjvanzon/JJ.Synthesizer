ALTER TABLE [dbo].[Inlet]  WITH CHECK ADD  CONSTRAINT [FK_Inlet_InletType] FOREIGN KEY([InletTypeID])
REFERENCES [dbo].[InletType] ([ID])

ALTER TABLE [dbo].[Inlet] CHECK CONSTRAINT [FK_Inlet_InletType]


