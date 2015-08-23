--update
	--Operator
--set 
--	OperatorTypeID = (select ID from OperatorType ot where ot.Name = OperatorTypeName)



select
	*,
	( select Name from OperatorType ot where ot.ID = o.OperatorTypeID)
from Operator o 

--alter table Operator drop column OperatoøørTypeNameøø