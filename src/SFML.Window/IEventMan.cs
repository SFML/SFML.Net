namespace SFML.Window
{
    /// <summary>An instance that manages events within a SFML.Window</summary>
    public interface IEventMan
    {

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This method is called by the window before
        /// detecting the events from the new. Use for any
        /// setup and cleanup of previous batch (if needed)
        /// </summary>
        ////////////////////////////////////////////////////////////
        void PrepareFrame();
        ////////////////////////////////////////////////////////////
        /// <summary>Handles the event which was passed to it by the Window class</summary>
        /// <param name="eve">The event object recieved</param>
        ////////////////////////////////////////////////////////////
        void HandleEvent(Event eve);
    }
}
