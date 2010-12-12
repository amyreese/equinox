using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Equinox.EngineComponents;
using Equinox.Objects;
using Equinox.Video;

namespace Equinox.Screens
{
    /// <summary>
    /// Main menu for the game.
    /// </summary>
    class MainMenu : Screen
    {
        private Scene _scene;
        private GameObject arrow;
        private GameObject truck;
        private GameObject lizard;

        public MainMenu(ScreenManager manager) : base(manager)
        {
        }

        protected override void LoadContent()
        {
            _scene = Engine.NewScene();

            arrow = new GameObject();
            arrow.model = Engine.ContentManager.Load<Model>("Models/Arrow");
            arrow.position = new Coords(-150, 100, 0);
            arrow.scale = 10f;
            arrow.position.R *= Quaternion.CreateFromYawPitchRoll(90f, 90f, 90f);

            _scene.Add(arrow);

            truck = new GameObject();
            truck.model = Engine.ContentManager.Load<Model>("Models/L200-FBX");
            truck.position = new Coords(0, -20, 0);
            truck.position.R *= Quaternion.CreateFromYawPitchRoll(90f, 0, 0);
            truck.scale = 0.25f;

            _scene.Add(truck);

            lizard = new GameObject();
            lizard.model = Engine.ContentManager.Load<Model>("Models/lizard");
            lizard.position = new Coords(150, -50, -200);
            lizard.scale = 0.25f;

            _scene.Add(lizard);

            Engine.AudioManager.MusicTrack("AdaptiveTest");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Engine.InputManager.Pressed(Buttons.Back))
                screenManager.game.Exit();

            if (Engine.InputManager.Down(Buttons.A))
            {
                arrow.position.T *= Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            }
            if (Engine.InputManager.Down(Keys.Right))
            {
                lizard.position.R *= Quaternion.CreateFromYawPitchRoll(-0.05f, 0, 0);
                truck.position.T *= Matrix.CreateTranslation(new Vector3(1f, 0f, 0f));
            }
            else if (Engine.InputManager.Down(Keys.Left))
            {
                lizard.position.R *= Quaternion.CreateFromYawPitchRoll(0.05f, 0, 0);
                truck.position.T *= Matrix.CreateTranslation(new Vector3(-1f, 0f, 0f));
            }

            if (Engine.InputManager.Pressed(Buttons.X))
            {
                arrow.position = new Coords(0, 0, -50);
                Engine.AudioManager.PlaySound("Bump");
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Engine.Renderer.Draw(Engine.Scene, Engine.Camera);

            base.Draw(gameTime);
        }
    }
}
