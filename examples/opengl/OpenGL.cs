using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace opengl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // NOTE : This is workaround to create a functioning opengl context for OpenTK (for current OpenTK version)
            var gameWindow = new OpenTK.GameWindow();

            // Request a 24-bits depth buffer when creating the window
            var contextSettings = new ContextSettings();
            contextSettings.DepthBits = 24;

            // Create the main window
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML graphics with OpenGL", Styles.Default, contextSettings);
            window.SetVerticalSyncEnabled(true);

            // Initialize OpenTK
            // NOTE : next 2 lines are kept from old examples until we resolve proper OpenTK versioning
            //Toolkit.Init();
            //GraphicsContext context = new GraphicsContext(new ContextHandle(IntPtr.Zero), null);

            // Setup event handlers
            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
            window.Resized += new EventHandler<SizeEventArgs>(OnResized);

            // Create a sprite for the background
            var background = new Sprite(new Texture("resources/background.jpg"));

            // Create a text to display on top of the OpenGL object
            var text = new Text("SFML / OpenGL demo", new Font("resources/sansation.ttf"));
            text.Position = new Vector2f(250, 450);
            text.FillColor = new SFML.Graphics.Color(255, 255, 255, 170);

            // Make the window the active target for OpenGL calls
            window.SetActive();

            // Load an OpenGL texture.
            // We could directly use a SFML.Graphics.Texture as an OpenGL texture (with its Bind() member function),
            // but here we want more control on it (generate mipmaps, ...) so we create a new one
            int texture = 0;
            using (var image = new SFML.Graphics.Image("resources/texture.jpg"))
            {
                GL.GenTextures(1, out texture);
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)image.Size.X, (int)image.Size.Y, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Pixels);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            }

            // Enable Z-buffer read and write
            GL.Enable(EnableCap.DepthTest);
            GL.DepthMask(true);
            GL.ClearDepth(1);

            // Disable lighting
            GL.Disable(EnableCap.Lighting);

            // Configure the viewport (the same size as the window)
            GL.Viewport(0, 0, (int)window.Size.X, (int)window.Size.Y);

            // Setup a perspective projection
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float ratio = (float)( window.Size.X ) / window.Size.Y;
            GL.Frustum(-ratio, ratio, -1, 1, 1, 500);

            // Bind the texture
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);

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
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.VertexPointer(3, VertexPointerType.Float, 5 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 5 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3));

            // Disable normal and color vertex components
            GL.DisableClientState(ArrayCap.NormalArray);
            GL.DisableClientState(ArrayCap.ColorArray);

            var clock = new Clock();

            // Start game loop
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Clear the window
                GL.Clear(ClearBufferMask.DepthBufferBit);

                // Draw background
                window.PushGLStates();
                window.Draw(background);
                window.PopGLStates();

                // Clear the depth buffer
                GL.Clear(ClearBufferMask.DepthBufferBit);

                // We get the position of the mouse cursor, so that we can move the box accordingly
                float x = Mouse.GetPosition(window).X * 200.0F / window.Size.X - 100.0F;
                float y = -Mouse.GetPosition(window).Y * 200.0F / window.Size.Y + 100.0F;

                // Apply some transformations
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Translate(x, y, -100.0F);
                GL.Rotate(clock.ElapsedTime.AsSeconds() * 50, 1.0F, 0.0F, 0.0F);
                GL.Rotate(clock.ElapsedTime.AsSeconds() * 30, 0.0F, 1.0F, 0.0F);
                GL.Rotate(clock.ElapsedTime.AsSeconds() * 90, 0.0F, 0.0F, 1.0F);

                // Draw the cube
                GL.DrawArrays(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles, 0, 36);

                // Draw some text on top of our OpenGL object
                window.PushGLStates();
                window.Draw(text);
                window.PopGLStates();

                // Finally, display the rendered frame on screen
                window.Display();
            }

            // Don't forget to destroy our texture
            GL.DeleteTextures(1, ref texture);
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
            {
                window.Close();
            }
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
