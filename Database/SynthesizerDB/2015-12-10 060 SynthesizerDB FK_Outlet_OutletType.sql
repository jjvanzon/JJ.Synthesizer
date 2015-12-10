ALTER TABLE [dbo].[Outlet]  WITH CHECK ADD  CONSTRAINT [FK_Outlet_OutletType] FOREIGN KEY([OutletTypeID])
REFERENCES [dbo].[OutletType] ([ID])

ALTER TABLE [dbo].[Outlet] CHECK CONSTRAINT [FK_Outlet_OutletType]


