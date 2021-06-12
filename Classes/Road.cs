using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Road
    {
        public Transform Transform;

        public Road(PointF pos, Size size)
        {
            Transform = new Transform(pos, size);
        }

        public void DrawSprite(Graphics g)
        
        {
            g.DrawImage(GameController.Spritesheet, new Rectangle(
                new Point((int)Transform.Position.X, (int)Transform.Position.Y), 
                new Size(Transform.Size.Width, Transform.Size.Height)), 
                2300, 112, 100, 17, GraphicsUnit.Pixel);
        }
    }
}
