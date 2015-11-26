alter table Document drop constraint FK_Document_ChildDocumentType;
drop index Document.IX_Document_ChildDocumentTypeID;
alter table Document drop column ChildDocumentTypeID;
drop table ChildDocumentType;
