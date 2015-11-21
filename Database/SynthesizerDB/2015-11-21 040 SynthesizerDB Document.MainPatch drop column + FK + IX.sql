alter table Document drop constraint FK_Document_Patch
drop index Document.IX_Document_MainPatchID;
alter table Document drop column MainPatchID;