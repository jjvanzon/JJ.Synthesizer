update Operator
set Data = null
from Operator o
where o.OperatorTypeID in (select ot.ID from OperatorType ot where ot.Name in ('Average', 'Minimum', 'Maximum'))