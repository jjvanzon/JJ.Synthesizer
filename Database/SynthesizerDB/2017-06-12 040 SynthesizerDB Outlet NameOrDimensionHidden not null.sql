update Outlet set NameOrDimensionHidden = 0 where NameOrDimensionHidden is null;
alter table Outlet alter column NameOrDimensionHidden bit not null;

