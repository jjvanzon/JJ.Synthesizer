with cte (ID, Current_ListIndex, New_ListIndex) as
(
	select
		i.ID,
		i.ListIndex as Current_ListIndex,
		case 
			when i.ListIndex = 1 then 0
			when i.ListIndex = 3 then 1
		end as New_ListIndex
	from
		Inlet i
		inner join Operator o on o.ID = i.OperatorID
	where
		o.OperatorTypeID = 8 /*Sine*/
)
update cte set Current_ListIndex = New_ListIndex