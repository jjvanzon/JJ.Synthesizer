update Outlet set IsRepeating = 0 where IsRepeating is null;
alter table Outlet alter column IsRepeating bit not null;

