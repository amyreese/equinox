using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Equinox.Audio;
using Equinox.Input;
using Equinox.Objects;
using Equinox.Video;

namespace Equinox.Screens
{
    /// <summary>
    /// Main menu for the game.
    /// </summary>
    class MainMenu : Screen
    {
        protected InputManager input;
        protected Renderer renderer;
        protected ContentManager contentManager;
        protected Scene scene;
        protected GameObject camera;
        protected AudioManager audio;

        GameObject arrow;

        public MainMenu(ScreenManager manager) : base(manager)
        {
            audio = new AudioManager();
            input = new InputManager(PlayerIndex.One);
            renderer = new Renderer(game);
            contentManager = new ContentManager(game.Services, "Resources");
        }

        protected override void LoadContent()
        {
            scene = new Scene();

            arrow = new GameObject();
            arrow.model = contentManager.Load<Model>("Models/Arrow");
            arrow.position = new Coords(0, 0, -50);

            scene.Add(arrow);

            camera = new GameObject();
            camera.position = new Coords(0, 0, 0);

            audio.MusicTrack("AdaptiveTest");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            input.Update();

            // Allows the game to exit
            if (input.Pressed(Buttons.Back))
                screenManager.game.Exit();

            if (input.Down(Buttons.A))
            {
                arrow.position.T *= Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            }
            if (input.Pressed(Buttons.X))
            {
                arrow.position = new Coords(0, 0, -50);
                audio.PlaySound("Bump");
            }

            audio.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            renderer.Draw(scene, camera);

            base.Draw(gameTime);
        }
    }
}
