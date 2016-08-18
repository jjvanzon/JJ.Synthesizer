update Inlet
set IsObsolete = 0
where IsObsolete is null;

alter table Inlet alter column IsObsolete bit not null;