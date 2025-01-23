using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Enumeration of all possible sound states
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum SoundStatus
    {
        /// <summary>Sound is not playing</summary>
        Stopped,

        /// <summary>Sound is paused</summary>
        Paused,

        /// <summary>Sound is playing</summary>
        Playing
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Regular sound that can be played in the audio environment
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Sound : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the sound with a buffer
        /// </summary>
        /// <param name="buffer">Sound buffer containing the audio data to play with the sound</param>
        ////////////////////////////////////////////////////////////
        public Sound(SoundBuffer buffer) :
            base(sfSound_create(buffer.CPointer)) => SoundBuffer = buffer;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the sound from another source
        /// </summary>
        /// <param name="copy">Sound to copy</param>
        ////////////////////////////////////////////////////////////
        public Sound(Sound copy) :
            base(sfSound_copy(copy.CPointer)) => SoundBuffer = copy.SoundBuffer;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Start or resume playing the sound.
        ///
        /// This function starts the stream if it was stopped, resumes
        /// it if it was paused, and restarts it from beginning if it
        /// was it already playing.
        /// This function uses its own thread so that it doesn't block
        /// the rest of the program while the sound is played.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Play() => sfSound_play(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pause the sound.
        ///
        /// This function pauses the sound if it was playing,
        /// otherwise (sound already paused or stopped) it has no effect.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Pause() => sfSound_pause(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Stop playing the sound.
        ///
        /// This function stops the sound if it was playing or paused,
        /// and does nothing if it was already stopped.
        /// It also resets the playing position (unlike pause()).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Stop() => sfSound_stop(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Buffer containing the sound data to play through the sound.
        /// 
        /// It is important to note that the sound buffer is not copied,
        /// thus the SoundBuffer instance must remain alive as long
        /// as it is attached to the sound.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundBuffer SoundBuffer
        {
            get => _buffer;
            set { _buffer = value; sfSound_setBuffer(CPointer, value != null ? value.CPointer : IntPtr.Zero); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current status of the sound (see SoundStatus enum)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundStatus Status => sfSound_getStatus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Flag if the sound should loop after reaching the end.
        ///
        /// If set, the sound will restart from beginning after
        /// reaching the end and so on, until it is stopped or
        /// IsLooping = false is set.
        /// The default looping state for sounds is false.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool IsLooping
        {
            get => sfSound_isLooping(CPointer);
            set => sfSound_setLooping(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Pitch of the sound.
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
            get => sfSound_getPitch(CPointer);
            set => sfSound_setPitch(CPointer, value);
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
            get => sfSound_getPan(CPointer);
            set => sfSound_setPan(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Volume of the sound.
        /// 
        /// The volume is a value between 0 (mute) and 100 (full volume).
        /// The default value for the volume is 100.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Volume
        {
            get => sfSound_getVolume(CPointer);
            set => sfSound_setVolume(CPointer, value);
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
            get => sfSound_isSpatializationEnabled(CPointer);
            set => sfSound_setSpatializationEnabled(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Current playing position of the sound.
        /// 
        /// The playing position can be changed when the sound is
        /// either paused or playing.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time PlayingOffset
        {
            get => sfSound_getPlayingOffset(CPointer);
            set => sfSound_setPlayingOffset(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D position of the sound in the audio scene.
        ///
        /// Only sounds with one channel (mono sounds) can be
        /// spatialized.
        /// The default position of a sound is (0, 0, 0).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Position
        {
            get => sfSound_getPosition(CPointer);
            set => sfSound_setPosition(CPointer, value);
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
            get => sfSound_getPosition(CPointer);
            set => sfSound_setPosition(CPointer, value);
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
            get => new Cone(sfSound_getCone(CPointer));
            set => sfSound_setCone(CPointer, value.Marshal());
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
            get => sfSound_getVelocity(CPointer);
            set => sfSound_setVelocity(CPointer, value);
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
            get => sfSound_getDopplerFactor(CPointer);
            set => sfSound_setDopplerFactor(CPointer, value);
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
            get => sfSound_getDirectionalAttenuationFactor(CPointer);
            set => sfSound_setDirectionalAttenuationFactor(CPointer, value);
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
            get => sfSound_isRelativeToListener(CPointer);
            set => sfSound_setRelativeToListener(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Minimum distance of the sound.
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
            get => sfSound_getMinDistance(CPointer);
            set => sfSound_setMinDistance(CPointer, value);
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
            get => sfSound_getMaxDistance(CPointer);
            set => sfSound_setMaxDistance(CPointer, value);
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
            get => sfSound_getMinGain(CPointer);
            set => sfSound_setMinGain(CPointer, value);
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
            get => sfSound_getMaxGain(CPointer);
            set => sfSound_setMaxGain(CPointer, value);
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
            get => sfSound_getAttenuation(CPointer);
            set => sfSound_setAttenuation(CPointer, value);
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

            sfSound_setEffectProcessor(CPointer, Marshal.GetFunctionPointerForDelegate(_effectProcessor));
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

            return "[Sound]" +
                   " Status(" + Status + ")" +
                   " IsLooping(" + IsLooping + ")" +
                   " Pitch(" + Pitch + ")" +
                   " Volume(" + Volume + ")" +
                   " Position(" + Position + ")" +
                   " RelativeToListener(" + RelativeToListener + ")" +
                   " MinDistance(" + MinDistance + ")" +
                   " Attenuation(" + Attenuation + ")" +
                   " PlayingOffset(" + PlayingOffset + ")" +
                   " SoundBuffer(" + SoundBuffer + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfSound_destroy(CPointer);

        private SoundBuffer _buffer;
        private EffectProcessorInternal _effectProcessor;

        #region Imports
        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfSound_create(IntPtr soundBuffer);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfSound_copy(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_destroy(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_play(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_pause(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_stop(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setBuffer(IntPtr sound, IntPtr buffer);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setLooping(IntPtr sound, bool loop);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfSound_isLooping(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern SoundStatus sfSound_getStatus(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setPitch(IntPtr sound, float pitch);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setPan(IntPtr sound, float pan);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setVolume(IntPtr sound, float volume);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setSpatializationEnabled(IntPtr sound, bool enabled);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setPosition(IntPtr sound, Vector3f position);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setDirection(IntPtr sound, Vector3f direction);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setCone(IntPtr sound, Cone.MarshalData cone);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setVelocity(IntPtr sound, Vector3f velocity);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setDopplerFactor(IntPtr sound, float factor);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setDirectionalAttenuationFactor(IntPtr sound, float factor);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setRelativeToListener(IntPtr sound, bool relative);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setMinDistance(IntPtr sound, float minDistance);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setMaxDistance(IntPtr sound, float maxDistance);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setMinGain(IntPtr sound, float gain);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setMaxGain(IntPtr sound, float gain);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setAttenuation(IntPtr sound, float attenuation);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setPlayingOffset(IntPtr sound, Time timeOffset);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfSound_setEffectProcessor(IntPtr sound, IntPtr effectProcessor);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getPitch(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getPan(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getVolume(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfSound_isSpatializationEnabled(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSound_getPosition(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSound_getDirection(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Cone.MarshalData sfSound_getCone(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfSound_getVelocity(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getDopplerFactor(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getDirectionalAttenuationFactor(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfSound_isRelativeToListener(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getMinDistance(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getMaxDistance(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getMinGain(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getMaxGain(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfSound_getAttenuation(IntPtr sound);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfSound_getPlayingOffset(IntPtr sound);
        #endregion
    }
}
