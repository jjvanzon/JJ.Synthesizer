--select *
update Inlet
set IsRepeating = 1
from
	Inlet i
	inner join Dimension d on d.ID = i.DimensionID
	inner join Operator o on o.ID = i.OperatorID
	inner join OperatorType ot on ot.ID = o.OperatorTypeID
where
	ot.Name in ('ClosestOverInlets', 'ClosestOverInletsExp') and
	d.Name = 'Item'
