namespace SFML.System.Test;

public class Vector2fTests
{
    private const float Eps = 1e-5f;
    private const float EpsRelaxed = 15e-5f;
    private const float Sqrt2Div2 = 0.7071067811865475f;

    private static void AssertVectorEqual(Vector2f lhs, Vector2f rhs, float tolerance = 0f)
    {
        Assert.Equal(lhs.X, rhs.X, tolerance);
        Assert.Equal(lhs.Y, rhs.Y, tolerance);
    }

    [Fact]
    public void DefaultConstructor()
    {
        var vec = new Vector2f();
        Assert.Equal(0, vec.X);
        Assert.Equal(0, vec.Y);
    }

    [Fact]
    public void CoordinateConstructor()
    {
        var vec = new Vector2f(1, 2);
        Assert.Equal(1, vec.X);
        Assert.Equal(2, vec.Y);
    }

    // TODO Conversion methods

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 45, 0, 0)]
    [InlineData(0, 90, 0, 0)]
    [InlineData(0, 135, 0, 0)]
    [InlineData(0, 180, 0, 0)]
    [InlineData(0, 270, 0, 0)]
    [InlineData(0, 360, 0, 0)]
    [InlineData(0, -90, 0, 0)]
    [InlineData(0, -180, 0, 0)]
    [InlineData(0, -270, 0, 0)]
    [InlineData(0, -360, 0, 0)]
    [InlineData(1, 0, 1, 0)]
    [InlineData(1, 45, Sqrt2Div2, Sqrt2Div2)]
    [InlineData(1, 90, 0, 1)]
    [InlineData(1, 135, -Sqrt2Div2, Sqrt2Div2)]
    [InlineData(1, 180, -1, 0)]
    [InlineData(1, 270, 0, -1)]
    [InlineData(1, 360, 1, 0)]
    [InlineData(1, -90, 0, -1)]
    [InlineData(1, -180, -1, 0)]
    [InlineData(1, -270, 0, 1)]
    [InlineData(1, -360, 1, 0)]
    [InlineData(-1, 0, -1, 0)]
    [InlineData(-1, 45, -Sqrt2Div2, -Sqrt2Div2)]
    [InlineData(-1, 90, 0, -1)]
    [InlineData(-1, 135, Sqrt2Div2, -Sqrt2Div2)]
    [InlineData(-1, 180, 1, 0)]
    [InlineData(-1, 270, 0, 1)]
    [InlineData(-1, 360, -1, 0)]
    [InlineData(-1, -90, 0, 1)]
    [InlineData(-1, -180, 1, 0)]
    [InlineData(-1, -270, 0, -1)]
    [InlineData(-1, -360, -1, 0)]
    [InlineData(4.2f, 0, 4.2f, 0)]
    [InlineData(4.2f, 45, 4.2f * Sqrt2Div2, 4.2f * Sqrt2Div2)]
    [InlineData(4.2f, 90, 0, 4.2f)]
    [InlineData(4.2f, 135, -4.2f * Sqrt2Div2, 4.2f * Sqrt2Div2)]
    [InlineData(4.2f, 180, -4.2f, 0)]
    [InlineData(4.2f, 270, 0, -4.2f)]
    [InlineData(4.2f, 360, 4.2f, 0)]
    [InlineData(4.2f, -90, 0, -4.2f)]
    [InlineData(4.2f, -180, -4.2f, 0)]
    [InlineData(4.2f, -270, 0, 4.2f)]
    [InlineData(4.2f, -360, 4.2f, 0)]
    public void LengthAndAngleConstructor(float radius, float degrees, float expectedX, float expectedY)
    {
        var expected = new Vector2f(expectedX, expectedY);
        var actual = new Vector2f(radius, Angle.FromDegrees(degrees));
        AssertVectorEqual(expected, actual, Eps);
    }

    [Fact]
    public void OperatorNeg()
    {
        var vec = new Vector2f(1, 2);
        var negatedVec = -vec;

        Assert.Equal(-1, negatedVec.X);
        Assert.Equal(-2, negatedVec.Y);
    }

    private static readonly Vector2f _lhs = new Vector2f(2, 5);
    private static readonly Vector2f _rhs = new Vector2f(8, 3);

    [Fact]
    public void OperatorAddAssign()
    {
        var vec = _lhs;
        vec += _rhs;

        Assert.Equal(10, vec.X);
        Assert.Equal(8, vec.Y);
    }

    [Fact]
    public void OperatorSubAssign()
    {
        var vec = _lhs;
        vec -= _rhs;

        Assert.Equal(-6, vec.X);
        Assert.Equal(2, vec.Y);
    }

    [Fact]
    public void OperatorAdd()
    {
        var vec = _lhs + _rhs;

        Assert.Equal(10, vec.X);
        Assert.Equal(8, vec.Y);
    }

    [Fact]
    public void OperatorSub()
    {
        var vec = _lhs - _rhs;

        Assert.Equal(-6, vec.X);
        Assert.Equal(2, vec.Y);
    }

    private static readonly Vector2f _vecMul = new Vector2f(26, 12);
    private static readonly float _floatMul = 2;

    [Fact]
    public void OperatorMul()
    {
        var vecFloat = _vecMul * _floatMul;

        Assert.Equal(52, vecFloat.X);
        Assert.Equal(24, vecFloat.Y);

        var floatVec = _floatMul * _vecMul;

        Assert.Equal(52, floatVec.X);
        Assert.Equal(24, floatVec.Y);
    }

    [Fact]
    public void OperatorMulAssign()
    {
        var vecFloat = _vecMul;
        vecFloat *= _floatMul;

        Assert.Equal(52, vecFloat.X);
        Assert.Equal(24, vecFloat.Y);
    }

    [Fact]
    public void OperatorDiv()
    {
        var vec = _vecMul / _floatMul;

        Assert.Equal(13, vec.X);
        Assert.Equal(6, vec.Y);
    }

    [Fact]
    public void OperatorDivAssign()
    {
        var vec = _vecMul;
        vec /= _floatMul;

        Assert.Equal(13, vec.X);
        Assert.Equal(6, vec.Y);
    }

    private static readonly Vector2f _equalFirst = new Vector2f(1, 5);
    private static readonly Vector2f _equalSecond = new Vector2f(1, 5);
    private static readonly Vector2f _different = new Vector2f(6, 9);

    [Fact]
    public void OperatorEq()
    {
        Assert.True(_equalFirst == _equalSecond);
        Assert.False(_equalFirst == _different);
    }

    [Fact]
    public void OperatorNotEq()
    {
        Assert.True(_equalFirst != _different);
        Assert.False(_equalFirst != _equalSecond);
    }

    // TODO Structured bindigns (tuples?)

    [Theory]
    [InlineData(2.4f, 3.0f, 3.84187f, 14.7599650969f, 0.624695f, 0.780869f)]
    [InlineData(-0.7f, -2.2f, 2.30868f, 5.3300033f, -0.303204f, -0.952926f)]
    public void LengthAndNormalization(float x, float y, float len, float lenSq, float normX, float normY)
    {
        var vec = new Vector2f(x, y);

        Assert.Equal(len, vec.Length, Eps);
        Assert.Equal(lenSq, vec.LengthSquared, EpsRelaxed);

        var normalized = vec.Normalized();
        var vec2 = new Vector2f(normX, normY);

        AssertVectorEqual(normalized, vec2, Eps);
    }

    [Fact]
    public void RotationsAndAngles()
    {
        var v = new Vector2f(2.4f, 3.0f);

        Assert.Equal(51.3402f, v.Angle().Degrees, Eps);
        Assert.Equal(51.3402f, Vector2f.UnitX.AngleTo(v).Degrees, Eps);
        Assert.Equal(-38.6598f, Vector2f.UnitY.AngleTo(v).Degrees, Eps);

        var w = new Vector2f(-0.7f, -2.2f);

        Assert.Equal(-107.65f, w.Angle().Degrees, EpsRelaxed);
        Assert.Equal(-107.65f, Vector2f.UnitX.AngleTo(w).Degrees, EpsRelaxed);
        Assert.Equal(162.35f, Vector2f.UnitY.AngleTo(w).Degrees, EpsRelaxed);

        Assert.Equal(-158.9902f, v.AngleTo(w).Degrees, EpsRelaxed);
        Assert.Equal(158.9902f, w.AngleTo(v).Degrees, EpsRelaxed);

        var ratio = w.Length / v.Length;

        AssertVectorEqual(v.RotatedBy(Angle.FromDegrees(-158.9902f)) * ratio, w, Eps);
        AssertVectorEqual(w.RotatedBy(Angle.FromDegrees(158.9902f)) / ratio, v, Eps);

        AssertVectorEqual(v.Perpendicular(), new Vector2f(-3.0f, 2.4f), Eps);
        AssertVectorEqual(v.Perpendicular().Perpendicular().Perpendicular().Perpendicular(), v, Eps);

        AssertVectorEqual(v.RotatedBy(Angle.FromDegrees(90)), new Vector2f(-3.0f, 2.4f), Eps);
        AssertVectorEqual(v.RotatedBy(Angle.FromDegrees(27.14f)), new Vector2f(0.767248f, 3.76448f), Eps);
        AssertVectorEqual(v.RotatedBy(Angle.FromDegrees(-36.11f)), new Vector2f(3.70694f, 1.00925f), Eps);
    }

    [Fact]
    public void ProductsAndQuotinents()
    {
        var v = new Vector2f(2.4f, 3.0f);
        var w = new Vector2f(-0.7f, -2.2f);

        Assert.Equal(-8.28f, v.Dot(w), Eps);
        Assert.Equal(-8.28f, w.Dot(v), Eps);

        Assert.Equal(-3.18f, v.Cross(w), Eps);
        Assert.Equal(3.18f, w.Cross(v), Eps);

        AssertVectorEqual(v.ComponentWiseMul(w), new Vector2f(-1.68f, -6.6f), Eps);
        AssertVectorEqual(w.ComponentWiseMul(v), new Vector2f(-1.68f, -6.6f), Eps);
        AssertVectorEqual(v.ComponentWiseDiv(w), new Vector2f(-3.428571f, -1.363636f), Eps);
        AssertVectorEqual(w.ComponentWiseDiv(v), new Vector2f(-0.291666f, -0.733333f), Eps);
    }

    [Fact]
    public void Projection()
    {
        var v = new Vector2f(2.4f, 3.0f);
        var w = new Vector2f(-0.7f, -2.2f);

        AssertVectorEqual(v.ProjectedOnto(w), new Vector2f(1.087430f, 3.417636f), Eps);
        AssertVectorEqual(v.ProjectedOnto(w), -1.55347f * w, Eps);

        AssertVectorEqual(w.ProjectedOnto(v), new Vector2f(-1.346342f, -1.682927f), Eps);
        AssertVectorEqual(w.ProjectedOnto(v), -0.560976f * v, Eps);

        AssertVectorEqual(v.ProjectedOnto(Vector2f.UnitX), new Vector2f(2.4f, 0.0f), Eps);
        AssertVectorEqual(v.ProjectedOnto(Vector2f.UnitY), new Vector2f(0.0f, 3.0f), Eps);
    }
}
