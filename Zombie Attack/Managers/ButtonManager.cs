using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class ButtonManager
    {
        private static List<Button> menuList = new List<Button>();
        private static int centerX = ZombieGame.Viewport.Width / 2;
        private static int centerY = ZombieGame.Viewport.Height / 2;

        public static void Add(Texture2D texture)
        {
            menuList.Add(new Button(texture));
            int gap = 10;
            int buttonHeight = 100;
            int totalHeight = (buttonHeight * menuList.Count) + (gap * menuList.Count);

            int count = 0;
            foreach (var button in menuList)
            {
                count++;
                button.YPosition = centerY;
            }
        }

        public static void Update()
        {
            foreach (var button in menuList)
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
