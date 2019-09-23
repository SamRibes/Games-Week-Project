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
            menuList.Add(new Button((centerX-((centerX / 5)/2)), centerY, centerX / 5, centerY / 10, texture));
            int gap = 10;
            int buttonHeight = 50 + gap;
            int totalHeight = buttonHeight * menuList.Count;

            for (int i = 0; i < menuList.Count; i++)
            {
                menuList[i].YPosition = (i * buttonHeight) + (centerY + (totalHeight / 2));
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
