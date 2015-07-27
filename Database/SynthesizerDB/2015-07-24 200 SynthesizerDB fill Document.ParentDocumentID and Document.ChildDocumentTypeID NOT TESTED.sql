update Document
set 
	ParentDocumentID = ISNULL(AsInstrumentInDocumentID, AsEffectInDocumentID),
	ChildDocumentTypeID = (case when AsInstrumentInDocumentID is not null then 1 else 2 end)
from Document
