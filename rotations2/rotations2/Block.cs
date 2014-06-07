using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;

namespace Aleterated
{
    class Block
    {
        // Letter texture
        Texture2D[] agent;

        public Vector2 position;

        // nextPosition
        Vector2 finalPosition;

        // the letter rotates around this point
        Vector2 pivotPosition;

        Vector2 spriteOrigin;
        

        //letter state
        enum lState { normal, pressed, guessed }
        
        float rotation;

        public bool bIsHit;
        
        float speedRotation;

        public char ID;

        /// <summary>
        /// if it has been guessed
        /// </summary>
        public bool bIsGuessed;

        /// <summary>
        /// Sound that play when a letter is hit
        /// </summary>
        SoundEffect SelectSound;

        /// <summary>
        /// Initialize class variables
        /// </summary>
        /// <param name="tex">Letter texture</param>
        /// <param name="pivot">the letter rotates around this point</param>
        /// <param name="radius">radius of the letter position</param>
        /// <param name="rot"> angle between each letter</param>
        /// <param name="speed">Rotation speed</param>
        public void Initialize(Texture2D[] texture, SoundEffect sonido, Vector2 pivot, float radius, float rot, float speed, char myID)
        {
            agent = texture;
            spriteOrigin = new Vector2(agent[0].Width / 2, agent[0].Height / 2);
            position = new Vector2(0, radius);
            pivotPosition = pivot;
            rotation = rot;
            speedRotation = speed;
            bIsHit = false;
            ID = myID;
            bIsGuessed = false;
            SelectSound = sonido;
            

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="deltaTime">Provides a snapshot of timing values.</param>
        /// <param name="touchPosition">Screen touch position</param>
        public void Update(float deltaTime, Vector2 touchPosition, TouchLocationState TouchState)
        {
            rotation += speedRotation * deltaTime;
            Matrix myRotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation));
            finalPosition = Vector2.Transform(position, myRotationMatrix);
            finalPosition += (pivotPosition - spriteOrigin);

           
                // Check collision
                Rectangle rect = new Rectangle((int)finalPosition.X, (int)finalPosition.Y, (int)agent[0].Width, (int)agent[0].Height);

                if (rect.Intersects(new Rectangle((int)touchPosition.X, (int)touchPosition.Y, 0, 0)))
                {
                    if (TouchState == TouchLocationState.Released)
                    {
                        SelectSound.Play();
                    }
                    bIsHit = true;
                    System.Diagnostics.Debug.WriteLine(touchPosition);
                }
                else { bIsHit = false; }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if(!bIsGuessed)
            {
                if (bIsHit)
                {
                    spriteBatch.Draw(agent[1], finalPosition, Color.White);
                }

                else
                {
                    spriteBatch.Draw(agent[0], finalPosition, Color.White);
                }
            }
        }


    }
}
