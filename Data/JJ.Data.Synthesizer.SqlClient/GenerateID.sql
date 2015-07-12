declare @i as int;
begin tran;
insert into Identities default values;
set @i = SCOPE_IDENTITY();
rollback;
select @i; 
