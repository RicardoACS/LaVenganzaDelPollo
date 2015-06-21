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
    class Enemyleft
    {
        public Rectangle colisionleft;
        Texture2D hombreLeft, disparoHombre;
        public Vector2 posicionleft;
        Rectangle sourceRect;
        Rectangle destRect;
        int speed, delayHuevo=120;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        public bool colision, destruido, isVisible;
        Random random = new Random();
        public List<Clases.Huevo> listHuevo;
        float randX, randY;

        public Enemyleft(Texture2D newTexture, Vector2 newPosicion)
        {
            posicionleft = newPosicion;
            hombreLeft = newTexture;
            speed = 3;
            isVisible = true;
            randX = random.Next(0, 750);
            randY = random.Next(-600, -50);
        }
        public Enemyleft(Texture2D newTexture, Vector2 newPosicion,Texture2D huevo)
        {
            listHuevo = new List<Huevo>();
            posicionleft = newPosicion;
            hombreLeft = newTexture;
            disparoHombre = huevo;
            isVisible = true;
            delayHuevo = 120;
            speed = 3;
        }


        public void loadContent(ContentManager Content)
        {
            //hombreLeft = Content.Load<Texture2D>("hombreLeft");
            //origen.X = hombreLeft.Width / 2;
            //origen.Y = hombreLeft.Height/ 2;
        }

        private void animacion(GameTime gameTime)
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
            sourceRect = new Rectangle(135 * frames, 0, 135, 135);
        }

        public void update (GameTime gameTime)
        {
                colisionleft = new Rectangle((int)posicionleft.X, (int)posicionleft.Y,45, 45);
                posicionleft.X = posicionleft.X - speed;
                if (posicionleft.X <= 0)
                {
                    posicionleft.X = 700;
                }
            
                animacion(gameTime);
                disparoEnemigo();
                updateHuevos();
                destRect = new Rectangle((int)posicionleft.X, (int)posicionleft.Y, 135, 135);
                
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                //spriteBatch.Draw(hombreLeft, posicion, null, Color.White, rotacion, origen, 1.0f, SpriteEffects.None, 0f);  
                spriteBatch.Draw(hombreLeft, destRect, sourceRect, Color.White);
                foreach (Clases.Huevo h in listHuevo)
                {
                    h.Draw(spriteBatch);
                }
            }
        }
        public void updateHuevos() 
        {
            foreach (Clases.Huevo h in listHuevo)
            {

                    h.colisionHuevo = new Rectangle((int)h.posicion.X, (int)h.posicion.Y, h.textura.Width, h.textura.Height);
                    h.posicion.X = h.posicion.X - h.speed;

                    if (h.posicion.Y >=  500)
                        h.isVisible = false;
                
            }
            for (int i = 0; i < listHuevo.Count; i++)
            {
                if (!listHuevo[i].isVisible)
                {
                    listHuevo.RemoveAt(i);
                    i--;
                }
            }
        }
        public void disparoEnemigo()
        {
            if (delayHuevo >= 0)
                delayHuevo--;
            if (delayHuevo <= 0)
            {
                Clases.Huevo h = new Huevo(disparoHombre);
                h.posicion = new Vector2(posicionleft.X + hombreLeft.Width / 2 - hombreLeft.Width / 2, posicionleft.Y + 30);
                h.isVisible = true;

                if (listHuevo.Count() < 20)
                {
                    listHuevo.Add(h);
                }
            }
            if (delayHuevo == 0)
                delayHuevo = 120;
        }
    }
}
