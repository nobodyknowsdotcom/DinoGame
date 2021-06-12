using System;
using System.Drawing;
using System.Windows.Forms;
using DinoGame.Classes;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace DinoGame
{
    public partial class GameForm : Form
    {
        Player _player;
        Timer _mainTimer;
        public static int CurrentSpeed = 1;
        private int _increasedSpeed = 0;
        private int _speedConst = 3;
        public GameForm()
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 300;
            this.Icon = Icon.ExtractAssociatedIcon("C:\\Users\\urmomlover\\RiderProjects\\DinoGame\\Sprites\\Icon.ico");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.Text = "Dino Runner";
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(DrawGame);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.KeyDown += new KeyEventHandler(OnKeyboardDown);
            _mainTimer = new Timer {Interval = 1};
            _mainTimer.Tick += new EventHandler(Update);

            Init();
        }

        private void Update(object sender, EventArgs e)
        {
            label1.Text = (_player.Score/10).ToString();
            label2.Text = "dino speed: " + _increasedSpeed;
            
            _player.Score++;
            
            _increasedSpeed = 1 + (_player.Score / 500 );
            CurrentSpeed = _speedConst + _increasedSpeed;
            
            GameController.GenerateObstacles(CurrentSpeed);
            if (_player.Physics.Collide())
            {
                Thread.Sleep(1000);
                Init();
            }

            
            _player.Physics.ApplyPhysics();
            GameController.MoveMap(CurrentSpeed);
            Invalidate();
        }
        
        private void OnKeyboardDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (!_player.Physics.IsJumping)
                    {
                        _player.Physics.IsCrouching = true;
                        _player.Physics.IsJumping = false;
                        _player.Physics.Transform.Size.Height = 25;
                        _player.Physics.Transform.Position.Y = 174;
                    }
                    break;
            }
        }

        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _player.Jump();
                    break;
                
                case Keys.Space:
                    _player.Jump();
                    break;

                case Keys.S:
                    _player.Crouch();
                    break;
            }
        }

        private void Init()
        {
            GameController.Init();
            _player = new Player(new PointF(50, 150), new Size(50, 50));
            _mainTimer.Start();
            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            _player.DrawSprite(g);
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
