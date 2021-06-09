﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DinoGame.Classes;

namespace DinoGame
{
    public partial class GameForm : Form
    {
        Player player;
        Timer mainTimer;
        private int speed = 0;
        private int speedConst = 4;
        public GameForm()
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 300;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.Text = "Dino Runner";
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(DrawGame);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.KeyDown += new KeyEventHandler(OnKeyboardDown);
            mainTimer = new Timer {Interval = 1};
            mainTimer.Tick += new EventHandler(Update);

            Init();
        }

        private void Update(object sender, EventArgs e)
        {
            player.score++;
            this.label1.Text = "meters ahead: " + player.score;
            speed = speedConst + (player.score / 1000);
            if (player.physics.Collide())
                Init();
            player.physics.ApplyPhysics();
            GameController.MoveMap(speed);
            Invalidate();
        }
        
        private void OnKeyboardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
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
                case Keys.W:
                    player.Jump();
                    break;
                
                case Keys.Space:
                    player.Jump();
                    break;

                case Keys.S:
                    player.Crouch();
                    break;
            }
        }

        private void Init()
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

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
