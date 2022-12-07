select rootdoc.Name, childDoc.Name
from
	Operator o
	inner join OperatorType ot on ot.ID = o.OperatorTypeID
	inner join Patch p on p.ID = o.PatchID
	inner join Document childDoc on childDoc.ID = p.DocumentID
	inner join Document rootDoc on rootDoc.ID = childDoc.ParentDocumentID
where
	ot.Name in ('Filter')