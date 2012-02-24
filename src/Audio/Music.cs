using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using SFML.Window;

namespace SFML
{
    namespace Audio
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Music defines a big sound played using streaming,
        /// so usually what we call a music :)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Music : ObjectBase
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the music from a file
            /// </summary>
            /// <param name="filename">Path of the music file to load</param>
            ////////////////////////////////////////////////////////////
            public Music(string filename) :
                base(sfMusic_CreateFromFile(filename))
            {
                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("music", filename);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the music from a custom stream
            /// </summary>
            /// <param name="stream">Source stream to read from</param>
            ////////////////////////////////////////////////////////////
            public Music(Stream stream) :
                base(IntPtr.Zero)
            {
                myStream = new StreamAdaptor(stream);
                SetThis(sfMusic_CreateFromStream(myStream.InputStreamPtr));

                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("music");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Play the music
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Play()
            {
                sfMusic_Play(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Pause the music
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Pause()
            {
                sfMusic_Pause(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Stop the music
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Stop()
            {
                sfMusic_Stop(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Samples rate, in samples per second
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint SampleRate
            {
                get {return sfMusic_GetSampleRate(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Number of channels (1 = mono, 2 = stereo)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint ChannelCount
            {
                get {return sfMusic_GetChannelCount(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Current status of the music (see SoundStatus enum)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public SoundStatus Status
            {
                get {return sfMusic_GetStatus(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Total duration of the music
            /// </summary>
            ////////////////////////////////////////////////////////////
            public TimeSpan Duration
            {
                get
                {
                    long microseconds = sfMusic_GetDuration(CPointer);
                    return TimeSpan.FromTicks(microseconds * TimeSpan.TicksPerMillisecond / 1000);
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Loop state of the sound. Default value is false
            /// </summary>
            ////////////////////////////////////////////////////////////
            public bool Loop
            {
                get {return sfMusic_GetLoop(CPointer);}
                set {sfMusic_SetLoop(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Pitch of the music. Default value is 1
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float Pitch
            {
                get {return sfMusic_GetPitch(CPointer);}
                set {sfMusic_SetPitch(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Volume of the music, in range [0, 100]. Default value is 100
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float Volume
            {
                get {return sfMusic_GetVolume(CPointer);}
                set {sfMusic_SetVolume(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// 3D position of the music. Default value is (0, 0, 0)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Vector3f Position
            {
                get {Vector3f v; sfMusic_GetPosition(CPointer, out v.X, out v.Y, out v.Z); return v;}
                set {sfMusic_SetPosition(CPointer, value.X, value.Y, value.Z);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Is the music's position relative to the listener's position,
            /// or is it absolute?
            /// Default value is false (absolute)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public bool RelativeToListener
            {
                get {return sfMusic_IsRelativeToListener(CPointer);}
                set {sfMusic_SetRelativeToListener(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Minimum distance of the music. Closer than this distance,
            /// the listener will hear the sound at its maximum volume.
            /// The default value is 1
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float MinDistance
            {
                get {return sfMusic_GetMinDistance(CPointer);}
                set {sfMusic_SetMinDistance(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Attenuation factor. The higher the attenuation, the
            /// more the sound will be attenuated with distance from listener.
            /// The default value is 1
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float Attenuation
            {
                get {return sfMusic_GetAttenuation(CPointer);}
                set {sfMusic_SetAttenuation(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Current playing position
            /// </summary>
            ////////////////////////////////////////////////////////////
            public TimeSpan PlayingOffset
            {
                get
                {
                    long microseconds = sfMusic_GetPlayingOffset(CPointer);
                    return TimeSpan.FromTicks(microseconds * TimeSpan.TicksPerMillisecond / 1000);
                }
                set
                {
                    long microseconds = value.Ticks / (TimeSpan.TicksPerMillisecond / 1000);
                    sfMusic_SetPlayingOffset(CPointer, microseconds);
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Music]" +
                       " SampleRate(" + SampleRate + ")" +
                       " ChannelCount(" + ChannelCount + ")" +
                       " Status(" + Status + ")" +
                       " Duration(" + Duration + ")" +
                       " Loop(" + Loop + ")" +
                       " Pitch(" + Pitch + ")" +
                       " Volume(" + Volume + ")" +
                       " Position(" + Position + ")" +
                       " RelativeToListener(" + RelativeToListener + ")" +
                       " MinDistance(" + MinDistance + ")" +
                       " Attenuation(" + Attenuation + ")" +
                       " PlayingOffset(" + PlayingOffset + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                if (disposing)
                {
                    if (myStream != null)
                        myStream.Dispose();
                }

                sfMusic_Destroy(CPointer);
            }

            private StreamAdaptor myStream = null;

            #region Imports
            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfMusic_CreateFromFile(string Filename);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern IntPtr sfMusic_CreateFromStream(IntPtr stream);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_Destroy(IntPtr MusicStream);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_Play(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_Pause(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_Stop(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern SoundStatus sfMusic_GetStatus(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern long sfMusic_GetDuration(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfMusic_GetChannelCount(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfMusic_GetSampleRate(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetPitch(IntPtr Music, float Pitch);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetLoop(IntPtr Music, bool Loop);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetVolume(IntPtr Music, float Volume);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetPosition(IntPtr Music, float X, float Y, float Z);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetRelativeToListener(IntPtr Music, bool Relative);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetMinDistance(IntPtr Music, float MinDistance);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetAttenuation(IntPtr Music, float Attenuation);
            
            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_SetPlayingOffset(IntPtr Music, long TimeOffset);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfMusic_GetLoop(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfMusic_GetPitch(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfMusic_GetVolume(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMusic_GetPosition(IntPtr Music, out float X, out float Y, out float Z);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfMusic_IsRelativeToListener(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfMusic_GetMinDistance(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfMusic_GetAttenuation(IntPtr Music);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern long sfMusic_GetPlayingOffset(IntPtr Music);

            #endregion
        }
    }
}
