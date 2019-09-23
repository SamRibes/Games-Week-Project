using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class Button
    {
        private int x, height, width;
        private Texture2D texture;
        public int YPosition {get;set;}

        public Button(Texture2D texture)
        {
            this.texture = texture;
            this.x = 0;
            this.YPosition = 0;
            this.height = 0;
            this.width = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, YPosition, width, height), Color.White); 
        }
        
    }
}
