update Patch
set Hidden = 0
where Name = 'Round'
and DocumentID = (select ID from Document where Name = 'System');