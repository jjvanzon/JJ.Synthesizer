if (tape.Buff == null)
{
    throw new Exception("tape.Buff = null. Are the actions run too early?");
}

var key = $"{new{freq, duration, depth, churnRate, interferenceRate, chorusRate, patchyEnvelope}}";

.Play(duration, key)