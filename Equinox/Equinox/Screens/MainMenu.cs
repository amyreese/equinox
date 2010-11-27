using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Equinox.Video;
using Equinox.Objects;

namespace Equinox.Screens
{
    /// <summary>
    /// Main menu for the game.
    /// </summary>
    class MainMenu : Screen
    {
        protected Renderer renderer;
        protected ContentManager contentManager;
        protected Scene scene;
        protected GameObject camera;

        public MainMenu(ScreenManager manager) : base(manager)
        {
            renderer = new Renderer(game);
            contentManager = new ContentManager(game.Services, "Resources");
        }

        protected override void LoadContent()
        {
            scene = new Scene();

            GameObject arrow = new GameObject();
            arrow.model = contentManager.Load<Model>("Models/Arrow");
            arrow.position = new Coords(0, 0, -50);

            scene.Add(arrow);

            camera = new GameObject();
            camera.position = new Coords(0, 0, 0);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                screenManager.game.Exit();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            renderer.Draw(scene, camera);

            base.Draw(gameTime);
        }
    }
}
