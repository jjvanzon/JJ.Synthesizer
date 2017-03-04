exec sp_rename 'DocumentReference.DependentOnDocumentID', 'LowerDocumentID', 'COLUMN';
exec sp_rename 'DocumentReference.DependentDocumentID', 'HigherDocumentID', 'COLUMN';
exec sp_rename 'FK_DocumentReference_DepedentOnDocument', 'FK_DocumentReference_LowerDocumentID';
exec sp_rename 'FK_DocumentReference_DependentDocument', 'FK_DocumentReference_HigherDocumentID';
exec sp_rename 'DocumentReference.IX_DocumentReference_DependentDocumentID', 'IX_DocumentReference_HigherDocumentID', 'INDEX';
