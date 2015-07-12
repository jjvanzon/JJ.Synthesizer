declare @i as int;
begin tran;
insert into IDs default values;
set @i = SCOPE_IDENTITY();
rollback;
select @i; 
