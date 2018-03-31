exec sp_rename 'MidiMapping', 'MidiMappingGroup';
exec sp_rename 'PK_MidiMapping', 'PK_MidiMappingGroup';
exec sp_rename 'FK_MidiMapping_Document', 'FK_MidiMappingGroup_Document';
exec sp_rename 'MidiMappingGroup.IX_MidiMapping_DocumentID', 'IX_MidiMappingGroup_DocumentID', 'INDEX';

exec sp_rename 'MidiMappingElement.MidiMappingID', 'MidiMappingGroupID';
exec sp_rename 'MidiMappingElement.IX_MidiMappingElement_MidiMappingID', 'IX_MidiMappingElement_MidiMappingGroupID', 'INDEX';
exec sp_rename 'FK_MidiMappingElement_MidiMapping', 'FK_MidiMappingElement_MidiMappingGroup';
