using System;
using System.Windows;
using System.Windows.Media;
using static GUI_2022_23_01_UHPYQ8.Logic.GameLogic;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public interface IGameLogic
    {
        void Control(ControlKey key);
        void Resize(Size size);
        void GameEngine();
        int force { get; set; }
        int speed { get; set; }
        int score { get; set; }
        bool SkillShoot { get; set; }
        bool IsStanding { get; set; }
        bool Katon { get; set; }
        bool Intro { get; set; }
        bool youcanspawnenemy { get; set; }
        bool gameOver { get; set; }
        double groundheight { get; set; }
        bool DisapearShuriken { get; set; }
        bool drawform { get; set; }
        bool ManaChanche { get; set; }
        bool DisapearMana { get; set; }
        public bool IsInForm { get; set; }

        int BackgroundMoveFirst { get; set; }
        int BackgroundMoveSecond { get; set; }
        event EventHandler Changed;
        double Y { get; set; }
        double X { get; set; }
        public bool EscON { get; set; }
        ImageBrush HPBar { get; set; }
        ImageBrush EnemySprite { get; set; }
        ImageBrush playerSprite { get; set; }
        ImageBrush Background { get; set; }
        ImageBrush obstacleSprite { get; set; }
        ImageBrush SkillOne { get; set; }
        ImageBrush PressStart { get; set; }
        ImageBrush ManaBar { get; set; }
        SolidColorBrush Esc { get; set; }
        ImageBrush MadaraDead { get; set; }
        MediaPlayer MainMusic { get; set; }
        MediaPlayer HurtMadara { get; set; }
        MediaPlayer Susano { get; set; }
        MediaPlayer WaterSound { get; set; }
        MediaPlayer KatonFireStyle { get; set; }
        MediaPlayer IntroMedia { get; set; }
        ImageBrush SkillDead { get; set; }
        ImageBrush Mana { get; set; }
        double SkillShootX { get; set; }
        double SkillShootY { get; set; }
        double EnemyX { get; set; }
        double EnemyY { get; set; }
        double ObstacleY { get; set; }
        double ObstacleX { get; set; }
        double ManaY { get; set; }
        double ManaX { get; set; }
        int BackgroundSpeed { get; set; }
        int hp { get; set; }
        string Name { get; set; }
    }
}