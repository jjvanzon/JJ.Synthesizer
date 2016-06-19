with OperatorsToUpdate (ID, Name_Current, Name_New, OperatorTypeID_Current, OperatorTypeID_New) as
(
	select
		o.ID as ID,
		o.Name as Name_Current,
		case 
			when o.Name = 'Adder' then null
			else o.Name
		end as Name_New,
		o.OperatorTypeID as OperatorTypeID_Current,
		1 /*Add*/ as OperatorTypeID_New
	from 
		Operator o
	where
		o.OperatorTypeID = 2 /*Adder*/
)
--select * from OperatorsToUpdate;
update OperatorsToUpdate 
set 
	Name_Current = Name_New, 
	OperatorTypeID_Current = OperatorTypeID_New;

delete from OperatorType where ID = 2 /*Adder*/;