begin try
	print 'Begin transaction.'
	begin transaction;

	with PatchInletOperatorsToUpdate (ID, Data_Current, Data_New) as
	(
		select 
			ID, 
			Data as Data_Current,
			Data + ';DimensionEnum=Undefined' as Data_New 
		from Operator 
		where 
			OperatorTypeID = 5 /*PatchInlet*/ and
			CharIndex('DimensionEnum', Data) = 0
	)
	--select * from PatchInletOperatorsToUpdate;
	update PatchInletOperatorsToUpdate set Data_Current = Data_New;

	with PatchOutletOperatorsToUpdate (ID, Data_Current, Data_New) as
	(
		select 
			ID, 
			Data as Data_Current,
			Data + ';OutletTypeEnum=Undefined' as Data_New 
		from Operator 
		where 
			OperatorTypeID = 6 /*PatchOutlet*/ and
			CharIndex('OutletTypeEnum', Data) = 0
	)
	--select * from PatchOutletOperatorsToUpdate;
	update PatchOutletOperatorsToUpdate set Data_Current = Data_New;

	-- Check result
	--select * from Operator

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