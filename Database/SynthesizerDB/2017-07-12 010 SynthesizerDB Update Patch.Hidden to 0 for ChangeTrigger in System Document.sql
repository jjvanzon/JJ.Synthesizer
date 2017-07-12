update Patch
set Hidden = 0
where Name = 'ChangeTrigger'
and DocumentID = (select ID from Document where Name = 'System');