update Patch
set Hidden = 0
where Name = 'MultiplyWithOrigin'
and DocumentID = (select ID from Document where Name = 'System');