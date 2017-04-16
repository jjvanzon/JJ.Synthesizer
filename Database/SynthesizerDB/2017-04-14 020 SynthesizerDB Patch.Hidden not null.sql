update Patch set Hidden = 0 where Hidden is null;
alter table Patch add Hidden bit not null;