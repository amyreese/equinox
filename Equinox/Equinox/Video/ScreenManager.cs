using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Equinox.Video
{
    /// <summary>
    /// Manages a stack of game screens and only updates and draws the top screen.
    /// </summary>
    class ScreenManager : Stack<Screen>
    {
        private Game _game;
        public Game game
        {
            get
            {
                return _game;
            }
        }

        public ScreenManager(Game game) : base()
        {
            _game = game;
        }

        /// <summary>
        /// Push and set the new top screen.
        /// </summary>
        /// <param name="screen"></param>
        public new void Push(Screen screen)
        {
            if (Count > 0)
            {
                game.Components.Remove(Peek());
            }

            game.Components.Add(screen);

            base.Push(screen);
        }

        /// <summary>
        /// Pop and unset the current top screen.
        /// </summary>
        /// <returns></returns>
        public new Screen Pop()
        {
            Screen current = base.Pop();
            game.Components.Remove(current);
            game.Components.Add(Peek());

            return current;
        }
    }
}
