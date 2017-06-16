update Patch
set Hidden = 0
where Name in ('Or', 'Power', 'Subtract')
and DocumentID = (select ID from Document where Name = 'System');