with OperatorsToUpdate (ID, Current_SampleID, New_SampleID) as
(
	select
		o.ID,
		o.SampleID as Current_SampleID,
		s.ID as New_SampleID
	from	
		Operator o
		inner join Sample s on s.OperatorID = o.ID
)
--select * from OperatorsToUpdate
up\date OperatorsToUpdate set Current_SampleID = New_SampleID

