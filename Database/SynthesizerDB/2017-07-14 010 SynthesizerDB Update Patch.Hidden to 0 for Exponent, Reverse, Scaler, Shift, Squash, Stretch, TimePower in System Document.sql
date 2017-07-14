update Patch
set Hidden = 0
where Name in ('Exponent', 'Reverse', 'Scaler', 'Shift', 'Squash', 'Stretch', 'TimePower')
and DocumentID = (select ID from Document where Name = 'System');