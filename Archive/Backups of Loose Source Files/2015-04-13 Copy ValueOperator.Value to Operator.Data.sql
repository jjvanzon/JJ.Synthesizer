use DEV_SynthesizerDB;

with cte (ID, Data, Value) as
(
	select
		o.ID,
		o.Data,
		vo.Value
	from 
		Operator o
		inner join ValueOperator vo on vo.OperatorID = o.ID
)
--update cte set Data = value
