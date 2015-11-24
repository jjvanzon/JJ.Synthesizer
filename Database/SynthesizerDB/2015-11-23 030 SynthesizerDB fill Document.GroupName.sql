with DocumentsToUpdate (ID, Name, ParentDocumentID, ChildDocumentTypeID, Current_GroupName, New_GroupName) as
(
	select
		ID, Name, ParentDocumentID, ChildDocumentTypeID,
		GroupName as Current_GroupName,
		case 
			when ChildDocumentTypeID = 1 /*Instrument*/ then 'Instruments'
			when ChildDocumentTypeID = 2 /*Effect*/ then 'Effects'
		end as New_GroupName
	from Document
	where IsNull(GroupName, '') = ''
)
update DocumentsToUpdate set Current_GroupName = New_GroupName;