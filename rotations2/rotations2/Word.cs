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
    class Word 
    {

        /// <summary>
        /// Number of blocks that will be on the screen
        /// </summary>
        int blockNumber;

        /// <summary>
        /// List of every neccessary blocks to complete the word
        /// </summary>
        public List<Block> blockList;

        /// <summary>
        /// Angle between blocks
        /// </summary>
        public float stepAngle;

        /// <summary>
        /// Letter radius
        /// </summary>
        public float radius;

        /// <summary>
        /// speed of the blocks
        /// </summary>
        public float rotationSpeed;

        /// <summary>
        /// String of the word
        /// </summary>
        public string word;

        /// <summary>
        /// Difficulty level of the word
        /// </summary>
        public int level;

        /// <summary>
        /// Initialize class variables
        /// </summary>
        /// <param name="gameWord">The word itself</param>
        /// <param name="lvl">Nivel a la que pertenece</param>
        public void Initialize(string gameWord, int lvl)
        {
            radius = 120;
            rotationSpeed = 36;
            blockList = new List<Block>();

            word = gameWord;
            blockNumber = word.Length;
            stepAngle = 360 / blockNumber;

            level = lvl;
        }

        /// <summary>
        /// Initialize each block individually
        /// </summary>
        /// <returns></returns>
        public char[] InitializeNewWord()
        {
            char[] ArrChar = word.ToCharArray();

            // Adds blocks to the list
            for (int i = 0; i < ArrChar.Length; i++)
            {
                Block b = new Block();
                blockList.Add(b);
            }
            return ArrChar;
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        /// <param name="touchLocation">Position where the screen was touched</param>
        public void Update(GameTime gameTime, Vector2 touchLocation, TouchLocationState TouchState)
        {
            // TODO: Add your update logic here
            // Block update
            foreach (Block b in blockList)
                b.Update((float)gameTime.ElapsedGameTime.TotalSeconds, touchLocation, TouchState);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {

            // TODO: Add your drawing code here

            // Block Draw
            foreach (Block b in blockList)
                b.Draw(spriteBatch);

        }
    }
}
