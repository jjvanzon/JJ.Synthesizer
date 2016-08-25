with PatchesToUpdate (Patch_ID, Patch_GroupName_Current, Patch_GroupName_New) as 
(
	select
		Patch_ID = p.ID,
		Patch_GroupName_Current = p.GroupName,
		Patch_GroupName_New = childDoc.GroupName
	from
		Patch p
		inner join Document childDoc on childDoc.ID = p.DocumentID
	where
		isnull(p.GroupName, '') = '' and
		isnull(childDoc.GroupName, '') != ''
)
--select * from PatchesToUpdate;
update PatchesToUpdate set Patch_GroupName_Current = Patch_GroupName_New;
