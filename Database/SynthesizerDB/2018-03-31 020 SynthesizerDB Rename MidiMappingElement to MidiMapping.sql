exec sp_rename 'MidiMappingElement', 'MidiMapping';
exec sp_rename 'PK_MidiMappingElement', 'PK_MidiMapping';
exec sp_rename 'FK_MidiMappingElement_Dimension', 'FK_MidiMapping_Dimension';
exec sp_rename 'FK_MidiMappingElement_EntityPosition', 'FK_MidiMapping_EntityPosition';
exec sp_rename 'FK_MidiMappingElement_MidiMappingGroup', 'FK_MidiMapping_MidiMappingGroup';
exec sp_rename 'FK_MidiMappingElement_Scale', 'FK_MidiMapping_Scale';
exec sp_rename 'MidiMapping.IX_MidiMappingElement_EntityPositionID_Unique', 'IX_MidiMapping_EntityPositionID_Unique', 'INDEX';
exec sp_rename 'MidiMapping.IX_MidiMappingElement_MidiMappingGroupID', 'IX_MidiMapping_MidiMappingGroupID', 'INDEX';
exec sp_rename 'MidiMapping.IX_MidiMappingElement_ScaleID', 'IX_MidiMapping_ScaleID', 'INDEX';
exec sp_rename 'MidiMapping.IX_MidiMappingElement_StandardDimensionID', 'IX_MidiMapping_StandardDimensionID', 'INDEX';