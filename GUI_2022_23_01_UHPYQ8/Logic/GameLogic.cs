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


            Intro = false;
            EscON = false;

            hp = 5;


            WaterSound.MediaEnded += WaterSound_MediaEnded;
            HurtMadara.MediaEnded += HurtMadara_MediaEnded;
            Susano.MediaEnded += Susano_MediaEnded;

            Susano.Open(new Uri(System.IO.Path.Combine("Music", "Susano.mp3"), UriKind.RelativeOrAbsolute));
            MainMusic.Open(new Uri(System.IO.Path.Combine("Music", "MainGameTheme.wav"), UriKind.RelativeOrAbsolute));
            HurtMadara.Open(new Uri(System.IO.Path.Combine("Music", "hurtmadara.wav"), UriKind.RelativeOrAbsolute));
            HurtMadara.Volume = 0.9;
            IntroMedia.Open(new Uri(System.IO.Path.Combine("Videos", "Intro.mp4"), UriKind.RelativeOrAbsolute));
            IntroMedia.MediaEnded += IntroMedia_MediaEnded;
        }
        private void SpawnMana(object sender, System.Timers.ElapsedEventArgs e)
        {
        }
        private void Delay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
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
        private void IntroMedia_MediaEnded(object sender, EventArgs e)
        {
            Intro = true;
        }
        public void Resize(Size size)
        {
            this.size = size;
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
                   
                    break;
                case ControlKey.skillone:
                    
                    break;
                case ControlKey.enter:
                    
                    break;
                case ControlKey.esc:
                   
                    break;
                case ControlKey.skilltwo:
                    
                    break;

                default:
                    break;
            }
        }
        public void GameEngine()
        {
            if (hp >1)
            {
                if (!EscON)
                {

                }
            }
        }


    }
}
