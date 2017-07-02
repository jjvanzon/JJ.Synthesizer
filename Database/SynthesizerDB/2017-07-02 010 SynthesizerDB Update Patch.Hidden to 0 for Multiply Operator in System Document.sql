update Patch
set Hidden = 0
where Name = 'Multiply'
and DocumentID = (select ID from Document where Name = 'System');