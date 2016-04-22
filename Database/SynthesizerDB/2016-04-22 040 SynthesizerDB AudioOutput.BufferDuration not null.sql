update AudioOutput set BufferDuration = 0.1
where BufferDuration is null;

alter table AudioOutput alter column BufferDuration float not null;