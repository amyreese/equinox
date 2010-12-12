using System;
using Microsoft.Xna.Framework;

using Equinox.EngineComponents;

namespace Equinox
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Equinox : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Equinox()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Resources";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Engine.Initialize(this, graphics, this.Content);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            Equinox game = new Equinox();
            game.Run();
        }

        public static void Log(string message)
        {
#if DEBUG
#if WINDOWS
            Console.Out.WriteLine(message);
#endif
#endif
        }
    }
}
