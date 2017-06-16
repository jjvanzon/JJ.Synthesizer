update Patch
set Hidden = 0
where Name in ('Negative', 'Not', 'OneOverX')
and DocumentID = (select ID from Document where Name = 'System');