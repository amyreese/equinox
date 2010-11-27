using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Equinox;

namespace Equinox.Input
{
    class InputManager
    {
        protected PlayerIndex player;
        protected GamePadState gamepad;
        protected KeyboardState keyboard;

        private List<Buttons> buttonList;
        private List<Keys> keyList;

        private Dictionary<Buttons, bool> pressedButtons;
        private Dictionary<Keys, bool> pressedKeys;

        private Dictionary<Buttons, bool> downButtons;
        private Dictionary<Keys, bool> downKeys;

        /// <summary>
        /// Create a new input manager for a given player index.
        /// </summary>
        /// <param name="playerIndex">Player index</param>
        public InputManager(PlayerIndex playerIndex)
        {
            player = playerIndex;

            pressedButtons = new Dictionary<Buttons, bool>();
            pressedKeys = new Dictionary<Keys, bool>();

            downButtons = new Dictionary<Buttons, bool>();
            downKeys = new Dictionary<Keys, bool>();

            keyList = new List<Keys>();
            Type keyType = typeof(Keys);
            for (int i = 0; i < 255; i++)
            {
                if (Enum.IsDefined(keyType, i))
                {
                    Keys key = (Keys) i;
                    keyList.Add(key);
                    pressedKeys[key] = false;
                    downKeys[key] = false;

                    Equinox.Log("Watching key " + key);
                }
            }

            buttonList = new List<Buttons>();
            Type buttonType = typeof(Buttons);
            for (int i = 1; i < int.MaxValue / 2; i *= 2)
            {
                if (Enum.IsDefined(buttonType, i))
                {
                    Buttons button = (Buttons)i;
                    buttonList.Add(button);
                    pressedButtons[button] = false;
                    downButtons[button] = false;

                    Equinox.Log("Watching button " + button);
                }
            }
        }

        /// <summary>
        /// Update the input button and key states.
        /// </summary>
        public void Update()
        {
            gamepad = GamePad.GetState(player);
            keyboard = Keyboard.GetState();

            foreach (Buttons button in buttonList)
            {
                bool down = gamepad.IsButtonDown(button);

                if (downButtons[button])
                {
                    pressedButtons[button] = false;
                }
                else if (down)
                {
                    pressedButtons[button] = true;
                    Equinox.Log("User pressed button " + button);
                }

                downButtons[button] = down;
            }

            foreach (Keys key in keyList)
            {
                bool down = keyboard.IsKeyDown(key);

                if (downKeys[key])
                {
                    pressedKeys[key] = false;
                }
                else if (down)
                {
                    pressedKeys[key] = true;
                    Equinox.Log("User pressed key " + key);
                }

                downKeys[key] = down;
            }
        }

        /// <summary>
        /// Determines if a gamepad button has been pressed since the last update.
        /// </summary>
        /// <param name="button">Button</param>
        /// <returns>True if button has been pressed</returns>
        public bool Pressed(Buttons button)
        {
            return pressedButtons[button];
        }

        /// <summary>
        /// Determines if a keyboard key has been pressed since the last update.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True if key has been pressed</returns>
        public bool Pressed(Keys key)
        {
            return pressedKeys[key];
        }

        /// <summary>
        /// Determines if a gamepad button is currently being held down.
        /// </summary>
        /// <param name="button">Button</param>
        /// <returns>True if button is currently held down</returns>
        public bool Down(Buttons button)
        {
            return downButtons[button];
        }

        /// <summary>
        /// Determines if a keyboard key is currently being held down.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True if key is currently held down</returns>
        public bool Down(Keys key)
        {
            return downKeys[key];
        }
    }
}
