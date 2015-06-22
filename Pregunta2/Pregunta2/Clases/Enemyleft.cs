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
        //Creacion Sprit Personaje
        public Rectangle colisionleft;
        Texture2D hombreLeft, disparoHombre;
        public Vector2 posicionleft;
        Rectangle sourceRect;
        Rectangle destRect;
        //Creacion para Sprit moviemiento y delay del disparo
        int speed, delayPiedra=120;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        public bool isVisible;
        Random random = new Random();
        public List<Clases.ProyectilEnemigo> listPiedra;
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
            listPiedra = new List<Clases.ProyectilEnemigo>();
            posicionleft = newPosicion;
            hombreLeft = newTexture;
            disparoHombre = huevo;
            isVisible = true;
            delayPiedra = 120;
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
            //Metodo Para ocupar Sprites
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
                    posicionleft.X = 800;
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
                foreach (Clases.ProyectilEnemigo h in listPiedra)
                {
                    h.Draw(spriteBatch);
                }
            }
        }
        public void updateHuevos() 
        {
            foreach (Clases.ProyectilEnemigo p in listPiedra)
            {

                    p.colisionProyectil = new Rectangle((int)p.posicion.X, (int)p.posicion.Y, p.textura.Width, p.textura.Height);
                    p.posicion.X = p.posicion.X - p.speed;

                    if (p.posicion.Y >=  500)
                        p.isVisible = false;
                
            }
            for (int i = 0; i < listPiedra.Count; i++)
            {
                if (!listPiedra[i].isVisible)
                {
                    listPiedra.RemoveAt(i);
                    i--;
                }
            }
        }
        public void disparoEnemigo()
        {
            if (delayPiedra >= 0)
                delayPiedra--;
            if (delayPiedra <= 0)
            {
                Clases.ProyectilEnemigo p = new Clases.ProyectilEnemigo(disparoHombre);
                p.posicion = new Vector2(posicionleft.X + hombreLeft.Width / 2 - hombreLeft.Width / 2, posicionleft.Y + 30);
                p.isVisible = true;

                if (listPiedra.Count() < 20)
                {
                    listPiedra.Add(p);
                }
            }
            if (delayPiedra == 0)
                delayPiedra = 120;
        }
    }
}
