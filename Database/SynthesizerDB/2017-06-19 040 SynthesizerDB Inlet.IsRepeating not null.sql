update Inlet set IsRepeating = 0 where IsRepeating is null;
alter table Inlet alter column IsRepeating bit not null;

