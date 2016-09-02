update OperatorType
set HasDimension = 0
where HasDimension is null;

alter table OperatorType alter column HasDimension bit not null;