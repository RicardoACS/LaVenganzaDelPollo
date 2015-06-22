using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Pregunta2.Clases
{
    class Musica
    {
        public Song bgMusica;

        public Musica()
        {
            bgMusica = null;
        }

        public void loadContetent(ContentManager Content)
        {
            bgMusica = Content.Load<Song>("bgGame");
        }
    }
}
