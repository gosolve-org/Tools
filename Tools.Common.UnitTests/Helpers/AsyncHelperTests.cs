using GoSolve.Tools.Common.Helpers;
using Xunit;

namespace GoSolve.Tools.Common.UnitTests.Helpers;

public class AsyncHelperTests
{
    [Fact]
    public async void GetInParallel_TwoTasks_ReturnsValues()
    {
        var (r1, r2) = await AsyncHelper.GetInParallel(CreateDummyTask(1), CreateDummyTask(2));

        Assert.Equal(1, r1);
        Assert.Equal(2, r2);
    }

    [Fact]
    public async void GetInParallel_ThreeTasks_ReturnsValues()
    {
        var (r1, r2, r3) = await AsyncHelper.GetInParallel(CreateDummyTask(1), CreateDummyTask(2), CreateDummyTask(3));

        Assert.Equal(1, r1);
        Assert.Equal(2, r2);
        Assert.Equal(3, r3);
    }

    [Fact]
    public async void GetInParallel_FourTasks_ReturnsValues()
    {
        var (r1, r2, r3, r4) = await AsyncHelper.GetInParallel(
            CreateDummyTask(1), CreateDummyTask(2), CreateDummyTask(3), CreateDummyTask(4));

        Assert.Equal(1, r1);
        Assert.Equal(2, r2);
        Assert.Equal(3, r3);
        Assert.Equal(4, r4);
    }

    [Fact]
    public async void GetInParallel_FiveTasks_ReturnsValues()
    {
        var (r1, r2, r3, r4, r5) = await AsyncHelper.GetInParallel(
            CreateDummyTask(1), CreateDummyTask(2), CreateDummyTask(3), CreateDummyTask(4), CreateDummyTask(5));

        Assert.Equal(1, r1);
        Assert.Equal(2, r2);
        Assert.Equal(3, r3);
        Assert.Equal(4, r4);
        Assert.Equal(5, r5);
    }

    [Fact]
    public async void GetInParallel_TaskThrowsException_ThrowsException()
    {
        Task<int> ThrowException()
        {
            throw new ArgumentException("Test Exception");
        }

        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            AsyncHelper.GetInParallel(CreateDummyTask(1), ThrowException()));

        Assert.Equal("Test Exception", ex.Message);
    }

    private async Task<int> CreateDummyTask(int returnValue)
    {
        await Task.Delay(1);
        return returnValue;
    }
}
