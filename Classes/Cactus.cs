using System;
using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    /// <summary>
    /// Класс Cactuses отвечает за кактусы.
    /// А кроме шуток, он имеет конструктор
    /// Для присвоения экземпляру класса размера и положения в окне WinForms;
    /// Также его задача - рандомно выбирать один из четырех кактусов из Spritesheet'а
    /// Рандомно выбранный спрайт кактуса будет отрисован при следующем вызове DrawSprite.
    /// </summary>
    public class Cactus
    {
        public readonly Transform Transform;
        int _sourceX;

        // Конструктор данного класса,
        // Задающий кактусу положение в окне WinForms и размер.
        public Cactus(PointF pos,Size size)
        {
            Transform = new Transform(pos, size);
            ChooseRandomPic();
        }

        // Метод рандомно выбирает, какой из спрайтов кактуса
        // Будет вырезан и отрисован при следующем вызове DrawSprite.
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

        // Рисует на экране кактус с заданными size, position и sourceX.
        public void DrawSprite(Graphics g)
        {
            g.DrawImage(GameController.Spritesheet, new Rectangle(
                new Point((int)Transform.Position.X, (int)Transform.Position.Y), 
                new Size(Transform.Size.Width, Transform.Size.Height)),
                _sourceX, 0, 48, 96, GraphicsUnit.Pixel);
        }
    }
}
