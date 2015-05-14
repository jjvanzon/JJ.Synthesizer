select count(*) 
from Document 
where 
	AsInstrumentInDocumentID is null and 
	AsEffectInDocumentID is null