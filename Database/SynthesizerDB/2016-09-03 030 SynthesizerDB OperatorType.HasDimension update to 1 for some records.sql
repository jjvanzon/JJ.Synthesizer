update OperatorType
set HasDimension = 1
where ID in (
	8, --Sine
	13, --TimePower
	16, --Curve
	17, --Sample
	18, --Noise
	19, --Resample
	21, --SawUp
	22, --Square
	23, --Triangle
	25, --Loop
	26, --Select
	27, --Bundle
	28, --Unbundle
	29, --Stretch
	30, --Squash
	31, --Shift
	35, --Spectrum
	36, --Pulse
	37, --Random
	48, --MinFollower
	49, --MaxFollower
	50, --AverageFollower
	52, --SawDown
	54, --Reverse
	58, --Cache
	63, --GetDimension
	64, --SetDimension
	66, --Range
	67, --MakeDiscrete
	68, --MakeContinuous
	72, --MaxOverDimension
	73, --MinOverDimension
	74, --AverageOverDimension
	75, --SumOverDimension
	76, --SumFollower
	79, --ClosestOverDimension
	81, --ClosestOverDimensionExp
	83 --SortOverDimension
)