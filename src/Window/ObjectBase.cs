using System;
using System.Runtime.InteropServices;

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
        private static System.Collections.Generic.List<ObjectBase> garbageCollectedObjects = new System.Collections.Generic.List<ObjectBase>();
        private ObjectBaseSafeHandle objectSafeHandle;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the object from a pointer to the C library object
        /// </summary>
        /// <param name="cPointer">Internal pointer to the object in the C libraries</param>
        ////////////////////////////////////////////////////////////
        public ObjectBase(IntPtr cPointer)
        {
            objectSafeHandle = new ObjectBaseSafeHandle(cPointer, this);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Access to the internal pointer of the object.
        /// For internal use only
        /// </summary>
        ////////////////////////////////////////////////////////////
        public IntPtr CPointer
        {
            get { return objectSafeHandle.Ptr; }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicitely dispose the object
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
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        private void Dispose(bool disposing)
        {
            if (!objectSafeHandle.IsInvalid)
            {
                Destroy(disposing);
                objectSafeHandle.Ptr = IntPtr.Zero;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Destroy the object (implementation is left to each derived class)
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected abstract void Destroy(bool disposing);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the pointer to the internal object. For internal use only
        /// </summary>
        /// <param name="cPointer">Pointer to the internal object in C library</param>
        ////////////////////////////////////////////////////////////
        protected void SetThis(IntPtr cPointer)
        {
            objectSafeHandle = new ObjectBaseSafeHandle(cPointer, this);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Dispose garbage collected Objects on current thread
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static void DisposeGarbageCollectedObjects()
        {
            if (garbageCollectedObjects.Count > 0)
            {
                ObjectBase[] garbageCollectedObjectsCopy;
                lock (garbageCollectedObjects)
                {
                    garbageCollectedObjectsCopy = new ObjectBase[garbageCollectedObjects.Count];
                    garbageCollectedObjects.CopyTo(garbageCollectedObjectsCopy);
                    garbageCollectedObjects.Clear();
                }
                foreach (var garbageCollectedObject in garbageCollectedObjectsCopy) 
                {
                    try 
                    {
                        garbageCollectedObject.Dispose ();
                    } catch (Exception e) 
                    {
                        Console.WriteLine (e.Message + " at " + e.StackTrace);
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Safe handle class to ensure all objects are eventually disposed
        /// </summary>
        ////////////////////////////////////////////////////////////
        private sealed class ObjectBaseSafeHandle : System.Runtime.InteropServices.SafeHandle
        {
            private ObjectBase objectBase;
            public IntPtr Ptr;
            public ObjectBaseSafeHandle(IntPtr ptr, ObjectBase objectBase)
                : base(IntPtr.Zero, true)
            {
                this.Ptr = ptr;
                this.objectBase = objectBase;
            }

            public override bool IsInvalid
            {
                get
                {
                    return Ptr == IntPtr.Zero;
                }
            }

            protected override bool ReleaseHandle()
            {
                //GC will wait here until all threads hit a safe point. Thus avoiding a deadlock.
                lock (garbageCollectedObjects)
                    garbageCollectedObjects.Add(objectBase);
                return true;
            }
        }
    }
}
