using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Zombie_Attack
{
    class ButtonManager
    {
        private static List<Button> menuList = new List<Button>();
        private static int centerX = ZombieGame.Viewport.Width / 2;
        private static int centerY = ZombieGame.Viewport.Height / 2;

        public static void Add(Button button)
        {
            if (button is StartButton)
            {
                menuList.Add(button as StartButton);
            }
            if (button is ExitButton)
            {
                menuList.Add(button as ExitButton);
            }

            int gap = 10;
            int buttonHeight = button.Height;
            int totalHeight = (buttonHeight * menuList.Count) + (gap * menuList.Count);

            int count = 0;
            foreach (var x in menuList)
            {
                x.Y = (centerY - (totalHeight/2)) + ((buttonHeight + gap) * count);
                count++;
            }
        }

        public static void ClearButtons()
        {
            menuList.Clear();
        }

        public static void Update()
        {
            foreach (Button button in menuList)
            {
                button.Update();
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var button in menuList)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
