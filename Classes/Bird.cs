using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    /// <summary>
    /// Bird содержит функционал по отрисовке птиц во время игры
    /// В лице публичного метода DrawSprite
    /// С помощью простого ветвления из if'ов поочередно рисуется
    /// Один из двух спрайтов птицы из Spritesheet'а
    /// Таким образом получается анимация птицы во время игры.
    /// </summary>
    public class Bird
    {
        public Transform Transform;
        int _frameCount = 0;
        int _animationCount = 0;
        
        // Конструктор данного класса, задающий птице размер
        // И положение в окне WinForms.
        public Bird(PointF pos,Size size)
        {
            Transform = new Transform(pos, size);
        }
        
        private void ChooseBirdSpriteFromSpritesheet()
        {
            _frameCount++;
            if (_frameCount <= 20)
                _animationCount = 0;
            else if (_frameCount > 20 && _frameCount <= 40)
                _animationCount = 1;
            else if (_frameCount > 40)
                _frameCount = 0;
        }
        
        public void DrawSprite(Graphics g)
        {
            ChooseBirdSpriteFromSpritesheet();

            g.DrawImage(GameController.Spritesheet, new Rectangle(
                new Point((int)Transform.Position.X, (int)Transform.Position.Y), 
                new Size(Transform.Size.Width, Transform.Size.Height)), 
                264+92*_animationCount, 6, 83, 71, GraphicsUnit.Pixel);
        }
    }
}
