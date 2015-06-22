using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pregunta2.Clases
{
    /******************************************/
    /*              Created By                */
    /*                                        */
    /*          Ricardo Carrasco Soto.-       */
    /*******************************************/ 
    class Huevo
    {
         public Rectangle colisionHuevo;
         public Texture2D textura;
         public Vector2 posicion,origen;
         public bool isVisible;
         public float speed;
         
         public Huevo(Texture2D nuevaTextura)
         {
             speed = 10;
             textura = nuevaTextura;
             isVisible = false;
             colisionHuevo = new Rectangle((int)posicion.X, (int)posicion.Y, 30, 30);
         }
         public void update(GameTime gameTime)
         {
             colisionHuevo = new Rectangle((int)posicion.X, (int)posicion.Y, 30, 30);
         }
         public void Draw(SpriteBatch spriteBatch)
         {
             spriteBatch.Draw(textura, posicion, Color.White);
         }
        
    }
}
