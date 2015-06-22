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
    class Enemy
    {
        //Creacion Sprit Personaje
        public Rectangle colisionRight;
        Texture2D hombreRight, disparoHombre;
        Vector2 posicionRight = new Vector2(0,500);
        Vector2 posicionleft = new Vector2(600, 0);
        Rectangle sourceRect;
        Rectangle destRect;
        //Creacion para Sprit moviemiento y delay del disparo
        int speed, delayPiedra=150;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        public bool isVisible;
        Random random = new Random();
        public List<Clases.ProyectilEnemigo> listaPiedra;
        float randX, randY;

        public Enemy(Texture2D newTexture, Vector2 newPosicion)
        {
            posicionRight = newPosicion;
            hombreRight = newTexture;
            speed = 2;
            isVisible = true;
            randX = random.Next(0, 750);
            randY = random.Next(-600, -50);
        }
        public Enemy(Texture2D newTexture, Vector2 newPosicion, Texture2D huevo)
        {
            listaPiedra = new List<Clases.ProyectilEnemigo>();
            posicionRight = newPosicion;
            hombreRight = newTexture;
            disparoHombre = huevo;
            isVisible = true;
            delayPiedra = 150;
            speed = 2;
        }

        public void loadContent(ContentManager Content)
        {
            //hombreRight = Content.Load<Texture2D>("hombreLeft");
            //origen.X = hombreRight.Width / 2;
            //origen.Y = hombreRight.Height/ 2;
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
                colisionRight = new Rectangle((int)posicionRight.X, (int)posicionRight.Y, 45, 45);
                posicionRight.X = posicionRight.X + speed;
                if (posicionRight.X >= 650)
                {
                    posicionRight.X = -50;
                }
            
                animacion(gameTime);
                disparoEnemigo();
                updateHuevos();
                destRect = new Rectangle((int)posicionRight.X, (int)posicionRight.Y, 135, 135);
                
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isVisible)
            {
                //spriteBatch.Draw(hombreLeft, posicion, null, Color.White, rotacion, origen, 1.0f, SpriteEffects.None, 0f);  
                spriteBatch.Draw(hombreRight, destRect, sourceRect, Color.White);
                foreach (Clases.ProyectilEnemigo p in listaPiedra)
                {
                    p.Draw(spriteBatch);
                }
            }
        }
        public void updateHuevos()
        {
            foreach (Clases.ProyectilEnemigo p in listaPiedra)
            {

                p.colisionProyectil = new Rectangle((int)p.posicion.X, (int)p.posicion.Y, p.textura.Width, p.textura.Height);
                p.posicion.X = p.posicion.X + p.speed;

                if (p.posicion.Y >= 950)
                    p.isVisible = false;

            }
            for (int i = 0; i < listaPiedra.Count; i++)
            {
                if (!listaPiedra[i].isVisible)
                {
                    listaPiedra.RemoveAt(i);
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
                p.posicion = new Vector2(posicionleft.X - hombreRight.Width / 2 - hombreRight.Width / 2, posicionleft.Y + 500);
                //p.posicion = new Vector2(0, 465);
                p.isVisible = true;

                if (listaPiedra.Count() < 20)
                {
                    listaPiedra.Add(p);
                }
            }
            if (delayPiedra == 0)
                delayPiedra = 100;
        }
    }
}
