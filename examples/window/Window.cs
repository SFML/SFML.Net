using System;
using System.Runtime.InteropServices;
using SFML;
using SFML.Window;
using SFML.System;
using Tao.OpenGl;

namespace window
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Request a 32-bits depth buffer when creating the window
            ContextSettings contextSettings = new ContextSettings();
            contextSettings.DepthBits = 32;

            // Create the main window
            Window window = new Window(new VideoMode(640, 480), "SFML window with OpenGL", Styles.Default, contextSettings);

            // Make it the active window for OpenGL calls
            window.SetActive();

            // Setup event handlers
            window.Closed     += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.Resized    += new EventHandler<SizeEventArgs>(OnResized);

            // Set the color and depth clear values
            Gl.glClearDepth(1);
            Gl.glClearColor(0, 0, 0, 1);

            // Enable Z-buffer read and write
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthMask(Gl.GL_TRUE);

            // Disable lighting and texturing
            Gl.glDisable(Gl.GL_LIGHTING);
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            // Configure the viewport (the same size as the window)
            Gl.glViewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            // Setup a perspective projection
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            float ratio = (float)(window.Size.X) / window.Size.Y;
            Gl.glFrustum(-ratio, ratio, -1, 1, 1, 500);

            // Define a 3D cube (6 faces made of 2 triangles composed by 3 vertices)
            float[] cube = new float[]
            {
                // positions    // colors (r, g, b, a)
                -50, -50, -50,  0, 0, 1, 1,
                -50,  50, -50,  0, 0, 1, 1,
                -50, -50,  50,  0, 0, 1, 1,
                -50, -50,  50,  0, 0, 1, 1,
                -50,  50, -50,  0, 0, 1, 1,
                -50,  50,  50,  0, 0, 1, 1,

                 50, -50, -50,  0, 1, 0, 1,
                 50,  50, -50,  0, 1, 0, 1,
                 50, -50,  50,  0, 1, 0, 1,
                 50, -50,  50,  0, 1, 0, 1,
                 50,  50, -50,  0, 1, 0, 1,
                 50,  50,  50,  0, 1, 0, 1,

                -50, -50, -50,  1, 0, 0, 1,
                 50, -50, -50,  1, 0, 0, 1,
                -50, -50,  50,  1, 0, 0, 1,
                -50, -50,  50,  1, 0, 0, 1,
                 50, -50, -50,  1, 0, 0, 1,
                 50, -50,  50,  1, 0, 0, 1,

                -50,  50, -50,  0, 1, 1, 1,
                 50,  50, -50,  0, 1, 1, 1,
                -50,  50,  50,  0, 1, 1, 1,
                -50,  50,  50,  0, 1, 1, 1,
                 50,  50, -50,  0, 1, 1, 1,
                 50,  50,  50,  0, 1, 1, 1,

                -50, -50, -50,  1, 0, 1, 1,
                 50, -50, -50,  1, 0, 1, 1,
                -50,  50, -50,  1, 0, 1, 1,
                -50,  50, -50,  1, 0, 1, 1,
                 50, -50, -50,  1, 0, 1, 1,
                 50,  50, -50,  1, 0, 1, 1,

                -50, -50,  50,  1, 1, 0, 1,
                 50, -50,  50,  1, 1, 0, 1,
                -50,  50,  50,  1, 1, 0, 1,
                -50,  50,  50,  1, 1, 0, 1,
                 50, -50,  50,  1, 1, 0, 1,
                 50,  50,  50,  1, 1, 0, 1,
            };

            // Enable position and color vertex components
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 7 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0));
            Gl.glColorPointer(4, Gl.GL_FLOAT, 7 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3));

            // Disable normal and texture coordinates vertex components
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);

            Clock clock = new Clock();

            // Start the game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Clear color and depth buffer
                Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

                // Apply some transformations
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();
                Gl.glTranslatef(0.0F, 0.0F, -200.0F);
                Gl.glRotatef(clock.ElapsedTime.AsSeconds() * 50, 1.0F, 0.0F, 0.0F);
                Gl.glRotatef(clock.ElapsedTime.AsSeconds() * 30, 0.0F, 1.0F, 0.0F);
                Gl.glRotatef(clock.ElapsedTime.AsSeconds() * 90, 0.0F, 0.0F, 1.0F);

                // Draw the cube
                Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 36);

                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();
        }

        /// <summary>
        /// Function called when the window is resized
        /// </summary>
        static void OnResized(object sender, SizeEventArgs e)
        {
            Gl.glViewport(0, 0, (int)e.Width, (int)e.Height);
        }
    }
}
