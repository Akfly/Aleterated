using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.IO.IsolatedStorage;
using XMLPalabras;

namespace Aleterated
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Needed for drawing
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// The word used to play
        /// </summary>
        Word auxWord;

        /// <summary>
        /// Different words for each level
        /// </summary>
        List<Word>[] wordList;

        /// <summary>
        /// Random declaration
        /// </summary>
        Random MyRand;

        /// <summary>
        /// game Background
        /// </summary>
        Texture2D Background;

        /// <summary>
        /// Game Menu
        /// </summary>
        Menu MyMenu;

        /// <summary>
        /// Play Button
        /// </summary>
        Texture2D MenuButton1;

        /// <summary>
        /// Continue Button
        /// </summary>
        Texture2D MenuButton2;

        /// <summary>
        /// Exit Button
        /// </summary>
        Texture2D MenuButton3;

        /// <summary>
        /// Title
        /// </summary>
        Texture2D titleTexture;

        /// <summary>
        /// Game Score
        /// </summary>
        int Score;

        /// <summary>
        /// Game status
        /// </summary>
        public enum GameState { MENU, LOADGAME, INGAME, EXIT, FINISH }

        /// <summary>
        /// actual game Status
        /// </summary>
        GameState MyStatus;

        /// <summary>
        /// Guessed letters so far
        /// </summary>
        string UncompletedWord;

        /// <summary>
        /// Main theme
        /// </summary>
        Song Music;

        /// <summary>
        /// Sound when selecting a letter
        /// </summary>
        SoundEffect Blop;

        /// <summary>
        /// Sound when selecting a menu button
        /// </summary>
        SoundEffect Boing;

        /// <summary>
        /// Font with which we write in the game
        /// </summary>
        SpriteFont Font;

        /// <summary>
        /// Level counter
        /// </summary>
        int Level;

        /// <summary>
        /// Gumber of guessed words
        /// </summary>
        int correctAnswers;

        /// <summary>
        /// Store each different (actual) touches on the screen (multitouch)
        /// </summary>
        TouchCollection Touches;

        /// <summary>
        /// Array in we store the words from an xml
        /// </summary>
        XMLWords.WordList[] list;

        /// <summary>
        /// Background of the time meter
        /// </summary>
        Texture2D bgMeter;

        /// <summary>
        /// texture of the time bar
        /// </summary>
        Texture2D timeBar;

        /// <summary>
        /// time visualization
        /// </summary>
        Texture2D timeContainer;

        /// <summary>
        /// Helping word (top left corner)
        /// </summary>
        StateWord stateWord;

        /// <summary>
        /// if a word has been chosen to start playing
        /// </summary>
        bool began;

        /// <summary>
        /// Time left
        /// </summary>
        float time;

        /// <summary>
        /// maximum left time
        /// </summary>
        const int maxTime = 60;

        /// <summary>
        /// Touch state (pressed, move and raised)
        /// </summary>
        TouchLocationState touchState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            // Enables the tap gesture (touch) on the screen
            TouchPanel.EnabledGestures = GestureType.Tap;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.MyRand = new Random();

            //First state is menu
            this.MyStatus = GameState.MENU;
            this.MyMenu = new Menu();

            this.Score = 0;
            this.Level = 1;
            this.UncompletedWord = "";

            // Initialize each word
            this.wordList = new List<Word>[5];
            for (int i = 0; i < 5; i++) {

                this.wordList[i] = new List<Word>();
            }


            this.auxWord = new Word();
            
            this.began = false;

            //initial time
            this.time = 20;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Textures
            Background = Content.Load<Texture2D>("fondo");
            MenuButton1 = Content.Load<Texture2D>("jugar");
            MenuButton2 = Content.Load<Texture2D>("continuar");
            MenuButton3 = Content.Load<Texture2D>("salir");
            
            //BG
            titleTexture = Content.Load<Texture2D>("aletterated");

            //sound
            Boing = Content.Load<SoundEffect>("boing");
            Blop = Content.Load<SoundEffect>("blop");
            Music = Content.Load<Song>("maintheme");
            Font = Content.Load<SpriteFont>("Font");

            //time bar
            bgMeter = Content.Load<Texture2D>("barra-roja");
            timeContainer = Content.Load<Texture2D>("contorno-morado");
            timeBar = Content.Load<Texture2D>("barra-verde-2º");

            //music
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(Music);

            //Initiailze the menu with the loaded textures
            MyMenu.Initialize(Background, MenuButton1, MenuButton2, MenuButton3, titleTexture, Boing, GraphicsDevice, MyStatus, IsolatedStorageFile.GetUserStoreForApplication().FileExists("saved_game.txt"));

            LoadList();
        }

        /// <summary>
        /// initialize StaetWord
        /// </summary>
        /// <param name="numLetters">total number of letters that the new word has</param>
        public void InitializeNewStateWord(int numLetters)
        {
            //auxiliar textures to load the real ones
            Texture2D[] nonGuessedTexture = new Texture2D[numLetters];
            Texture2D[] guessedTexture = new Texture2D[numLetters];

            //Converts the word into an array
            char[] igWord = auxWord.word.ToCharArray();

            //Add the textures to the auxiliar arrays
            for (int i = 0; i < numLetters; i++)
            {
                nonGuessedTexture[i] = Content.Load<Texture2D>(igWord[i].ToString() + "1");
                guessedTexture[i] = Content.Load<Texture2D>(igWord[i].ToString() + "2");
            }

            stateWord = new StateWord(nonGuessedTexture, guessedTexture, auxWord.word.ToCharArray());

            began = true;
        }

        /// <summary>
        /// Load the XML that contains the list of words
        /// </summary>
        private void LoadList()
        {
            this.list = Content.Load<XMLWords.WordList[]>("listapalabras");

            //Switch levels
            for (int i = 0; i < 5; i++)
            {
                //Adds 20 words for each level
                for (int j = 0; j < 20; j++)
                {
                    Word word = new Word();
                    word.Initialize(list[j + 20 * i].word, list[j + 20 * i].level);
                    wordList[i].Add(word);
                }
            }
        }

        /// <summary>
        /// Sets a new word
        /// </summary>
        protected void InitializeNewWord()
        {
             //Selects a random word from a list (in the actual level)
             int wordNumber = MyRand.Next(0, wordList[Level-1].Count - 1);
             char[] ArrRandom = wordList[Level - 1][wordNumber].word.ToCharArray();
             auxWord.Initialize(wordList[Level - 1][wordNumber].word, wordList[Level - 1][wordNumber].level);
                
             //Shuffle the letter order
             char[] ArrChar = ArrRandom;
             for (int i = 0; i < ArrRandom.Length; i++)
             {
                 int k = MyRand.Next(i + 1);
                 char w = ArrRandom[k];
                 ArrRandom[k] = ArrRandom[i];
                 ArrRandom[i] = w;
            }

             auxWord.InitializeNewWord();

            //A vector2 which the letters will rotate around
            Vector2 position = new Vector2((GraphicsDevice.Viewport.Width / 2) + 100, GraphicsDevice.Viewport.Height / 2);
            //Initialize each letter
            for (int i = 0; i < ArrChar.Length; i++)
            {
                Texture2D [] agente = new Texture2D[2];
                agente[0] = Content.Load<Texture2D>(ArrChar[i].ToString() + "1");
                agente[1] = Content.Load<Texture2D>(ArrChar[i].ToString() + "3");
                auxWord.blockList[i].Initialize(agente, Blop, position, auxWord.radius, auxWord.stepAngle * i, auxWord.rotationSpeed, ArrChar[i]);
                auxWord.blockList[i].bIsGuessed = false;
                auxWord.blockList[i].bIsHit = false;
            }
            UncompletedWord = "";
            InitializeNewStateWord(auxWord.word.Length);
        }
        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Save every game data
        /// </summary>
        protected void SaveGame()
        {
            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            //file path
            using (var isoFileStream = new IsolatedStorageFileStream("saved_game.txt", FileMode.OpenOrCreate, myStore))
            {   // Write data
                using (var isoFileWriter = new StreamWriter(isoFileStream))
                {
                    string data = Score.ToString() + "|" + UncompletedWord + "|" + auxWord.level.ToString() + "|" + auxWord.word + "|" + correctAnswers.ToString() + "|" + time.ToString();
                    isoFileWriter.WriteLine(data);

                    for (int i = 0; i < auxWord.word.Length; i++)
                    {
                        string datablock = auxWord.blockList[i].position.X + "|" + auxWord.blockList[i].position.Y + "|" + auxWord.blockList[i].ID.ToString() + "|" + auxWord.blockList[i].bIsGuessed.ToString();
                        isoFileWriter.WriteLine(datablock);
                    }
                }
            }
        }

        /// <summary>
        /// Load a save file
        /// </summary>
        protected void LoadGame()
        {

            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            if (myStore.FileExists("saved_game.txt"))
            {
                //  path
                using (var isoFileStream = new IsolatedStorageFileStream("saved_game.txt", FileMode.Open, myStore))
                {
                    //  Read data
                    using (var isoFileReader = new StreamReader(isoFileStream))
                    {
                        int lvl;
                        string word;

                        string[] datos = isoFileReader.ReadLine().Split('|');
                        Score = Convert.ToInt32(datos[0]);
                        UncompletedWord = Convert.ToString(datos[1]);
                        lvl = Convert.ToInt32(datos[2]);
                        word = Convert.ToString(datos[3]);
                        correctAnswers = Convert.ToInt32(datos[4]);
                        time = (float)Convert.ToDecimal(datos[5]);

                        auxWord = new Word();
                        auxWord.Initialize(word, lvl);

                        List<Block> bList = new List<Block>();

                        for (int i = 0; i < word.Length; i++)
                        {

                            Block b = new Block();
                            Texture2D[] agent = new Texture2D[3]; ;

                            string[] datosbloque = isoFileReader.ReadLine().Split('|');

                            agent[0] = Content.Load<Texture2D>(datosbloque[2] + "1");
                            agent[1] = Content.Load<Texture2D>(datosbloque[2] + "3");
                            agent[2] = Content.Load<Texture2D>(datosbloque[2] + "2");

                            b.Initialize(agent, Blop, new Vector2((GraphicsDevice.Viewport.Width / 2) + 100, GraphicsDevice.Viewport.Height / 2), auxWord.radius, auxWord.stepAngle * i, auxWord.rotationSpeed, System.Convert.ToChar(datosbloque[2]));
                            b.bIsGuessed = Convert.ToBoolean(datosbloque[3]);
                            b.position = new Vector2(Convert.ToInt32(datosbloque[0]), Convert.ToInt32(datosbloque[1]));
                            bList.Add(b);

                        }
                        auxWord.blockList = bList;

                        InitializeNewStateWord(auxWord.word.Length);

                    }
                }
            }

            if (auxWord == null) { InitializeNewWord(); }
        }

        /// <summary>
        /// If the word has been completed, we change into a new one
        /// </summary>
        protected void CheckFinishWord()
        {
            if (auxWord.word == UncompletedWord)
            {
                //Remove the word from the list
                for (int i = 0; i < wordList[Level-1].Count; i++)
                {
                    if (wordList[Level - 1][i].word == auxWord.word)
                    {
                        wordList[Level - 1].RemoveAt(i);
                    }
                }

                CheckLevel();

                correctAnswers++;

                InitializeNewWord();

                //Raise time
                if (time+10 > 60)
                    time = 60;
                else
                    time += 10;

                //Raise score
                Score += (int)time;    
            }
        }

        /// <summary>
        /// Switch level difficulty based on completed words
        /// </summary>
        private void CheckLevel()
        {
            if (correctAnswers < 4)
                this.Level = 1;//lvl 1
            else if (correctAnswers > 3 && correctAnswers < 9)
                this.Level = 2;//lvl 2
            else if (correctAnswers > 8 && correctAnswers < 14)
                this.Level = 3;//lvl 3
            else if (correctAnswers > 13 && correctAnswers < 19)
                this.Level = 4;//lvl 4
            else if (correctAnswers > 18)
                this.Level = 5;//lvl 5
        }

        /// <summary>
        /// Gets touch position on the screen
        /// </summary>
        /// <returns></returns>
        protected Vector2 DetectTouch()
        {
            Vector2 TouchPosition = new Vector2(0, 0);

            Touches = TouchPanel.GetState();

            // The screen is multitouch, we could have gathered different touches
            foreach (TouchLocation touch in Touches)
            {
                TouchPosition = new Vector2(touch.Position.X, touch.Position.Y);

                // We can identify each touch
                int TouchID = touch.Id;
                touchState = touch.State;
            }
            return TouchPosition;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (MyStatus == GameState.INGAME)
                {
                    SaveGame();
                }
                this.Exit();
            }

            // TODO: Add your update logic here

            Vector2 touchLocation = DetectTouch();

            //Menu update
            if (MyStatus == GameState.MENU)
            {
                MyMenu.Update((float)gameTime.ElapsedGameTime.TotalSeconds, touchLocation, touchState);
                MyStatus = MyMenu.MyStatus;
                if (MyStatus == GameState.INGAME) { InitializeNewWord(); }
            }
            //In-Game update
            else if (MyStatus == GameState.INGAME)
            {
                CheckFinishWord();

                auxWord.Update(gameTime, touchLocation, touchState);
                stateWord.Update(gameTime, UncompletedWord);
                CheckPressedBlock();

                //Lower the time
                if (time - 2 * (float)gameTime.ElapsedGameTime.TotalSeconds > 0)
                {
                    time -= 2 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (time <= 1 || correctAnswers >= 30)
                {
                    MyStatus = GameState.FINISH;
                }
            }
            //Loading
            else if (MyStatus == GameState.LOADGAME) 
            { 
                LoadGame();
                MyStatus = GameState.INGAME;
            }
            //Game Exit
            else if (MyStatus == GameState.EXIT) { this.Exit(); }
            else if (MyStatus == GameState.FINISH) {

                if (IsolatedStorageFile.GetUserStoreForApplication().FileExists("saved_game.txt")) {

                    IsolatedStorageFile.GetUserStoreForApplication().DeleteFile("saved_game.txt");
                }

                if (touchState == TouchLocationState.Pressed) {

                    Initialize();
                    LoadContent();
                    MyStatus = GameState.MENU;
                }
            }
     
            base.Update(gameTime);
        }


        /// <summary>
        /// Check if a block has been pressed. If so, we remove it from the screen
        /// </summary>
        private void CheckPressedBlock()
        {
            if (UncompletedWord.Length < auxWord.word.Length)
            {
                for (int i = 0; i < auxWord.blockList.Count(); i++)
                {
                    //If it is the correct block, we switch it to guessed
                    if ((auxWord.blockList[i].bIsHit) && (auxWord.blockList[i].ID == auxWord.word.ToCharArray()[UncompletedWord.Length] &&
                       auxWord.blockList[i].bIsGuessed != true) && (touchState == TouchLocationState.Released))
                    {
                        UncompletedWord += auxWord.blockList[i].ID;
                        auxWord.blockList[i].bIsGuessed = true;          
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Background color
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //In Game
            if (MyStatus == GameState.INGAME)
            {
                spriteBatch.Draw(Background, new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                auxWord.Draw(spriteBatch);

               if(began)
                    stateWord.Draw(spriteBatch);

                spriteBatch.DrawString(Font, Score.ToString(), new Vector2(630, 30), Color.Tomato);

                spriteBatch.Draw(bgMeter,
                    new Rectangle(GraphicsDevice.Viewport.Width - (bgMeter.Width + 30), (GraphicsDevice.Viewport.Height / 2) - (bgMeter.Height / 2), bgMeter.Width, bgMeter.Height),
                    Color.White);
                spriteBatch.Draw(timeBar,
                    new Rectangle(GraphicsDevice.Viewport.Width - (timeBar.Width + 30), (GraphicsDevice.Viewport.Height / 2) - (bgMeter.Height / 2) + (60 - (int)time) * timeBar.Height / maxTime,
                        timeBar.Width, (int)time * timeBar.Height / maxTime),
                    Color.White);
                spriteBatch.Draw(timeContainer,
                    new Rectangle(GraphicsDevice.Viewport.Width - (timeContainer.Width + 30), (GraphicsDevice.Viewport.Height / 2) - (timeContainer.Height / 2), timeContainer.Width, timeContainer.Height),
                    Color.White);

            }

            else if (MyStatus == GameState.FINISH)
            {

                spriteBatch.Draw(Background, new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                
                spriteBatch.DrawString(Font, "HAS ACABADO", new Vector2((GraphicsDevice.Viewport.Width / 2) - Font.MeasureString("HAS ACABADO").X / 2, (GraphicsDevice.Viewport.Height / 2)), Color.Blue);
            }

            //Menu
            else
            {
                MyMenu.Draw(spriteBatch);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
