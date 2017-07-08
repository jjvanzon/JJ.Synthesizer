update Patch
set Hidden = 0
where Name in (
'InletsToDimension',
'DimensionToOutlets')
and DocumentID = (select ID from Document where Name = 'System');