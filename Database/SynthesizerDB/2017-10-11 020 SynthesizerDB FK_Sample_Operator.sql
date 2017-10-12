ALTER TABLE [dbo].[Sample]  WITH CHECK ADD  CONSTRAINT [FK_Sample_Operator] FOREIGN KEY([OperatorID])
REFERENCES [dbo].[Operator] ([ID])

ALTER TABLE [dbo].[Sample] CHECK CONSTRAINT [FK_Sample_Operator]


