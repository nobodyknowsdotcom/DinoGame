using System;
using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Physics
    {
        public Transform transform;
        float gravity;
        float a;

        public bool isJumping;
        public bool isCrouching;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;
            isJumping = false;
            isCrouching = false;
        }

        

        public bool Collide()
        {
            foreach (var cactus in GameController.cactuses)
            {
                PointF delta = new PointF();
                delta.X = (transform.position.X + transform.size.Width / 2) - (cactus.transform.position.X + cactus.transform.size.Width / 2);
                delta.Y = (transform.position.Y + transform.size.Height / 2) - (cactus.transform.position.Y + cactus.transform.size.Height / 2);
                if (Math.Abs(delta.X) <= transform.size.Width / 2 + cactus.transform.size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= transform.size.Height / 2 + cactus.transform.size.Height / 2)
                    {
                        return true;
                    }
                }
            }
            foreach (var bird in GameController.birds)
            {
                PointF delta = new PointF();
                delta.X = (transform.position.X + transform.size.Width / 2) - (bird.transform.position.X + bird.transform.size.Width / 2);
                delta.Y = (transform.position.Y + transform.size.Height / 2) - (bird.transform.position.Y + bird.transform.size.Height / 2);
                if (Math.Abs(delta.X) <= transform.size.Width / 2 + bird.transform.size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= transform.size.Height / 2 + bird.transform.size.Height / 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public void AddForce()
        {
            if (!isJumping)
            {
                isJumping = true;
                gravity = -10;
            }
        }
        
        public void ApplyPhysics()
        {
            CalculatePhysics();
        }
        
        private void CalculatePhysics()
        {
            if(transform.position.Y<150 || isJumping)
            {
                transform.position.Y += gravity;
                gravity += a;
            }
            if (transform.position.Y > 150)
                isJumping = false;
        }    }
}
