ALTER TABLE [dbo].[Operator]  WITH CHECK ADD  CONSTRAINT [FK_Operator_Curve] FOREIGN KEY([CurveID])
REFERENCES [dbo].[Curve] ([ID])

ALTER TABLE [dbo].[Operator] CHECK CONSTRAINT [FK_Operator_Curve]


