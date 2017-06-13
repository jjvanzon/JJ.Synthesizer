update Operator set HasDimension = 0 where HasDimension is null;
alter table Operator alter column HasDimension bit not null;