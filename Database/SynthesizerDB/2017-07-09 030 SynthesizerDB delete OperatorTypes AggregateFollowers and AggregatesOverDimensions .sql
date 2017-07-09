delete from OperatorType
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