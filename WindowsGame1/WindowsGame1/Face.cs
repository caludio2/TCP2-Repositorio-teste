using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StarRePhantasy
{
    public class CriadorDeFaces
    {
        VertexPositionColor[] cuboVert;

        short[] cuboIndexes;

        public CriadorDeFaces()
        {
            cuboVert = new VertexPositionColor[8];
            cuboVert[0] = new VertexPositionColor(new Vector3(-2, 4, 2), Color.Red);
            cuboVert[1] = new VertexPositionColor(new Vector3(2, 4, 2), Color.DarkRed);
            cuboVert[2] = new VertexPositionColor(new Vector3(2, 0, 2), Color.Red);
            cuboVert[3] = new VertexPositionColor(new Vector3(-2, 0, 2), Color.Red);

            cuboVert[4] = new VertexPositionColor(new Vector3(-2, 4, -2), Color.Red);
            cuboVert[5] = new VertexPositionColor(new Vector3(2, 4, -2), Color.DarkRed);
            cuboVert[6] = new VertexPositionColor(new Vector3(2, 0, -2), Color.Red);
            cuboVert[7] = new VertexPositionColor(new Vector3(-2, 0, -2), Color.Red);

            this.cuboIndexes = new short[]
            {
                //Frente
                0, 1, 3,
                   1, 2, 3,

                //Direita
                1, 5, 2,
                   5, 6, 2,

                //Esquerda
                0, 4, 3,
                   4, 7, 3,

                //Cima
                4, 5, 0,
                   5, 1, 0,

                //Baixo
                7, 6, 3,
                   6, 2, 3,

                //Trás
                4, 5, 7,
                   5, 6, 7

            };
        }

        public VertexPositionColor[] GetCuboVert()
        { return cuboVert; }

        public short[] GetIndexes()
        { return cuboIndexes; }
    }
}