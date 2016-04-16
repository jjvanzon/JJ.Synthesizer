/****** Object:  Index [IX_AudioFileOutput_SpeakerSetupID]    Script Date: 2016-04-16 02:01:00 ******/
CREATE NONCLUSTERED INDEX [IX_AudioFileOutput_SpeakerSetupID] ON [dbo].[AudioOutput]
(
	[SpeakerSetupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
