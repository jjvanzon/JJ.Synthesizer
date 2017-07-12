update Patch
set Hidden = 0
where Name = 'SetDimension'
and DocumentID = (select ID from Document where Name = 'System');