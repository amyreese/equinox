using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Equinox.Video;

namespace Equinox.Screens
{
    /// <summary>
    /// Main menu for the game.
    /// </summary>
    class MainMenu : Screen
    {
        public MainMenu(ScreenManager manager) : base(manager)
        {
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
            GraphicsDevice.Clear(Color.WhiteSmoke);

            base.Draw(gameTime);
        }
    }
}
