begin try
	print 'Begin transaction.'
	begin transaction

	delete i
	from 
		Operator o
		inner join Inlet i on i.OperatorID = o.ID
	where
		o.OperatorTypeID in ( 11 /*SpeedUp*/, 12 /*SlowDown*/)
		and i.ListIndex = 2
		
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