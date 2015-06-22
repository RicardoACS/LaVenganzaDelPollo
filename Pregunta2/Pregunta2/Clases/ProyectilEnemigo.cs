using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pregunta2.Clases
{
    /******************************************/
    /*              Created By                */
    /*                                        */
    /*          Ricardo Carrasco Soto.-       */
    /*******************************************/ 
    class ProyectilEnemigo
    {
         public Rectangle colisionProyectil;
         public Texture2D textura;
         public Vector2 posicion;
         public bool isVisible;
         public float speed;

         public ProyectilEnemigo(Texture2D nuevaTextura)
         {
             speed = 5;
             textura = nuevaTextura;
             isVisible = false;
             colisionProyectil = new Rectangle((int)posicion.X, (int)posicion.Y, 30, 30);
         }
         public void update(GameTime gameTime)
         {
             colisionProyectil = new Rectangle((int)posicion.X, (int)posicion.Y, 30, 30);
         }
         public void Draw(SpriteBatch spriteBatch)
         {
             spriteBatch.Draw(textura, posicion, Color.White);
         }
        
    }
}
