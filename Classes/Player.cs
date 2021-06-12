using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Player
    {
        public Physics Physics;
        public int FramesCount = 0;
        public int AnimationCount = 0;
        public int Score = 0;

        public Player(PointF position, Size size)
        {
            Physics = new Physics(position, size);
            FramesCount = 0;
            Score = 0;
        }

        public void DrawSprite(Graphics g)
        {
            if (Physics.IsCrouching)
            {
                DrawNeededSprite(g, 1870, 40, 109, 51, 118, 1.35f);
            }
            else
            {
                DrawNeededSprite(g, 1518, 0, 79, 91, 88, 1);
            }
        }

        private void DrawNeededSprite(Graphics g, int srcX, int srcY, int width, int height, int delta, float multiplier)
        {
            FramesCount++;
            if (FramesCount <= 10)
                AnimationCount = 0;
            else if (FramesCount > 10 && FramesCount <= 20)
                AnimationCount = 1;
            else if (FramesCount > 20)
                FramesCount = 0;

            g.DrawImage(GameController.Spritesheet, new Rectangle(
                new Point((int)Physics.Transform.Position.X, (int)Physics.Transform.Position.Y),
                new Size((int)(Physics.Transform.Size.Width * multiplier), Physics.Transform.Size.Height)),
                srcX + delta * AnimationCount, srcY, width, height, GraphicsUnit.Pixel);
        }
    }

    public static class PlayerExtensions
    {
        public static void Jump(this Player player)
        {
            if (player.Physics.IsCrouching) return;
            player.Physics.IsCrouching = false;
            player.Physics.AddForce();
        }
        
        public static void Crouch(this Player player)
        {
            player.Physics.IsCrouching = false;
            player.Physics.Transform.Size.Height = 50;
            player.Physics.Transform.Position.Y = 150.2f;
        }
    }
}
