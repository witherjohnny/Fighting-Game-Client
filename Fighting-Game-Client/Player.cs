﻿using Fighting_Game_Client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        private string Id { get; }
        private bool isJumping;
        private int jumpHeight = 50;
        public string nome { get; set; }

        private AnimationBox characterBox;
        private Direction currentDirection; // direzione corrente

        public Player()
        {
            characterBox = null;
            currentDirection = Direction.Right;
            Speed = 5; //velocità predefinita
        }

        public Player(string id, string characterName, int startX, int startY, Direction direction)
        {
            Id = id;
            X = startX;
            Y = startY;
            Speed = 5;

            characterBox = new AnimationBox(characterName, "Idle", X, Y, 128, 128, direction);

        }
        public void setAnimation(string animationName, Direction direction, bool runOnce, bool isCancelable)
        {
            characterBox.setAnimation(animationName, direction, runOnce, isCancelable);
        }
        public void setPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
            characterBox.setPosition(X, Y);
        }
        public string getId() { return Id; }
        public AnimationBox getCharacterBox() { return this.characterBox; }
        public void MoveLeft()
        {
            //aggiorna la posizione
            X -= Speed;

            //aggiorna l'animazione corrente
            //characterBox.setAnimation("Run");
            currentDirection = Direction.Left; //cambia la direzione a sinistra
        }

        public void MoveRight()
        {
            //aggiorna la posizione
            X += Speed;

            //aggiorna l'animazione corrente
            //characterBox.setAnimation("Run");
            currentDirection = Direction.Right; //cambia la direzione a destra
        }

        public void Jump()
        {
            //logica di salto
            if (isJumping) return; //evita doppi salti

            isJumping = true;
            Y -= jumpHeight; //alza il giocatore

            //aggiorna l'animazione
            //characterBox.setAnimation("Jump");

            //simula la discesa (puoi aggiungere un timer per rendere il salto più realistico)
            Task.Delay(500).ContinueWith(_ =>
            {
                Y += jumpHeight;
                isJumping = false;
                //characterBox.setAnimation("Idle");
            });
        }
    }
}
