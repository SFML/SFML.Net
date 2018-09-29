using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace shader
{
    /// <summary>Base class for effects</summary>
    abstract class Effect : Drawable
    {
        protected Effect(string name)
        {
            myName = name;
        }

        public string Name => myName;
        public void Update(float time, float x, float y)
        {
            if (Shader.IsAvailable)
            {
                OnUpdate(time, x, y);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (Shader.IsAvailable)
            {
                OnDraw(target, states);
            }
            else
            {
                var error = new Text("Shader not\nsupported", GetFont())
                {
                    Position = new Vector2f(320, 200),
                    CharacterSize = 36
                };
                target.Draw(error, states);
            }
        }

        public static void SetFont(Font font)
        {
            ourFont = font;
        }

        protected abstract void OnUpdate(float time, float x, float y);
        protected abstract void OnDraw(RenderTarget target, RenderStates states);

        protected Font GetFont()
        {
            return ourFont;
        }

        private readonly string myName;
        private static Font ourFont;
    }

    /// <summary>"Pixelate" fragment shader</summary>
    class Pixelate : Effect
    {
        public Pixelate() : base("pixelate")
        {
            // Load the texture and initialize the sprite
            myTexture = new Texture("resources/background.jpg");
            mySprite = new Sprite(myTexture);

            // Load the shader
            myShader = new Shader(null, null, "resources/pixelate.frag");
            myShader.SetUniform("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            myShader.SetUniform("pixel_threshold", ( x + y ) / 30);
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = myShader
            };
            target.Draw(mySprite, states);
        }

        private readonly Texture myTexture;
        private readonly Sprite mySprite;
        private Shader myShader;
    }

    /// <summary>"Wave" vertex shader + "blur" fragment shader</summary>
    class WaveBlur : Effect
    {
        public WaveBlur() : base("wave + blur")
        {
            // Create the text
            myText = new Text
            {
                DisplayedString = "Praesent suscipit augue in velit pulvinar hendrerit varius purus aliquam.\n" +
                                     "Mauris mi odio, bibendum quis fringilla a, laoreet vel orci. Proin vitae vulputate tortor.\n" +
                                     "Praesent cursus ultrices justo, ut feugiat ante vehicula quis.\n" +
                                     "Donec fringilla scelerisque mauris et viverra.\n" +
                                     "Maecenas adipiscing ornare scelerisque. Nullam at libero elit.\n" +
                                     "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.\n" +
                                     "Nullam leo urna, tincidunt id semper eget, ultricies sed mi.\n" +
                                     "Morbi mauris massa, commodo id dignissim vel, lobortis et elit.\n" +
                                     "Fusce vel libero sed neque scelerisque venenatis.\n" +
                                     "Integer mattis tincidunt quam vitae iaculis.\n" +
                                     "Vivamus fringilla sem non velit venenatis fermentum.\n" +
                                     "Vivamus varius tincidunt nisi id vehicula.\n" +
                                     "Integer ullamcorper, enim vitae euismod rutrum, massa nisl semper ipsum,\n" +
                                     "vestibulum sodales sem ante in massa.\n" +
                                     "Vestibulum in augue non felis convallis viverra.\n" +
                                     "Mauris ultricies dolor sed massa convallis sed aliquet augue fringilla.\n" +
                                     "Duis erat eros, porta in accumsan in, blandit quis sem.\n" +
                                     "In hac habitasse platea dictumst. Etiam fringilla est id odio dapibus sit amet semper dui laoreet.\n",
                Font = GetFont(),
                CharacterSize = 22,
                Position = new Vector2f(30, 20)
            };

            // Load the shader
            myShader = new Shader("resources/wave.vert", null, "resources/blur.frag");
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            myShader.SetUniform("wave_phase", time);
            myShader.SetUniform("wave_amplitude", new Vector2f(x * 40, y * 40));
            myShader.SetUniform("blur_radius", ( x + y ) * 0.008F);
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = myShader
            };
            target.Draw(myText, states);
        }

        private readonly Text myText;
        private Shader myShader;
    }

    /// <summary>"Storm" vertex shader + "blink" fragment shader</summary>
    class StormBlink : Effect
    {
        public StormBlink() : base("storm + blink")
        {
            Random random = new Random();

            // Create the points
            myPoints = new VertexArray(PrimitiveType.Points);
            for (int i = 0; i < 40000; ++i)
            {
                float x = random.Next(0, 800);
                float y = random.Next(0, 600);
                byte r = (byte)random.Next(0, 255);
                byte g = (byte)random.Next(0, 255);
                byte b = (byte)random.Next(0, 255);
                myPoints.Append(new Vertex(new Vector2f(x, y), new Color(r, g, b)));
            }

            // Load the shader
            myShader = new Shader("resources/storm.vert", null, "resources/blink.frag");
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            float radius = 200 + (float)Math.Cos(time) * 150;
            myShader.SetUniform("storm_position", new Vector2f(x * 800, y * 600));
            myShader.SetUniform("storm_inner_radius", radius / 3);
            myShader.SetUniform("storm_total_radius", radius);
            myShader.SetUniform("blink_alpha", 0.5F + (float)Math.Cos(time * 3) * 0.25F);
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states);
            states.Shader = myShader;
            target.Draw(myPoints, states);
        }

        private VertexArray myPoints;
        private Shader myShader;
    }

    /// <summary>"Edge" post-effect fragment shader</summary>
    class Edge : Effect
    {
        public Edge() : base("edge post-effect")
        {
            // Create the off-screen surface
            mySurface = new RenderTexture(800, 600)
            {
                Smooth = true
            };

            // Load the textures
            myBackgroundTexture = new Texture("resources/sfml.png")
            {
                Smooth = true
            };
            myEntityTexture = new Texture("resources/devices.png")
            {
                Smooth = true
            };

            // Initialize the background sprite
            myBackgroundSprite = new Sprite(myBackgroundTexture);
            myBackgroundSprite.Position = new Vector2f(135, 100);

            // Load the moving entities
            myEntities = new Sprite[6];
            for (int i = 0; i < myEntities.Length; ++i)
            {
                myEntities[i] = new Sprite(myEntityTexture, new IntRect(96 * i, 0, 96, 96));
            }

            // Load the shader
            myShader = new Shader(null, null, "resources/edge.frag");
            myShader.SetUniform("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            myShader.SetUniform("edge_threshold", 1 - ( x + y ) / 2);

            // Update the position of the moving entities
            for (var i = 0; i < myEntities.Length; ++i)
            {
                float posX = (float)Math.Cos(0.25F * ( time * i + ( myEntities.Length - i ) )) * 300 + 350;
                float posY = (float)Math.Sin(0.25F * ( time * ( myEntities.Length - i ) + i )) * 200 + 250;
                myEntities[i].Position = new Vector2f(posX, posY);
            }

            // Render the updated scene to the off-screen surface
            mySurface.Clear(Color.White);
            mySurface.Draw(myBackgroundSprite);
            foreach (Sprite entity in myEntities)
            {
                mySurface.Draw(entity);
            }

            mySurface.Display();
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = myShader
            };
            target.Draw(new Sprite(mySurface.Texture), states);
        }

        private RenderTexture mySurface;
        private readonly Texture myBackgroundTexture;
        private readonly Texture myEntityTexture;
        private Sprite myBackgroundSprite;
        Sprite[] myEntities;
        private Shader myShader;
    }

    static class Program
    {
        private static Effect[] effects;
        private static int current;
        private static Text description;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Create the main window
            var window = new RenderWindow(new VideoMode(800, 600), "SFML.Net Shader");
            window.SetVerticalSyncEnabled(true);

            // Setup event handlers
            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;

            // Load the application font and pass it to the Effect class
            var font = new Font("resources/sansation.ttf");
            Effect.SetFont(font);

            // Create the effects
            effects = new Effect[]
            {
                new Pixelate(),
                new WaveBlur(),
                new StormBlink(),
                new Edge()
            };
            current = 0;

            // Create the messages background
            var textBackgroundTexture = new Texture("resources/text-background.png");
            var textBackground = new Sprite(textBackgroundTexture)
            {
                Position = new Vector2f(0, 520),
                Color = new Color(255, 255, 255, 200)
            };

            // Create the description text
            description = new Text("Current effect: " + effects[current].Name, font, 20)
            {
                Position = new Vector2f(10, 530),
                FillColor = new Color(80, 80, 80)
            };

            // Create the instructions text
            var instructions = new Text("Press left and right arrows to change the current shader", font, 20)
            {
                Position = new Vector2f(280, 555),
                FillColor = new Color(80, 80, 80)
            };

            // Start the game loop
            var clock = new Clock();
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Update the current example
                var x = (float)Mouse.GetPosition(window).X / window.Size.X;
                var y = (float)Mouse.GetPosition(window).Y / window.Size.Y;
                effects[current].Update(clock.ElapsedTime.AsSeconds(), x, y);

                // Clear the window
                window.Clear(new Color(255, 128, 0));

                // Draw the current example
                window.Draw(effects[current]);

                // Draw the text
                window.Draw(textBackground);
                window.Draw(instructions);
                window.Draw(description);

                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            var window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (RenderWindow)sender;

            // Escape key : exit
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }


            // Left arrow key: previous shader
            if (e.Code == Keyboard.Key.Left)
            {
                if (current == 0)
                {
                    current = effects.Length - 1;
                }
                else
                {
                    current--;
                }

                description.DisplayedString = $"Current effect: {effects[current].Name}";
            }

            // Right arrow key: next shader
            if (e.Code == Keyboard.Key.Right)
            {
                if (current == effects.Length - 1)
                {
                    current = 0;
                }
                else
                {
                    current++;
                }

                description.DisplayedString = $"Current effect: {effects[current].Name}";
            }
        }
    }
}
