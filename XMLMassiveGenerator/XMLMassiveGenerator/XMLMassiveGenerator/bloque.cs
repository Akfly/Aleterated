using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace XMLMassiveGenerator
{
    public class bloque
    {
        // Textura de la letra
        Texture2D[] agente;

        // Posición actual de la letra
        Vector2 position;

        // Siguiente posición de la letra
        Vector2 finalPosition;

        // Centro de la circunferencia que realiza la letra
        Vector2 pivotPosition;

        // 
        Vector2 spriteOrigin;


        //estado de la palbra
        enum estado { normal, pulsada, acierto }


        // Separación de las letras
        float rotation;

        // La letra ha sido pulsada
        public bool bIsHit;

        // Velocidad de rotación de la letra
        float speedRotation;

        // ID de la letra
        public char ID;

        /// <summary>
        /// Indica si la letra se ha acertado o no
        /// </summary>
        public bool bIsGuessed;

        /// <summary>
        /// Sonido que se reproduce al seleccionar una letra
        /// </summary>
        SoundEffect SelectSound;

        /// <summary>
        /// Método que inicializa el contenido de la clase
        /// </summary>
        /// <param name="tex">Textura de la letra</param>
        /// <param name="pivot">Centro de la circunferencia que realiza la letra</param>
        /// <param name="radius">Radio de distancia en el que rota la letra</param>
        /// <param name="rot"> Separación de las letras</param>
        /// <param name="speed">Velocidad de rotación de la letra</param>
        public void Initialize(Texture2D[] texture, SoundEffect sonido, Vector2 pivot, float radius, float rot, float speed, char myID)
        {
            agente = texture;
            spriteOrigin = new Vector2(agente[0].Width / 2, agente[0].Height / 2);
            position = new Vector2(0, radius);
            pivotPosition = pivot;
            rotation = rot;
            speedRotation = speed;
            bIsHit = false;
            ID = myID;
            bIsGuessed = false;
            SelectSound = sonido;


        }
    }
}
