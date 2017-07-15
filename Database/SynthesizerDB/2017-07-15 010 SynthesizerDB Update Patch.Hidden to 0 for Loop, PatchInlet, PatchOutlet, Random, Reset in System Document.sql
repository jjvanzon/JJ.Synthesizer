update Patch
set Hidden = 0
where Name in ('Loop', 'PatchInlet', 'PatchOutlet', 'Random', 'Reset')
and DocumentID = (select ID from Document where Name = 'System');