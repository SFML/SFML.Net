using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Abstract base class for streamed audio sources
    /// </summary>
    ////////////////////////////////////////////////////////////
    public abstract class SoundStream : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundStream() :
            base(IntPtr.Zero)
        {
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
        public void Play() => sfSoundStream_play(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pause the audio stream.
        ///
        /// This function pauses the stream if it was playing,
        /// otherwise (stream already paused or stopped) it has no effect.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Pause() => sfSoundStream_pause(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Stop playing the audio stream.
        ///
        /// This function stops the stream if it was playing or paused,
        /// and does nothing if it was already stopped.
        /// It also resets the playing position (unlike pause()).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Stop() => sfSoundStream_stop(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Sample rate of the stream
        ///
        /// The sample rate is the number of audio samples played per
        /// second. The higher, the better the quality.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint SampleRate => sfSoundStream_getSampleRate(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the map of position in sample frame to sound channel
        /// <para/>
        /// This is used to map a sample in the sample stream to a
        /// position during spatialisation.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundChannel[] ChannelMap
        {
            get
            {
                unsafe
                {
                    var channels = sfSoundStream_getChannelMap(CPointer, out var count);
                    var arr = new SoundChannel[(int)count];

                    for (var i = 0; i < arr.Length; i++)
                    {
                        arr[i] = channels[i];
                    }

                    return arr;
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Number of channels (1 = mono, 2 = stereo)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint ChannelCount => sfSoundStream_getChannelCount(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current status of the sound stream (see SoundStatus enum)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundStatus Status => sfSoundStream_getStatus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Flag if the music should loop after reaching the end.
        ///
        /// If set, the music will restart from beginning after
        /// reaching the end and so on, until it is stopped or
        /// IsLooping = false is set.
        /// The default looping state for music is false.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool IsLooping
        {
            get => sfSoundStream_isLooping(CPointer);
            set => sfSoundStream_setLooping(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pitch of the stream.
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
            get => sfSoundStream_getPitch(CPointer);
            set => sfSoundStream_setPitch(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the pan of the sound
        /// <para/>
        /// Using panning, a mono sound can be panned between
        /// stereo channels. When the pan is set to -1, the sound
        /// is played only on the left channel, when the pan is set
        /// to +1, the sound is played only on the right channel.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Pan
        {
            get => sfSoundStream_getPan(CPointer);
            set => sfSoundStream_setPan(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Volume of the stream.
        /// 
        /// The volume is a value between 0 (mute) and 100 (full volume).
        /// The default value for the volume is 100.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Volume
        {
            get => sfSoundStream_getVolume(CPointer);
            set => sfSoundStream_setVolume(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets or sets whether spatialization of the sound is enabled
        /// <para/>
        /// Spatialization is the application of various effects to
        /// simulate a sound being emitted at a virtual position in
        /// 3D space and exhibiting various physical phenomena such as
        /// directional attenuation and doppler shift.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool IsSpatializationEnabled
        {
            get => sfSoundStream_isSpatializationEnabled(CPointer);
            set => sfSoundStream_setSpatializationEnabled(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D position of the stream in the audio scene.
        ///
        /// Only sounds with one channel (mono sounds) can be
        /// spatialized.
        /// The default position of a sound is (0, 0, 0).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Position
        {
            get => sfSoundStream_getPosition(CPointer);
            set => sfSoundStream_setPosition(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D direction of the sound in the audio scene
        /// <para/>
        /// The direction defines where the sound source is facing
        /// in 3D space. It will affect how the sound is attenuated
        /// if facing away from the listener.
        /// The default direction of a sound is (0, 0, -1).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Direction
        {
            get => sfSoundStream_getPosition(CPointer);
            set => sfSoundStream_setPosition(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the cone properties of the sound in the audio scene
        /// <para/>
        /// The cone defines how directional attenuation is applied.
        /// The default cone of a sound is (2 * PI, 2 * PI, 1).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Cone Cone
        {
            get => new Cone(sfSoundStream_getCone(CPointer));
            set => sfSoundStream_setCone(CPointer, value.Marshal());
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D velocity of the sound in the audio scene
        /// <para/>
        /// The velocity is used to determine how to doppler shift
        /// the sound. Sounds moving towards the listener will be
        /// perceived to have a higher pitch and sounds moving away
        /// from the listener will be perceived to have a lower pitch.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Velocity
        {
            get => sfSoundStream_getVelocity(CPointer);
            set => sfSoundStream_setVelocity(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Doppler factor of the sound
        /// <para/>
        /// The doppler factor determines how strong the doppler
        /// shift will be.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float DopplerFactor
        {
            get => sfSoundStream_getDopplerFactor(CPointer);
            set => sfSoundStream_setDopplerFactor(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Directional attenuation factor of the sound
        /// <para/>
        /// Depending on the virtual position of an output channel
        /// relative to the listener (such as in surround sound
        /// setups), sounds will be attenuated when emitting them
        /// from certain channels. This factor determines how strong
        /// the attenuation based on output channel position
        /// relative to the listener is.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float DirectionalAttenuationFactor
        {
            get => sfSoundStream_getDirectionalAttenuationFactor(CPointer);
            set => sfSoundStream_setDirectionalAttenuationFactor(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Make the stream's position relative to the listener or absolute.
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
            get => sfSoundStream_isRelativeToListener(CPointer);
            set => sfSoundStream_setRelativeToListener(CPointer, value);
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
            get => sfSoundStream_getMinDistance(CPointer);
            set => sfSoundStream_setMinDistance(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Maximum distance of the sound
        /// <para/>
        /// The "maximum distance" of a sound is the minimum
        /// distance at which it is heard at its minimum volume. Closer
        /// than the maximum distance, it will start to fade in according
        /// to its attenuation factor.
        /// The default value of the maximum distance is the maximum
        /// value a float can represent.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float MaxDistance
        {
            get => sfSoundStream_getMaxDistance(CPointer);
            set => sfSoundStream_setMaxDistance(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Minimum gain of the sound
        /// <para/>
        /// When the sound is further away from the listener than
        /// the "maximum distance" the attenuated gain is clamped
        /// so it cannot go below the minimum gain value.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float MinGain
        {
            get => sfSoundStream_getMinGain(CPointer);
            set => sfSoundStream_setMinGain(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Maximum gain of the sound
        /// <para/>
        /// When the sound is closer from the listener than
        /// the "minimum distance" the attenuated gain is clamped
        /// so it cannot go above the maximum gain value.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float MaxGain
        {
            get => sfSoundStream_getMaxGain(CPointer);
            set => sfSoundStream_setMaxGain(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Attenuation factor of the stream.
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
            get => sfSoundStream_getAttenuation(CPointer);
            set => sfSoundStream_setAttenuation(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current playing position of the stream.
        /// 
        /// The playing position can be changed when the music is
        /// either paused or playing.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time PlayingOffset
        {
            get => sfSoundStream_getPlayingOffset(CPointer);
            set => sfSoundStream_setPlayingOffset(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the effect processor to be applied to the sound.
        /// <para/>
        /// The effect processor is a callable that will be called
        /// with sound data to be processed.
        /// </summary>
        /// <param name="effectProcessor">The effect processor to attach to this sound, attach a null processor to disable processing</param>
        ////////////////////////////////////////////////////////////
        public void SetEffectProcessor(EffectProcessor effectProcessor)
        {
            _effectProcessor = (inputFrames, inputFrameCount, outputFrames, outputFrameCount, frameChannelCount) =>
            {
                var inputFramesArray = new float[inputFrameCount];
                var outputFramesArray = new float[outputFrameCount];

                Marshal.Copy(inputFrames, inputFramesArray, 0, inputFramesArray.Length);
                var written = effectProcessor(inputFramesArray, outputFramesArray, frameChannelCount);
                Marshal.Copy(outputFramesArray, 0, outputFrames, outputFramesArray.Length);

                return written;
            };

            sfSoundStream_setEffectProcessor(CPointer, Marshal.GetFunctionPointerForDelegate(_effectProcessor));
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            if (IsInvalid)
            {
                return MakeDisposedObjectString();
            }

            return "[SoundStream]" +
                   " SampleRate(" + SampleRate + ")" +
                   " ChannelCount(" + ChannelCount + ")" +
                   " Status(" + Status + ")" +
                   " IsLooping(" + IsLooping + ")" +
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
        /// Set the audio stream parameters, you must call it before Play()
        /// </summary>
        /// <param name="channelCount">Number of channels</param>
        /// <param name="sampleRate">Sample rate, in samples per second</param>
        /// <param name="channelMapData">Map of position in sample frame to sound channel</param>
        ////////////////////////////////////////////////////////////
        protected void Initialize(uint channelCount, uint sampleRate, SoundChannel[] channelMapData)
        {
            _getDataCallback = new GetDataCallbackType(GetData);
            _seekCallback = new SeekCallbackType(Seek);

            unsafe
            {
                fixed (SoundChannel* data = channelMapData)
                {
                    CPointer = sfSoundStream_create(_getDataCallback, _seekCallback, channelCount, sampleRate, data, (UIntPtr)channelMapData.Length, IntPtr.Zero);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Virtual function called each time new audio data is needed to feed the stream
        /// </summary>
        /// <param name="samples">Array of samples to fill for the stream</param>
        /// <returns>True to continue playback, false to stop</returns>
        ////////////////////////////////////////////////////////////
        protected abstract bool OnGetData(out short[] samples);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Virtual function called to seek into the stream
        /// </summary>
        /// <param name="timeOffset">New position</param>
        ////////////////////////////////////////////////////////////
        protected abstract void OnSeek(Time timeOffset);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfSoundStream_destroy(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Structure mapping the C library arguments passed to the data callback
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        private struct Chunk
        {
            public unsafe short* Samples;
            public uint SampleCount;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called each time new audio data is needed to feed the stream
        /// </summary>
        /// <param name="dataChunk">Data chunk to fill with new audio samples</param>
        /// <param name="userData">User data -- unused</param>
        /// <returns>True to continue playback, false to stop</returns>
        ////////////////////////////////////////////////////////////
        private bool GetData(ref Chunk dataChunk, IntPtr userData)
        {
            if (OnGetData(out _tempBuffer))
            {
                unsafe
                {
                    fixed (short* samplesPtr = _tempBuffer)
                    {
                        dataChunk.Samples = samplesPtr;
                        dataChunk.SampleCount = (uint)_tempBuffer.Length;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called to seek in the stream
        /// </summary>
        /// <param name="timeOffset">New position</param>
        /// <param name="userData">User data -- unused</param>
        /// <returns>If false is returned, the playback is aborted</returns>
        ////////////////////////////////////////////////////////////
        private void Seek(Time timeOffset, IntPtr userData) => OnSeek(timeOffset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GetDataCallbackType(ref Chunk dataChunk, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SeekCallbackType(Time timeOffset, IntPtr userData);

        private GetDataCallbackType _getDataCallback;
        private SeekCallbackType _seekCallback;
        private EffectProcessorInternal _effectProcessor;
        private short[] _tempBuffer;

        #region Imports
        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe IntPtr sfSoundStream_create(GetDataCallbackType onGetData, SeekCallbackType onSeek, uint channelCount, uint sampleRate, SoundChannel* channelMapData, UIntPtr channelMapSize, IntPtr userData);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_destroy(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_play(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_pause(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_stop(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern SoundStatus sfSoundStream_getStatus(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfSoundStream_getChannelCount(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfSoundStream_getSampleRate(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe SoundChannel* sfSoundStream_getChannelMap(IntPtr soundStream, out UIntPtr count);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPitch(IntPtr soundStream, float pitch);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPan(IntPtr soundStream, float pan);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setVolume(IntPtr soundStream, float volume);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setSpatializationEnabled(IntPtr soundStream, bool enabled);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPosition(IntPtr soundStream, Vector3f position);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setDirection(IntPtr soundStream, Vector3f direction);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setCone(IntPtr soundStream, Cone.MarshalData cone);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setVelocity(IntPtr soundStream, Vector3f velocity);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setDopplerFactor(IntPtr soundStream, float factor);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setDirectionalAttenuationFactor(IntPtr soundStream, float factor);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setRelativeToListener(IntPtr soundStream, bool relative);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setMinDistance(IntPtr soundStream, float minDistance);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setMaxDistance(IntPtr soundStream, float maxDistance);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setMinGain(IntPtr soundStream, float gain);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setMaxGain(IntPtr soundStream, float gain);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setAttenuation(IntPtr soundStream, float attenuation);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setPlayingOffset(IntPtr soundStream, Time timeOffset);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setLooping(IntPtr soundStream, bool loop);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getPitch(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getPan(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getVolume(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfSoundStream_isSpatializationEnabled(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSoundStream_getPosition(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSoundStream_getDirection(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Cone.MarshalData sfSoundStream_getCone(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSoundStream_getVelocity(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getDopplerFactor(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getDirectionalAttenuationFactor(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfSoundStream_isRelativeToListener(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getMinDistance(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getMaxDistance(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getMinGain(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getMaxGain(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSoundStream_getAttenuation(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfSoundStream_isLooping(IntPtr soundStream);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSoundStream_setEffectProcessor(IntPtr soundStream, IntPtr effectProcessor);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfSoundStream_getPlayingOffset(IntPtr soundStream);
        #endregion
    }
}
