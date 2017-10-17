-- Bytes must be nullable, because bytes is set in a separate SQL statement after Sample has been inserted.
alter table Sample alter column bytes varbinary(max) null;