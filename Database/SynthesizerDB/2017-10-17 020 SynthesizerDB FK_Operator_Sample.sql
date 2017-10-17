ALTER TABLE [dbo].[Operator]  WITH CHECK ADD  CONSTRAINT [FK_Operator_Sample] FOREIGN KEY([SampleID])
REFERENCES [dbo].[Sample] ([ID])

ALTER TABLE [dbo].[Operator] CHECK CONSTRAINT [FK_Operator_Sample]


