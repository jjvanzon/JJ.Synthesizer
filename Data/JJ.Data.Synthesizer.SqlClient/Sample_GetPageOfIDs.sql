select top (@count) x.ID
from 
(
	select 
		ID, 
		ROW_NUMBER() over (order by ID) as RowNumber 
	from Sample
) as x
where x.RowNumber >= @firstIndex + 1;
