update Patch
set Hidden = 0
where Name in (
'AllPassFilter',
'BandPassFilterConstantPeakGain',
'BandPassFilterConstantTransitionGain',
'HighPassFilter',
'HighShelfFilter',
'LowPassFilter',
'LowShelfFilter',
'NotchFilter',
'PeakingEQFilter')
and DocumentID = (select ID from Document where Name = 'System');