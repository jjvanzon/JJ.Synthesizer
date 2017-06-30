update Patch
set Hidden = 0
where Name = 'Add'
and DocumentID = (select ID from Document where Name = 'System');