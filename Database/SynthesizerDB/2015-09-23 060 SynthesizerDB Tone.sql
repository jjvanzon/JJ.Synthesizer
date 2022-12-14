CREATE TABLE [dbo].[Tone](
	[ID] [int] NOT NULL,
	[ScaleID] [int] NOT NULL,
	[Octave] [int] NOT NULL,
	[Number] [float] NOT NULL,
 CONSTRAINT [PK_Tone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Tone]  WITH CHECK ADD  CONSTRAINT [FK_Tone_Scale] FOREIGN KEY([ScaleID])
REFERENCES [dbo].[Scale] ([ID])

ALTER TABLE [dbo].[Tone] CHECK CONSTRAINT [FK_Tone_Scale]
