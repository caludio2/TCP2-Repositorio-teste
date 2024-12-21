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
    class plano
    {
        VertexPositionColor[] vets;
        VertexBuffer vertexBuffer;

        VertexPositionColor[] chao; 

        Matrix World;

        GraphicsDevice graps;
        camera camera;

        Vector3 pos;

        Model model;

        BoundingBox box1 = new BoundingBox(new Vector3(0, 0, 0), new Vector3(50, 50, 50));

        BasicEffect basiceffect;
        public plano(GraphicsDevice graps,camera camera,Vector3 pos,Game game)
        {
            this.pos = pos;
            this.camera = camera;
            this.graps = graps;
            World = Matrix.Identity;
            box1 = new BoundingBox(new Vector3(0, 0, 0) + pos, new Vector3(50, 50, 50) + pos);
            model = game.Content.Load<Model>(@"Modelos/Itens");
            
            vets = new VertexPositionColor[17];
            //parte inferior
            vets[0] = new VertexPositionColor(new Vector3(0, 0, 0) + pos, Color.Red);
            vets[1] = new VertexPositionColor(new Vector3(50, 0, 0) + pos, Color.Red);
            vets[2] = new VertexPositionColor(new Vector3(50, 0, 50) + pos, Color.Red);
            vets[3] = new VertexPositionColor(new Vector3(0, 0, 50) + pos, Color.Red);
            vets[4] = new VertexPositionColor(new Vector3(0, 0, 0) + pos, Color.Red);
            //parte superior
            vets[5] = new VertexPositionColor(new Vector3(0, 50, 0) + pos, Color.Red);
            vets[6] = new VertexPositionColor(new Vector3(50, 50, 0) + pos, Color.Red);
            vets[7] = new VertexPositionColor(new Vector3(50, 50, 50) + pos, Color.Red);
            vets[8] = new VertexPositionColor(new Vector3(0, 50, 50) + pos, Color.Red);
            vets[9] = new VertexPositionColor(new Vector3(0, 50, 0) + pos, Color.Red);
            //paredes
            vets[10] = new VertexPositionColor(new Vector3(0, 0, 50) + pos, Color.Red);
            vets[11] = new VertexPositionColor(new Vector3(0, 50, 50) + pos, Color.Red);
            vets[12] = new VertexPositionColor(new Vector3(50, 0, 50) + pos, Color.Red);
            vets[13] = new VertexPositionColor(new Vector3(50, 50, 50) + pos, Color.Red);
            vets[14] = new VertexPositionColor(new Vector3(50, 0, 0) + pos, Color.Red);
            vets[15] = new VertexPositionColor(new Vector3(50, 50, 0) + pos, Color.Red);
            vets[16] = new VertexPositionColor(new Vector3(0, 0, 0) + pos, Color.Red);

            chao = new VertexPositionColor[4];
            chao[0] = new VertexPositionColor(new Vector3(-24, 0, 26f) * 20, Color.DarkGreen);
            chao[1] = new VertexPositionColor(new Vector3(-24, 0, -26f) * 20, Color.DarkGreen);
            chao[2] = new VertexPositionColor(new Vector3(24, 0, 26f) * 20, Color.LawnGreen);

            chao[3] = new VertexPositionColor(new Vector3(24, 0, -26f) * 20, Color.DarkGreen);

            vertexBuffer = new VertexBuffer(graps, typeof(VertexPositionColor), vets.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(vets);
            basiceffect = new BasicEffect(graps);
        }
        public BoundingBox GetBox()
        {
            return box1;
        }
        public void Draw()
        {
            graps.SetVertexBuffer(vertexBuffer);
            basiceffect.World = World;
            basiceffect.View = camera.GetView();
            basiceffect.Projection = camera.GetProjection();
            basiceffect.VertexColorEnabled = true;


            foreach(EffectPass pass in basiceffect.CurrentTechnique.Passes)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        World = Matrix.CreateTranslation(pos);
                        World = Matrix.CreateScale(0.5f);
                        effect.EnableDefaultLighting();
                        effect.World = World;
                        effect.View = camera.GetView();
                        effect.Projection = camera.GetProjection();
                    }
                    mesh.Draw();
                }
                pass.Apply();

                graps.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vets, 0, 16);
            }
            foreach (EffectPass pass in basiceffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graps.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, chao, 0, 2);
            }
        }
    }
}
