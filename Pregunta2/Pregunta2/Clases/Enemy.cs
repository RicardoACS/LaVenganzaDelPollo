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
        public Rectangle colisionRight;
        Texture2D hombreRight, disparoHombre;
        Vector2 posicionRight = new Vector2(0,500);
        Vector2 posicionleft = new Vector2(600, 0);
        Rectangle sourceRect;
        Rectangle destRect;
        float rotacion;
        int speed, delayHuevo=150;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        public bool colision, destruido, isVisible;
        Random random = new Random();
        public List<Clases.Huevo> listHuevo;
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
            listHuevo = new List<Huevo>();
            posicionRight = newPosicion;
            hombreRight = newTexture;
            disparoHombre = huevo;
            isVisible = true;
            delayHuevo = 150;
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
                h.posicion.X = h.posicion.X + h.speed;

                if (h.posicion.Y >= 950)
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
                h.posicion = new Vector2(posicionleft.X - hombreRight.Width / 2 - hombreRight.Width / 2, posicionleft.Y + 500);
                //h.posicion = new Vector2(0, 465);
                h.isVisible = true;

                if (listHuevo.Count() < 20)
                {
                    listHuevo.Add(h);
                }
            }
            if (delayHuevo == 0)
                delayHuevo = 150;
        }
    }
}
