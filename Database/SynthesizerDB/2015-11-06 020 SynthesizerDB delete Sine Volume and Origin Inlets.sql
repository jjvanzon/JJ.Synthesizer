with cte(ID) as
(
	select
		i.ID
	from
		Inlet i
		inner join Operator o on o.ID = i.OperatorID
	where
		o.OperatorTypeID = 8 /*Sine*/
		and i.ListIndex in (0 /*Volume*/, 2 /* Origin */)
)
delete from Inlet where ID in (select ID from cte)
