declare @entityID int;
declare @entityPositionID int;

declare cur cursor local for
select e.ID
from Operator e
where e.EntityPositionID is null;

open cur
	fetch next from cur into @entityID;

	while @@FETCH_STATUS = 0
	begin
		insert into IDs default values;
		set @entityPositionID = SCOPE_IDENTITY();

		insert into EntityPosition (ID, X, Y) values (@entityPositionID, 50, 50);

		update Operator set EntityPositionID = @entityPositionID where Operator.ID = @entityID;

		fetch next from cur into @entityID;
	end
close cur
