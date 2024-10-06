using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace shader
{
    /// <summary>Base class for effects</summary>
    internal abstract class Effect : IDrawable
    {
        protected Effect(string name) => Name = name;

        public string Name { get; }
        public void Update(float time, float x, float y)
        {
            if (Shader.IsAvailable)
            {
                OnUpdate(time, x, y);
            }
        }

        public void Draw(IRenderTarget target, RenderStates states)
        {
            if (Shader.IsAvailable)
            {
                OnDraw(target, states);
            }
            else
            {
                var error = new Text(Font, "Shader not\nsupported")
                {
                    Position = new Vector2f(320, 200),
                    CharacterSize = 36
                };
                target.Draw(error, states);
            }
        }

        protected abstract void OnUpdate(float time, float x, float y);
        protected abstract void OnDraw(IRenderTarget target, RenderStates states);

        public static Font Font { get; set; }
    }

    /// <summary>"Pixelate" fragment shader</summary>
    internal class Pixelate : Effect
    {
        public Pixelate() : base("pixelate")
        {
            // Load the texture and initialize the sprite
            _texture = new Texture("resources/background.jpg");
            _sprite = new Sprite(_texture);

            // Load the shader
            _shader = new Shader(null, null, "resources/pixelate.frag");
            _shader.SetUniform("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(float time, float x, float y) => _shader.SetUniform("pixel_threshold", (x + y) / 30);

        protected override void OnDraw(IRenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = _shader
            };
            target.Draw(_sprite, states);
        }

        private readonly Texture _texture;
        private readonly Sprite _sprite;
        private readonly Shader _shader;
    }

    /// <summary>"Wave" vertex shader + "blur" fragment shader</summary>
    internal class WaveBlur : Effect
    {
        public WaveBlur() : base("wave + blur")
        {
            // Create the text
            _text = new Text(Font)
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
                CharacterSize = 22,
                Position = new Vector2f(30, 20)
            };

            // Load the shader
            _shader = new Shader("resources/wave.vert", null, "resources/blur.frag");
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            _shader.SetUniform("wave_phase", time);
            _shader.SetUniform("wave_amplitude", new Vector2f(x * 40, y * 40));
            _shader.SetUniform("blur_radius", (x + y) * 0.008F);
        }

        protected override void OnDraw(IRenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = _shader
            };
            target.Draw(_text, states);
        }

        private readonly Text _text;
        private readonly Shader _shader;
    }

    /// <summary>"Storm" vertex shader + "blink" fragment shader</summary>
    internal class StormBlink : Effect
    {
        public StormBlink() : base("storm + blink")
        {
            var random = new Random();

            // Create the points
            _points = new VertexArray(PrimitiveType.Points);
            for (var i = 0; i < 40000; ++i)
            {
                float x = random.Next(0, 800);
                float y = random.Next(0, 600);
                var r = (byte)random.Next(0, 255);
                var g = (byte)random.Next(0, 255);
                var b = (byte)random.Next(0, 255);
                _points.Append(new Vertex(new Vector2f(x, y), new Color(r, g, b)));
            }

            // Load the shader
            _shader = new Shader("resources/storm.vert", null, "resources/blink.frag");
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            var radius = 200 + ((float)Math.Cos(time) * 150);
            _shader.SetUniform("storm_position", new Vector2f(x * 800, y * 600));
            _shader.SetUniform("storm_inner_radius", radius / 3);
            _shader.SetUniform("storm_total_radius", radius);
            _shader.SetUniform("blink_alpha", 0.5F + ((float)Math.Cos(time * 3) * 0.25F));
        }

        protected override void OnDraw(IRenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = _shader
            };
            target.Draw(_points, states);
        }

        private readonly VertexArray _points;
        private readonly Shader _shader;
    }

    /// <summary>"Edge" post-effect fragment shader</summary>
    internal class Edge : Effect
    {
        public Edge() : base("edge post-effect")
        {
            // Create the off-screen surface
            _surface = new RenderTexture((800, 600))
            {
                Smooth = true
            };

            // Load the textures
            _backgroundTexture = new Texture("resources/sfml.png")
            {
                Smooth = true
            };
            _entityTexture = new Texture("resources/devices.png")
            {
                Smooth = true
            };

            // Initialize the background sprite
            _backgroundSprite = new Sprite(_backgroundTexture)
            {
                Position = new Vector2f(135, 100)
            };

            // Load the moving entities
            _entities = new Sprite[6];
            for (var i = 0; i < _entities.Length; ++i)
            {
                _entities[i] = new Sprite(_entityTexture, new IntRect((96 * i, 0), (96, 96)));
            }

            // Load the shader
            _shader = new Shader(null, null, "resources/edge.frag");
            _shader.SetUniform("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(float time, float x, float y)
        {
            _shader.SetUniform("edge_threshold", 1 - ((x + y) / 2));

            // Update the position of the moving entities
            for (var i = 0; i < _entities.Length; ++i)
            {
                var posX = ((float)Math.Cos(0.25F * ((time * i) + (_entities.Length - i))) * 300) + 350;
                var posY = ((float)Math.Sin(0.25F * ((time * (_entities.Length - i)) + i)) * 200) + 250;
                _entities[i].Position = new Vector2f(posX, posY);
            }

            // Render the updated scene to the off-screen surface
            _surface.Clear(Color.White);
            _surface.Draw(_backgroundSprite);
            foreach (var entity in _entities)
            {
                _surface.Draw(entity);
            }

            _surface.Display();
        }

        protected override void OnDraw(IRenderTarget target, RenderStates states)
        {
            states = new RenderStates(states)
            {
                Shader = _shader
            };
            target.Draw(new Sprite(_surface.Texture), states);
        }

        private readonly RenderTexture _surface;
        private readonly Texture _backgroundTexture;
        private readonly Texture _entityTexture;
        private readonly Sprite _backgroundSprite;
        private readonly Sprite[] _entities;
        private readonly Shader _shader;
    }

    internal static class Program
    {
        private static Effect[] _effects;
        private static int _current;
        private static Text _description;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            // Create the main window
            var window = new RenderWindow(new VideoMode((800, 600)), "SFML.Net Shader");
            window.SetVerticalSyncEnabled(true);

            // Setup event handlers
            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;

            // Load the application font and pass it to the Effect class
            var font = new Font("resources/sansation.ttf");
            Effect.Font = font;

            // Create the effects
            _effects = new Effect[]
            {
                new Pixelate(),
                new WaveBlur(),
                new StormBlink(),
                new Edge()
            };
            _current = 0;

            // Create the messages background
            var textBackgroundTexture = new Texture("resources/text-background.png");
            var textBackground = new Sprite(textBackgroundTexture)
            {
                Position = new Vector2f(0, 520),
                Color = new Color(255, 255, 255, 200)
            };

            // Create the description text
            _description = new Text(font, "Current effect: " + _effects[_current].Name, 20)
            {
                Position = new Vector2f(10, 530),
                FillColor = new Color(80, 80, 80)
            };

            // Create the instructions text
            var instructions = new Text(font, "Press left and right arrows to change the current shader", 20)
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
                _effects[_current].Update(clock.ElapsedTime.AsSeconds(), x, y);

                // Clear the window
                window.Clear(new Color(255, 128, 0));

                // Draw the current example
                window.Draw(_effects[_current]);

                // Draw the text
                window.Draw(textBackground);
                window.Draw(instructions);
                window.Draw(_description);

                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        private static void OnClosed(object sender, EventArgs e)
        {
            var window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        private static void OnKeyPressed(object sender, KeyEventArgs e)
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
                if (_current == 0)
                {
                    _current = _effects.Length - 1;
                }
                else
                {
                    _current--;
                }

                _description.DisplayedString = $"Current effect: {_effects[_current].Name}";
            }

            // Right arrow key: next shader
            if (e.Code == Keyboard.Key.Right)
            {
                if (_current == _effects.Length - 1)
                {
                    _current = 0;
                }
                else
                {
                    _current++;
                }

                _description.DisplayedString = $"Current effect: {_effects[_current].Name}";
            }
        }
    }
}
