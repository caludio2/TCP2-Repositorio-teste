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
    class inimigo 
    {
        Vector3 pos;
        Vector3 pospassada;
        Matrix world;
        BoundingBox bb;

        Model model;

        float raio;
        float speed;
        float range;
        float vida;
        int dano;

        player player;
        Game1 game;

        bool vivo;

        public inimigo(Vector3 position,Game1 game)
        {

            vivo = true;


            this.game = game;
            pos = position;
            model = game.Content.Load<Model>(@"Modelos/bixao de 6 patas");
            
            vida = 100;
            dano = 1;
            speed = 0.5f;
            raio = 7;
            range = 13;
        }

        //Geters
        public Vector3 GetPos()
        {
            return pos;
        }
        public float GetRaio()
        {
            return raio;
        }
        public float GetRange()
        {
            if (vivo == true)
            {
                return range;
            }
            else
            { return 0; }
        }
        public int GetDano()
        {
            return dano;
        }
        public void SetVida(float dano)
        {
            this.vida = this.vida - dano;
            if (vida < 0)
            {
                vivo = false;
                item item = new item(game.GetGraps(), player.GetCamera(), pos, game);
            }
        }

        public BoundingBox GetBox()
        {
            this.bb = new BoundingBox(pos - new Vector3(-1, -1, -1), pos - new Vector3(1, 1, 1));
            return bb;
        }

        public void colidiuParde(plano[] caixas)
        {
            this.bb = new BoundingBox(new Vector3(-1, -1, -1) + this.pos, new Vector3(1, 1, 1) + this.pos);
            for (int i = 0; i < caixas.Length; i++)
            {
                if (bb.Intersects(caixas[i].GetBox()))
                {
                    pos = pospassada;
                }
            }
        }
        public void salvarpos()
        {
            pospassada = pos;
        }

        public float distanciadoplayer(player player)
        {
            this.player = player;
            if (vivo == true)
            {
                var distance = (float)Math.Sqrt(Math.Pow(player.GetPos().X - pos.X, 2) + Math.Pow(player.GetPos().Z - pos.Z, 2));
                return distance;
            }
            else
            {
                return 100000000;
            }
        } 

        public void DrawModel(Matrix view, Matrix projection)
        {
            if (vivo == true)
            {
                foreach (ModelMesh mesh in model.Meshes)
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
        } // nao sei se devida estar aqui acho que vou tentar fazer um pra todos os modelos do jogo mais pra frente

        public void followP(player player)
        {
            if (vivo == true)
            {
            //range maximo pra seguir o player
                if (distanciadoplayer(player) > range)
                {
                    if (pos.X < player.GetPos().X)
                    {
                        pos.X = pos.X + speed;
                    }
                    if (pos.X > player.GetPos().X)
                    {
                        pos.X = pos.X - speed;
                    }
                    if (pos.Z < player.GetPos().Z)
                    {
                        pos.Z = pos.Z + speed;
                    }
                    if (pos.Z > player.GetPos().Z)
                    {
                        pos.Z = pos.Z - speed;
                    }

                    
                }
                world = Matrix.Identity;
                world *= Matrix.CreateScale(0.07f * vida * 0.01f);
                Vector3 directionToTarget = Vector3.Normalize(player.GetPos() - pos);
                float yaw = (float)Math.Atan2(directionToTarget.X, directionToTarget.Z);
                world *= Matrix.CreateRotationY(yaw);
                world *= Matrix.CreateTranslation(pos);
            }
        } //vai ate o player ate chegar no range de ataque
    }
}
