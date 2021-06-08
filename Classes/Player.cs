﻿using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Player
    {
        public Physics physics;
        public int framesCount = 0;
        public int animationCount = 0;
        public int score = 0;

        public Player(PointF position, Size size)
        {
            physics = new Physics(position, size);
            framesCount = 0;
            score = 0;
        }

        public void DrawSprite(Graphics g)
        {
            if (physics.isCrouching)
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
            framesCount++;
            if (framesCount <= 10)
                animationCount = 0;
            else if (framesCount > 10 && framesCount <= 20)
                animationCount = 1;
            else if (framesCount > 20)
                framesCount = 0;

            g.DrawImage(GameController.spritesheet, new Rectangle(
                new Point((int)physics.transform.position.X, (int)physics.transform.position.Y),
                new Size((int)(physics.transform.size.Width * multiplier), physics.transform.size.Height)),
                srcX + delta * animationCount, srcY, width, height, GraphicsUnit.Pixel);
        }
    }

    public static class PlayerExtensions
    {
        public static void Jump(this Player player)
        {
            if (player.physics.isCrouching) return;
            player.physics.isCrouching = false;
            player.physics.AddForce();
        }
        
        public static void Crouch(this Player player)
        {
            player.physics.isCrouching = false;
            player.physics.transform.size.Height = 50;
            player.physics.transform.position.Y = 150.2f;
        }
    }
}
