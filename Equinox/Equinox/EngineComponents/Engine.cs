using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Equinox.Audio;
using Equinox.Input;
using Equinox.Objects;
using Equinox.Video;
using Equinox.Screens;

namespace Equinox.EngineComponents
{
    static class Engine
    {
        #region Members
        /// <summary>
        /// Engine GameTime
        /// </summary>
        public static GameTime GameTime
        {
            get { return _gameTime; }
            set { } 
        }
        private static GameTime _gameTime;

        /// <summary>
        /// Engine GraphicsDevice
        /// </summary>
        public static GraphicsDevice GraphicsDevice
        {
            get { return _graphicsDevice; }
            set { }
        }
        private static GraphicsDevice _graphicsDevice;

        /// <summary>
        /// Engine Scene
        /// </summary>
        public static Scene Scene
        {
            get { return _scene; }
            set { }
        }
        private static Scene _scene;

        /// <summary>
        /// Engine Camera
        /// </summary>
        public static GameObject Camera
        {
            get { return _camera; }
            set { }
        }
        private static GameObject _camera;

        /// <summary>
        /// Engine Audio Manager
        /// </summary>
        public static AudioManager AudioManager
        {
            get { return _audio; }
            set { }
        }
        private static AudioManager _audio;

        /// <summary>
        /// Engine Input Manager
        /// </summary>
        public static InputManager InputManager
        {
            get { return _input; }
            set { }
        }
        private static InputManager _input;

        /// <summary>
        /// Engine Renderer
        /// </summary>
        public static Renderer Renderer
        {
            get { return _renderer; }
            set { }
        }
        private static Renderer _renderer;

        /// <summary>
        /// Engine Content Manager
        /// </summary>
        public static ContentManager ContentManager
        {
            get { return _contentManager; }
            set { }
        }
        private static ContentManager _contentManager;

        
        private static Game _game;
        private static ScreenManager _screenManager;
        private static bool _isInitialized = false;
#endregion

         /// <summary>
        /// Initialize game engine
        /// </summary>
        public static void Initialize(Game game, IGraphicsDeviceService graphicsDevice, ContentManager content)
        {
            if (_isInitialized)
                return;

            _audio = new AudioManager();
            _input = new InputManager(PlayerIndex.One);
            _renderer = new Renderer(game);

            _game = game;
            _graphicsDevice = graphicsDevice.GraphicsDevice;
            _contentManager = content;

            _screenManager = new ScreenManager(game);
            _screenManager.Push(new MainMenu(_screenManager));

            _isInitialized = true;
        }

        /// <summary>
        /// Create a new scene
        /// </summary>
        /// <returns></returns>
        public static Scene NewScene()
        {
            // TODO:  Depending on how we do this, we may need
            //        need to unload content before setting up
            //        a new scene

            _scene = new Scene();
            _camera = new GameObject();

            _camera = new GameObject();
            _camera.position = new Coords(0, 0, -500);

            return _scene;
        }

        /// <summary>
        /// Update engine and all components
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public static void Update(GameTime gameTime)
        {
            _gameTime = gameTime;

            _input.Update();
            _audio.Update();
            
            // TODO:  We'll probably need something like this soon
            // _camera.Update()  
        }
    }
}
