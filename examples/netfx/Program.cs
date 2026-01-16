using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace netfx;

public static class Program
{
    private static void Main()
    {
        var window = new RenderWindow(new VideoMode((800, 600)), "SFML running with .NET Framework");
        window.Closed += (_, _) => window.Close();

        var shape = new CircleShape(100f, 50)
        {
            FillColor = Color.Green,
            Origin = new Vector2f(100f, 100f),
            Position = new Vector2f(400f, 300f)
        };

        var sound = new Sound(GenerateSineWave(frequency: 440.0, volume: .25, seconds: 1));

        sound.Play();

        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear();
            window.Draw(shape);
            window.Display();
        }
    }

    private static SoundBuffer GenerateSineWave(double frequency, double volume, int seconds)
    {
        uint sampleRate = 44100;
        var samples = new short[seconds * sampleRate];

        for (var i = 0; i < samples.Length; i++)
        {
            samples[i] = (short)(Math.Sin(frequency * (2 * Math.PI) * i / sampleRate) * volume * short.MaxValue);
        }

        return new SoundBuffer(samples, 1, sampleRate, [SoundChannel.Mono]);
    }
}
