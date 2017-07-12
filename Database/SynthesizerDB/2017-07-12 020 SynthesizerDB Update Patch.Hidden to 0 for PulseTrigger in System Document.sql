update Patch
set Hidden = 0
where Name = 'PulseTrigger'
and DocumentID = (select ID from Document where Name = 'System');