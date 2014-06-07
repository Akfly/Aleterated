using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;



namespace XMLMassiveGenerator
{
    public class Palabra
    {
        public string palabra;
        public int nivel;

        public Palabra(string p, int l)
        {

            this.palabra = p;
            this.nivel = l;
        }
    }
}
