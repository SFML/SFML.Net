using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Streamed music played from an audio file
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Music : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructs a music from a file
        ///
        /// This constructor doesn't start playing the music (call
        /// <see cref="Play"/> to do so).
        /// Here is a complete list of all the supported audio formats:
        /// ogg, wav, flac, mp3, aiff, au, raw, paf, svx, nist, voc, ircam,
        /// w64, mat4, mat5 pvf, htk, sds, avr, sd2, caf, wve, mpc2k, rf64.
        /// </summary>
        /// <param name="filename">Path of the music file to open</param>
        ////////////////////////////////////////////////////////////
        public Music(string filename) :
            base(sfMusic_createFromFile(filename))
        {
            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("music", filename);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructs a music from a custom stream
        ///
        /// This constructor doesn't start playing the music (call
        /// <see cref="Play"/> to do so).
        /// Here is a complete list of all the supported audio formats:
        /// ogg, wav, flac, mp3, aiff, au, raw, paf, svx, nist, voc, ircam,
        /// w64, mat4, mat5 pvf, htk, sds, avr, sd2, caf, wve, mpc2k, rf64.
        /// </summary>
        /// <param name="stream">Source stream to read from</param>
        ////////////////////////////////////////////////////////////
        public Music(Stream stream) :
            base(IntPtr.Zero)
        {
            myStream = new StreamAdaptor(stream);
            CPointer = sfMusic_createFromStream(myStream.InputStreamPtr);

            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("music");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructs a music from an audio file in memory
        ///
        /// This constructor doesn't start playing the music (call
        /// <see cref="Play"/> to do so).
        /// Here is a complete list of all the supported audio formats:
        /// ogg, wav, flac, mp3, aiff, au, raw, paf, svx, nist, voc, ircam,
        /// w64, mat4, mat5 pvf, htk, sds, avr, sd2, caf, wve, mpc2k, rf64.
        /// </summary>
        /// <param name="bytes">Byte array containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Music(Span<byte> bytes) :
            base(IntPtr.Zero)
        {
            unsafe
            {
                fixed (byte* ptr = bytes)
                {
                    CPointer = sfMusic_createFromMemory((IntPtr)ptr, (UIntPtr)bytes.Length);
                }
            }

            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("music");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Start or resume playing the audio stream.
        ///
        /// This function starts the stream if it was stopped, resumes
        /// it if it was paused, and restarts it from beginning if it
        /// was it already playing.
        /// This function uses its own thread so that it doesn't block
        /// the rest of the program while the stream is played.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Play()
        {
            sfMusic_play(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pause the audio stream.
        ///
        /// This function pauses the stream if it was playing,
        /// otherwise (stream already paused or stopped) it has no effect.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Pause()
        {
            sfMusic_pause(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Stop playing the audio stream.
        ///
        /// This function stops the stream if it was playing or paused,
        /// and does nothing if it was already stopped.
        /// It also resets the playing position (unlike Pause()).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Stop()
        {
            sfMusic_stop(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Sample rate of the music.
        ///
        /// The sample rate is the number of audio samples played per
        /// second. The higher, the better the quality.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint SampleRate
        {
            get { return sfMusic_getSampleRate(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Number of channels (1 = mono, 2 = stereo)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint ChannelCount
        {
            get { return sfMusic_getChannelCount(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current status of the music (see SoundStatus enum)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundStatus Status
        {
            get { return sfMusic_getStatus(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Total duration of the music
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time Duration
        {
            get
            {
                return sfMusic_getDuration(CPointer);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Flag if the music should loop after reaching the end.
        ///
        /// If set, the music will restart from beginning after
        /// reaching the end and so on, until it is stopped or
        /// Loop = false is set.
        /// The default looping state for music is false.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool Loop
        {
            get { return sfMusic_getLoop(CPointer); }
            set { sfMusic_setLoop(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pitch of the music.
        /// 
        /// The pitch represents the perceived fundamental frequency
        /// of a sound; thus you can make a sound more acute or grave
        /// by changing its pitch. A side effect of changing the pitch
        /// is to modify the playing speed of the sound as well.
        /// The default value for the pitch is 1.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Pitch
        {
            get { return sfMusic_getPitch(CPointer); }
            set { sfMusic_setPitch(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Volume of the music.
        /// 
        /// The volume is a value between 0 (mute) and 100 (full volume).
        /// The default value for the volume is 100.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Volume
        {
            get { return sfMusic_getVolume(CPointer); }
            set { sfMusic_setVolume(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D position of the music in the audio scene.
        ///
        /// Only sounds with one channel (mono sounds) can be
        /// spatialized.
        /// The default position of a sound is (0, 0, 0).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Position
        {
            get { return sfMusic_getPosition(CPointer); }
            set { sfMusic_setPosition(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Make the music's position relative to the listener or absolute.
        ///
        /// Making a sound relative to the listener will ensure that it will always
        /// be played the same way regardless the position of the listener.
        /// This can be useful for non-spatialized sounds, sounds that are
        /// produced by the listener, or sounds attached to it.
        /// The default value is false (position is absolute).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool RelativeToListener
        {
            get { return sfMusic_isRelativeToListener(CPointer); }
            set { sfMusic_setRelativeToListener(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Minimum distance of the music.
        ///
        /// The "minimum distance" of a sound is the maximum
        /// distance at which it is heard at its maximum volume. Further
        /// than the minimum distance, it will start to fade out according
        /// to its attenuation factor. A value of 0 ("inside the head
        /// of the listener") is an invalid value and is forbidden.
        /// The default value of the minimum distance is 1.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float MinDistance
        {
            get { return sfMusic_getMinDistance(CPointer); }
            set { sfMusic_setMinDistance(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Attenuation factor of the music.
        ///
        /// The attenuation is a multiplicative factor which makes
        /// the music more or less loud according to its distance
        /// from the listener. An attenuation of 0 will produce a
        /// non-attenuated sound, i.e. its volume will always be the same
        /// whether it is heard from near or from far. On the other hand,
        /// an attenuation value such as 100 will make the sound fade out
        /// very quickly as it gets further from the listener.
        /// The default value of the attenuation is 1.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Attenuation
        {
            get { return sfMusic_getAttenuation(CPointer); }
            set { sfMusic_setAttenuation(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current playing position of the music.
        /// 
        /// The playing position can be changed when the music is
        /// either paused or playing.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time PlayingOffset
        {
            get { return sfMusic_getPlayingOffset(CPointer); }
            set { sfMusic_setPlayingOffset(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current loop points of the music.
        /// 
        /// Since setting performs some adjustments on the
        /// provided values and rounds them to internal samples, getting this
        /// value later is not guaranteed to return the same times passed
        /// into it. However, it is guaranteed to return times that will map 
        /// to the valid internal samples of this Music if they are later
        /// set again.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public TimeSpan LoopPoints
        {
            get { return sfMusic_getLoopPoints(CPointer); }
            set { sfMusic_setLoopPoints(CPointer, value); }
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
                   " PlayingOffset(" + PlayingOffset + ")" +
                   " LoopPoints(" + LoopPoints + ")";
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
                {
                    myStream.Dispose();
                }
            }

            sfMusic_destroy(CPointer);
        }

        private StreamAdaptor myStream = null;

        /// <summary>
        /// Structure defining a Time range. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TimeSpan
        {
            /// <summary>
            /// Constructs TimeSpan from offset and a length
            /// </summary>
            /// <param name="offset">beginning offset of the time range</param>
            /// <param name="length">length of the time range</param>
            public TimeSpan(Time offset, Time length)
            {
                Offset = offset;
                Length = length;
            }

            /// <summary>
            /// The beginning of the time range
            /// </summary>
            public Time Offset;

            /// <summary>
            /// The length of the time range
            /// </summary>
            public Time Length;
        }

        #region Imports
        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfMusic_createFromFile(string Filename);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private unsafe static extern IntPtr sfMusic_createFromStream(IntPtr stream);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfMusic_createFromMemory(IntPtr data, UIntPtr size);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_destroy(IntPtr MusicStream);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_play(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_pause(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_stop(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern SoundStatus sfMusic_getStatus(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfMusic_getDuration(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern TimeSpan sfMusic_getLoopPoints(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setLoopPoints(IntPtr Music, TimeSpan timePoints);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfMusic_getChannelCount(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfMusic_getSampleRate(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setPitch(IntPtr Music, float Pitch);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setLoop(IntPtr Music, bool Loop);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setVolume(IntPtr Music, float Volume);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setPosition(IntPtr Music, Vector3f position);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setRelativeToListener(IntPtr Music, bool Relative);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setMinDistance(IntPtr Music, float MinDistance);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setAttenuation(IntPtr Music, float Attenuation);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMusic_setPlayingOffset(IntPtr Music, Time TimeOffset);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfMusic_getLoop(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfMusic_getPitch(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfMusic_getVolume(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfMusic_getPosition(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfMusic_isRelativeToListener(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfMusic_getMinDistance(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfMusic_getAttenuation(IntPtr Music);

        [DllImport(CSFML.audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfMusic_getPlayingOffset(IntPtr Music);
        #endregion
    }
}
