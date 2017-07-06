update Patch
set Hidden = 0
where Name in ('Noise', 'Pulse', 'SawDown', 'SawUp', 'Square', 'Triangle')
and DocumentID = (select ID from Document where Name = 'System');