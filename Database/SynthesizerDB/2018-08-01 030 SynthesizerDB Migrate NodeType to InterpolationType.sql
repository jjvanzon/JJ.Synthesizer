with NodesToUpdate (ID, Informative_NodeTypeID, Current_InterpolationTypeID, New_InterpolationTypeID) as
(
	select
		ID,
		NodeTypeID as Informative_NodeTypeID,
		InterpolationTypeID as Current_InterpolationTypeID,
		case
			when NodeTypeID = 1 /*Off*/ then null
			when NodeTypeID = 2 /*Block*/ then 1 /*Block*/
			when NodeTypeID = 3 /*Line*/ then 2 /*Line*/
			when NodeTypeID = 4 /*Curve*/ then 4 /*Cubic*/
			
			-- Make it fail on FK constraint violation
			else 100000000000 
		end as New_InterpolationTypeID
	from Node
)
--select * from NodesToUpdate order by Informative_NodeTypeID;
update NodesToUpdate
set Current_InterpolationTypeID = New_InterpolationTypeID
