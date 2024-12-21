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
    class Class1
    {
        VertexPositionColor[] chao;
        VertexBuffer vertexBuffer;

        Matrix World;
        Matrix View;
        Matrix Projection;

        GraphicsDevice graps;

        RasterizerState rs = new RasterizerState(); 

        camera camera;

        GraphicsDevice Device;
        public Class1(GraphicsDevice graps, camera camera)
        {
            this.graps = graps;
            this.camera = camera;
            Device = graps;
            chao = new VertexPositionColor[4];
            chao[0] = new VertexPositionColor(new Vector3(-24, 0, 26f) * 100, Color.DarkGreen);
            chao[1] = new VertexPositionColor(new Vector3(-24, 0, -26f) * 100, Color.DarkGreen);
            chao[2] = new VertexPositionColor(new Vector3(24, 0, 26f) * 100, Color.LawnGreen);

            chao[3] = new VertexPositionColor(new Vector3(24, 0, -26f) * 5, Color.DarkGreen);

            rs.CullMode = CullMode.None;
            graps.RasterizerState = rs;
            

            vertexBuffer = new VertexBuffer(graps, typeof(VertexPositionColor),
                                            chao.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(chao);
            
            
        }
        public void Draw(BasicEffect effect)
        {
            graps.SetVertexBuffer(vertexBuffer);
            effect.World = World;
            effect.View = camera.GetView();
            effect.Projection = camera.GetProjection();
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.graps.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, chao, 0, 2);
            }
        }
    }
}
