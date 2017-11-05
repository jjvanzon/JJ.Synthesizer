exec sp_rename 'Patch.DefaultStandardDimensionID', 'StandardDimensionID', 'COLUMN';
exec sp_rename 'Patch.IX_Patch_DefaultStandardDimensionID', 'IX_Patch_StandardDimensionID';
