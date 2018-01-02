CREATE TABLE [dbo].[MidiMappingElement](
	[ID] [int] NOT NULL,
	[MidiMappingID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsRelative] [bit] NOT NULL,
	[NoteNumberFrom] [int] NULL,
	[NoteNumberTill] [int] NULL,
	[ControllerCode] [int] NULL,
	[ControllerValueFrom] [int] NULL,
	[ControllerValueTill] [int] NULL,
	[VelocityValueFrom] [int] NULL,
	[VelocityValueTill] [int] NULL,
	[StandardDimensionID] [int] NULL,
	[CustomDimensionName] [nvarchar](256) NULL,
	[DimensionValueFrom] [float] NULL,
	[DimensionValueTill] [float] NULL,
	[DimensionMinValue] [float] NULL,
	[DimensionMaxValue] [float] NULL,
	[ListIndexFrom] [int] NULL,
	[ListIndexTill] [int] NULL,
	[ScaleID] [int] NULL,
	[ToneIndexFrom] [int] NULL,
	[ToneIndexTill] [int] NULL,
 CONSTRAINT [PK_MidiMappingElement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
