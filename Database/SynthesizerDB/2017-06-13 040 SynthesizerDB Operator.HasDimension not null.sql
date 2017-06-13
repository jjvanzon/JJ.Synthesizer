update Patch set HasDimension = 0 where HasDimension is null;
alter table Patch alter column HasDimension bit not null;