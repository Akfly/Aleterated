using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Aleterated
{
    /// <summary>
    /// Game menu
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Needed to draw
        /// </summary>
        GraphicsDevice MyGraphics;

        /// <summary>
        /// Title Background
        /// </summary>
        Texture2D Background;

        /// <summary>
        /// To start playign
        /// </summary>
        Texture2D PlayButton;
        

        /// <summary>
        /// Resume play (load)
        /// </summary>
        Texture2D ContinueButton;
        

        /// <summary>
        /// Exit the app
        /// </summary>
        Texture2D ExitButton;

        /// <summary>
        /// Button positions
        /// </summary>
        Rectangle Button1;
        Rectangle Button2;
        Rectangle Button3;

        /// <summary>
        /// If there is a save file found
        /// </summary>
        bool bIsSaved;

        /// <summary>
        /// Sounds that plays when a button is pressed
        /// </summary>
        SoundEffect SelectSound;

        /// <summary>
        /// Game State
        /// </summary>
        public Game1.GameState MyStatus;

        /// <summary>
        /// title Texture
        /// </summary>
        Texture2D title;


        /// <summary>
        /// Initialize class variables
        /// </summary>
        /// <param name="bg">Background texture</param>
        /// <param name="bot1">play button texture</param>
        /// <param name="bot2">continue button texture</param>
        /// <param name="bot3">exit button texture</param>
        /// <param name="sound">click sound</param>
        /// <param name="gd">graphics manager</param>
        /// <param name="status">game state</param>
        /// <param name="saveexists">If a safe file is found</param>
        public void Initialize(Texture2D bg, Texture2D bot1, Texture2D bot2, Texture2D bot3,Texture2D texturaTitulo, SoundEffect sound, GraphicsDevice gd, Game1.GameState status, bool saveexists) {

            this.MyGraphics = gd;
            this.bIsSaved = saveexists;
            this.MyStatus = status;
            this.Background = bg;
            this.PlayButton = bot1;
            this.ContinueButton = bot2;
            this.ExitButton = bot3;
            this.SelectSound = sound;

            this.title = texturaTitulo;

            //Definen los rectangulos sobre los que se pintan los botones
            this.Button1 = new Rectangle((MyGraphics.Viewport.Width / 2) - PlayButton.Width / 2, (MyGraphics.Viewport.Height / 2) - (int)PlayButton.Height + 20, PlayButton.Width, PlayButton.Height);
            this.Button2 = new Rectangle((MyGraphics.Viewport.Width / 2) - PlayButton.Width / 2, MyGraphics.Viewport.Height / 2 + 20, PlayButton.Width, PlayButton.Height);
            this.Button3 = new Rectangle((MyGraphics.Viewport.Width / 2) - PlayButton.Width / 2, (MyGraphics.Viewport.Height / 2) + (int)ExitButton.Height + 20, PlayButton.Width, PlayButton.Height);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="deltaTime">time between frames</param>
        /// <param name="touchPosition">screen touch location</param>
        public void Update(float deltaTime, Vector2 touchPosition, TouchLocationState TouchState)
        {
            if (TouchState == TouchLocationState.Released)
            {
                //If a save file is found we show the Continue button
                if (bIsSaved)
                {

                    if (Button1.Intersects(new Rectangle((int)touchPosition.X, (int)touchPosition.Y, 0, 0)))
                    {
                        SelectSound.Play();
                        this.MyStatus = Game1.GameState.INGAME;
                    }
                    else if (Button2.Intersects(new Rectangle((int)touchPosition.X, (int)touchPosition.Y, 0, 0)))
                    {
                        SelectSound.Play();
                        this.MyStatus = Game1.GameState.LOADGAME;
                    }
                    else if (Button3.Intersects(new Rectangle((int)touchPosition.X, (int)touchPosition.Y, 0, 0)))
                    {
                        SelectSound.Play();
                        this.MyStatus = Game1.GameState.EXIT;
                    }
                }
                //If no asve file
                else
                {

                    if (Button1.Intersects(new Rectangle((int)touchPosition.X, (int)touchPosition.Y, 0, 0)))
                    {
                        SelectSound.Play();
                        this.MyStatus = Game1.GameState.INGAME;
                    }
                    else if (Button2.Intersects(new Rectangle((int)touchPosition.X, (int)touchPosition.Y, 0, 0)))
                    {
                        SelectSound.Play();
                        this.MyStatus = Game1.GameState.EXIT;
                    }
                }
            }
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">Renderer</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            
            //Draws the background
            spriteBatch.Draw(Background, new Rectangle(0, 0, MyGraphics.Viewport.Width, MyGraphics.Viewport.Height), Color.Green);
            spriteBatch.Draw(title, new Rectangle((MyGraphics.Viewport.Width / 2) -(title.Width+190) / 2, -35, title.Width+200, title.Height+100), Color.White);
            
            if (bIsSaved) {

                spriteBatch.Draw(PlayButton, Button1, Color.White);
                spriteBatch.Draw(ContinueButton, Button2, Color.White);
                spriteBatch.Draw(ExitButton, Button3, Color.White);
            }
            else {

                spriteBatch.Draw(PlayButton, Button1, Color.White);
                spriteBatch.Draw(ExitButton, Button2, Color.White);
            }
            
        }
    }
}
