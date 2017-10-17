update Operator
set Data = null
from Operator o
where exists (select * from Sample s where s.OperatorID = o.ID);