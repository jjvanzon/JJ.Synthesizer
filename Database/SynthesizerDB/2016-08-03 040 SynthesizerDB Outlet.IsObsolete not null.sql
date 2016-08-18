update Outlet
set IsObsolete = 0
where IsObsolete is null;

alter table Outlet alter column IsObsolete bit not null;