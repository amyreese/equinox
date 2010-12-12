using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Equinox.Audio;
using Equinox.Input;
using Equinox.Screens;
using Equinox.Video;

namespace Equinox
{
    class Engine : GameComponent
    {
        public static Game game;
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static GraphicsDevice graphics;

        public static AudioManager audio;
        public static InputManager input;
        public static ScreenManager screens;
        public static Renderer render;

        /// <summary>
        /// Initialize the core engine subsystems.
        /// </summary>
        /// <param name="g">Game instance</param>
        public Engine(Game g)
            : base(g)
        {
            Log("New Engine");

            game = g;

            graphicsDeviceManager = new GraphicsDeviceManager(game);
            graphics = graphicsDeviceManager.GraphicsDevice;

            audio = new AudioManager();
            game.Components.Add(audio);

            input = new InputManager(PlayerIndex.One);
            game.Components.Add(input);

            screens = new ScreenManager();
            screens.Push(new MainMenu());

            render = new Renderer();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Log an event to the debug console.
        /// </summary>
        /// <param name="message">Log message</param>
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
