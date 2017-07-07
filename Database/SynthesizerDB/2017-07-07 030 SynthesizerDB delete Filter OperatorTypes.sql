delete from OperatorType
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