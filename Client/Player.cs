using FightingGameClient;
using FightingGameClient.Enums;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Speed { get; set; }
    public int Id { get; set; }
    private bool isJumping;
    private int jumpHeight = 50;
    public string nome { get; set; }

    private CharacterAnimation characterBox;
    private Direction currentDirection; // direzione corrente

    public Player()
    {
        characterBox = new CharacterAnimation("",0,0);
        characterBox = null;
        currentDirection = Direction.Right;
        Speed = 5; //velocità predefinita
    }

    public Player(string characterName, int startX, int startY)
    {
        X = startX;
        Y = startY;
        Speed = 5;

        characterBox = new CharacterAnimation(characterName, 80, 80);
    }
    public void setAnimation(string animationName)
    {
        characterBox.setAnimation(animationName);
    }
    public void drawOnPanel(Panel panel)
    {
        if (characterBox != null)
        {
            panel.Controls.Add(characterBox);
        }
    }
    public void MoveLeft()
    {
        //aggiorna la posizione
        X -= Speed;

        //aggiorna l'animazione corrente
        characterBox.setAnimation("Run");
        currentDirection = Direction.Left; //cambia la direzione a sinistra
    }

    public void MoveRight()
    {
        //aggiorna la posizione
        X += Speed;

        //aggiorna l'animazione corrente
        characterBox.setAnimation("Run");
        currentDirection = Direction.Right; //cambia la direzione a destra
    }

    public void Jump()
    {
        //logica di salto
        if (isJumping) return; //evita doppi salti

        isJumping = true;
        Y -= jumpHeight; //alza il giocatore

        //aggiorna l'animazione
        characterBox.setAnimation("Jump");

        //simula la discesa (puoi aggiungere un timer per rendere il salto più realistico)
        Task.Delay(500).ContinueWith(_ =>
        {
            Y += jumpHeight;
            isJumping = false;
            characterBox.setAnimation("Idle");
        });
    }

    public void Attack()
    {
        characterBox.setAnimation("Attack_1");
        characterBox.setAnimation("Attack_2");

        //logica per verificare il successo dell'attacco (da implementare)
    }
    public void Idle()
    {
        //torna all'animazione "Idle"
        characterBox.setAnimation("Idle");
    }
}