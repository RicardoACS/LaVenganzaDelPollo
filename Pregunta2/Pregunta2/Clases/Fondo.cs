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
using Microsoft.Xna.Framework.Media;

namespace Pregunta2.Clases
{
    /******************************************/
    /*              Created By                */
    /*                                        */
    /*          Ricardo Carrasco Soto.-       */
    /*******************************************/ 
    class Fondo
    {
        Texture2D texture;
        Vector2 bgPosicion, bgPosicion2;
        public Fondo()
        {
            texture = null;
            bgPosicion = new Vector2(0, 0);
            bgPosicion2 = new Vector2(0, -950);
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("background");
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bgPosicion, Color.White);
        }
    }
}
