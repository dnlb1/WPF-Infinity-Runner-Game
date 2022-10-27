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
