﻿using Domain.Trainer;

namespace Domain.Unit.Trainer;

public class RankTests
{
    [Fact]
    public void RankCreate_ValidData_ReturnsRank()
    {
        var rank = Rank.Create("name", 0, 1, 1);
        rank.Should().NotBeNull();
    }

    [Fact]
    public void RankCreate_InvalidBounds_ThrowsException()
    {
        Action create = () => Rank.Create("name", 2, 1, 1);
        create.Invoking(action => action.Invoke()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void RankCreate_NameIsEmpty_ThrowsException()
    {
        Action create = () => Rank.Create("", 0, 1, 1);
        create.Invoking(action => action.Invoke()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void RankCreate_NameIsNull_ThrowsException()
    {
        Action create = () => Rank.Create(null, 0, 1, 1);
        create.Invoking(action => action.Invoke()).Should().Throw<ArgumentException>();
    }
}