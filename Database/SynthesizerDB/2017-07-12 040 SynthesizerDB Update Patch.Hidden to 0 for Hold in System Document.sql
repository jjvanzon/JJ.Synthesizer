update Patch
set Hidden = 0
where Name = 'Hold'
and DocumentID = (select ID from Document where Name = 'System');