namespace GoSolve.Tools.Common.HttpClients.Models;

public class CircuitBreakerConfiguration
{
    /// <summary>
    /// The failure threshold at which the circuit will break (a number between 0 and 1; eg 0.5 represents breaking if 50% or more of actions result in a handled failure).
    /// </summary>
    public double FailureThreshold { get; set; } = 0.25;

    /// <summary>
    /// The duration of the timeslice over which failure ratios are assessed, in seconds.
    /// </summary>
    public double SamplingDuration { get; set; } = 60;

    /// <summary>
    /// The minimum throughput: this many actions or more must pass through the circuit in the timeslice, for statistics to be considered significant and the circuit-breaker to come into action.
    /// </summary>
    public int MinimumThroughput { get; set; } = 10;

    /// <summary>
    /// The duration the circuit will stay open before resetting, in seconds.
    /// </summary>
    public double DurationOfBreak { get; set; } = 30;
}
