namespace SFML.System.Test;

public class Vector2iTests
{
    private const float Eps = 1e-5f;
    private const float EpsRelaxed = 15e-5f;
    private const float Sqrt2Div2 = 0.7071067811865475f;

    [Fact]
    public void DefaultConstructor()
    {
        var vec = new Vector2i();
        Assert.Equal(0, vec.X);
        Assert.Equal(0, vec.Y);
    }

    [Fact]
    public void CoordinateConstructor()
    {
        var vec = new Vector2i(1, 2);
        Assert.Equal(1, vec.X);
        Assert.Equal(2, vec.Y);
    }

    [Fact]
    public void OperatorNeg()
    {
        var vec = new Vector2i(1, 2);
        var negatedVec = -vec;

        Assert.Equal(-1, negatedVec.X);
        Assert.Equal(-2, negatedVec.Y);
    }

    private static readonly Vector2i _lhs = new Vector2i(2, 5);
    private static readonly Vector2i _rhs = new Vector2i(8, 3);

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

    private static readonly Vector2i _vecMul = new Vector2i(26, 12);
    private static readonly int _intMul = 2;

    [Fact]
    public void OperatorMul()
    {
        var vecInt = _vecMul * _intMul;

        Assert.Equal(52, vecInt.X);
        Assert.Equal(24, vecInt.Y);

        var floatVec = _intMul * _vecMul;

        Assert.Equal(52, floatVec.X);
        Assert.Equal(24, floatVec.Y);
    }

    [Fact]
    public void OperatorMulAssign()
    {
        var vecInt = _vecMul;
        vecInt *= _intMul;

        Assert.Equal(52, vecInt.X);
        Assert.Equal(24, vecInt.Y);
    }

    [Fact]
    public void OperatorDiv()
    {
        var vec = _vecMul / _intMul;

        Assert.Equal(13, vec.X);
        Assert.Equal(6, vec.Y);
    }

    [Fact]
    public void OperatorDivAssign()
    {
        var vec = _vecMul;
        vec /= _intMul;

        Assert.Equal(13, vec.X);
        Assert.Equal(6, vec.Y);
    }

    private static readonly Vector2i _equalFirst = new Vector2i(1, 5);
    private static readonly Vector2i _equalSecond = new Vector2i(1, 5);
    private static readonly Vector2i _different = new Vector2i(6, 9);

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
}
