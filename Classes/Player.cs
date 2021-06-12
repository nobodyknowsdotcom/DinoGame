using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    /// <summary>
    /// Player отвечает за отрисовку динозавра в зависимости от ситуации
    /// Также имеет конструктор, принимающий гравитацию, действующую на динозавра
    /// </summary>
    public class Player
    {
        public Physics Physics;
        public int FramesCount = 0;
        public int AnimationCount = 0;
        public int Score = 0;

        // Конструктор данного класса, задает динозавру размер
        // Положение в окне WinForms и гравитацию, действующую на него
        public Player(PointF position, Size size, float gravity)
        {
            Physics = new Physics(position, size, gravity);
            FramesCount = 0;
            Score = 0;
        }

        // При вызове отрисовывает кадр бегущего или крадущегося динозавра
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

        // Рассчет нужных кадров для анимации динозавра и их отрисовка
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

    /// <summary>
    /// Расширения для удобного назначения действий нв кнопки
    /// </summary>
    public static class PlayerExtensions
    {
        public static void Jump(this Player player)
        {
            if (player.Physics.IsCrouching) return;
            player.Physics.IsCrouching = false;
            player.Physics.Jump();
        }
        
        public static void Crouch(this Player player)
        {
            player.Physics.IsCrouching = false;
            player.Physics.Transform.Size.Height = 50;
            player.Physics.Transform.Position.Y = 150.2f;
        }
    }
}
