namespace SFML.System.Test;

public class AngleTests
{
    private const float Eps = 1e-5f;

    [Fact]
    public void Construction()
    {
        var angle = new Angle();
        Assert.Equal(0f, angle.Degrees);
        Assert.Equal(0f, angle.Radians);
    }

    [Fact]
    public void WrapSigned_WithZero()
        => Assert.Equal(Angle.Zero, Angle.Zero.WrapSigned());

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(90, 90)]
    [InlineData(-90, -90)]
    [InlineData(180, -180)]
    [InlineData(-180, -180)]
    [InlineData(360, 0)]
    [InlineData(-360, 0)]
    [InlineData(720, 0)]
    [InlineData(-720, 0)]
    public void WrapSigned(float input, float expectedWrap)
        => Assert.Equal(Angle.FromDegrees(expectedWrap).Radians, Angle.FromDegrees(input).WrapSigned().Radians, Eps);

    [Fact]
    public void WrapUnsigned_WithZero()
        => Assert.Equal(Angle.Zero, Angle.Zero.WrapUnsigned());

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(-1, 359)]
    [InlineData(90, 90)]
    [InlineData(-90, 270)]
    [InlineData(180, 180)]
    [InlineData(-180, 180)]
    [InlineData(360, 0)]
    [InlineData(-360, 0)]
    [InlineData(720, 0)]
    [InlineData(-720, 0)]
    public void WrapUnsigned(float input, float expectedWrap)
        => Assert.Equal(Angle.FromDegrees(expectedWrap).Radians, Angle.FromDegrees(input).WrapUnsigned().Radians, Eps);

    [Theory]
    [InlineData(15, 0.26179939f)]
    [InlineData(1000, 17.453293f)]
    [InlineData(-4321, -75.415677f)]
    public void Degrees(float degrees, float expectedRadians)
    {
        var angle = Angle.FromDegrees(degrees);
        Assert.Equal(degrees, angle.Degrees, Eps);
        Assert.Equal(expectedRadians, angle.Radians, Eps);
    }

    [Theory]
    [InlineData(1, 57.2957795f)]
    [InlineData(72, 4125.29612f)]
    [InlineData(-200, -11459.1559f)]
    public void Radians(float radians, float expectedDegrees)
    {
        var angle = Angle.FromRadians(radians);
        Assert.Equal(radians, angle.Radians, Eps);
        Assert.Equal(expectedDegrees, angle.Degrees, Eps);
    }

    [Fact]
    public void Constants()
    {
        Assert.Equal(0f, Angle.Zero.Degrees);
        Assert.Equal(0f, Angle.Zero.Radians);
    }

    [Fact]
    public void OperatorEq()
    {
        Assert.True(new Angle() == new Angle());
        Assert.True(new Angle() == Angle.Zero);
        Assert.True(new Angle() == Angle.FromDegrees(0));
        Assert.True(new Angle() == Angle.FromRadians(0));
        Assert.True(Angle.FromDegrees(0) == Angle.FromRadians(0));
        Assert.True(Angle.FromDegrees(15) == Angle.FromDegrees(15));
        Assert.True(Angle.FromRadians(15) == Angle.FromRadians(15));
        Assert.True(Angle.FromDegrees(360) == Angle.FromDegrees(360));
        Assert.True(Angle.FromDegrees(720) == Angle.FromDegrees(720));
    }

    [Fact]
    public void OperatorNotEq()
    {
        Assert.True(new Angle() != Angle.FromRadians(2));
        Assert.True(Angle.FromDegrees(1) != Angle.FromRadians(1));
        Assert.True(Angle.FromRadians(0) != Angle.FromRadians(0.1f));
    }

    [Fact]
    public void OperatorLt()
    {
        Assert.True(Angle.FromRadians(0) < Angle.FromDegrees(0.1f));
        Assert.True(Angle.FromDegrees(0) < Angle.FromRadians(0.1f));
        Assert.True(Angle.FromRadians(-0.1f) < Angle.FromRadians(0f));
        Assert.True(Angle.FromDegrees(-0.1f) < Angle.FromDegrees(0f));
    }

    [Fact]
    public void OperatorGt()
    {
        Assert.True(Angle.FromRadians(0.1f) > Angle.FromDegrees(0));
        Assert.True(Angle.FromDegrees(0.1f) > Angle.FromRadians(0));
        Assert.True(Angle.FromRadians(0) > Angle.FromRadians(-0.1f));
        Assert.True(Angle.FromDegrees(0) > Angle.FromDegrees(-0.1f));
    }

    [Fact]
    public void OperatorLte()
    {
        Assert.True(Angle.FromRadians(0) <= Angle.FromDegrees(0.1f));
        Assert.True(Angle.FromDegrees(0) <= Angle.FromRadians(0.1f));
        Assert.True(Angle.FromRadians(-0.1f) <= Angle.FromRadians(0f));
        Assert.True(Angle.FromDegrees(-0.1f) <= Angle.FromDegrees(0f));

        Assert.True(new Angle() <= new Angle());
        Assert.True(new Angle() <= Angle.Zero);
        Assert.True(new Angle() <= Angle.FromDegrees(0));
        Assert.True(new Angle() <= Angle.FromRadians(0));
        Assert.True(Angle.FromDegrees(0) <= Angle.FromRadians(0));
        Assert.True(Angle.FromDegrees(15) <= Angle.FromDegrees(15));
        Assert.True(Angle.FromRadians(15) <= Angle.FromRadians(15));
        Assert.True(Angle.FromDegrees(360) <= Angle.FromDegrees(360));
        Assert.True(Angle.FromDegrees(720) <= Angle.FromDegrees(720));
    }

    [Fact]
    public void OperatorGte()
    {
        Assert.True(Angle.FromRadians(0.1f) > Angle.FromDegrees(0));
        Assert.True(Angle.FromDegrees(0.1f) > Angle.FromRadians(0));
        Assert.True(Angle.FromRadians(0) > Angle.FromRadians(-0.1f));
        Assert.True(Angle.FromDegrees(0) > Angle.FromDegrees(-0.1f));

        Assert.True(new Angle() >= new Angle());
        Assert.True(new Angle() >= Angle.Zero);
        Assert.True(new Angle() >= Angle.FromDegrees(0));
        Assert.True(new Angle() >= Angle.FromRadians(0));
        Assert.True(Angle.FromDegrees(0) >= Angle.FromRadians(0));
        Assert.True(Angle.FromDegrees(15) >= Angle.FromDegrees(15));
        Assert.True(Angle.FromRadians(15) >= Angle.FromRadians(15));
        Assert.True(Angle.FromDegrees(360) >= Angle.FromDegrees(360));
        Assert.True(Angle.FromDegrees(720) >= Angle.FromDegrees(720));
    }

    [Fact]
    public void OperatorNeg()
    {
        Assert.Equal(-new Angle(), new Angle());
        Assert.Equal(-Angle.FromRadians(-1), Angle.FromRadians(1));
        Assert.Equal(-Angle.FromDegrees(15), Angle.FromDegrees(-15));
        Assert.Equal(-Angle.FromRadians(1), Angle.FromRadians(-1));
    }

    [Fact]
    public void OperatorAdd()
    {
        Assert.Equal(new Angle() + new Angle(), new Angle());
        Assert.Equal(Angle.Zero + Angle.FromRadians(0.5f), Angle.FromRadians(0.5f));
        Assert.Equal(Angle.FromRadians(6) + Angle.FromRadians(0.5f), Angle.FromRadians(6.5f));
        Assert.Equal(Angle.FromRadians(10) + Angle.FromRadians(0.5f), Angle.FromRadians(10.5f));
        Assert.Equal(Angle.FromDegrees(360) + Angle.FromDegrees(360), Angle.FromDegrees(720));
    }

    [Fact]
    public void OperatorAddAssign()
    {
        var angle = Angle.FromDegrees(-15);
        angle += Angle.FromDegrees(15);
        Assert.Equal(Angle.FromDegrees(0).Degrees, angle.Degrees, Eps);
        angle += Angle.FromDegrees(10);
        Assert.Equal(Angle.FromDegrees(10).Degrees, angle.Degrees, Eps);
    }

    [Fact]
    public void OperatorSub()
    {
        Assert.Equal(new Angle() - new Angle(), new Angle());
        Assert.Equal(Angle.FromRadians(1) - Angle.FromRadians(0.5f), Angle.FromRadians(0.5f));
        Assert.Equal(Angle.Zero - Angle.FromRadians(0.5f), Angle.FromRadians(-0.5f));
        Assert.Equal(Angle.FromDegrees(900) - Angle.FromDegrees(1), Angle.FromDegrees(899));
    }

    [Fact]
    public void OperatorSubAssign()
    {
        var angle = Angle.FromDegrees(15);
        angle -= Angle.FromDegrees(15);
        Assert.Equal(Angle.FromDegrees(0).Degrees, angle.Degrees, Eps);
        angle -= Angle.FromDegrees(10);
        Assert.Equal(Angle.FromDegrees(-10).Degrees, angle.Degrees, Eps);
    }

    [Fact]
    public void OperatorMul()
    {
        Assert.Equal(10 * Angle.FromRadians(0), Angle.Zero);
        Assert.Equal((2.5f * Angle.FromDegrees(10)).Degrees, Angle.FromDegrees(25).Degrees, Eps);
        Assert.Equal((10f * Angle.FromDegrees(100)).Degrees, Angle.FromDegrees(1000).Degrees, Eps);

        Assert.Equal(10 * Angle.FromRadians(0), Angle.Zero);
        Assert.Equal((2.5f * Angle.FromDegrees(10)).Degrees, Angle.FromDegrees(25).Degrees, Eps);
        Assert.Equal((10f * Angle.FromDegrees(100)).Degrees, Angle.FromDegrees(1000).Degrees, Eps);
    }

    [Fact]
    public void OperatorMulAssign()
    {
        var angle = Angle.FromDegrees(1);
        angle *= 10;
        Assert.Equal(Angle.FromDegrees(10).Degrees, angle.Degrees, Eps);
    }

    [Fact]
    public void OperatorDiv()
    {
        Assert.Equal(Angle.Zero / 10, Angle.Zero);
        Assert.Equal(Angle.FromDegrees(10) / 2.5f, Angle.FromDegrees(4));
        Assert.Equal(Angle.FromRadians(12) / 3, Angle.FromRadians(4));

        Assert.Equal(0f, Angle.Zero / Angle.FromDegrees(1));
        Assert.Equal(1f, Angle.FromDegrees(10) / Angle.FromDegrees(10));
        Assert.Equal(5f, Angle.FromRadians(10) / Angle.FromRadians(2), Eps);
    }

    [Fact]
    public void OperatorDivAssign()
    {
        var angle = Angle.FromDegrees(60);
        angle /= 5;
        Assert.Equal(Angle.FromDegrees(12).Degrees, angle.Degrees, Eps);
    }

    [Fact]
    public void OperatorMod()
    {
        Assert.Equal(Angle.Zero % Angle.FromRadians(0.5f), Angle.Zero);
        Assert.Equal(Angle.FromRadians(10) % Angle.FromRadians(1), Angle.FromRadians(0));
        Assert.Equal((Angle.FromDegrees(90) % Angle.FromDegrees(30)).Degrees, Angle.FromDegrees(0).Degrees, Eps);
        Assert.Equal((Angle.FromDegrees(90) % Angle.FromDegrees(40)).Degrees, Angle.FromDegrees(10).Degrees, Eps);
        Assert.Equal((Angle.FromDegrees(-90) % Angle.FromDegrees(30)).Degrees, Angle.FromDegrees(0).Degrees, Eps);
        Assert.Equal((Angle.FromDegrees(-90) % Angle.FromDegrees(40)).Degrees, Angle.FromDegrees(30).Degrees, Eps);
    }

    [Fact]
    public void OperatorModAssign()
    {
        var angle = Angle.FromDegrees(59);
        angle %= Angle.FromDegrees(10);
        Assert.Equal(Angle.FromDegrees(9).Degrees, angle.Degrees, Eps);
    }
}