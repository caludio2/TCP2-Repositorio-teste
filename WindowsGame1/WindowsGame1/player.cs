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
    class player 
    {
        //VARIAVEIS
        Matrix world;

        //COISAS RELACINADAS COM MOVIMENTAÇAO
        Vector3 pos;
        float rotation;
        float velrotacao;
        float vel;

        //AUXILIO
        Model model;
        camera camplayer;
        Game game;

        //COLLISAO
        Vector3 pospassada;
        float raio;
        float range;
        float dano;
        int colldown;
        bool morto;
        int vida;
        float menordistancia = 100;
        int ninventario;
        float velcorrendo,velandando;

        BoundingBox box1;

        //inventario
        int[] inventario;

        //CRIADOR DA CLASSE
        public player(Vector3 posdecriaçao, GameWindow window, Game game)
        {
            inventario = new int[20];
            atualizarMatrix();
            model = game.Content.Load<Model>(@"Modelos/untitled");
            this.game = game;
            vel = 2;
            raio = 5;
            camplayer = new camera(this, window);
            pos = posdecriaçao;
            velrotacao = 0.1f;
            vida = 500;
            velandando = 2;
            colldown = 10;
            range = 300;
            dano = 20;
            velcorrendo = 9;

            
        }

        //FUNCIONALIDADES
        public void andar()
        {
            if (morto == false)
            {
                KeyboardState state = Keyboard.GetState();
                Keys[] pressedKeys = state.GetPressedKeys();
                atualizarMatrix();
                if (state.IsKeyDown(Keys.D))
                {
                    rotation -= velrotacao;
                }
                if (state.IsKeyDown(Keys.A))
                {
                    rotation += velrotacao;
                }
                if (state.IsKeyDown(Keys.W))
                {
                    pos.X -= (float)Math.Sin((rotation)) * vel;

                    pos.Z -= (float)Math.Cos((rotation)) * vel;
                }
                if (state.IsKeyDown(Keys.S))
                {
                    pos.X += (float)Math.Sin((rotation)) * vel;

                    pos.Z += (float)Math.Cos((rotation)) * vel;
                }
                if (state.IsKeyDown(Keys.Space))
                {
                    vel = velcorrendo;
                }
                else
                {
                    vel = velandando;
                }
            }
        }
        private void atualizarMatrix()
        {
            if (morto)
                return;

            world = Matrix.Identity;
            world *= Matrix.CreateScale(0.2f * vida * 0.0025f);
            world *= Matrix.CreateRotationY((rotation));
            world *= Matrix.CreateTranslation(pos);
        }

        //COMBATE
        public void receberdano(inimigo[] lista)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i].distanciadoplayer(this) < lista[i].GetRange())
                {
                    this.vida -= lista[i].GetDano();
                }
            }
            if (vida < 0)
            {
                morto = true;
            }
        }
        public void mirar(inimigo[] lista)
        {
            int i = 0;
            int id = 0;
            menordistancia = 10000;
            for (i = 0; i <= 2; i++)
            {
                if (lista[i].distanciadoplayer(this) < menordistancia)
                {
                    menordistancia = (float)lista[i].distanciadoplayer(this);
                    id = i;
                }
                if (i == 2)
                {
                    dardano(lista, id);
                }
            }
           
                if (colldown >= 0)
                {
                    colldown--;
                }
        }
        public void dardano(inimigo[] lista, int i)
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] pressedKeys = state.GetPressedKeys();

            if (lista[i].distanciadoplayer(this) < range)
            {
                if (state.IsKeyDown(Keys.P))
                {
                    if (colldown <= 0)
                    {
                        olharpara(lista, i);
                        atacar(lista, i);
                        colldown = 100;
                    }
                }
            }
        }
        public void olharpara(inimigo[] lista, int i)
        {
            Vector3 directionToTarget = Vector3.Normalize(lista[i].GetPos() - pos);
            rotation = (float)Math.Atan2(directionToTarget.X, directionToTarget.Z) + 3.14f;
            atualizarMatrix();
        }
        public void atacar(inimigo[] lista, int i)
        {
            if (colldown < 0)
            {
                lista[i].SetVida(dano);
            }
        }

        //GETERS
        public Vector3 GetPos()
        {
            return pos;
        }
        public Model GetModel()
        {
            return model;
        }
        public Matrix GetMatrix()
        {
            return world;
        }
        public camera GetCamera()
        {
            return camplayer;
        }
        public int GetVida()
        {
            return vida;
        }
        public BoundingBox GetBox()
        {
            return box1;
        }

        //DRAW
        public void DrawModel(Matrix view, Matrix projection)   
        {
            if (morto)
                return;

            foreach (ModelMesh mesh in this.model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }

        //COLLISAO
        public void Collisao(inimigo[] inimigos, item[] item)
        {
            if (morto == false)
            {
                for (int i = 0; i < inimigos.Length; i++)
                {
                    if (inimigos[i].distanciadoplayer(this) < raio + inimigos[i].GetRaio())
                    {
                        pos = pospassada;
                    }
                }
                for (int i = 0; i < item.Length; i++)
                {
                    if (item[i].distanciadoplayer(this) < raio + item[i].GetRaio())
                    {
                        item[i].SetPegou(true);
                        inventario[ninventario] = item[i].GetId();
                    }
                }
            }
        }
        public void colidiuParde(plano[] caixas)
        {
            box1 = new BoundingBox(new Vector3(-1, -1, -1) + pos, new Vector3(1, 1, 1) + pos);
            for (int i = 0; i < caixas.Length; i++)
            {
                if (box1.Intersects(caixas[i].GetBox()))
                {
                    pos = pospassada;
                }
            }
        }
        public void salvarpos()
        {
            pospassada = pos;
        }

    }
}

