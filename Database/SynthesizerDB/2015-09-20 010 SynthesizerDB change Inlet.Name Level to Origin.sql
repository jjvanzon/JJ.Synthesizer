update i
set Name = 'Origin' 
from 
	Inlet i
	inner join Operator o on o.ID = i.OperatorID
	inner join OperatorType ot on ot.ID = o.OperatorTypeID
where 
	i.Name = 'Level' and
	-- Just to make this script a little safer: also filter by OperatorType
	ot.ID = 8 /*Sine*/
