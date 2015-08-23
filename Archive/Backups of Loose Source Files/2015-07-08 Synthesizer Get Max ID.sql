select MAX(ID) from 
(
	select ID from AudioFileOutput union
	select ID from AudioFileOutputChannel union
	select ID from Channel union
	select ID from Curve union
	select ID from Document union
	select ID from DocumentReference union
	select ID from EntityPosition union
	select ID from Inlet union
	select ID from Node union
	select ID from Operator union
	select ID from Outlet union
	select ID from Patch union
	select ID from Sample 
) as x

-- 2051