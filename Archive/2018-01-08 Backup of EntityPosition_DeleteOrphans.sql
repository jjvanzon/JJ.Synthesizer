if (select count(distinct EntityTypeName) from EntityPosition) > 1
raiserror ('This script only works if there is only one EntityTypeName in the EntityPosition table.', 11, -1); 

if (select distinct EntityTypeName from EntityPosition) != 'Operator'
raiserror ('This script only works if the one and only EntityTypeName in the EntityPosition table is equal to ''Operator''.', 11, -1); 

set nocount off
set rowcount 0;

delete
from EntityPosition
where not exists (select * from Operator o where o.ID = EntityID)

select @@rowcount;