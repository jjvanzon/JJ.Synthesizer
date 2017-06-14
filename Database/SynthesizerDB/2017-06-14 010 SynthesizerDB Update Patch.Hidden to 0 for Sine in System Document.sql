update Patch
set Hidden = 0
where Name = 'Sine'
and DocumentID = (select ID from Document where Name = 'System');