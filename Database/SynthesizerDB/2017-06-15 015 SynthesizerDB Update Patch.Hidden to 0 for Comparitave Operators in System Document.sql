update Patch
set Hidden = 0
where Name in ('Equal', 'GreaterThan', 'GreaterThanOrEqual', 'LessThan', 'LessThanOrEqual', 'NotEqual')
and DocumentID = (select ID from Document where Name = 'System');