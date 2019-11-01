using Microsoft.Xna.Framework.Graphics;
using System;

namespace Zombie_Attack
{
    class AddButton : Button
    {
        public AddButton(Texture2D buttonTexture)
        {
            Texture = buttonTexture;
        }

        public override void Update()
        {
            if (ButtonRectangle.Contains(Input.MousePosition.ToPoint()) && Input.WasLeftMouseClicked())
            {
                Console.WriteLine("Triggered");
            }
        }
    }
}
