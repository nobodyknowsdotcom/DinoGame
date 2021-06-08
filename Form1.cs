﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DinoGame.Classes;

namespace DinoGame
{
    public partial class Form1 : Form
    {
        Player player;
        Timer mainTimer;
        public Form1()
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 300;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(DrawGame);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.KeyDown += new KeyEventHandler(OnKeyboardDown);
            mainTimer = new Timer {Interval = 1};
            mainTimer.Tick += new EventHandler(Update);

            Init();
        }

        public void Update(object sender, EventArgs e)
        {
            player.score++;
            this.Text = "Dino - Score: " + player.score;
            if (player.physics.Collide())
                Init();
            player.physics.ApplyPhysics();
            GameController.MoveMap();
            Invalidate();
        }
        
        private void OnKeyboardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if (!player.physics.isJumping)
                    {
                        player.physics.isCrouching = true;
                        player.physics.isJumping = false;
                        player.physics.transform.size.Height = 25;
                        player.physics.transform.position.Y = 174;
                    }
                    break;
            }
        }

        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    player.Jump();
                    break;

                case Keys.Down:
                    player.Crouch();
                    break;

                case Keys.W:
                    player.Jump();
                    break;
            }
        }

        public void Init()
        {
            GameController.Init();
            player = new Player(new PointF(50, 149), new Size(50, 50));
            mainTimer.Start();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player.DrawSprite(g);
            GameController.DrawObjets(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
