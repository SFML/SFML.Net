using System;
using Version = SFML.System.Version;

namespace window;

internal static class Program
{
    private static void Main()
    {
        var version = Version.GetVersion();
        Console.WriteLine(version.Major);
        Console.WriteLine(version.Minor);
        Console.WriteLine(version.Patch);
        Console.WriteLine(version.IsRelease);
        Console.WriteLine(version.String);

        Console.WriteLine("Press ESC key to close window");
        var window = new SimpleWindow();
        window.Run();

        Console.WriteLine("All done");
    }
}

internal class SimpleWindow
{
    public void Run()
    {
        var mode = new SFML.Window.VideoMode((800, 600));
        var window = new SFML.Graphics.RenderWindow(mode, "SFML works!");
        window.KeyPressed += Window_KeyPressed;
        window.Closed += (_, _) => window.Close();

        var circle = new SFML.Graphics.CircleShape(100f)
        {
            FillColor = SFML.Graphics.Color.Blue
        };

        circle.MiterLimit = 2;
        Console.WriteLine(circle.MiterLimit);

        // Start the game loop
        while (window.IsOpen)
        {
            // Process events
            window.DispatchEvents();
            window.Draw(circle);

            // Finally, display the rendered frame on screen
            window.Display();
        }
    }

    /// <summary>
    /// Function called when a key is pressed
    /// </summary>
    private void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
    {
        var window = (SFML.Window.Window)sender;
        if (e.Code == SFML.Window.Keyboard.Key.Escape)
        {
            window.Close();
        }
    }
}