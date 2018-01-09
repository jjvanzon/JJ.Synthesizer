with OperatorsToUpdate (OperatorID, Current_EntityPositionID, New_EntityPositionID) as
(
	select
		o.ID as OperatorID,
		o.EntityPositionID as Current_EntityPositionID,
		e.ID as New_EntityPositionID
	from EntityPosition e
	inner join Operator o on o.ID = e.EntityID
)
update OperatorsToUpdate set Current_EntityPositionID = New_EntityPositionID;
