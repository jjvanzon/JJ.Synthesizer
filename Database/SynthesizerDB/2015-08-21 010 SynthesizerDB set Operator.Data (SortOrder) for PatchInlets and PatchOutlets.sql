update Operator 
set Data = '1'
where 
	OperatorTypeID in (5, 6) and
	isnull(Data, '') = ''
