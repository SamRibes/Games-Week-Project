using Microsoft.Xna.Framework.Graphics;

namespace Zombie_Attack
{
    class ExitButton : Button
    {
        public ExitButton(Texture2D buttonTexture)
        {
            texture = buttonTexture;
        }

        public override void Update()
        {
            if (ButtonRectangle.Contains(Input.MousePosition.ToPoint()) && Input.WasLeftMouseClicked())
            {
                ZombieGame.SetQuit = true;
            }
        }
    }
}
