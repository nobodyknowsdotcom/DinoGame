using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoGame.Classes
{
    public class Cactus
    {
        public Transform transform;
        int cactusImageXCoordinate = 0;

        public Cactus(PointF pos, Size size)
        {
            transform = new Transform(pos, size);
            ChooseRandomPic();
        }

        public void ChooseRandomPic()
        {
            Random randomGenegator = new Random();
            int randomNumber = randomGenegator.Next(0, 4);
            switch (randomNumber)
            {
                case 0:
                    cactusImageXCoordinate = 804;
                    break;
                case 1:
                    cactusImageXCoordinate = 754;
                    break;
                case 2:
                    cactusImageXCoordinate = 704;
                    break;
                case 3:
                    cactusImageXCoordinate = 654;
                    break;
            }
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(GameController.spriteSheet,
                new Rectangle(
                    new Point((int)transform.position.X, (int)transform.position.Y),
                    new Size(transform.size.Width, transform.size.Height)),
                cactusImageXCoordinate, 0, 48, 100, GraphicsUnit.Pixel);
        }
    }
}
