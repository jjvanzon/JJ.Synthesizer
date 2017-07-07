delete from OperatorType
where Name in (
'AverageOverInlets',
'ClosestOverInlets',
'ClosestOverInletsExp',
'MaxOverInlets',
'MinOverInlets',
'RangeOverOutlets',
'SortOverInlets')