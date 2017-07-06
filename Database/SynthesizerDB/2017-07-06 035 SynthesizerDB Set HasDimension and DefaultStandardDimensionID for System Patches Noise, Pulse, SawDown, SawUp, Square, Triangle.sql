update Patch
set
	HasDimension = 1,
	DefaultStandardDimensionID = 22 /*Time*/
where Name in ('Noise', 'Pulse', 'SawDown', 'SawUp', 'Square', 'Triangle')
and DocumentID = (select ID from Document where Name = 'System');