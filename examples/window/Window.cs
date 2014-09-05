using System;
using System.Runtime.InteropServices;
using SFML;
using SFML.Window;
using SFML.System;
using OpenTK;
using OpenTK.Graphics;

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

            // Initialize OpenTK
            Toolkit.Init();
            GraphicsContext context = new GraphicsContext(new ContextHandle(IntPtr.Zero), null);

            // Setup event handlers
            window.Closed     += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.Resized    += new EventHandler<SizeEventArgs>(OnResized);

            // Set the color and depth clear values
            GL.ClearDepth(1);
            GL.ClearColor(0, 0, 0, 1);

            // Enable Z-buffer read and write
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);

            // Disable lighting and texturing
            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.Texture2D);

            // Configure the viewport (the same size as the window)
            GL.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            // Setup a perspective projection
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float ratio = (float)(window.Size.X) / window.Size.Y;
            GL.Frustum(-ratio, ratio, -1, 1, 1, 500);

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
            GL.EnableClientState(EnableCap.VertexArray);
            GL.EnableClientState(EnableCap.ColorArray);
            GL.VertexPointer(3, VertexPointerType.Float, 7 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0));
            GL.ColorPointer(4, ColorPointerType.Float, 7 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3));

            // Disable normal and texture coordinates vertex components
            GL.DisableClientState(EnableCap.NormalArray);
            GL.DisableClientState(EnableCap.TextureCoordArray);

            Clock clock = new Clock();

            // Start the game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Clear color and depth buffer
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                // Apply some transformations
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Translate(0.0F, 0.0F, -200.0F);
                GL.Rotate(clock.ElapsedTime.AsSeconds() * 50, 1.0F, 0.0F, 0.0F);
                GL.Rotate(clock.ElapsedTime.AsSeconds() * 30, 0.0F, 1.0F, 0.0F);
                GL.Rotate(clock.ElapsedTime.AsSeconds() * 90, 0.0F, 0.0F, 1.0F);

                // Draw the cube
                GL.DrawArrays(BeginMode.Triangles, 0, 36);

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
            GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
        }
    }
}
