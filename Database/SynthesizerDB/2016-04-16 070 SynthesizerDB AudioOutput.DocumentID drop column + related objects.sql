alter table AudioOutput drop constraint FK_AudioOutput_Document
drop index AudioOutput.IX_AudioOutput_DocumentID
alter table AudioOutput drop column DocumentID