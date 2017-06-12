update Inlet set NameOrDimensionHidden = 0 where NameOrDimensionHidden is null;
alter table Inlet alter column NameOrDimensionHidden bit not null;

