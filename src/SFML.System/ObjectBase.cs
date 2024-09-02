using System;

namespace SFML
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The ObjectBase class is an abstract base for every
    /// SFML object. It's meant for internal use only
    /// </summary>
    ////////////////////////////////////////////////////////////
    public abstract class ObjectBase : IDisposable
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the object from a pointer to the C library object
        /// </summary>
        /// <param name="cPointer">Internal pointer to the object in the C libraries</param>
        ////////////////////////////////////////////////////////////
        public ObjectBase(IntPtr cPointer) => _cPointer = cPointer;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Dispose the object
        /// </summary>
        ////////////////////////////////////////////////////////////
        ~ObjectBase()
        {
            Dispose(false);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Access to the internal pointer of the object.
        /// For internal use only
        /// <para/>
        /// Attempting to get the value while <see cref="IsInvalid"/> is true will throw an <see cref="ObjectDisposedException"/>.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public IntPtr CPointer
        {
            get
            {
                if (_cPointer == IntPtr.Zero)
                {
                    throw new ObjectDisposedException($"This {GetType().Name} instance has been disposed and should not be used.");
                }

                return _cPointer;
            }
            protected set => _cPointer = value;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns true if the object is in an invalid state, false otherwise.
        /// For internal use only
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool IsInvalid => _cPointer == IntPtr.Zero;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicitly dispose the object
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        ////////////////////////////////////////////////////////////
        private void Dispose(bool disposing)
        {
            if (_cPointer != IntPtr.Zero)
            {
                Destroy(disposing);
                _cPointer = IntPtr.Zero;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Destroy the object (implementation is left to each derived class)
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        ////////////////////////////////////////////////////////////
        protected abstract void Destroy(bool disposing);

        /// <summary>
        /// Create a string that can be used in <see cref="object.ToString"/> overrides to describe disposed objects
        /// </summary>
        /// <returns>A string representation of the disposed object</returns>
        protected string MakeDisposedObjectString() => $"[{GetType().Name} (disposed)]";

        private IntPtr _cPointer = IntPtr.Zero;
    }
}
