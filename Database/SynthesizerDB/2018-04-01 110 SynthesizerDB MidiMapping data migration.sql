with MidiMappingsToUpdate
(
	ID, 
	-- Midi
	Current_MidiMappingTypeID, New_MidiMappingTypeID, New_MidiMappingTypeName, 
	MidiControllerCode, 
	Current_FromMidiValue, New_FromMidiValue,
	Current_TillMidiValue, New_TillMidiValue,
	Source_FromMidiControllerValue, Source_TillMidiControllerValue,
	Source_FromMidiVelocity, Source_TillMidiVelocity,
	Source_FromMidiNoteNumber, Source_TillMidiNoteNumber,
	-- Synth
	Current_DimensionID, Current_DimensionName, New_DimensionID, New_DimensionName, 
	Current_FromDimensionValue, New_FromDimensionValue,
	Current_TillDimensionValue, New_TillDimensionValue, 
	Source_FromToneNumber, Source_TillToneNumber,
	Current_Position, New_Position, Source_FromPosition, Source_TillPosition
) as
(
	select 
		ID,
		-- Midi
		Current_MidiMappingTypeID = MidiMappingTypeID,
		New_MidiMappingTypeID =
		case
			when FromMidiControllerValue is not null then 1
			when FromMidiVelocity is not null then 2
			when FromMidiNoteNumber is not null then 3
		end,
		New_MidiMappingTypeName =
		case
			when FromMidiControllerValue is not null then (select Name from MidiMappingType where ID = 1)
			when FromMidiVelocity is not null then (select Name from MidiMappingType where ID = 2)
			when FromMidiNoteNumber is not null then (select Name from MidiMappingType where ID = 3)
		end,
		MidiControllerCode,
		Current_FromMidiValue = FromMidiValue, New_FromMidiValue = isnull(FromMidiControllerValue, isnull(FromMidiVelocity, FromMidiNoteNumber)),
		Current_TillMidiValue = TillMidiValue, New_TillMidiValue = isnull(TillMidiControllerValue, isnull(TillMidiVelocity, TillMidiNoteNumber)),
		Source_FromMidiControllerValue = FromMidiControllerValue,
		Source_TillMidiControllerValue = TillMidiControllerValue,
		Source_FromMidiVelocity = FromMidiVelocity,
		Source_TillMidiVelocity = TillMidiVelocity,
		Source_FromMidiNoteNumber = FromMidiNoteNumber,
		Source_TillMidiNoteNumber = TillMidiNoteNumber,
		-- Synth
		Current_DimensionID = DimensionID,
		Current_DimensionName = (select Name from Dimension where ID = DimensionID),
		New_DimensionID = 
			case
				when FromToneNumber is not null then 78 /* NoteNumber */
				when DimensionID is not null then DimensionID 
			end,
		New_DimensionName = 
			case
				when FromToneNumber is not null then (select Name from Dimension where ID = 78 /* NoteNumber */)
				when DimensionID is not null then (select Name from Dimension where ID = DimensionID)
			end,
		Current_FromDimensionValue = FromDimensionValue, New_FromDimensionValue = case when FromToneNumber is not null then FromToneNumber - 1 else FromDimensionValue end,
		Current_TillDimensionVale = TillDimensionValue, New_TillDimensionValue = case when TillToneNumber is not null then TillToneNumber - 1 else TillDimensionValue end,
		Source_FromToneNumber = FromToneNumber,
		Source_TillToneNumber = TillToneNumber,
		Current_Position = Position, New_Position = TillPosition, Source_FromPosition = FromPosition, Source_TillPosition = TillPosition
	from MidiMapping
)
--select * from MidiMappingsToUpdate;
/*
select
	-- Midi
	Current_MidiMappingTypeID = New_MidiMappingTypeID, 
	Current_FromMidiValue = New_FromMidiValue,
	Current_TillMidiValue = New_TillMidiValue,
	-- Synth
	Current_DimensionID = New_DimensionID, 
	Current_FromDimensionValue = New_FromDimensionValue, 
	Current_TillDimesionValue = New_TillDimensionValue, 
	Current_Position = New_Position
from MidiMappingsToUpdate;
*/
update MidiMappingsToUpdate set
	-- Midi
	Current_MidiMappingTypeID = New_MidiMappingTypeID, 
	Current_FromMidiValue = New_FromMidiValue,
	Current_TillMidiValue = New_TillMidiValue,
	-- Synth
	Current_DimensionID = New_DimensionID, 
	Current_FromDimensionValue = New_FromDimensionValue, 
	Current_TillDimensionValue = New_TillDimensionValue, 
	Current_Position = New_Position
