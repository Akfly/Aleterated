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

namespace Aleterated
{
    /// <summary>
    /// This class manages the word that helps the player to complete the word
    /// </summary>
    class StateWord
    {
        /// <summary>
        /// Textures of the letters that haven't been guessed yet
        /// </summary>
        Texture2D[] NormalWord;

        /// <summary>
        /// Textures of the letters that have been guessed
        /// </summary>
        Texture2D[] GuessedWord;

        /// <summary>
        /// If a block has to draw blue or red
        /// </summary>
        bool[] guessings;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="myTextNormal">Green Textures</param> 
        /// <param name="myTextGuessed">Blue Textures</param>
        /// <param name="word">The word itself</param>
        public StateWord(Texture2D[] myTextNormal, Texture2D[] myTextGuessed, char[] word)
        {
            this.guessings = new bool[word.Length];
            this.NormalWord = myTextNormal;
            this.GuessedWord = myTextGuessed;

            //Sets all the guessings to "false", so every letter is in green 
            for (int i = 0; i < word.Length; i++)
            {
                this.guessings[i] = false;
            }
        }

        /// <summary>
        /// Updates the guessing array
        /// </summary>
        /// <param name="guessedWords">word string that have all the guessed words</param>
        public void CheckDraw(string guessedWords)
        {
            //Changes the value to "true" to the number of guessed letters
            for (int i = 0; i < guessedWords.Length; i++)
            {
                this.guessings[i] = true;
            }
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="guessedWords">word string that have all the guessed words</param>
        public void Update(GameTime gameTime, string guessedWords)
        {
            CheckDraw(guessedWords);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Not guessed words
            for (int i = 0; i < NormalWord.Length; i++)
            {
                if (!guessings[i])
                {
                    spriteBatch.Draw(NormalWord[i], new Vector2(57 * i + 10, 15), Color.White);
                }
            }

            //Guessed words
            for (int i = 0; i < GuessedWord.Length; i++)
            {
                if (guessings[i])
                {
                    spriteBatch.Draw(GuessedWord[i], new Vector2(57 * i + 10, 15), Color.White);
                }
            }
        }
    }
}
