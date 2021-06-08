using System;
using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Cactus
    {
        public readonly Transform transform;
        // sourceX - это иксовая координата начала вырезаемого спрайта из спрайтшита
        // используется в ChooseRandomPic для разнообразия видов кактусов в игре
        int _sourceX;

        public Cactus(PointF pos,Size size)
        {
            transform = new Transform(pos, size);
            ChooseRandomPic();
        }

        // Метод рандомно выбирает, какой из спрайтов кактуса
        // будет вырезан и передан в метод DrawSprite
        private void ChooseRandomPic()
        {
            var r = new Random();
            var rnd = r.Next(0, 4);
            switch (rnd)
            {
                case 0:
                    _sourceX = 754;
                    break;
                case 1:
                    _sourceX = 804;
                    break;
                case 2:
                    _sourceX = 704;
                    break;
                case 3:
                    _sourceX = 654;
                    break;
            }
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(GameController.spritesheet, new Rectangle(
                new Point((int)transform.position.X, (int)transform.position.Y), 
                new Size(transform.size.Width, transform.size.Height)),
                _sourceX, 0, 48, 100, GraphicsUnit.Pixel);
        }
    }
}
