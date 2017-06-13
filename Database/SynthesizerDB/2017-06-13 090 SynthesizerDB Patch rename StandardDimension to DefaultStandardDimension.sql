exec sp_rename 'Patch.StandardDimensionID', 'DefaultStandardDimensionID', 'COLUMN';
exec sp_rename 'Patch.IX_Patch_StandardDimensionID', 'IX_Patch_DefaultStandardDimensionID';
