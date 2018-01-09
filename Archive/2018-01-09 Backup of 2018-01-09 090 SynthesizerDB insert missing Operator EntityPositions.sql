set nocount off;

begin try
	print 'Begin transaction.'
	begin transaction

	declare @operatorID int;
	declare @entityPositionID int;

	declare cur cursor local for
	select o.ID
	from Operator o
	where o.EntityPositionID is null;

	open cur
		fetch next from cur into @operatorID;

		while @@FETCH_STATUS = 0
		begin
			insert into IDs default values;
			set @entityPositionID = SCOPE_IDENTITY();

			insert into EntityPosition (ID, X, Y) values (@entityPositionID, 50, 50);
			--select * from EntityPosition where ID = @entityPositionID;
			--print 'EntityPosition inserted';

			--select * from Operator where Operator.ID = @operatorID;
			update Operator set EntityPositionID = @entityPositionID where Operator.ID = @operatorID;
			--select * from Operator where Operator.ID = @operatorID;
			--print 'Operator updated';

			fetch next from cur into @operatorID;
		end
	close cur

	
	rollback transaction
	print 'Rolled back.'
	
	--commit transaction
	--print 'Committed.'
	
end try
begin catch

	rollback transaction
	print 'Rolled back.'

	print (ERROR_MESSAGE());
end catch