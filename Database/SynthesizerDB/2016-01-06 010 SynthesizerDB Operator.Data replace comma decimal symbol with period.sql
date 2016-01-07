with OperatorsToUpdate (ID, Data_Current, Data_New) as
(
	select
		ID as ID,
		Data as Data_Current,
		Replace(Data, ',','.') as Data_New
	from Operator
	where 
		OperatorTypeID = 15 /*Number*/
		and CHARINDEX(',', Data) != 0
)
--select * from OperatorsToUpdate;
update OperatorsToUpdate set Data_Current = Data_New;
