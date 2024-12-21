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
    class item
    {
        Matrix World;

        camera camera;

        Vector3 pos;
        float raio;
        bool pegou;

        int id = 2;

        Model model;
        public item(GraphicsDevice graps, camera camera, Vector3 pos, Game game)
        {
            this.camera = camera;
            this.pos = pos;
            
            raio = 5;
            model = game.Content.Load<Model>(@"Modelos/Itens");
        }
        public float GetRaio()
        {
            return raio;
        }
        public int GetId()
        {
            return id;
        }
        public float distanciadoplayer(player player)
        {
            var distance = (float)Math.Sqrt(Math.Pow(player.GetPos().X - pos.X, 2) + Math.Pow(player.GetPos().Z - pos.Z, 2));
            return distance;
        }
        public void SetPegou(bool pegou)
        {
            this.pegou = pegou;
        }
        public void DrawModel(Matrix view, Matrix projection)
        {
            if (pegou == false)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = World;
                        effect.View = view;
                        effect.Projection = projection;
                    }
                    mesh.Draw();
                }
                atualizarMatrix();
            }
        }
        public void atualizarMatrix()
        {
            World = Matrix.Identity;
            World *= Matrix.CreateScale(0.1f);
            World *= Matrix.CreateTranslation(pos);
        }
    }
}
        
