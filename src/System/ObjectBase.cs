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
        public ObjectBase(IntPtr cPointer)
        {
            myCPointer = cPointer;
        }

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
        /// </summary>
        ////////////////////////////////////////////////////////////
        public IntPtr CPointer
        {
            get { return myCPointer; }
            protected set { myCPointer = value; }
        }

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
            if (myCPointer != IntPtr.Zero)
            {
                Destroy(disposing);
                myCPointer = IntPtr.Zero;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Destroy the object (implementation is left to each derived class)
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        ////////////////////////////////////////////////////////////
        protected abstract void Destroy(bool disposing);

        private IntPtr myCPointer = IntPtr.Zero;
    }
}
