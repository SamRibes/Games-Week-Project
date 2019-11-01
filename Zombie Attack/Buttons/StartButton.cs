using Microsoft.Xna.Framework.Graphics;
using System;

namespace Zombie_Attack
{
    class StartButton : Button
    {
        public StartButton(Texture2D buttonTexture)
        {
            Texture = buttonTexture;
        }

        public override void Update()
        {
            if (ButtonRectangle.Contains(Input.MousePosition.ToPoint()) && Input.WasLeftMouseClicked())
            {
                ZombieGame.State = ZombieGame.GameState.MainGame;
            }
        }
    }
}
