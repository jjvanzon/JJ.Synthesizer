ALTER TABLE [Curve]  WITH CHECK ADD  CONSTRAINT [FK_Curve_XDimension] FOREIGN KEY([XDimensionID])
REFERENCES [Dimension] ([ID])

ALTER TABLE [Curve] CHECK CONSTRAINT [FK_Curve_XDimension]
