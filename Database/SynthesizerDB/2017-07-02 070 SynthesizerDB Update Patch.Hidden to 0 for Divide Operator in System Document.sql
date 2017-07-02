update Patch
set Hidden = 0
where Name = 'Divide'
and DocumentID = (select ID from Document where Name = 'System');