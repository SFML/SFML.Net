using System;
using SFML.Graphics;
using SFML.System;

namespace BouncingBall {
	static class Program {

		static void Main( string[] args ) {
			Console.WriteLine( "Press ESC key to close window" );
			var window = new SimpleWindow();
			window.Run();

			Console.WriteLine( "All done" );
		}
	}

	class SimpleWindow {
		public void Run() {
			var mode = new SFML.Window.VideoMode( 800, 600 );
			var window = new SFML.Graphics.RenderWindow( mode, "SFML works!" );
			window.KeyPressed += Window_KeyPressed;

			var ballSprite = new SFML.Graphics.CircleShape( 40f ) {
				FillColor = SFML.Graphics.Color.Blue,
			};
			var random = new Random( DateTime.Now.Millisecond );    // init random generator

			var vector = new SFML.System.Vector2i() {
				X = random.Next( 20 ),
				Y = random.Next( 20 )
			};
			uint frameRate = 30;
			Console.WriteLine($"Running at {frameRate} fps");
			window.SetFramerateLimit( frameRate );


			// Start the game loop
			while (window.IsOpen) {
				window.Clear();

				// Process events
				window.DispatchEvents();

				CalculateSpriteLocation( window.Size, ballSprite, ref vector );
				window.Draw( ballSprite );

				// Finally, display the rendered frame on screen
				window.Display();
			}
		}

		private void CalculateSpriteLocation( Vector2u box, CircleShape sprite, ref Vector2i direction ) {
			sprite.Position = new Vector2f( sprite.Position.X + direction.X, sprite.Position.Y + direction.Y );

			if (( sprite.Position.X + 2*sprite.Radius ) > box.X || sprite.Position.X < 0)
				direction.X = -direction.X; // reverse direction
			if (( sprite.Position.Y + 2*sprite.Radius ) > box.Y || sprite.Position.Y < 0)
				direction.Y = -direction.Y; // reverse direction
		}


		/// <summary>
		/// Function called when a key is pressed
		/// </summary>
		private void Window_KeyPressed( object sender, SFML.Window.KeyEventArgs e ) {
			var window = (SFML.Window.Window) sender;
			if (e.Code == SFML.Window.Keyboard.Key.Escape)
				window.Close();
		}
	}

}
