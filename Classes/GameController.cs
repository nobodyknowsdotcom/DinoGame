using System;
using System.Collections.Generic;
using System.Drawing;
using Dino.Classes;

namespace DinoGame.Classes
{
    /// <summary>
    /// GameController отвечает игровую логику
    /// Он приводит игровые объекты в движение
    /// Генерирует и отрисовывает препятствия и дорогу.
    /// </summary>
    public static class GameController
    {
        public static Image Spritesheet;
        public static List<Road> Roads;
        public static List<Cactus> Cactuses;
        public static List<Bird> Birds;

        public static int DangerSpawn = 5;
        public static int CountDangerSpawn = 0;

        // Initialize инициализирует roads, birds, cactuses и привязывает sprite.png к переменной spritesheet
        // Отрисовка и генерация дороги в игре через GenegateRoad.
        public static void Initialize()
        {
            Roads = new List<Road>();
            Birds = new List<Bird>();
            Cactuses = new List<Cactus>();
            Spritesheet = DinoGame.Properties.Resources.sprite;
            GenerateRoad();
        }

        // MoveMap меняет X координату игровых объектов в их листах на -= speed
        // Также произходит чистка листов объектов от объектов, которые находятся за окном WinForms.
        public static void MoveMap(int speed)
        {
            for(int i = 0; i < Roads.Count; i++)
            {
                Roads[i].Transform.Position.X -= speed;
                if (Roads[i].Transform.Position.X + Roads[i].Transform.Size.Width < 0)
                {
                    Roads.RemoveAt(i);
                    GetNewRoad();
                }
            }
            for (int i = 0; i < Cactuses.Count; i++)
            {
                Cactuses[i].Transform.Position.X -= speed;
                if (Cactuses[i].Transform.Position.X + Cactuses[i].Transform.Size.Width < 0)
                {
                    Cactuses.RemoveAt(i);
                }
            }
            for (int i = 0; i < Birds.Count; i++)
            {
                Birds[i].Transform.Position.X -= speed;
                if (Birds[i].Transform.Position.X + Birds[i].Transform.Size.Width < 0)
                {
                    Birds.RemoveAt(i);
                }
            }
        }

        // DrawObjects отрисовывает все объекты из листов Roads, Birds и Cactuses.
        public static void DrawObjeсts(Graphics g)
        {
            foreach (var road in Roads)
                road.DrawSprite(g);
            foreach (var cactus in Cactuses)
                cactus.DrawSprite(g);
            foreach (var bird in Birds)
                bird.DrawSprite(g);
        }
        
        // Генерирует препятствия, частота появления которых напрямую связана с скоростью движения динозавра
        // (На самом деле не динозавра, а всех объектов кроме него)
        public static void GenerateObstacles(int minimalDistance)
        {
            if (CountDangerSpawn >= DangerSpawn)
            {
                var r = new Random();
                DangerSpawn = r.Next(minimalDistance, minimalDistance+5);
                CountDangerSpawn = 0;
                var obj = r.Next(0, 2);
                switch (obj)
                {
                    case 0:
                        var cactus = new Cactus(new PointF(0 + 100 * 9, 150), new Size(30, 60));
                        Cactuses.Add(cactus);
                        break;
                    case 1:
                        var bird = new Bird(new PointF(0 + 100 * 9, 120), new Size(50, 50));
                        Birds.Add(bird);
                        break;
                }
            }
        }
        
        private static void GetNewRoad()
        {
            Roads.Add(new Road(new PointF(200 * 9, 200), new Size(200, 17)));
            CountDangerSpawn+=2;
        }

        // Заполняет массив дорог и увеличивает CounterDangerSpawn
        // При достижении определенного значения CounterDangerSpawn генерируется случайное препятствие.
        private static void GenerateRoad()
        {
            for(int i = 0; i < 10; i++)
            {
                Road road = new Road(new PointF(0 + 200 * i, 200), new Size(200, 17));
                Roads.Add(road);
                CountDangerSpawn+=2;
            }
        }
    }
}
