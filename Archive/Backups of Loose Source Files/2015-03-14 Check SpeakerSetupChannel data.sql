select
	ss.Name, 
	c.Name,
	c.IndexNumber,
	ssc.IndexNumber
from
	SpeakerSetup ss
	inner join SpeakerSetupChannel ssc on ssc.SpeakerSetupID = ss.ID
	inner join Channel c on c.ID = ssc.ChannelID
	