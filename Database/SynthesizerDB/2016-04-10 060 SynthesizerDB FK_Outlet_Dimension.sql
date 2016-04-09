ALTER TABLE [dbo].[Outlet]  WITH CHECK ADD  CONSTRAINT [FK_Outlet_Dimension] FOREIGN KEY([DimensionID])
REFERENCES [dbo].[Dimension] ([ID])

ALTER TABLE [dbo].[Outlet] CHECK CONSTRAINT [FK_Outlet_Dimension]


