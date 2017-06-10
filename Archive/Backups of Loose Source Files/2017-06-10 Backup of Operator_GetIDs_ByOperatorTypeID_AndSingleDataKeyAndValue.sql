--declare @operatorTypeID int = 20; /*CustomOperator*/
--declare @dataKey nvarchar(512) = 'UnderlyingPatchID';
--declare @dataValue nvarchar(512) = 5541;

declare @data nvarchar(255) = @dataKey + '=' + @dataValue;

select
	o.ID
from
	Operator o
where
	o.OperatorTypeID = @operatorTypeID and
	o.Data = @data;