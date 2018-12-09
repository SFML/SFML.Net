using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace netcore
{
    class Program
    {


        static void Main(string[] args)
        {
            var shape = new RectangleShape(new Vector2f(100, 100))
            {
                FillColor = Color.Black
            };

            var sound = new Sound(GenerateSineWave(frequency: 440.0, volume: .25, seconds: 1));

            var window = new RenderWindow(new VideoMode(800, 600), "SFML running in .NET Core");
            window.Closed += (_, __) => window.Close();

            sound.Play();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.White);
                window.Draw(shape);
                window.Display();
            }
        }

        private static SoundBuffer GenerateSineWave(double frequency, double volume, int seconds)
        {
            uint sampleRate = 44100;
            var samples = new short[seconds * sampleRate];

            for (int i = 0; i < samples.Length; i++)
                samples[i] = (short)(Math.Sin(frequency * (2 * Math.PI) * i / sampleRate) * volume * short.MaxValue);

            return new SoundBuffer(samples, 1, sampleRate);
        }
    }
}
