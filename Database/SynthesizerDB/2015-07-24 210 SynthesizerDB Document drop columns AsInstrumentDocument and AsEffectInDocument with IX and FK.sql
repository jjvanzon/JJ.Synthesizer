drop index Document.IX_Document_AsEffectInDocumentID
drop index Document.IX_Document_AsInstrumentInDocumentID
alter table Document drop constraint FK_Document_AsEffectInDocument
alter table Document drop constraint FK_Document_AsInstrumentInDocument
alter table Document drop column AsInstrumentInDocumentID
alter table Document drop column AsEffectInDocumentID
