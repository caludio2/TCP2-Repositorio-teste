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
    class hightmap
    {
        VertexPositionColor[] listadepontos;
        VertexBuffer vertexBuffer;

        Matrix World;

        GraphicsDevice graps;
        camera camera;

        BasicEffect basiceffect;
        public hightmap(GraphicsDevice graps, camera camera, Vector3 pos)
        {
            Random random = new Random();
            this.camera = camera;
            this.graps = graps;
            World = Matrix.Identity;
            graps.RasterizerState = RasterizerState.CullNone;

            listadepontos = new VertexPositionColor[100];
            for (int x = 0; x < 10; x++)
            {

                listadepontos[x ] = new VertexPositionColor(new Vector3(x * 50,0, 0), (Color.Red));

            }
            vertexBuffer = new VertexBuffer(graps, typeof(VertexPositionColor), listadepontos.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(listadepontos);
            basiceffect = new BasicEffect(graps);
        }
        public VertexPositionColor[] Getlistadepontos()
        {
            return listadepontos;
        }
        public void Draw()
        {
            graps.SetVertexBuffer(vertexBuffer);
            basiceffect.World = World;
            basiceffect.View = camera.GetView();
            basiceffect.Projection = camera.GetProjection();
            basiceffect.VertexColorEnabled = true;

            foreach (EffectPass pass in basiceffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graps.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, listadepontos, 0, 20);
            }
        }
    }
}
