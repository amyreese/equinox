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
        public ScreenManager() : base()
        {
        }

        /// <summary>
        /// Push and set the new top screen.
        /// </summary>
        /// <param name="screen"></param>
        public new void Push(Screen screen)
        {
            if (Count > 0)
            {
                Engine.game.Components.Remove(Peek());
            }

            Engine.game.Components.Add(screen);

            base.Push(screen);
        }

        /// <summary>
        /// Pop and unset the current top screen.
        /// </summary>
        /// <returns></returns>
        public new Screen Pop()
        {
            Screen current = base.Pop();
            Engine.game.Components.Remove(current);
            Engine.game.Components.Add(Peek());

            return current;
        }
    }
}
