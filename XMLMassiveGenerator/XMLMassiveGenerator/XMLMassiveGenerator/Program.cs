using System;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;


namespace XMLMassiveGenerator
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {


            Palabra[] Lista = new Palabra[100]
            {
            
                new Palabra("casa", 1),
                new Palabra("gato", 1),
                new Palabra("esne", 1),
                new Palabra("tubo", 1),
                new Palabra("nube", 1),
                new Palabra("niño", 1),
                new Palabra("rata", 1),
                new Palabra("tela", 1),
                new Palabra("saco", 1),
                new Palabra("pato", 1),
                new Palabra("sopa", 1),
                new Palabra("vaso", 1),
                new Palabra("beso", 1),
                new Palabra("cubo", 1),
                new Palabra("pelo", 1),
                new Palabra("kilo", 1),
                new Palabra("lazo", 1),
                new Palabra("lata", 1),
                new Palabra("bola", 1),
                new Palabra("lupa", 1),

                new Palabra("chino", 2),
                new Palabra("raton", 2),
                new Palabra("tarta", 2),
                new Palabra("perla", 2),
                new Palabra("arbol", 2),
                new Palabra("pinza", 2),
                new Palabra("recta", 2),
                new Palabra("curva", 2),
                new Palabra("circo", 2),
                new Palabra("juego", 2),
                new Palabra("barco", 2),
                new Palabra("araña", 2),
                new Palabra("silla", 2),
                new Palabra("coser", 2),
                new Palabra("perro", 2),
                new Palabra("sabor", 2),
                new Palabra("burro", 2),
                new Palabra("tigre", 2),
                new Palabra("lanza", 2),
                new Palabra("balon", 2),

                new Palabra("puerta", 3),
                new Palabra("tesoro", 3),
                new Palabra("alubia", 3),
                new Palabra("abuela", 3),
                new Palabra("niebla", 3),
                new Palabra("turron", 3),
                new Palabra("carton", 3),
                new Palabra("bombon", 3),
                new Palabra("tocino", 3),
                new Palabra("cocina", 3),
                new Palabra("tambor", 3),
                new Palabra("tomate", 3),
                new Palabra("tetera", 3),
                new Palabra("cereza", 3),
                new Palabra("coraza", 3),
                new Palabra("dorado", 3),
                new Palabra("roncar", 3),
                new Palabra("gruñir", 3),
                new Palabra("navaja", 3),
                new Palabra("rocoso", 3),

                new Palabra("caracol", 4),
                new Palabra("forjado", 4),
                new Palabra("tostada", 4),
                new Palabra("conjuro", 4),
                new Palabra("castaña", 4),
                new Palabra("ponzoña", 4),
                new Palabra("termino", 4),
                new Palabra("fruncir", 4),
                new Palabra("marisco", 4),
                new Palabra("gestora", 4),
                new Palabra("cascada", 4),
                new Palabra("respiro", 4),
                new Palabra("abanico", 4),
                new Palabra("alegria", 4),
                new Palabra("encanto", 4),
                new Palabra("setenta", 4),
                new Palabra("sistema", 4),
                new Palabra("soldado", 4),
                new Palabra("manzano", 4),
                new Palabra("taberna", 4),

                new Palabra("pantalon", 5),
                new Palabra("panadero", 5),
                new Palabra("justicia", 5),
                new Palabra("ladrador", 5),
                new Palabra("coliflor", 5),
                new Palabra("castillo", 5),
                new Palabra("medicina", 5),
                new Palabra("telefono", 5),
                new Palabra("codorniz", 5),
                new Palabra("pastilla", 5),
                new Palabra("pegajoso", 5),
                new Palabra("pregunta", 5),
                new Palabra("absorber", 5),
                new Palabra("aceituna", 5),
                new Palabra("vendaval", 5),
                new Palabra("adorable", 5),
                new Palabra("adverbio", 5),
                new Palabra("pinguino", 5),
                new Palabra("elefante", 5),
                new Palabra("lavadora", 5)
            };


            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("listapalabras.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, Lista, null);
            }
        }
    }

}

