update AudioOutput set MaxConcurrentNotes = 16
where MaxConcurrentNotes is null;

alter table AudioOutput alter column MaxConcurrentNotes int not null;