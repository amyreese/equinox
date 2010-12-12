using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Equinox.Video
{
    /// <summary>
    /// Generalized screen that can handle updates and draw itself.
    /// </summary>
    class Screen : DrawableGameComponent
    {
        /// <summary>
        /// Initialize a screen.
        /// </summary>
        /// <param name="manager"></param>
        public Screen() : base(Engine.game)
        {
        }
    }
}
