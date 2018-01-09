delete EntityPosition
from EntityPosition e
where not exists (select * from Operator o where o.EntityPositionID = e.ID)
and not exists (select * from MidiMappingElement m where m.EntityPositionID = e.ID)