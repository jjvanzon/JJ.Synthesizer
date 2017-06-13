update Operator 
set
	OperatorTypeID = (select ID from OperatorType where Name = 'Sine'),
	UnderlyingPatchID = null 
from Operator 
where UnderlyingPatchID = 
(
	select ID
	from Patch 
	where Name = 'Sine'
	and DocumentID =
	(
		select ID 
		from Document 
		where Name = 'System'
	)
);