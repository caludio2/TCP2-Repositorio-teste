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
    class camera
    {
        Vector3 pos;
        player player;
        float speed;
        Matrix view , projection;

        double distance;
        float distmax;
        
        GameWindow janela;

        //CRIADOR DA CLASSE
        public camera(player playerref,GameWindow window)
        {
            player = playerref;
            
            janela = window;
            speed = 1;
            distmax = 150;
            pos = new Vector3(0,100, 150);
            //definindo variaveis caralho
        }
        
        //FUNCIONALIDADES
        public void seguirplayer()
        {
            //PROJECAO E PRA ONDE A CAMERA TA OLHANDO
            float fovAngle = MathHelper.ToRadians(60);
            projection = Matrix.CreatePerspectiveFieldOfView(fovAngle, janela.ClientBounds.Width / (float)janela.ClientBounds.Height, 0.01f, 1000);
            view = Matrix.CreateLookAt(pos, player.GetPos() + new Vector3(0,30,0), Vector3.Up);

            //nao quero usar seno e coseno pra fazer os vetores nao pq vai dar mais
            //trabalho de pensar entao vou fazer mais uma porqueira delas a menor ne
            //ja que isso aqui ta orientado a fodase
            Mudandopos();
        }
        public void Mudandopos()
        {
            distance = this.distanciadoplayer(player);
            if (distance > distmax)
            {
                speed = 1;
            }
            if (distance < distmax)
            {
                speed = -1;
            }
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

        //GETERS
        public Vector3 GetPos()
        {
            return pos;
        }
        public Matrix GetView()
        {
            return view;
        }
        public Matrix GetProjection()
        {
            return projection;
        }

        //DESCUBLINDO A DISTANCIA DA CAMERA PRO PLAYER
        public double distanciadoplayer(player player)
        {
            var distance = Math.Sqrt(Math.Pow(player.GetPos().X - pos.X, 2) + Math.Pow(player.GetPos().Z - pos.Z, 2));
            return distance;
        }
    }
}
