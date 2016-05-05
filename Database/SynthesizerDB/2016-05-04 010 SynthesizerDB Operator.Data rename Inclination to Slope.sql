with OperatorsToUpdate (ID, Current_Data, New_Data) as
(
	select 
		o.ID,
		o.Data as Current_Data,
		replace(o.Data, 'Inclination', 'Slope') as New_Data
	from Operator o
	where o.Data like '%Inclination%'
)
--select * from OperatorsToUpdate;
update OperatorsToUpdate set Current_Data = New_Data;