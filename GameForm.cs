using System;
using System.Drawing;
using System.Windows.Forms;
using DinoGame.Classes;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace DinoGame
{
    /// <summary>
    /// GameForm использует все классы DinoGame
    /// При запуске приложение отрисовывается главный герой (Динозавр),
    /// Генерируется дорога, а затем- препятствия.
    /// </summary>
    public partial class GameForm : Form
    {
        private Player _player;
        private Timer _mainTimer;
        private Timer _obstacleTimer;
        
        private int _currentSpeed = 1;
        private int _increasedSpeed = 0;
        private int _speedConst = 3;
        
        private float gravity = 0.6f;

        // Запускается из Program.Main
        // Инициализирует главный и таймер препятствий
        // Запускает всю игру с помощью таймеров и StartGame
        public GameForm()
        {
            InitializeComponent();
            InitializeForm();
            
            _mainTimer = new Timer {Interval = 10};
            _mainTimer.Tick += new EventHandler(Update);

            _obstacleTimer = new Timer {Interval = 1};
            _obstacleTimer.Tick += new EventHandler(GetObstacles);
            
            StartGame();
        }
        // Вызывается при первом запуске приложения.
        // Задает форме размер, отрисовывает все элементы формы и игры
        // Также назначает события на нажатие и отпускание клавиш клавиатуры
        private void InitializeForm()
        {
            Width = 700;
            Height = 300;
            Icon = Icon.ExtractAssociatedIcon("C:\\Users\\urmomlover\\RiderProjects\\DinoGame\\Sprites\\Icon.ico");
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            Text = "Dino Runner";
            DoubleBuffered = true;
            Paint += new PaintEventHandler(DrawGame);
            KeyUp += new KeyEventHandler(OnKeyboardUp);
            KeyDown += new KeyEventHandler(OnKeyboardDown);
        }
        // Вызывается каждый раз, когда нужно начать новую игру (проигрыш или ппервый запуск)
        // Инициализирует листы птиц, кактусов и дорог
        // также инициализирует динозавра и запускает главный таймер событий и таймер препятствий.
        private void StartGame()
        {
            GameController.Initialize();
            _player = new Player(new PointF(50, 150), new Size(50, 50), gravity);
            _mainTimer.Start();
            _obstacleTimer.Start();
            Invalidate();
        }
        // DrawGame вызывается при инициализации формы, или при первом запуске приложения.
        // Отрисовывает вообще все (динозавра, дороги, птиц и кактусы)
        private void DrawGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            _player.DrawSprite(g);
            GameController.DrawObjeсts(g);
        }
        
        /// <summary>
        /// Update вызывается каждые 10 милисекунд с помошью Timer _maintimer
        /// Данный метод обновляет текст счета и скорости в окне WinForms
        /// Увеличивает счет игрока (пройденный путь);
        /// Увеличивает скорость на 1 каждые 5000 тиков таймера или каждые 500 очков;
        /// Проверяет факт коллизии, если она произошла - игра зависает на секунду и начинается заново;
        /// Двигает все кроме динозывра вызывая MoveMap и применяет гравитацию к динозавру, если он в прыжке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update(object sender, EventArgs e)
        {
            label1.Text = (_player.Score/10).ToString();
            label2.Text = "speed: " + _increasedSpeed;
            
            _player.Score++;
            
            _increasedSpeed = 1 + (_player.Score / 500 );
            _currentSpeed = _speedConst + _increasedSpeed;
            
            if (_player.Physics.Collide())
            {
                Thread.Sleep(1000);
                StartGame();
            }

            
            _player.Physics.ApplyPhysics();
            GameController.MoveMap(_currentSpeed);
            Invalidate();
        }
        private void GetObstacles(object sender, EventArgs e)
        {
            GameController.GenerateObstacles(_currentSpeed);
        }
        // OnKeyboardDown и OnKeyboardUp задают действия на нажатие
        // И при отпускание кнопки соответственно, используя switch конструкцию.
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
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
