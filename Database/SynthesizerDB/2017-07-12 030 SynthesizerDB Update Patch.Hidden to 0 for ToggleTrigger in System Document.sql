update Patch
set Hidden = 0
where Name = 'ToggleTrigger'
and DocumentID = (select ID from Document where Name = 'System');