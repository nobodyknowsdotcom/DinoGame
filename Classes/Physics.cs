using System;
using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    // Physics отвечает за взаимодействие внутриигровых объектов друг с другом
    // (прыжок, коллизия объектов и тд)
    public class Physics
    {
        public Transform Transform;
        private float _force;
        private float _gravity;
        
        public bool IsJumping;
        public bool IsCrouching;

        // Конструктор данного класса, задающий положение и размер экземпляру объекта
        // Также принимает и задает величину гравитации
        public Physics(PointF position, Size size, float gravity)
        {
            Transform = new Transform(position, size);
            _gravity = gravity;
            IsJumping = false;
            IsCrouching = false;
        }
        
        // Проверка всех кактусов и птиц на коллизию
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
        
        // Присвоение _force отрицательной величины, вследствии чего 
        // Происходит прыжок
        public void Jump()
        {
            if (!IsJumping)
            {
                IsJumping = true;
                _force = -12;
            }
        }

        // ApplyPhysics изменяет положение объекта на экране
        // Если он в прыжке - то на него действует гравитация
        // Иначе статус "в прыжке" становится ложным.
        public void ApplyPhysics()
        {
            if(IsJumping)
            {
                Transform.Position.Y += _force;
                _force += _gravity;
            }
            if (Transform.Position.Y >= 150)
                IsJumping = false;
        }    
    }
}
