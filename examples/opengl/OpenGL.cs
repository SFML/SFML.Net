using System;
using System.Runtime.InteropServices;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Tao.OpenGl;

namespace opengl
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
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML graphics with OpenGL", Styles.Default, contextSettings);
            window.SetVerticalSyncEnabled(true);

            // Make it the active window for OpenGL calls
            window.SetActive();

            // Setup event handlers
            window.Closed     += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.Resized    += new EventHandler<SizeEventArgs>(OnResized);

            // Create a sprite for the background
            Sprite background = new Sprite(new Texture("resources/background.jpg"));

            // Create a text to display on top of the OpenGL object
            Text text = new Text("SFML / OpenGL demo", new Font("resources/sansation.ttf"));
            text.Position = new Vector2f(250, 450);
            text.Color = new Color(255, 255, 255, 170);

            // Load an OpenGL texture.
            // We could directly use a SFML.Graphics.Texture as an OpenGL texture (with its Bind() member function),
            // but here we want more control on it (generate mipmaps, ...) so we create a new one
            int texture = 0;
            using (Image image = new Image("resources/texture.jpg"))
            {
                Gl.glGenTextures(1, out texture);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
                Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, Gl.GL_RGBA, (int)image.Size.X, (int)image.Size.Y, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, image.Pixels);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_LINEAR);
            }

            // Enable Z-buffer read and write
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glDepthMask(Gl.GL_TRUE);
            Gl.glClearDepth(1);

            // Disable lighting
            Gl.glDisable(Gl.GL_LIGHTING);

            // Configure the viewport (the same size as the window)
            Gl.glViewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            // Setup a perspective projection
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            float ratio = (float)(window.Size.X) / window.Size.Y;
            Gl.glFrustum(-ratio, ratio, -1, 1, 1, 500);

            // Enable 2D Textures
            Gl.glEnable(Gl.GL_TEXTURE_2D);

            // Define a 3D cube (6 faces made of 2 triangles composed by 3 vertices)
            float[] cube = new float[]
            {
                // positions    // texture coordinates
                -20, -20, -20,  0, 0,
                -20,  20, -20,  1, 0,
                -20, -20,  20,  0, 1,
                -20, -20,  20,  0, 1,
                -20,  20, -20,  1, 0,
                -20,  20,  20,  1, 1,

                 20, -20, -20,  0, 0,
                 20,  20, -20,  1, 0,
                 20, -20,  20,  0, 1,
                 20, -20,  20,  0, 1,
                 20,  20, -20,  1, 0,
                 20,  20,  20,  1, 1,

                -20, -20, -20,  0, 0,
                 20, -20, -20,  1, 0,
                -20, -20,  20,  0, 1,
                -20, -20,  20,  0, 1,
                 20, -20, -20,  1, 0,
                 20, -20,  20,  1, 1,

                -20,  20, -20,  0, 0,
                 20,  20, -20,  1, 0,
                -20,  20,  20,  0, 1,
                -20,  20,  20,  0, 1,
                 20,  20, -20,  1, 0,
                 20,  20,  20,  1, 1,

                -20, -20, -20,  0, 0,
                 20, -20, -20,  1, 0,
                -20,  20, -20,  0, 1,
                -20,  20, -20,  0, 1,
                 20, -20, -20,  1, 0,
                 20,  20, -20,  1, 1,

                -20, -20,  20,  0, 0,
                 20, -20,  20,  1, 0,
                -20,  20,  20,  0, 1,
                -20,  20,  20,  0, 1,
                 20, -20,  20,  1, 0,
                 20,  20,  20,  1, 1
            };

            // Enable position and texture coordinates vertex components
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 5 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0));
            Gl.glTexCoordPointer(2, Gl.GL_FLOAT, 5 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3));

            // Disable normal and color vertex components
            Gl.glDisableClientState(Gl.GL_NORMAL_ARRAY);
            Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);

            Clock clock = new Clock();

            // Start game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Clear the window
                window.Clear();

                // Draw background
                window.PushGLStates();
                window.Draw(background);
                window.PopGLStates();

                // Clear the depth buffer
                Gl.glClear(Gl.GL_DEPTH_BUFFER_BIT);

                // We get the position of the mouse cursor, so that we can move the box accordingly
                float x =  Mouse.GetPosition(window).X * 200.0F / window.Size.X - 100.0F;
                float y = -Mouse.GetPosition(window).Y * 200.0F / window.Size.Y + 100.0F;

                // Apply some transformations
                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();
                Gl.glTranslatef(x, y, -100.0F);
                Gl.glRotatef(clock.ElapsedTime.AsSeconds() * 50, 1.0F, 0.0F, 0.0F);
                Gl.glRotatef(clock.ElapsedTime.AsSeconds() * 30, 0.0F, 1.0F, 0.0F);
                Gl.glRotatef(clock.ElapsedTime.AsSeconds() * 90, 0.0F, 0.0F, 1.0F);

                // Bind the texture
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);

                // Draw the cube
                Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, 36);

                // Draw some text on top of our OpenGL object
                window.PushGLStates();
                window.Draw(text);
                window.PopGLStates();

                // Finally, display the rendered frame on screen
                window.Display();
            }

            // Don't forget to destroy our texture
            Gl.glDeleteTextures(1, ref texture);
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
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
