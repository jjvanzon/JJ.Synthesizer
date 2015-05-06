select top (@count) x.ID
from 
(
	select 
		ID, 
		ROW_NUMBER() over (order by ID) as RowNumber 
	from Document
	where 
		AsInstrumentInDocumentID is null and 
		AsEffectInDocumentID is null
) as x
where x.RowNumber >= @firstIndex + 1;
