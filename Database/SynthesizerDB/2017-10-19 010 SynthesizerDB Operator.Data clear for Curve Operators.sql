update Operator
set Data = null
from Operator o
where o.CurveID is not null;