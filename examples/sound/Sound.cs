using System;
using System.Threading;
using SFML.Audio;

namespace sound
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            // Play a sound
            PlaySound();
            Console.Clear();

            // Play a music
            PlayMusic();
        }

        /// <summary>
        /// Play a sound
        /// </summary>
        private static void PlaySound()
        {
            // Load a sound buffer from a wav file
            var buffer = new SoundBuffer("resources/canary.wav");

            // Display sound information
            Console.WriteLine("canary.wav :");
            Console.WriteLine(" " + buffer.Duration.AsSeconds() + " sec");
            Console.WriteLine(" " + buffer.SampleRate + " samples / sec");
            Console.WriteLine(" " + buffer.ChannelCount + " channels");

            // Create a sound instance and play it
            var sound = new Sound(buffer);
            sound.Play();

            // Loop while the sound is playing
            while (sound.Status == SoundStatus.Playing)
            {
                // Display the playing position
                Console.CursorLeft = 0;
                Console.Write("Playing... " + sound.PlayingOffset.AsSeconds() + " sec     ");

                // Leave some CPU time for other processes
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Play a music
        /// </summary>
        private static void PlayMusic()
        {
            // Load an ogg music file
            var music = new Music("resources/orchestral.ogg");

            // Display music information
            Console.WriteLine("orchestral.ogg :");
            Console.WriteLine(" " + music.Duration.AsSeconds() + " sec");
            Console.WriteLine(" " + music.SampleRate + " samples / sec");
            Console.WriteLine(" " + music.ChannelCount + " channels");

            // Play it
            music.Play();

            // Loop while the music is playing
            while (music.Status == SoundStatus.Playing)
            {
                // Display the playing position
                Console.CursorLeft = 0;
                Console.Write("Playing... " + music.PlayingOffset.AsSeconds() + " sec     ");

                // Leave some CPU time for other processes
                Thread.Sleep(100);
            }
        }
    }
}
