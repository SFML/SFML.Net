namespace SFML.System.Test;

public class Vector2uTests
{
    private const float Eps = 1e-5f;
    private const float EpsRelaxed = 15e-5f;
    private const float Sqrt2Div2 = 0.7071067811865475f;

    [Fact]
    public void DefaultConstructor()
    {
        var vec = new Vector2u();
        Assert.Equal(0u, vec.X);
        Assert.Equal(0u, vec.Y);
    }

    [Fact]
    public void CoordinateConstructor()
    {
        var vec = new Vector2u(1, 2);
        Assert.Equal(1u, vec.X);
        Assert.Equal(2u, vec.Y);
    }

    private static readonly Vector2u _lhs = new Vector2u(2, 5);
    private static readonly Vector2u _rhs = new Vector2u(8, 3);

    [Fact]
    public void OperatorAddAssign()
    {
        var vec = _lhs;
        vec += _rhs;

        Assert.Equal(10u, vec.X);
        Assert.Equal(8u, vec.Y);
    }

    [Fact]
    public void OperatorSubAssign()
    {
        var vec = new Vector2u(10, 5);
        vec -= new Vector2u(8, 2);

        Assert.Equal(2u, vec.X);
        Assert.Equal(3u, vec.Y);
    }

    [Fact]
    public void OperatorAdd()
    {
        var vec = _lhs + _rhs;

        Assert.Equal(10u, vec.X);
        Assert.Equal(8u, vec.Y);
    }

    [Fact]
    public void OperatorSub()
    {
        var vec = new Vector2u(10, 5) - new Vector2u(8, 2);

        Assert.Equal(2u, vec.X);
        Assert.Equal(3u, vec.Y);
    }

    private static readonly Vector2u _vecMul = new Vector2u(26, 12);
    private static readonly uint _uintMul = 2;

    [Fact]
    public void OperatorMul()
    {
        var vecUint = _vecMul * _uintMul;

        Assert.Equal(52u, vecUint.X);
        Assert.Equal(24u, vecUint.Y);

        var floatVec = _uintMul * _vecMul;

        Assert.Equal(52u, floatVec.X);
        Assert.Equal(24u, floatVec.Y);
    }

    [Fact]
    public void OperatorMulAssign()
    {
        var vecUint = _vecMul;
        vecUint *= _uintMul;

        Assert.Equal(52u, vecUint.X);
        Assert.Equal(24u, vecUint.Y);
    }

    [Fact]
    public void OperatorDiv()
    {
        var vec = _vecMul / _uintMul;

        Assert.Equal(13u, vec.X);
        Assert.Equal(6u, vec.Y);
    }

    [Fact]
    public void OperatorDivAssign()
    {
        var vec = _vecMul;
        vec /= _uintMul;

        Assert.Equal(13u, vec.X);
        Assert.Equal(6u, vec.Y);
    }

    private static readonly Vector2u _equalFirst = new Vector2u(1, 5);
    private static readonly Vector2u _equalSecond = new Vector2u(1, 5);
    private static readonly Vector2u _different = new Vector2u(6, 9);

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
