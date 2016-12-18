using System.Collections.Generic;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Specialized SoundRecorder which saves the captured
    /// audio data into a sound buffer
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class SoundBufferRecorder : SoundRecorder
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Sound buffer containing the captured audio data
        ///
        /// The sound buffer is valid only after the capture has ended.
        /// This function provides a reference to the internal
        /// sound buffer, but you should make a copy of it if you want
        /// to make any modifications to it.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SoundBuffer SoundBuffer
        {
            get { return mySoundBuffer; }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[SoundBufferRecorder]" +
                   " SampleRate(" + SampleRate + ")" +
                   " SoundBuffer(" + SoundBuffer + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when a new capture starts
        /// </summary>
        /// <returns>False to abort recording audio data, true to continue</returns>
        ////////////////////////////////////////////////////////////
        protected override bool OnStart()
        {
            mySamplesArray.Clear();
            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Process a new chunk of recorded samples
        /// </summary>
        /// <param name="samples">Array of samples to process</param>
        /// <returns>False to stop recording audio data, true to continue</returns>
        ////////////////////////////////////////////////////////////
        protected override bool OnProcessSamples(short[] samples)
        {
            mySamplesArray.AddRange(samples);
            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called when the current capture stops
        /// </summary>
        ////////////////////////////////////////////////////////////
        protected override void OnStop()
        {
            mySoundBuffer = new SoundBuffer(mySamplesArray.ToArray(), 1, SampleRate);
        }

        private List<short> mySamplesArray = new List<short>();
        private SoundBuffer mySoundBuffer = null;
    }
}
