update Inlet set WarnIfEmpty = 0 where WarnIfEmpty is null;
alter table Inlet alter column WarnIfEmpty bit not null;

