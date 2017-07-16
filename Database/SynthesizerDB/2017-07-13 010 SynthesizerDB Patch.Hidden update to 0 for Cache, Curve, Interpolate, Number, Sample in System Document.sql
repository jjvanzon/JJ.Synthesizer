update Patch
set Hidden = 0
where Name in (
'Cache', 
'Curve', 
'Interpolate', 
'Number', 
'Sample')
and DocumentID = (select ID from Document where Name = 'System');