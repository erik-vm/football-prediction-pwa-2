using FluentAssertions;
using FootballPrediction.Application.Interfaces;
using FootballPrediction.Infrastructure.Services;
using Moq;

namespace FootballPrediction.Tests;

public class ScoringServiceTests
{
    private readonly ScoringService _scoring;

    public ScoringServiceTests()
    {
        _scoring = new ScoringService(
            new Mock<IMatchRepository>().Object,
            new Mock<IPredictionRepository>().Object);
    }

    [Theory]
    [InlineData(2, 1, 2, 1, 5)]
    [InlineData(0, 0, 0, 0, 5)]
    [InlineData(3, 2, 3, 2, 5)]
    public void ExactScore_Returns5Points(int ph, int pa, int ah, int aa, int expected)
    {
        _scoring.CalculateBasePoints(ph, pa, ah, aa).Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 0, 2, 1, 4)]
    [InlineData(0, 1, 1, 2, 4)]
    [InlineData(3, 0, 4, 1, 4)]
    [InlineData(2, 2, 1, 1, 4)]
    public void CorrectWinnerAndDifference_Returns4Points(int ph, int pa, int ah, int aa, int expected)
    {
        _scoring.CalculateBasePoints(ph, pa, ah, aa).Should().Be(expected);
    }

    [Theory]
    [InlineData(2, 0, 1, 0, 3)]
    [InlineData(0, 3, 0, 1, 3)]
    [InlineData(3, 1, 1, 0, 3)]
    public void CorrectWinnerOnly_Returns3Points(int ph, int pa, int ah, int aa, int expected)
    {
        _scoring.CalculateBasePoints(ph, pa, ah, aa).Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 0, 1, 3, 1)]
    [InlineData(0, 2, 3, 2, 1)]
    [InlineData(2, 0, 2, 2, 1)]
    public void OneScoreCorrect_Returns1Point(int ph, int pa, int ah, int aa, int expected)
    {
        _scoring.CalculateBasePoints(ph, pa, ah, aa).Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 0, 0, 1, 0)]
    [InlineData(3, 2, 0, 0, 0)]
    public void NoMatch_Returns0Points(int ph, int pa, int ah, int aa, int expected)
    {
        _scoring.CalculateBasePoints(ph, pa, ah, aa).Should().Be(expected);
    }

    [Fact]
    public void DrawPrediction_DrawActual_DifferentScores_Returns4Points()
    {
        _scoring.CalculateBasePoints(1, 1, 2, 2).Should().Be(4);
    }

    [Fact]
    public void DrawPrediction_DrawActual_SameScores_Returns5Points()
    {
        _scoring.CalculateBasePoints(0, 0, 0, 0).Should().Be(5);
    }
}
