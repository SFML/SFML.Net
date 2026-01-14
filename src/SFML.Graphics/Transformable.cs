using System;
using SFML.System;

namespace SFML.Graphics;

////////////////////////////////////////////////////////////
/// <summary>
/// Decomposed transform defined by a position, a rotation and a scale
/// </summary>
/// <remarks>
/// A note on coordinates and undistorted rendering:
/// By default, SFML (or more exactly, OpenGL) may interpolate drawable objects
/// such as sprites or texts when rendering. While this allows transitions
/// like slow movements or rotations to appear smoothly, it can lead to
/// unwanted results in some cases, for example blurred or distorted objects.
/// In order to render a SFML.Graphics.Drawable object pixel-perfectly, make sure
/// the involved coordinates allow a 1:1 mapping of pixels in the window
/// to texels (pixels in the texture). More specifically, this means:
/// * The object's position, origin and scale have no fractional part
/// * The object's and the view's rotation are a multiple of 90 degrees
/// * The view's center and size have no fractional part
/// </remarks>
////////////////////////////////////////////////////////////
public class Transformable : ObjectBase
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Default constructor
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Transformable() :
        base(IntPtr.Zero)
    {
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Construct the transformable from another transformable
    /// </summary>
    /// <param name="transformable">Transformable to copy</param>
    ////////////////////////////////////////////////////////////
    public Transformable(Transformable transformable) :
        base(IntPtr.Zero)
    {
        Origin = transformable.Origin;
        Position = transformable.Position;
        Rotation = transformable.Rotation;
        Scale = transformable.Scale;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Position of the object
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Vector2f Position
    {
        get => _position;
        set
        {
            _position = value;
            _transformNeedUpdate = true;
            _inverseNeedUpdate = true;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Rotation of the object
    /// </summary>
    ////////////////////////////////////////////////////////////
    public float Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;
            _transformNeedUpdate = true;
            _inverseNeedUpdate = true;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Scale of the object
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Vector2f Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            _transformNeedUpdate = true;
            _inverseNeedUpdate = true;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The origin of an object defines the center point for
    /// all transformations (position, scale, rotation).
    /// The coordinates of this point must be relative to the
    /// top-left corner of the object, and ignore all
    /// transformations (position, scale, rotation).
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Vector2f Origin
    {
        get => _origin;
        set
        {
            _origin = value;
            _transformNeedUpdate = true;
            _inverseNeedUpdate = true;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The combined transform of the object
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Transform Transform
    {
        get
        {
            if (_transformNeedUpdate)
            {
                _transformNeedUpdate = false;

                var angle = -_rotation * 3.141592654F / 180.0F;
                var cosine = (float)Math.Cos(angle);
                var sine = (float)Math.Sin(angle);
                var sxc = _scale.X * cosine;
                var syc = _scale.Y * cosine;
                var sxs = _scale.X * sine;
                var sys = _scale.Y * sine;
                var tx = (-_origin.X * sxc) - (_origin.Y * sys) + _position.X;
                var ty = (_origin.X * sxs) - (_origin.Y * syc) + _position.Y;

                _transform = new Transform(sxc, sys, tx,
                                            -sxs, syc, ty,
                                            0.0F, 0.0F, 1.0F);
            }
            return _transform;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The combined transform of the object
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Transform InverseTransform
    {
        get
        {
            if (_inverseNeedUpdate)
            {
                _inverseTransform = Transform.GetInverse();
                _inverseNeedUpdate = false;
            }
            return _inverseTransform;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Construct the object from its internal C pointer
    /// </summary>
    /// <param name="cPointer">Pointer to the object in the C library</param>
    ////////////////////////////////////////////////////////////
    protected Transformable(IntPtr cPointer) :
        base(cPointer)
    {
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Handle the destruction of the object
    /// </summary>
    /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
    ////////////////////////////////////////////////////////////
    protected override void Destroy(bool disposing)
    {
        // Does nothing, this instance is either pure C# (if created by the user)
        // or not the final object (if used as a base for a drawable class)
    }

    private Vector2f _origin = new(0, 0);
    private Vector2f _position = new(0, 0);
    private float _rotation;
    private Vector2f _scale = new(1, 1);
    private Transform _transform;
    private Transform _inverseTransform;
    private bool _transformNeedUpdate = true;
    private bool _inverseNeedUpdate = true;
}
