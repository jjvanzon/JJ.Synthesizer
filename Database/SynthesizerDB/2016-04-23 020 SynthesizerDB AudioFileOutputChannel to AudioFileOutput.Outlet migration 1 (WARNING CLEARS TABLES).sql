delete from AudioFileOutputChannel;
delete from AudioFileOutput;
drop table AudioFileOutputChannel;

alter table AudioFileOutput add OutletID int not null;
