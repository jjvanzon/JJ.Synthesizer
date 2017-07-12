update Patch
set Hidden = 0
where Name = 'Spectrum'
and DocumentID = (select ID from Document where Name = 'System');