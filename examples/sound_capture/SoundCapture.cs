using System;
using System.Threading;
using SFML.Audio;

namespace sound_capture;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    private static void Main()
    {
        // Check that the device can capture audio
        if (!SoundRecorder.IsAvailable)
        {
            Console.WriteLine("Sorry, audio capture is not supported by your system");
            return;
        }

        // Choose the sample rate
        uint sampleRate;
        do
        {
            Console.WriteLine("Please choose the sample rate for sound capture (44100 is CD quality) : ");
        } while (!uint.TryParse(Console.ReadLine(), out sampleRate));

        // Wait for user input...
        Console.WriteLine("Press enter to start recording audio");
        _ = Console.ReadLine();

        // Here we'll use an integrated custom recorder, which saves the captured data into a SoundBuffer
        var recorder = new SoundBufferRecorder();

        // Audio capture is done in a separate thread, so we can block the main thread while it is capturing
        _ = recorder.Start(sampleRate);
        Console.WriteLine("Recording... press enter to stop");
        _ = Console.ReadLine();
        recorder.Stop();

        // Get the buffer containing the captured data
        var buffer = recorder.SoundBuffer;

        // Display captured sound information
        Console.WriteLine("Sound information :");
        Console.WriteLine(" " + buffer.Duration + " seconds");
        Console.WriteLine(" " + buffer.SampleRate + " samples / seconds");
        Console.WriteLine(" " + buffer.ChannelCount + " channels");

        // Choose what to do with the recorded sound data
        char choice;
        do
        {
            Console.WriteLine("What do you want to do with captured sound (p = play, s = save) ? ");
        } while (!char.TryParse(Console.ReadLine(), out choice));

        if (choice == 's')
        {
            // Choose the filename
            Console.WriteLine("Choose the file to create : ");
            var filename = Console.ReadLine();

            // Save the buffer
            _ = buffer.SaveToFile(filename);
        }
        else
        {
            // Create a sound instance and play it
            var sound = new Sound(buffer);
            sound.Play();

            // Wait until finished
            while (sound.Status == SoundStatus.Playing)
            {
                // Display the playing position
                Console.CursorLeft = 0;
                Console.Write("Playing... " + sound.PlayingOffset.AsSeconds() + " sec     ");

                // Leave some CPU time for other threads
                Thread.Sleep(100);
            }
        }

        // Finished !
        Console.WriteLine("\nDone !");
    }
}