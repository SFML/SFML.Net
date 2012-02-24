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
        /// SoundBuffer is the low-level class for loading and manipulating
        /// sound buffers. A sound buffer holds audio data (samples)
        /// which can then be played by a Sound or saved to a file.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class SoundBuffer : ObjectBase
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the sound buffer from a file
            /// </summary>
            /// <param name="filename">Path of the sound file to load</param>
            /// <exception cref="LoadingFailedException" />
            ////////////////////////////////////////////////////////////
            public SoundBuffer(string filename) :
                base(sfSoundBuffer_CreateFromFile(filename))
            {
                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("sound buffer", filename);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Load the sound buffer from a custom stream
            /// </summary>
            /// <param name="stream">Source stream to read from</param>
            /// <exception cref="LoadingFailedException" />
            ////////////////////////////////////////////////////////////
            public SoundBuffer(Stream stream) :
                base(IntPtr.Zero)
            {
                using (StreamAdaptor adaptor = new StreamAdaptor(stream))
                {
                    SetThis(sfSoundBuffer_CreateFromStream(adaptor.InputStreamPtr));
                }

                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("sound buffer");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the sound buffer from an array of samples
            /// </summary>
            /// <param name="samples">Array of samples</param>
            /// <param name="channelCount">Channel count</param>
            /// <param name="sampleRate">Sample rate</param>
            /// <exception cref="LoadingFailedException" />
            ////////////////////////////////////////////////////////////
            public SoundBuffer(short[] samples, uint channelCount, uint sampleRate) :
                base(IntPtr.Zero)
            {
                unsafe
                {
                    fixed (short* SamplesPtr = samples)
                    {
                        SetThis(sfSoundBuffer_CreateFromSamples(SamplesPtr, (uint)samples.Length, channelCount, sampleRate));
                    }
                }

                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("sound buffer");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the sound buffer from another sound buffer
            /// </summary>
            /// <param name="copy">Sound buffer to copy</param>
            ////////////////////////////////////////////////////////////
            public SoundBuffer(SoundBuffer copy) :
                base(sfSoundBuffer_Copy(copy.CPointer))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Save the sound buffer to an audio file
            /// </summary>
            /// <param name="filename">Path of the sound file to write</param>
            /// <returns>True if saving has been successful</returns>
            ////////////////////////////////////////////////////////////
            public bool SaveToFile(string filename)
            {
                return sfSoundBuffer_SaveToFile(CPointer, filename);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Samples rate, in samples per second
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint SampleRate
            {
                get {return sfSoundBuffer_GetSampleRate(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Number of channels (1 = mono, 2 = stereo)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint ChannelCount
            {
                get {return sfSoundBuffer_GetChannelCount(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Total duration of the buffer, in milliseconds
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint Duration
            {
                get {return sfSoundBuffer_GetDuration(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Array of samples contained in the buffer
            /// </summary>
            ////////////////////////////////////////////////////////////
            public short[] Samples
            {
                get
                {
                    short[] SamplesArray = new short[sfSoundBuffer_GetSampleCount(CPointer)];
                    Marshal.Copy(sfSoundBuffer_GetSamples(CPointer), SamplesArray, 0, SamplesArray.Length);
                    return SamplesArray;
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
                return "[SoundBuffer]" +
                       " SampleRate(" + SampleRate + ")" +
                       " ChannelCount(" + ChannelCount + ")" +
                       " Duration(" + Duration + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfSoundBuffer_Destroy(CPointer);
            }

            #region Imports
            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfSoundBuffer_CreateFromFile(string Filename);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern IntPtr sfSoundBuffer_CreateFromStream(IntPtr stream);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern IntPtr sfSoundBuffer_CreateFromSamples(short* Samples, uint SampleCount, uint ChannelsCount, uint SampleRate);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfSoundBuffer_Copy(IntPtr SoundBuffer);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfSoundBuffer_Destroy(IntPtr SoundBuffer);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfSoundBuffer_SaveToFile(IntPtr SoundBuffer, string Filename);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfSoundBuffer_GetSamples(IntPtr SoundBuffer);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfSoundBuffer_GetSampleCount(IntPtr SoundBuffer);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfSoundBuffer_GetSampleRate(IntPtr SoundBuffer);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfSoundBuffer_GetChannelCount(IntPtr SoundBuffer);

            [DllImport("csfml-audio-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfSoundBuffer_GetDuration(IntPtr SoundBuffer);
            #endregion
        }
    }
}
