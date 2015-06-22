using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pregunta2.Clases
{
    class PajaroRight
    {
        Texture2D pajaroRight,animacionActual;
        Rectangle destRect;
        Rectangle sourceRect;
        KeyboardState kb;
        Vector2 velocidadPajaro = new Vector2(0, 100);
        float elapsed;
        float delay = 200f;
        int frames = 0, speed = 2;


        public PajaroRight()
        {

        }
        public void Initialize()
        {
            destRect = new Rectangle(100, 100, 142, 111);
        }


        public void LoadContent(ContentManager Content)
        {


            pajaroRight = Content.Load<Texture2D>("pajaroLeft");
            animacionActual = pajaroRight;

        }

        protected void UnloadContent()
        {

        }
        public void animacion(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(175 * frames, 0, 160, 111);
        }
        public void Update(GameTime gameTime)
        {

            velocidadPajaro.X = velocidadPajaro.X + speed;
            if (velocidadPajaro.X >= 650)
            {
                velocidadPajaro.X = -200;
            }

            animacionActual = pajaroRight;
            animacion(gameTime);
            destRect = new Rectangle((int)velocidadPajaro.X, (int)velocidadPajaro.Y, 142, 111);
        }
       
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(animacionActual, destRect, sourceRect, Color.White);
            
        }
    }
}
