update Patch
set Hidden = 0
where Name in (
'AverageFollower',
'AverageOverDimension',
'ClosestOverDimension',
'ClosestOverDimensionExp',
'MaxFollower',
'MaxOverDimension',
'MinFollower',
'MinOverDimension',
'RangeOverDimension',
'SortOverDimension',
'SumFollower',
'SumOverDimension')
and DocumentID = (select ID from Document where Name = 'System');