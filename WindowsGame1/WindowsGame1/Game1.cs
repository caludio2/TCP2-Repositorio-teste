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

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        player player;
        camera camera;
        plano[] plano;
        item[] item;
        inimigo[] ListaDeInimigos;
        Class1 Class1;

        BasicEffect effect;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Phantasy Star Online TCP2";
            effect = new BasicEffect(this.GraphicsDevice);
            //player e camera 
            player = new player(new Vector3(80, 0, -100), this.Window, this);
            camera = player.GetCamera();

            //hightmap = new hightmap(this.GraphicsDevice,camera,new Vector3(0,0,0));

            Class1 = new Class1(this.GraphicsDevice, this.camera);

            plano = new plano[5];
            plano[0] = new plano(this.GraphicsDevice, camera, new Vector3(0, 0, 0),this);
            plano[1] = new plano(this.GraphicsDevice, camera, new Vector3(50, 0, 0), this);
            plano[2] = new plano(this.GraphicsDevice, camera, new Vector3(100, 0, 0), this);
            plano[3] = new plano(this.GraphicsDevice, camera, new Vector3(100, 0, 50), this);
            plano[4] = new plano(this.GraphicsDevice, camera, new Vector3(100, 0, 100), this);

            item = new item[3];
            item[0] = new item(this.GraphicsDevice, camera, new Vector3(400, 0,0),this);
            item[1] = new item(this.GraphicsDevice, camera, new Vector3(300, 0, 0), this);
            item[2] = new item(this.GraphicsDevice, camera, new Vector3(500, 0, 0), this);
            
            //criador dos inimigos
            ListaDeInimigos = new inimigo[3];
            ListaDeInimigos[0] = new inimigo(new Vector3(0, 0, -100), this);
            ListaDeInimigos[1] = new inimigo(new Vector3(0, 0, -200), this);
            ListaDeInimigos[2] = new inimigo(new Vector3(40, 0, -300), this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            //player e camera 
            player.salvarpos();
            player.andar();
            player.colidiuParde(plano);
            player.receberdano(ListaDeInimigos);
            camera.seguirplayer();
            player.mirar(ListaDeInimigos);
            Class1.Draw(this.effect);

            //collisao
            player.Collisao(ListaDeInimigos, item);

            //inimigo
            for (int i = 0; i < ListaDeInimigos.Length; i++)
            {
                ListaDeInimigos[i].salvarpos();
            }
            for (int i = 0; i < ListaDeInimigos.Length; i++)
            {
                ListaDeInimigos[i].followP(player);
            }
            for (int i = 0; i < ListaDeInimigos.Length; i++)
            {
                ListaDeInimigos[i].colidiuParde(plano);
            }
            base.Update(gameTime);
        }

        public GraphicsDevice GetGraps()
        {
            return this.GraphicsDevice;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //desenhando o player :  OBS: O CARREGADOR DE MODELOS TA DENTRO DO PLAYER :)
            player.DrawModel(camera.GetView(), camera.GetProjection());
            Class1.Draw(this.effect);
            //hightmap.Draw();

            for (int i = 0; i < ListaDeInimigos.Length; i++)
            {
                ListaDeInimigos[i].DrawModel(camera.GetView(), camera.GetProjection());
            }

            for (int i = 0; i < plano.Length; i++)
            {
                plano[i].Draw();
            }
            for (int i = 0; i < item.Length; i++)
            {
                item[i].DrawModel(camera.GetView(), camera.GetProjection());
            }
            base.Draw(gameTime);
        }
    }
}
