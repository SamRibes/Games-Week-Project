using Microsoft.Xna.Framework.Graphics;
using System;

namespace Zombie_Attack
{
    class StartButton : Button
    {
        public StartButton(Texture2D buttonTexture)
        {
            texture = buttonTexture;
        }

        public override void Update()
        {
            if (ButtonRectangle.Contains(Input.MousePosition.ToPoint()) && Input.WasLeftMouseClicked())
            {
                ZombieGame._state = ZombieGame.GameState.MainGame;
            }
        }
    }
}
