using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public class GameLogic : IGameLogic
    {
        public GameLogic()
        {

            this.Esc = new SolidColorBrush(Color.FromRgb(105, 105, 105));
            this.Esc.Opacity = 0.5;

            timer = new System.Timers.Timer();
            timer.Interval = 15000; //15mpig lehetsz a formában. Villogás, hogyha kezd lejárni
            timer.Elapsed += Timer_Elapsed;

            Delay = new System.Timers.Timer();
            Delay.Interval = 12000;
            Delay.Elapsed += Delay_Elapsed;

            BonusMana = new System.Timers.Timer();
            BonusMana.Interval = 1000; //10mp
            //10mp-nként van 50% esélye, hogy spawnol
            BonusMana.Elapsed += SpawnMana;
            ManaMove(r.Next(1, 6));

            Intro = false;
            EscON = false;
            Katon = false;
            IsStanding = false;
            IsSkilledOne = false;
            IsSkilledTwo = false;
            Once = false;
            SkillShoot = false;
            GoAfterSkill = false;
            DisapearShuriken = false;
            gameOver = false;
            enemyspawn = false;
            youcanspawnenemy = false;
            MMEnded = false;
            Hitted = false;
            IsInForm = false;
            canjump = false;
            drawform = false;
            DisapearMana = false;
            ChaningBack = false;

            hp = 5;
            mana = 0;
            force = 20;
            speed = 10;
            BackgroundSpeed = 15;
            ManaSpeed = 15;
            ShurikenSpeed = 50;
            counter = 0;
            score = 0;
            TimeLimit = 0;



            MadaraDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "Dead3.png"), UriKind.RelativeOrAbsolute)));
            WaterSound.Open(new Uri(Path.Combine("Music", "Water.wav"), UriKind.RelativeOrAbsolute));
            WaterSound.Volume = 0.9;

            WaterSound.MediaEnded += WaterSound_MediaEnded;
            HurtMadara.MediaEnded += HurtMadara_MediaEnded;
            Susano.MediaEnded += Susano_MediaEnded;
            //Susano1.MediaEnded += Susano1_MediaEnded;

            WoodStyle.Open(new Uri(System.IO.Path.Combine("Music", "WoodStyle.mp3"), UriKind.RelativeOrAbsolute));
            Susano.Open(new Uri(System.IO.Path.Combine("Music", "Susano.mp3"), UriKind.RelativeOrAbsolute));
            MainMusic.Open(new Uri(System.IO.Path.Combine("Music", "MainGameTheme.wav"), UriKind.RelativeOrAbsolute));
            HurtMadara.Open(new Uri(System.IO.Path.Combine("Music", "hurtmadara.wav"), UriKind.RelativeOrAbsolute));
            HurtMadara.Volume = 0.9;
            IntroMedia.Open(new Uri(System.IO.Path.Combine("Videos", "Intro.mp4"), UriKind.RelativeOrAbsolute));
            IntroMedia.MediaEnded += IntroMedia_MediaEnded;
        }
        private void SpawnMana(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (r.Next(0, 1) == 0)//ez adja meg a chanchet 10mp.nként. 50% h spawnolhat
            {
                if (r.Next(0, 100) > 50)
                {
                    TopOrBot = true; //true top
                }
                ManaChanche = true;
                BonusMana.Stop();
            }
        }
        private void Delay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Delay.Stop();
            ChaningBack = true;
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            //30mp után ujra lehet ugrani.
            //többet nem mehetünk formba
            IsInForm = false;
            canjump = false;
            drawform = false;
            ChaningBack = false;
        }
        private void HurtMadara_MediaEnded(object sender, EventArgs e)
        {
            HurtMadara.Stop();
            HurtMadara.Position = TimeSpan.Zero;
        }
        private void WaterSound_MediaEnded(object sender, EventArgs e)
        {
            WaterSound.Stop();
            WaterSound.Position = TimeSpan.Zero;
        }
        private void Susano_MediaEnded(object sender, EventArgs e)
        {
            Susano.Stop();
            Susano.Position = TimeSpan.Zero;
        }
        private void MainMusic_MediaEnded(object sender, EventArgs e)
        {
            MainMusic.Position = TimeSpan.Zero;
            if (!MMEnded && !EscON)
            {
                BackgroundSpeed = 30;
                ManaSpeed = 30;
                MMEnded = true;
                Changed?.Invoke(this, null);
            }
            if (ShurikenSpeed > 80)
            {
                ShurikenSpeed += 10;
            }
            MainMusic.Play();
        } 
        private void IntroMedia_MediaEnded(object sender, EventArgs e)
        {
            Intro = true;
        }
        public void Resize(Size size)
        {
            this.size = size;
            Y = size.Height / 2;
            X = 50;
            groundheight = size.Height / 2 * 1.5;

            ObstacleX = size.Width * 1.5;
            ObstacleY = size.Height / 2 * 1.2;

            ManaX = size.Width * 1.5;
            ManaY = size.Height / 2 * 1.2;

            SkillShootX = size.Width / 6;
            SkillShootY = size.Height / 4 * 1.59;

            old_skillx = size.Width / 6;
            old_skilly = size.Height / 4 * 1.59;

            EnemyX = size.Width * 1.45;
            EnemyY = size.Height / 8.2;
        }
        Random r { get; set; } = new Random();
        public ImageBrush Background
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Background", "SandVillage.png"), UriKind.RelativeOrAbsolute)));
            }
            set { }
        }
        public ImageBrush playerSprite { get; set; }
        public ImageBrush SkillOne { get; set; }
        public ImageBrush SkillTwo { get; set; }
        public ImageBrush obstacleSprite { get; set; }
        public ImageBrush ManaBar { get; set; }
        public ImageBrush HPBar { get; set; }
        public ImageBrush EnemySprite { get; set; }
        public ImageBrush PressStart { get; set; }
        public SolidColorBrush Esc { get; set; }
        public ImageBrush MadaraDead { get; set; }
        public ImageBrush SkillDead { get; set; }
        public ImageBrush Mana { get; set; }
        public MediaPlayer IntroMedia { get; set; } = new MediaPlayer();
        public MediaPlayer MainMusic { get; set; } = new MediaPlayer();
        public MediaPlayer clicksound { get; set; } = new MediaPlayer();
        public MediaPlayer WaterSound { get; set; } = new MediaPlayer();
        public MediaPlayer KatonFireStyle { get; set; } = new MediaPlayer();
        public MediaPlayer Susano { get; set; } = new MediaPlayer();
        public MediaPlayer HurtMadara { get; set; } = new MediaPlayer();
        public MediaPlayer WoodStyle { get; set; } = new MediaPlayer();
        Size size;
        public event EventHandler Changed; //változott a background. akkor ÚJRA RAJZOLUNK
        System.Timers.Timer timer;
        System.Timers.Timer BonusMana;
        System.Timers.Timer Delay;

        public string Name { get; set; }

        bool recast { get; set; } = false;
        bool music = false;
        bool watermusic = false;
        public bool Intro { get; set; }
        public bool EscON { get; set; }
        public bool jumping { get; set; }
        public bool GoinDown { get; set; }
        public bool Katon { get; set; }
        bool down { get; set; }
        public bool IsStanding { get; set; }
        bool IsSkilledOne { get; set; }
        bool IsSkilledTwo { get; set; }
        bool Once { get; set; }
        public bool IsInForm { get; set; }
        public bool SkillShoot { get; set; }
        public bool GoAfterSkill { get; set; }
        public bool DisapearShuriken { get; set; }
        public bool gameOver { get; set; } //vége van-e a játéknak
        public bool enemyspawn { get; set; }
        public bool youcanspawnenemy { get; set; }
        public bool canjump { get; set; }
        public bool ChaningBack { get; set; }
        bool TopOrBot { get; set; }
        public bool drawform { get; set; }
        public bool DisapearMana { get; set; }
        public bool SetHighScore { get; set; } //ha meghalt akkor ezzel jelezzük, majd úgy rajzolunk !
        public bool Hitted { get; set; }
        private bool MMEnded { get; set; }
        public bool ManaChanche { get; set; }

        public int score { get; set; }
        public int hp { get; set; }
        public int BackgroundMoveFirst { get; set; } = 10;
        public int BackgroundMoveSecond { get; set; } = 10;
        int counter;
        public int chanche { get; set; }
        public int mana { get; set; }
        public int force { get; set; }//toljunk 20-al
        public int speed { get; set; } //5-tel ugrik
        public int BackgroundSpeed { get; set; }
        public int ManaSpeed { get; set; }
        public int ShurikenSpeed;

        Rect PlayerHitbox; //Madara
        Rect groundHitBox; //Föld
        Rect obstacleHitbox; //Akadály
        Rect manaHitbox; //Mana
        Rect EnemyHitbox; //Enemy
        Rect SkillHitbox; //Skill

        public double Y { get; set; } //Madara
        public double X { get; set; } //Madara
        public double groundheight { get; set; }

        public double ObstacleY { get; set; } //Shuriken
        public double ObstacleX { get; set; } //Shuriken

        public double ManaY { get; set; } //Mana
        public double ManaX { get; set; } //Mana

        public double SkillShootX { get; set; } //Shinobies
        public double SkillShootY { get; set; } //Shinobies
        private double old_skillx { get; set; }
        private double old_skilly { get; set; }

        public double EnemyX { get; set; }
        public double EnemyY { get; set; }

        double spriteIndex;
        double susanoIndex;
        double ShurikenIndex;
        double SkillIndex;
        double SkillTwoIndex;
        double EnemyIndex;
        double PressIndex;
        double DeadMadaraIndex;
        double TimeLimit;

        public void HpBarChanged(double i)
        {
            switch (i)
            {
                case 1:
                    HPBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "hpbar5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 2:
                    HPBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "hpbar4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    HPBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "hpbar3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 4:
                    HPBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "hpbar2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    HPBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "hpbar1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                default:
                    HPBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "hpbar1.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        }
        public void ManaBarChanged(double i)
        {
            switch (i)
            {
                case 0:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar0.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 1:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 2:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 4:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                default:
                    ManaBar = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/HpBar", "manabar5.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        }
        public void RunMadara(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 2:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 4:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 6:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "6.png"), UriKind.RelativeOrAbsolute)));
                    break;

            }

        }
        public void StandMadara(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Stand", "stand3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Stand", "stand2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 14:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Stand", "stand1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 21:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Stand", "stand2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 28:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Stand", "stand3.png"), UriKind.RelativeOrAbsolute)));
                    break;

            }

        }
        public void DeadMadara(double i)
        {
            switch (i)
            {
                case 1:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 14:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 21:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 28:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 35:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill6.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 42:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill7.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 49:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill8.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 56:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill9.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 63:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill10.png"), UriKind.RelativeOrAbsolute)));
                    MadaraDead = null;
                    break;
                case 70:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill11.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 77:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill12.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 84:
                    SkillDead = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Dead", "skill13.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        }
        public void PressEnterI(double i)
        {
            switch (i)
            {
                case 1:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 14:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 21:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 28:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 35:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press6.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 42:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press7.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 49:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press8.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 56:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press9.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 63:
                    PressStart = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/PressEnter", "press10.png"), UriKind.RelativeOrAbsolute)));
                    break;

            }

        }
        public void SkillOneMadara(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 15:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 20:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu2.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        }
        public void SkillTwoMadara(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsu2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 9:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 10:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 12:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 13:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 15:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 16:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 18:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 19:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 21:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 22:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 24:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform6.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 25:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform6.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 27:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform7.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 28:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform7.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 30:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform8.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 31:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Transform", "transform8.png"), UriKind.RelativeOrAbsolute)));
                    break;

            }

        } //SkillTwo
        public void InForm(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 9:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 10:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "6.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 12:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "7.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 13:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "8.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 15:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "9.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 16:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/Susano", "10.png"), UriKind.RelativeOrAbsolute)));
                    break;

            }

        } //SkillTwo
        public void ChangeBackForm(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 9:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "5.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 10:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "6.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 12:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "7.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 13:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "8.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 15:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "9.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 16:
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run/ChangeBack", "10.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        } //SkillTwo
        private void SkillMove(double i)
        {
            switch (i)
            {
                case 1:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "2s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "3s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "3s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "4s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 9:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "5s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 11:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "6s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 13:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "7s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 15:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "8s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 17:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "9s.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 19:
                    SkillOne = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/SkillShoot", "10s.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }
        }
        public void ShurikenMove(double i)
        {
            switch (i)
            {
                case 1:
                    obstacleSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Shuriken", "shuriken1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 4:
                    obstacleSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Shuriken", "shuriken2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    obstacleSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Shuriken", "shuriken1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 10:
                    obstacleSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Shuriken", "shuriken2.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        }
        public void ManaMove(double i)
        {
            switch (i)
            {
                case 1:
                    Mana = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Mana", "sushi1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 2:
                    Mana = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Mana", "sushi2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    Mana = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Mana", "sushi3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 4:
                    Mana = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Mana", "sushi4.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    Mana = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Mana", "sushi5.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }

        }
        private void EnemyMove(double i)
        {
            switch (i)
            {
                case 1:
                    EnemySprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Water", "water1.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 3:
                    EnemySprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Water", "water2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 5:
                    EnemySprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Water", "water3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 7:
                    EnemySprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Water", "water3.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 9:
                    EnemySprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Water", "water2.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case 12:
                    EnemySprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Water", "water1.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }
        }
        private void SkillMovement()
        {
            SkillIndex += 1;
            if (SkillIndex < 19)
            {
                SkillMove(SkillIndex);
            }
            else if (SkillShootX > size.Width)
            {
                SkillIndex = 0;
            }
        }
        private void ShurikenMovement()
        {

            if (ObstacleX < 0 - size.Width + 50)
            {
                //Spawn
                if (RandomGene.r.Next(0, 2) == 0 && chanche % 2 == 0)
                {
                    ObstacleX = size.Width * 1.5;
                    DisapearShuriken = false;
                    score += 1;
                }
                else
                {
                    chanche++;
                    if (chanche % 5 == 0)
                    {
                        youcanspawnenemy = true;
                    }
                }
            }
            else
            {
                ObstacleX = ObstacleX - ShurikenSpeed;
                obstacleHitbox = new Rect(ObstacleX, ObstacleY, size.Width / 10, size.Height / 10);
                ShurikenIndex += 1.5;
                if (ShurikenIndex > 10)
                {
                    ShurikenIndex = 1;
                }
                ShurikenMove(ShurikenIndex);
            }
        }
        private void ManaMovement()
        {

            if (ManaX < 0 - size.Width + 50)
            {
                ManaX = size.Width * 1.5;
                DisapearMana = false;
                ManaChanche = false; //kikapcsoolom hha vége az animacionak és elindítom a timert megint
                BonusMana.Start();
                ManaMove(r.Next(1, 6));
                TopOrBot = false;
            }
            else
            {
                if (TopOrBot)
                {
                    ManaY = size.Height / 2 * 0.5;
                }
                else
                {
                    ManaY = size.Height / 2 * 1.2;
                }
                ManaX = ManaX - ManaSpeed;
                manaHitbox = new Rect(ManaX, ManaY, size.Width / 10, size.Height / 10);
            }
        }
        private void MadaraWaitingStart() //+presstart
        {
            spriteIndex += .5;
            if (spriteIndex > 28)
            {
                spriteIndex = 1;
            }
            StandMadara(spriteIndex);

            PressIndex += .5;
            if (PressIndex > 80)
            {
                PressIndex = 1;
            }
            PressEnterI(PressIndex);

        }
        private void EnemyMovement()
        {
            if (!watermusic)
            {
                WaterSound.Play();
                watermusic = true;
            }
            //csúsztatunk ellenkező esetben

            if (PlayerHitbox.IntersectsWith(EnemyHitbox))
            {
                EnemyX = size.Width * 1.5;
                EnemyHitbox = new Rect(EnemyX, EnemyY, size.Width / 3, size.Width / 3);
                HpBarChanged(hp);
                if (IsSkilledOne)
                {
                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsuhit.png"), UriKind.RelativeOrAbsolute)));
                }
                else
                {
                    if (!jumping && !IsInForm)
                    {
                        playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "hit1.png"), UriKind.RelativeOrAbsolute)));
                        Hitted = true;
                    }
                }
                if (!IsInForm)
                {
                    hp -= 1;
                    HpBarChanged(hp);
                    HurtMadara.Play();
                }
                youcanspawnenemy = false;
                watermusic = false;
            }
            else if (SkillShoot && SkillHitbox.IntersectsWith(EnemyHitbox))
            {

                double x = size.Width / 3.5;
                EnemyX = size.Width * 1.5;
                EnemyHitbox = new Rect(EnemyX, EnemyY, size.Width / 3, size.Width / 3);
                //Ezutan rajzoljuk ki erre a 2 koordinátára 1
                //kis ködöt!
                SkillShootX = 0 - size.Width;
                SkillShootY = 0 - size.Height;
                SkillIndex = 0;
                recast = true;
                SkillShoot = false;
                SkillHitbox = new Rect(SkillShootX, SkillShootY, size.Width / 3 + 20, size.Height / 3 + 20);
                score += 1;
                youcanspawnenemy = false;
                watermusic = false;

            }
            else
            {
                youcanspawnenemy = true;
                EnemyX = EnemyX - 20;
                EnemyIndex += 0.5;
                EnemyHitbox = new Rect(EnemyX, EnemyY, size.Width / 3, size.Width / 3);
                if (EnemyIndex > 12)
                {
                    EnemyIndex = 0.5;
                }
                EnemyMove(EnemyIndex);
            }

        }
        public enum ControlKey
        {
            space,
            skillone,
            skilltwo,
            enter, //később new game
            esc //back to main menu
        }
        public void Control(ControlKey key)
        {
            switch (key)
            {
                case ControlKey.space:
                    if (down == false && IsStanding) //ide még kell, hogy leért -e a foldre KOORDINATA
                    {
                        if (!IsSkilledOne && !GoAfterSkill && !EscON && !IsInForm && !canjump)
                        {
                            jumping = true;
                            playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jump", "jump1.png"), UriKind.RelativeOrAbsolute)));
                        }
                    }
                    break;
                case ControlKey.skillone:
                    if (IsStanding && !jumping && !down) //ha már nem állok - enter után és 
                    {
                        IsSkilledOne = true;
                        Katon = true;
                    }
                    break;
                case ControlKey.enter:
                    if (Intro)
                    {
                        if (!EscON)
                        {
                            IsStanding = true;
                            BonusMana.Start();
                            ManaMove(r.Next(1, 6));
                        }
                    }
                    else
                    {
                        if (!EscON)
                        {
                            IntroMedia.Stop();
                            Intro = true;
                        }
                    }
                    break;
                case ControlKey.esc:
                    if (EscON)
                    {
                        EscON = false;
                    }
                    else
                    {
                        EscON = true;
                    }
                    break;
                case ControlKey.skilltwo:
                    if (Once && IsStanding && mana == 5 && !jumping && !down)
                    {
                        mana = 0;
                        IsSkilledTwo = true;
                        Susano.Play();

                    }
                    break;

                default:
                    break;
            }
        }
        public void GameEngine()
        {
            if (hp > 1) //meghalt hha eléri az 1-et --> mehet a dead animacio 
            {

                if (!EscON) //escon true
                {
                    if (Intro)
                    {
                        if (!music)
                        {
                            MainMusic.Position = new TimeSpan(0, 0, 49);
                            MainMusic.Play();
                            MainMusic.MediaEnded += MainMusic_MediaEnded;
                            music = true;
                        }
                        HpBarChanged(hp);
                        ManaBarChanged(mana);
                        if (IsStanding && !IsSkilledOne && !GoAfterSkill && !IsSkilledTwo)
                        {
                            BackgroundMoveFirst += BackgroundSpeed;
                            BackgroundMoveSecond += BackgroundSpeed; //1865

                            if (BackgroundMoveFirst > 3450) //ha nagyobb mint 3450- akkor tudjuk, hogy eltoltuk. Mehet a végére.
                            {
                                if (!MMEnded)
                                {
                                    BackgroundMoveFirst = -3440; //azért 40 mert közbe tolódik 1-et
                                }
                                else
                                {
                                    BackgroundMoveFirst = -3420;
                                }

                            }

                            if (BackgroundMoveSecond > 6900)
                            {
                                if (!MMEnded)
                                {
                                    BackgroundMoveSecond = 10;
                                }
                                else
                                {
                                    BackgroundMoveSecond = 30;
                                }
                            }

                            if (jumping) //ugrok hha nincs közös pont
                            {
                                Y -= 20;
                            }

                            if (Y > (size.Height / 2 * 0.2) * 2 && Y < (size.Height / 2 * 0.2) * 2.5 && !down)
                            {
                                playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jump", "jump0.png"), UriKind.RelativeOrAbsolute)));
                                Changed?.Invoke(this, null);
                            }

                            if (Y < size.Height / 2 * 0.2 && !down) //elérem a tetejét - jumping kikapcsol és ereszkedni kéne
                            {
                                jumping = false;
                                playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jump", "jump2.png"), UriKind.RelativeOrAbsolute)));
                                down = true;
                                Changed?.Invoke(this, null);
                            }

                            PlayerHitbox = new Rect(X, Y, size.Height / 4, size.Height / 4);
                            groundHitBox = new Rect(0, groundheight, size.Width, 1);
                            bool x = PlayerHitbox.IntersectsWith(groundHitBox);

                            if (down && !x)
                            {

                                if (GoinDown && counter % 3 == 0)
                                {
                                    playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jump", "jump3.png"), UriKind.RelativeOrAbsolute)));
                                    GoinDown = false;
                                    Changed?.Invoke(this, null);
                                }
                                Y += 20;//elsőre lemenni
                                counter += 1;
                                GoinDown = true;
                            }
                            else
                            {
                                down = false;
                            }

                            if (x)
                            {
                                down = false;
                                speed = 0;
                                jumping = false;
                                if (!IsInForm)
                                {
                                    spriteIndex += 0.5;
                                    if (spriteIndex > 6)
                                    {
                                        spriteIndex = 1;
                                    }
                                    RunMadara(spriteIndex);

                                    if (Hitted)
                                    {
                                        if (!jumping)
                                        {
                                            playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "hit2.png"), UriKind.RelativeOrAbsolute)));
                                            Hitted = false;
                                        }
                                    }
                                }
                                else
                                {

                                    susanoIndex += 0.5;
                                    if (susanoIndex > 16)
                                    {
                                        susanoIndex = 1;
                                    }

                                    if (ChaningBack) //true
                                    {
                                        ChangeBackForm(susanoIndex);
                                    }
                                    else
                                    {
                                        InForm(susanoIndex);
                                    }

                                }
                            }

                        }
                        else if (!IsStanding) //Madara várakozik, hogy elinduljon a game.
                        {
                            MadaraWaitingStart();
                        }


                        if (IsSkilledOne)
                        {
                            if (!IsInForm)
                            {
                                spriteIndex += 0.5;
                                if (spriteIndex > 20)
                                {
                                    IsSkilledOne = false; //animáció vége
                                                          //Katoon-nak vége ezután kilőjük magát a skillt
                                    SkillShoot = true;
                                }
                                SkillOneMadara(spriteIndex);
                            }
                            else
                            {
                                IsSkilledOne = false;
                                SkillShoot = true;
                            }

                        }

                        if (IsSkilledTwo)
                        {
                            canjump = true;
                            if (SkillTwoIndex > 31)
                            {
                                IsSkilledTwo = false; //animáció vége bemegyünk formba
                                Once = false;
                                IsInForm = true;
                                SkillTwoIndex = 0;
                                timer.Start();
                                Delay.Start();
                            }
                            SkillTwoMadara(SkillTwoIndex);
                            SkillTwoIndex += 0.5;
                            if (SkillTwoIndex > 9)
                            {
                                drawform = true;
                            }
                        }

                        //miután kirajzolódott a fireball mehetunk előtte 1 picivel mehetünk.
                        if (SkillShoot)
                        {
                            GoAfterSkill = false;
                            if (recast)
                            {
                                SkillShootX = old_skillx;
                                SkillShootY = old_skilly;
                                recast = false;
                            }

                            if (SkillShootX > size.Width * 1.1)
                            {
                                //Hide de ki is kell rajzolni.
                                SkillShootX = 0 - size.Width;
                                SkillShootY = 0 - size.Height;
                            }
                            else
                            {
                                //csúsztassuk
                                SkillShootX += 50;
                                SkillHitbox = new Rect(SkillShootX, SkillShootY, size.Width / 3 + 20, size.Height / 3 + 20);
                                SkillMovement();

                            }
                            if (SkillShootX > size.Width * 1.1) //ha már eltalt 1 enemy-t vagy kiért a mezőből
                            {
                                SkillShoot = false;
                                recast = true;
                            }
                        }
                        //shuriken mozgás
                        if (IsStanding && !IsSkilledTwo) //Shuriken move és logic
                        {
                            if (youcanspawnenemy)
                            {
                                EnemyMovement();
                            }
                            else
                            {

                                ShurikenMovement();
                                if (PlayerHitbox.IntersectsWith(obstacleHitbox) && !DisapearShuriken)
                                {
                                    DisapearShuriken = true;
                                    if (IsSkilledOne)
                                    {
                                        playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Jutsu", "jutsuhit.png"), UriKind.RelativeOrAbsolute)));
                                    }
                                    else
                                    {
                                        if (!jumping && !IsInForm)
                                        {
                                            playerSprite = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images/Run", "hit1.png"), UriKind.RelativeOrAbsolute)));
                                            Hitted = true;

                                        }
                                    }
                                    HpBarChanged(hp);
                                    if (!IsInForm)
                                    {
                                        hp -= 1;
                                        HpBarChanged(hp);
                                        HurtMadara.Play();
                                    }
                                }
                            }

                            if (ManaChanche && !IsInForm && !IsSkilledOne) //50% esély a spawnolásra - lassabban mozogjon mint a shuriken
                            {
                                //ha igaz akkor spawnolhat
                                ManaMovement();
                                if (PlayerHitbox.IntersectsWith(manaHitbox) && !DisapearMana)
                                {
                                    if (mana < 5)
                                    {
                                        mana++;
                                        if (mana == 5)
                                        {
                                            Once = true; //újra tudja használni!
                                        }
                                    }
                                    ManaX = size.Width * 1.5;
                                    DisapearMana = true;
                                    ManaChanche = false; //kikapcsoolom hha vége az animacionak és elindítom a timert megint
                                    BonusMana.Start();
                                    TopOrBot = false;
                                    ManaMove(r.Next(1, 6));
                                }
                            }
                            if (IsInForm)
                            {
                                ManaX = size.Width * 1.5;
                            }
                        }
                    }
                }
            }
            else
            {
                if (TimeLimit > 7) //7*30ms = 210ms = 2.1mp
                {
                    //dead animacio
                    //kis delay kellene
                    DeadMadaraIndex += 0.5;
                    if (DeadMadaraIndex > 95)
                    {
                        gameOver = true;
                    }
                    DeadMadara(DeadMadaraIndex);
                }
                else
                {
                    TimeLimit += 1;
                    if (TimeLimit == 7)
                    {
                        WoodStyle.Play();
                    }
                }
            }
            Changed?.Invoke(this, null);
        }

    }
}
