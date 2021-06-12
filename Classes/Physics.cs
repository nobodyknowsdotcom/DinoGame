using System;
using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    public class Physics
    {
        public Transform Transform;
        float _gravity;
        float _a;

        public bool IsJumping;
        public bool IsCrouching;

        public Physics(PointF position, Size size)
        {
            Transform = new Transform(position, size);
            _gravity = 0;
            _a = 0.4f;
            IsJumping = false;
            IsCrouching = false;
        }

        

        public bool Collide()
        {
            foreach (var cactus in GameController.Cactuses)
            {
                PointF delta = new PointF();
                delta.X = (Transform.Position.X + Transform.Size.Width / 2) - (cactus.Transform.Position.X + cactus.Transform.Size.Width / 2);
                delta.Y = (Transform.Position.Y + Transform.Size.Height / 2) - (cactus.Transform.Position.Y + cactus.Transform.Size.Height / 2);
                if (Math.Abs(delta.X) <= Transform.Size.Width / 2 + cactus.Transform.Size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= Transform.Size.Height / 2 + cactus.Transform.Size.Height / 2)
                    {
                        return true;
                    }
                }
            }
            foreach (var bird in GameController.Birds)
            {
                PointF delta = new PointF();
                delta.X = (Transform.Position.X + Transform.Size.Width / 2) - (bird.Transform.Position.X + bird.Transform.Size.Width / 2);
                delta.Y = (Transform.Position.Y + Transform.Size.Height / 2) - (bird.Transform.Position.Y + bird.Transform.Size.Height / 2);
                if (Math.Abs(delta.X) <= Transform.Size.Width / 2 + bird.Transform.Size.Width / 2)
                {
                    if (Math.Abs(delta.Y) <= Transform.Size.Height / 2 + bird.Transform.Size.Height / 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public void AddForce()
        {
            if (!IsJumping)
            {
                IsJumping = true;
                _gravity = -10;
            }
        }
        
        public void ApplyPhysics()
        {
            CalculatePhysics();
        }
        
        private void CalculatePhysics()
        {
            if(Transform.Position.Y<150 || IsJumping)
            {
                Transform.Position.Y += _gravity;
                _gravity += _a;
            }
            if (Transform.Position.Y > 150)
                IsJumping = false;
        }    }
}
