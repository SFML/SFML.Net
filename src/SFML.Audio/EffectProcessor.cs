
using System.Runtime.InteropServices;
using System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Callable that is provided with sound data for processing
    /// <para/>
    /// When the audio engine sources sound data from sound
    /// sources it will pass the data through an effects
    /// processor if one is set. The sound data will already be
    /// converted to the internal floating point format.
    /// <para/>
    /// Sound data that is processed this way is provided in
    /// frames. Each frame contains 1 floating point sample per
    /// channel. If e.g. the data source provides stereo data,
    /// each frame will contain 2 floats.
    /// <para/>
    /// The effects processor function takes 4 parameters:
    ///   - The input data frames, channels interleaved
    ///   - The number of input data frames available
    ///   - The buffer to write output data frames to, channels interleaved
    ///   - The number of output data frames that the output buffer can hold
    ///   - The channel count
    /// <para/>
    /// The input and output frame counts are in/out parameters.
    /// <para/>
    /// When this function is called, the input count will
    /// contain the number of frames available in the input
    /// buffer. The output count will contain the size of the
    /// output buffer i.e. the maximum number of frames that
    /// can be written to the output buffer.
    /// <para/>
    /// Attempting to read more frames than the input frame
    /// count or write more frames than the output frame count
    /// will result in undefined behaviour.
    /// <para/>
    /// Attempting to read more frames than the input frame
    /// count or write more frames than the output frame count
    /// will result in undefined behaviour.
    /// <para/>
    /// It is important to note that the channel count of the
    /// audio engine currently sourcing data from this sound
    /// will always be provided in `frameChannelCount`. This can
    /// be different from the channel count of the sound source
    /// so make sure to size necessary processing buffers
    /// according to the engine channel count and not the sound
    /// source channel count.
    /// <para/>
    /// When done processing the frames, the input and output
    /// frame counts must be updated to reflect the actual
    /// number of frames that were read from the input and
    /// written to the output.
    /// <para/>
    /// The processing function should always try to process as
    /// much sound data as possible i.e. always try to fill the
    /// output buffer to the maximum. In certain situations for
    /// specific effects it can be possible that the input frame
    /// count and output frame count aren't equal. As long as
    /// the frame counts are updated accordingly this is
    /// perfectly valid.
    /// <para/>
    /// If the audio engine determines that no audio data is
    /// available from the data source, the input data frames
    /// pointer is set to `nullptr` and the input frame count is
    /// set to 0. In this case it is up to the function to
    /// decide how to handle the situation. For specific effects
    /// e.g. Echo/Delay buffered data might still be able to be
    /// written to the output buffer even if there is no longer
    /// any input data.
    /// <para/>
    /// An important thing to remember is that this function is
    /// directly called by the audio engine. Because the audio
    /// engine runs on an internal thread of its own, make sure
    /// access to shared data is synchronized appropriately.
    /// <para/>
    /// Because this function is stored by the `SoundSource`
    /// object it will be able to be called as long as the
    /// `SoundSource` object hasn't yet been destroyed. Make sure
    /// that any data this function references outlives the
    /// SoundSource object otherwise use-after-free errors will
    /// occur.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public delegate long EffectProcessor(float[] inputFrames, float[] outputFrames, uint frameChannelCount);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate long EffectProcessorInternal(IntPtr inputFrames, uint inputFrameCount, IntPtr outputFrames, uint outputFrameCount, uint frameChannelCount);
}