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
        protected ContentManager contentManager;
        protected Scene scene;
        protected GameObject camera;

        GameObject arrow;

        public MainMenu() : base()
        {
            contentManager = new ContentManager(Engine.game.Services, "Resources");
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

            Engine.Audio.MusicTrack("AdaptiveTest");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Engine.Input.Pressed(Buttons.Back))
                Engine.game.Exit();

            if (Engine.Input.Down(Buttons.A))
            {
                arrow.position.T *= Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            }
            if (Engine.Input.Pressed(Buttons.X))
            {
                arrow.position = new Coords(0, 0, -50);
                Engine.Audio.PlaySound("Bump");
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.Renderer.Draw(scene, camera);

            base.Draw(gameTime);
        }
    }
}
