drop index MidiMapping.IX_MidiMapping_ScaleID;
alter table MidiMapping drop constraint FK_MidiMapping_Scale;
alter table MidiMapping drop column ScaleID;
