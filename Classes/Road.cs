using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    /// <summary>
    /// Road только отрисовывает дорогу)
    /// </summary>
    public class Road
    {
        public Transform Transform;

        // Конструктор Road, принимающий размеры и положение дороги в окне WinForms
        public Road(PointF pos, Size size)
        {
            Transform = new Transform(pos, size);
        }

        // Отрисовка вырезанного из Spritesheet'а куска дороги
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(GameController.Spritesheet, new Rectangle(
                new Point((int)Transform.Position.X, (int)Transform.Position.Y), 
                new Size(Transform.Size.Width, Transform.Size.Height)), 
                0, 112, 200, 17, GraphicsUnit.Pixel);
        }
    }
}
