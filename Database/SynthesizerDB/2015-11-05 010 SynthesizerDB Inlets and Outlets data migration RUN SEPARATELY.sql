-- Renumbers all Inlets' and Outlets' SortOrders and renames those columns to ListIndex.
-- Also wipes out the names for Inlets and Outlets of operators that are not of type CustomOperator.

begin try
	print 'Begin transaction.'
	begin transaction

	declare @operatorID int;

	declare operatorCursor cursor local for
	select o.ID from Operator o;

	open operatorCursor;

		fetch operatorCursor into @operatorID;

		while (@@FETCH_STATUS = 0)
		begin
			with cte (ID, Name, OperatorID, InputOutletID, SortOrder, ListIndex) as
			(
				select *, (ROW_NUMBER() over (order by i.SortOrder)) - 1 as ListIndex
				from Inlet i
				where i.OperatorID = @operatorID
			)
			update cte set SortOrder = ListIndex;

			with cte2 (ID, Name, OperatorID, SortOrder, ListIndex) as
			(
				select *, (ROW_NUMBER() over (order by o.SortOrder)) - 1 as ListIndex
				from Outlet o
				where o.OperatorID = @operatorID
			)
			update cte2 set SortOrder = ListIndex;

			fetch operatorCursor into @operatorID;
		end

	close operatorCursor

	exec sp_rename 'Inlet.SortOrder', 'ListIndex', 'COLUMN';
	exec sp_rename 'Outlet.SortOrder', 'ListIndex', 'COLUMN';

	update Inlet
	set Name = null	
	from 
		Inlet i
		inner join Operator o on o.ID = i.OperatorID
	where o.OperatorTypeID != 20 /*CustomOperator*/

	update Outlet
	set Name = null	
	from 
		Outlet i
		inner join Operator o on o.ID = i.OperatorID
	where o.OperatorTypeID != 20 /*CustomOperator*/

	-- Check result
	select * from Inlet;
	select * from Outlet;
	
	--rollback transaction
	--print 'Rolled back.'
	
	commit transaction
	print 'Committed.'
end try
begin catch

	rollback transaction
	print 'Rolled back.'

	print (ERROR_MESSAGE());
end catch