using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pregunta2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Inicio Juego Estados
        public enum Estados
        {
            menu,
            juego,
            GameOver
        }
        //Inicio de variable
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D chickenRight, chickenLeft, chickenRightDisparo, chickenLeftDisparo, animacionActual, huevoSprite;
        Texture2D barraVida, welcome, gameOver;
        Rectangle destRect, sourceRect, rectVida;
        KeyboardState kb;
        Vector2 posicionChicken = new Vector2(350,400);
        Vector2 seguimientoChicken, posicionVida;
        Vector2 velocidad;
        Rectangle Colision;
        int vida,dañoEnemigo;

        //Animacion
        float elapsed;
        float delay = 200f;
        int frames = 0;
        //Huevo
        float delayHuevo = 20;
        List<Clases.Huevo> listaHuevosChicken;
        //Fondo
        Clases.Fondo bg = new Clases.Fondo();
        //EnemyRight
        List<Clases.Enemy> listEnemyRight = new List<Clases.Enemy>();
        //EnemyLeft
        List<Clases.Enemyleft> listEnemyLeft = new List<Clases.Enemyleft>();
        Random ran = new Random();
        //Jump
        bool salto;
        //Pantalla
        int pantallaWidth;
        int pantallaHeight;
        //Musica
        Clases.Musica music = new Clases.Musica();
        //primer estado
        Estados gameMenu = Estados.menu;
        //Proyectil Humano
        List<Clases.ProyectilEnemigo> listaPiedraHumano;
        //PajaroLeft
        Clases.PajaroLeft pajaroLeft = new Clases.PajaroLeft();
        Clases.PajaroRight pajaroRight = new Clases.PajaroRight();
        public Game1()
        {
            //Creacion de listas
            listaHuevosChicken = new List<Clases.Huevo>();
            listaPiedraHumano = new List<Clases.ProyectilEnemigo>();
            // Tamaño de la pantalla
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 801;
            graphics.PreferredBackBufferHeight = 601;
            //Nombre del juego
            this.Window.Title = "La Venganza del Pollo";
            Content.RootDirectory = "Content";
            //Valores de la variables a usar
            vida = 200;
            dañoEnemigo = 20;
            posicionVida = new Vector2(50, 50);
            welcome = null;
            gameOver = null;
        }

        protected override void Initialize()
        {
            
            destRect = new Rectangle(100, 100, 100, 188);
            pajaroLeft.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
             
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Sprite Mr.Chicken
            chickenRight = Content.Load<Texture2D>("ChickenRight");
            seguimientoChicken = new Vector2(30,40);
            chickenLeft = Content.Load<Texture2D>("ChickenLeft");
            huevoSprite = Content.Load<Texture2D>("huevoGrande");
            chickenRightDisparo = Content.Load<Texture2D>("ChickenRightDisparo");
            chickenLeftDisparo = Content.Load<Texture2D>("ChickenLeftDisparo");
            //Sprite Barra de vida
            barraVida = Content.Load<Texture2D>("BarraVida");
            //Sprite Menu Juego
            welcome = Content.Load<Texture2D>("Entrada");
            //Sprite Game Over
            gameOver = Content.Load<Texture2D>("GameOver");
            //AnimacionActual, es el cual toma los Sprite de Mr.Chicken
            animacionActual = chickenRight;
            //Sprite Background
            bg.LoadContent(Content);
            //Porte de la pantalla
            pantallaWidth = GraphicsDevice.Viewport.Width + 290;
            pantallaHeight = GraphicsDevice.Viewport.Height + 170;
            //Inicio de la musica
            music.loadContetent(Content);
            MediaPlayer.Play(music.bgMusica);
            //Inicio Pajaro
            pajaroLeft.LoadContent(Content);
            pajaroRight.LoadContent(Content);
        }


        protected override void UnloadContent()
        {
          
        }
        // Metodos Propios
        //Metodo para Sprite 
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
            sourceRect = new Rectangle(95 * frames, 0, 100, 188);
            
        }
      
        public void Disparo()
        {
            if (delayHuevo >= 0)
                delayHuevo--;

            if (delayHuevo <= 0)
            {
            Clases.Huevo h =new Clases.Huevo(huevoSprite);
            h.posicion = new Vector2(posicionChicken.X + 32 - h.textura.Width / 2, posicionChicken.Y + 30);
            h.posicion += seguimientoChicken;
            h.isVisible = true;
            if (listaHuevosChicken.Count() < 20)
                listaHuevosChicken.Add(h);
            }
             if (delayHuevo == 0)
                delayHuevo = 20; 
        }


        public void updateHuevos()
        {
            foreach (Clases.Huevo h in listaHuevosChicken)
            {
                if (animacionActual == chickenRightDisparo || animacionActual == chickenRight )
                {
                    h.colisionHuevo = new Rectangle((int)h.posicion.X, (int)h.posicion.Y, h.textura.Width, h.textura.Height);
                    h.posicion.X = h.posicion.X + h.speed;

                    if (h.posicion.X >= 750)
                        h.isVisible = false;
                }
                else if (animacionActual == chickenLeftDisparo || animacionActual == chickenLeft)
                {
                    h.colisionHuevo = new Rectangle((int)h.posicion.X, (int)h.posicion.Y, h.textura.Width, h.textura.Height);
                    h.posicion.X = h.posicion.X - h.speed;
                    if (h.posicion.X <= 0)
                        h.isVisible = false;
                }
            }
            for (int i = 0; i < listaHuevosChicken.Count; i++)
            {
                if (!listaHuevosChicken[i].isVisible)
                {
                    listaHuevosChicken.RemoveAt(i);
                    i--;
                }
            }
        }


        protected override void Update(GameTime gameTime)
        {
            //Entada al juego
            kb = Keyboard.GetState();
            switch (gameMenu)
            {
                case Estados.menu:
                    {
                        if (kb.IsKeyDown(Keys.Enter))
                        {
                            gameMenu = Estados.juego;
                        }
                    }
                    break;
                case Estados.juego:
                    {
                        posicionChicken += velocidad;
                        Colision = new Rectangle((int)posicionChicken.X, (int)posicionChicken.Y, 50, 180);

                        rectVida = new Rectangle((int)posicionVida.X, (int)posicionVida.Y, vida, 25);

                        if (kb.IsKeyDown(Keys.Right))
                        {
                            posicionChicken.X += 2f;
                            animacionActual = chickenRight;
                            animacion(gameTime);
                        }
                        else if (kb.IsKeyDown(Keys.Left))
                        {
                            posicionChicken.X -= 2f;
                            animacionActual = chickenLeft;
                            animacion(gameTime);
                        }

                        else
                        {
                            sourceRect = new Rectangle(100, 0, 100, 188);
                        }
                        if (kb.IsKeyDown(Keys.Space))
                        {
                            Disparo();
                            if (animacionActual == chickenRight)
                            {
                                animacionActual = chickenRightDisparo;
                            }
                            else if (animacionActual == chickenLeft)
                            {
                                animacionActual = chickenLeftDisparo;
                            }

                            animacion(gameTime);
                        }
                        if (kb.IsKeyDown(Keys.Up) && salto == false)
                        {
                            posicionChicken.Y -= 10f;
                            velocidad.Y = -7f;
                            salto = true;
                        }
                        if (salto == true)
                        {
                            float i = 1;
                            velocidad.Y += 0.15f * i;
                        }
                        if (posicionChicken.Y + animacionActual.Height >= 600)
                        {
                            salto = false;
                        }
                        if (salto == false)
                        {
                            velocidad.Y = 0f;
                        }
                        if (posicionChicken.X <= 0)
                        { 
                            posicionChicken.X = 0;
                        }
                        if (posicionChicken.X + animacionActual.Width >= pantallaWidth)
                           
                            posicionChicken.X = pantallaWidth - animacionActual.Width;

                        if (posicionChicken.Y <= 0)
                        { 
                            posicionChicken.Y = 0; 
                        }
                        if (posicionChicken.Y + animacionActual.Height >= pantallaHeight)
                            
                            posicionChicken.Y = pantallaHeight - animacionActual.Height;
                        pajaroLeft.Update(gameTime);
                        pajaroLeft.animacion(gameTime);
                        pajaroRight.Update(gameTime);
                        pajaroRight.animacion(gameTime);
                        crearEnemigos(gameTime);
                        updateHuevos();
                        if (vida <= 0 || barraVida.Width <= 0)
                        {
                            MediaPlayer.Stop();
                            gameMenu = Estados.GameOver;
                        }
                        destRect = new Rectangle((int)posicionChicken.X, (int)posicionChicken.Y, 100, 188);
                    }
                    break;
                case Estados.GameOver:
                    {
                        if (kb.IsKeyDown(Keys.Enter))
                        {
                            Exit();  
                        }
                    }
                    break;
                default:
                    break;
            }
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (gameMenu)
            {
                case Estados.menu:
                    {
                        //Dibujo BG menu
                        bg.draw(spriteBatch);
                        //Dibujo Foto menu
                        spriteBatch.Draw(welcome,new Vector2(0,0),Color.White);
                    }
                    break;
                case Estados.juego:
                    {
                        //Dibujo BG
                        bg.draw(spriteBatch);
                        //Dibujo Mr.Chicken
                        spriteBatch.Draw(animacionActual, destRect, sourceRect, Color.White);
                        //Dibujo Barra de vida
                        spriteBatch.Draw(barraVida, rectVida, Color.White);
                        //Dibujo Huevo
                        foreach (Clases.Huevo b in listaHuevosChicken)
                        {
                            b.Draw(spriteBatch);
                        }
                        //Dibujo enemigo Right
                        foreach (Clases.Enemy e in listEnemyRight)
                        {
                            e.draw(spriteBatch);
                        }
                        //Dibujo enemigo Left
                        foreach (Clases.Enemyleft e in listEnemyLeft)
                        {
                            e.draw(spriteBatch);
                        }
                        //Dibujo Pajaro
                        pajaroLeft.Draw(spriteBatch);
                        pajaroRight.Draw(spriteBatch);
                        //Carga de enemigos
                        loadEnemyRight();
                        loadEnemyLeft();
                        
                        
                    }
                    break;
                case Estados.GameOver:
                    {
                        //Dibujo Foto GameOver
                        spriteBatch.Draw(gameOver, new Vector2(0, 0), Color.White);
                    }
                    break;
                default:
                    break;
            }
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
        //Cargar Enemigos
        public void loadEnemyRight()
        {
            if (listEnemyRight.Count() < 1)
            {
                listEnemyRight.Add(new Clases.Enemy(Content.Load<Texture2D>("hombreRight"), new Vector2(-100, 465), Content.Load<Texture2D>("piedra")));
                
            }

            for (int i = 0; i < listEnemyRight.Count; i++)
            {
                if (!listEnemyRight[i].isVisible)
                {
                    listEnemyRight.RemoveAt(i);
                    i--;
                }
            }
        }

        public void loadEnemyLeft()
        {
            if (listEnemyLeft.Count() < 1)
            {
                listEnemyLeft.Add(new Clases.Enemyleft(Content.Load<Texture2D>("hombreLeft"), new Vector2(700, 465), Content.Load<Texture2D>("piedra")));   
            }

            for (int i = 0; i < listEnemyLeft.Count; i++)
            {
                if (!listEnemyLeft[i].isVisible)
                {
                    listEnemyLeft.RemoveAt(i);
                    i--;
                }
            }
        }
        public void crearEnemigos(GameTime gameTime)
        {
            
            foreach (Clases.Enemy e in listEnemyRight)
            {
                if (e.colisionRight.Intersects(Colision))
                {
                    vida -= 10;
                    e.isVisible = false;
                }
                for (int i = 0; i < e.listaPiedra.Count; i++)
                {
                    if (Colision.Intersects(e.listaPiedra[i].colisionProyectil))
                    {
                        vida -= dañoEnemigo;
                        e.listaPiedra[i].isVisible = false;
                    }
                }
                for (int i = 0; i < listaHuevosChicken.Count; i++)
                {
                    if (listaHuevosChicken[i].colisionHuevo.Intersects(e.colisionRight))
                    {
                        listaHuevosChicken[i].isVisible = false;
                        e.isVisible = false;
                    } 
                }
             
                e.update(gameTime);
            }
            foreach (Clases.Enemyleft el in listEnemyLeft)
            {
                if (el.colisionleft.Intersects(Colision))
                {
                    vida -= 10;
                    el.isVisible = false;
                }
                for (int i = 0; i < el.listPiedra.Count; i++)
                {
                    if (Colision.Intersects(el.listPiedra[i].colisionProyectil))
                    {
                        vida -= dañoEnemigo;
                        el.listPiedra[i].isVisible = false;
                    }
                }
                for (int i = 0; i < listaHuevosChicken.Count; i++)
                {
                    if (listaHuevosChicken[i].colisionHuevo.Intersects(el.colisionleft))
                    {
                        listaHuevosChicken[i].isVisible = false;
                        el.isVisible = false;
                    }
                }
                el.update(gameTime);
            }
        }
        
    }
}
