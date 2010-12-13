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
        public static GraphicsDeviceManager Graphics;

        public static AudioManager Audio;
        public static InputManager Input;
        public static ScreenManager Screens;
        public static Renderer Renderer;

        /// <summary>
        /// Initialize the core engine subsystems.
        /// </summary>
        /// <param name="g">Game instance</param>
        public Engine(Game g, GraphicsDeviceManager graphicsDevice)
            : base(g)
        {
            Log("New Engine");

            game = g;
            Graphics = graphicsDevice;
                        
            Audio = new AudioManager();
            game.Components.Add(Audio);

            Input = new InputManager(PlayerIndex.One);
            game.Components.Add(Input);

            Screens = new ScreenManager();
            Screens.Push(new MainMenu());

            Renderer = new Renderer();
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
