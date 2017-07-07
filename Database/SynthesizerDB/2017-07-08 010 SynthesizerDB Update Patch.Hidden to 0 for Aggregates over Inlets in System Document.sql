update Patch
set Hidden = 0
where Name in (
'AverageOverInlets',
'ClosestOverInlets',
'ClosestOverInletsExp',
'MaxOverInlets',
'MinOverInlets',
'RangeOverOutlets',
'SortOverInlets')
and DocumentID = (select ID from Document where Name = 'System');