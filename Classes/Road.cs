﻿using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Road
    {
        public Transform transform;

        public Road(PointF pos, Size size)
        {
            transform = new Transform(pos, size);
        }

        public void DrawSprite(Graphics g)
        {
            g.DrawImage(GameController.spritesheet, new Rectangle(
                new Point((int)transform.position.X, (int)transform.position.Y), 
                new Size(transform.size.Width, transform.size.Height)), 
                2300, 112, 100, 17, GraphicsUnit.Pixel);
        }
    }
}
