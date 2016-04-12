ALTER TABLE [Curve]  WITH CHECK ADD  CONSTRAINT [FK_Curve_YDimension] FOREIGN KEY([YDimensionID])
REFERENCES [Dimension] ([ID])

ALTER TABLE [Curve] CHECK CONSTRAINT [FK_Curve_YDimension]
