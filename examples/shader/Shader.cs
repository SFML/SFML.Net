using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace shader
{
    /// <summary>Base class for effects</summary>
    abstract class Effect : Drawable
    {
        protected Effect(string name)
        {
            myName = name;
        }

        public string Name
        {
            get {return myName;}
        }

        public void Update(float time, float x, float y)
        {
            if (Shader.IsAvailable)
                OnUpdate(time, x, y);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (Shader.IsAvailable)
            {
                OnDraw(target, states);
            }
            else
            {
                Text error = new Text("Shader not\nsupported", GetFont());
                error.Position = new Vector2f(320, 200);
                error.CharacterSize = 36;
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

        private string myName;
        private static Font ourFont = null;
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
            myShader = new Shader(null, "resources/pixelate.frag");
            myShader.SetParameter("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            myShader.SetParameter("pixel_threshold", (x + y) / 30);
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states);
            states.Shader = myShader;
            target.Draw(mySprite, states);
        }

        private Texture myTexture = null;
        private Sprite mySprite = null;
        private Shader myShader = null;
    }

    /// <summary>"Wave" vertex shader + "blur" fragment shader</summary>
    class WaveBlur : Effect
    {
        public WaveBlur() : base("wave + blur")
        {
            // Create the text
            myText = new Text();
            myText.DisplayedString = "Praesent suscipit augue in velit pulvinar hendrerit varius purus aliquam.\n" +
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
                                     "In hac habitasse platea dictumst. Etiam fringilla est id odio dapibus sit amet semper dui laoreet.\n";
            myText.Font = GetFont();
            myText.CharacterSize = 22;
            myText.Position = new Vector2f(30, 20);

            // Load the shader
            myShader = new Shader("resources/wave.vert", "resources/blur.frag");
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            myShader.SetParameter("wave_phase", time);
            myShader.SetParameter("wave_amplitude", x * 40, y * 40);
            myShader.SetParameter("blur_radius", (x + y) * 0.008F);
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states);
            states.Shader = myShader;
            target.Draw(myText, states);
        }

        private Text myText = null;
        private Shader myShader = null;
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
                float x = (float)random.Next(0, 800);
                float y = (float)random.Next(0, 600);
                byte r = (byte)random.Next(0, 255);
                byte g = (byte)random.Next(0, 255);
                byte b = (byte)random.Next(0, 255);
                myPoints.Append(new Vertex(new Vector2f(x, y), new Color(r, g, b)));
            }

            // Load the shader
            myShader = new Shader("resources/storm.vert", "resources/blink.frag");
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            float radius = 200 + (float)Math.Cos(time) * 150;
            myShader.SetParameter("storm_position", x * 800, y * 600);
            myShader.SetParameter("storm_inner_radius", radius / 3);
            myShader.SetParameter("storm_total_radius", radius);
            myShader.SetParameter("blink_alpha", 0.5F + (float)Math.Cos(time * 3) * 0.25F);
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states);
            states.Shader = myShader;
            target.Draw(myPoints, states);
        }

        private VertexArray myPoints = null;
        private Shader myShader = null;
    }

    /// <summary>"Edge" post-effect fragment shader</summary>
    class Edge : Effect
    {
        public Edge() : base("edge post-effect")
        {
            // Create the off-screen surface
            mySurface = new RenderTexture(800, 600);
            mySurface.Smooth = true;

            // Load the textures
            myBackgroundTexture = new Texture("resources/sfml.png");
            myBackgroundTexture.Smooth = true;
            myEntityTexture = new Texture("resources/devices.png");
            myEntityTexture.Smooth = true;

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
            myShader = new Shader(null, "resources/edge.frag");
            myShader.SetParameter("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            myShader.SetParameter("edge_threshold", 1 - (x + y) / 2);

            // Update the position of the moving entities
            for (int i = 0; i < myEntities.Length; ++i)
            {
                float posX = (float)Math.Cos(0.25F * (time * i + (myEntities.Length - i))) * 300 + 350;
                float posY = (float)Math.Sin(0.25F * (time * (myEntities.Length - i) + i)) * 200 + 250;
                myEntities[i].Position = new Vector2f(posX, posY);
            }

            // Render the updated scene to the off-screen surface
            mySurface.Clear(Color.White);
            mySurface.Draw(myBackgroundSprite);
            foreach (Sprite entity in myEntities)
                mySurface.Draw(entity);
            mySurface.Display();
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states);
            states.Shader = myShader;
            target.Draw(new Sprite(mySurface.Texture), states);
        }

        private RenderTexture mySurface = null;
        private Texture myBackgroundTexture = null;
        private Texture myEntityTexture = null;
        private Sprite myBackgroundSprite = null;
        Sprite[] myEntities = null;
        private Shader myShader = null;
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
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML.Net Shader");
            window.SetVerticalSyncEnabled(true);

            // Setup event handlers
            window.Closed     += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

            // Load the application font and pass it to the Effect class
            Font font = new Font("resources/sansation.ttf");
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
            Texture textBackgroundTexture = new Texture("resources/text-background.png");
            Sprite textBackground = new Sprite(textBackgroundTexture);
            textBackground.Position = new Vector2f(0, 520);
            textBackground.Color = new Color(255, 255, 255, 200);

            // Create the description text
            description = new Text("Current effect: " + effects[current].Name, font, 20);
            description.Position = new Vector2f(10, 530);
            description.Color = new Color(80, 80, 80);

            // Create the instructions text
            Text instructions = new Text("Press left and right arrows to change the current shader", font, 20);
            instructions.Position = new Vector2f(280, 555);
            instructions.Color = new Color(80, 80, 80);

            // Start the game loop
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                // Process events
                window.DispatchEvents();

                // Update the current example
                float x = (float)Mouse.GetPosition(window).X / window.Size.X;
                float y = (float)Mouse.GetPosition(window).Y / window.Size.Y;
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
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;

            // Escape key : exit
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }

            // Left arrow key: previous shader
            if (e.Code == Keyboard.Key.Left)
            {
                if (current == 0)
                    current = effects.Length - 1;
                else
                    current--;
                description.DisplayedString = "Current effect: " + effects[current].Name;
            }

            // Right arrow key: next shader
            if (e.Code == Keyboard.Key.Right)
            {
                if (current == effects.Length - 1)
                    current = 0;
                else
                    current++;
                description.DisplayedString = "Current effect: " + effects[current].Name;
            }
        }
    }
}
