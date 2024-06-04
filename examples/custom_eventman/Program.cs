using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
namespace Program;

public struct SimpleEventPasser : IEventMan
{
    private readonly List<Event> _events;
    public SimpleEventPasser() => _events = new();
    public void HandleEvent(Event eve)
    {
        switch (eve.Type)
        {
            case EventType.Closed:
            case EventType.LostFocus:
            case EventType.GainedFocus:
            case EventType.KeyPressed:
            case EventType.KeyReleased:
                _events.Add(eve);
                break;
            default: // Filtering out all other events
                break;
        }
    }
    public void PrepareFrame() => _events.Clear();
    public IEnumerable<Event> ProcessEvents() => _events;
}
public static class Program
{
    public static void Main()
    {
        var event_man = new SimpleEventPasser();
        var window = new RenderWindow(
            new(640, 480),
            "Custom Event Manager example",
            Styles.Default,
            event_man
        );
        while (window.IsOpen)
        {
            window.Clear(Color.Black);
            window.DispatchEvents();
            window.Display();
            foreach (var e in event_man.ProcessEvents())
            {
                System.Console.WriteLine(e.ToString());
                if (e.Type == EventType.Closed)
                {
                    window.Close();
                }
            }
        }
    }
}