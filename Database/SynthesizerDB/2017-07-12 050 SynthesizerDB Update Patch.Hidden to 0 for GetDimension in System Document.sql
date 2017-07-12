update Patch
set Hidden = 0
where Name = 'GetDimension'
and DocumentID = (select ID from Document where Name = 'System');