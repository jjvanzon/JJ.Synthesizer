-- Converts the Data property of Operators of type PatchInlet and PatchOutlet
-- from '0', '1', etc. to 'ListIndex=0', 'ListIndex=1' etc.
-- Also corrects Inlets and Outlets of Operators of type CustomOperator,
-- so they start numbering at 0 again.
-- (They accidently became one-based, due to an error in the ToEntity code and a lack of validation.)

begin try
	print 'Begin transaction.'
	begin transaction;

	with PatchInletOperatorsToUpdate (ID, Current_Data, New_Data) as
	(
		select
			ID,
			Data as Current_Data,
			'ListIndex=' + cast((cast(o.Data as int) - 1) as nvarchar(10)) as New_Data
		from Operator o
		where o.OperatorTypeID = 5 /*PatchInlet*/ or o.OperatorTypeID = 6 /*PatchOutlet*/
	)
	--select * from PatchInletOperatorsToUpdate;
	update PatchInletOperatorsToUpdate set Current_Data = New_Data;

	declare @operatorID int;

	declare operatorCursor cursor local for
	select o.ID from Operator o where o.OperatorTypeID = (select ID from OperatorType where Name = 'CustomOperator')

	open operatorCursor;

		fetch operatorCursor into @operatorID;

		while (@@FETCH_STATUS = 0)
		begin
			with cte (ID, Name, OperatorID, InputOutletID, Current_ListIndex, New_ListIndex) as
			(
				select 
					ID, Name, OperatorID, InputOutletID, ListIndex as Current_ListIndex,
					(ROW_NUMBER() over (order by i.ListIndex)) - 1 as New_ListIndex
				from Inlet i
				where i.OperatorID = @operatorID
			)
			--select * from cte;
			update cte set Current_ListIndex = New_ListIndex;

			with cte2 (ID, Name, OperatorID, Current_ListIndex, New_ListIndex) as
			(
				select
					ID, Name, OperatorID, ListIndex as Current_ListIndex,
					(ROW_NUMBER() over (order by o.ListIndex)) - 1 as New_ListIndex
				from Outlet o
				where o.OperatorID = @operatorID
			)
			--select * from cte2;
			update cte2 set Current_ListIndex = New_ListIndex;

			fetch operatorCursor into @operatorID;
		end

	close operatorCursor

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