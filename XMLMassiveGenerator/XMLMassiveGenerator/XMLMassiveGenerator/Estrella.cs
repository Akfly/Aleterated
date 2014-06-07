using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;



namespace XMLMassiveGenerator
{
    public class Estrella
    {
        public string nombre;
        public Vector2 coordenadas;
        public float brillo;
        public Color color;

        // Constructor
        public Estrella(string nombre, Vector2 coordenadas, float brillo, Color color)
        {
            this.nombre = nombre;
            this.coordenadas = coordenadas;
            this.brillo = brillo;
            this.color = color;
        }

    }
}
