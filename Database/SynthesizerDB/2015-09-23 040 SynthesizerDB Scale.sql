CREATE TABLE [dbo].[Scale](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DocumentID] [int] NOT NULL,
	[ScaleTypeID] [int] NOT NULL,
	[BaseFrequency] [float] NULL,
 CONSTRAINT [PK_Scale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Scale]  WITH CHECK ADD  CONSTRAINT [FK_Scale_Document] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Document] ([ID])

ALTER TABLE [dbo].[Scale] CHECK CONSTRAINT [FK_Scale_Document]

ALTER TABLE [dbo].[Scale]  WITH CHECK ADD  CONSTRAINT [FK_Scale_ScaleType] FOREIGN KEY([ScaleTypeID])
REFERENCES [dbo].[ScaleType] ([ID])

ALTER TABLE [dbo].[Scale] CHECK CONSTRAINT [FK_Scale_ScaleType]


