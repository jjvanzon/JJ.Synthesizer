select top (@count) x.ID
from 
(
	select 
		d.ID, 
		ROW_NUMBER() over (order by d.Name) as RowNumber 
	from Document d
	where d.ParentDocumentID is null
) as x
where x.RowNumber > @firstIndex
order by x.RowNumber;
