CREATE TABLE [dbo].[AudioOutput](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SpeakerSetupID] [int] NOT NULL,
	[VolumeFactor] [real] NOT NULL,
	[SpeedFactor] [real] NOT NULL,
	[DocumentID] [int] NOT NULL,
 CONSTRAINT [PK_AudioOutput] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[AudioOutput]  WITH CHECK ADD  CONSTRAINT [FK_AudioOutput_Document] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[Document] ([ID])

ALTER TABLE [dbo].[AudioOutput] CHECK CONSTRAINT [FK_AudioOutput_Document]

ALTER TABLE [dbo].[AudioOutput]  WITH CHECK ADD  CONSTRAINT [FK_AudioOutput_SpeakerSetup] FOREIGN KEY([SpeakerSetupID])
REFERENCES [dbo].[SpeakerSetup] ([ID])

ALTER TABLE [dbo].[AudioOutput] CHECK CONSTRAINT [FK_AudioOutput_SpeakerSetup]
