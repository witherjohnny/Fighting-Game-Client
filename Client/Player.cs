using corretto;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

public enum Action
{
    Idle,
    Run,
    Jump,
    Attack,
    Dead
}

public enum Direction
{
    Left,
    Right
}

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Speed { get; set; }
    public int Id { get; set; }
    private bool isJumping;
    private int jumpHeight = 50;
    public string nome { get; set; }

    private Animation currentAction;
    private Direction currentDirection; // direzione corrente
    private Dictionary<Action, Animation> animations;

    public Player()
    {
        animations = new Dictionary<Action, Animation>();
        currentAction = null;
        currentDirection = Direction.Right;
        Speed = 5; //velocità predefinita
    }

    public Player(string characterName, int startX, int startY)
    {
        X = startX;
        Y = startY;
        Speed = 5;

        animations = new Dictionary<Action, Animation>();
        LoadAnimations(characterName);
        currentAction = animations[Action.Idle]; //animazione iniziale
    }

    private void LoadAnimations(string characterName)
    {
        string basePath = "Sprites/" + characterName + "/";

        animations[Action.Idle] = new Animation(basePath + "Idle", 10);
        animations[Action.Run] = new Animation(basePath + "Run", 5);
        animations[Action.Jump] = new Animation(basePath + "Jump", 8);
        animations[Action.Attack] = new Animation(basePath + "Attack_1", 8);
        animations[Action.Dead] = new Animation(basePath + "Dead", 15);
    }

    public void ChangeAnimation(Action action)
    {
        if (animations.ContainsKey(action))
        {
            currentAction = animations[action];
        }
    }

    public void Update()
    {
        if (currentAction != null)
        {
            currentAction.Update();
        }
    }

    public void Draw(Graphics g)
    {
        //disegna lo sprite attuale
        if (currentAction != null)
        {
            g.DrawImage(currentAction.GetCurrentFrame(), X, Y);
        }
    }

    public void MoveLeft()
    {
        //aggiorna la posizione
        X -= Speed;

        //aggiorna l'animazione corrente
        ChangeAnimation(Action.Run);
        currentDirection = Direction.Left; //cambia la direzione a sinistra
    }

    public void MoveRight()
    {
        //aggiorna la posizione
        X += Speed;

        //aggiorna l'animazione corrente
        ChangeAnimation(Action.Run);
        currentDirection = Direction.Right; //cambia la direzione a destra
    }

    public void Jump()
    {
        //logica di salto
        if (isJumping) return; //evita doppi salti

        isJumping = true;
        Y -= jumpHeight; //alza il giocatore

        //aggiorna l'animazione
        ChangeAnimation(Action.Jump);

        //simula la discesa (puoi aggiungere un timer per rendere il salto più realistico)
        Task.Delay(500).ContinueWith(_ =>
        {
            Y += jumpHeight;
            isJumping = false;
            ChangeAnimation(Action.Idle); //torna all'animazione Idle dopo il salto
        });
    }

    public void Attack()
    {
        //aggiorna l'animazione corrente
        ChangeAnimation(Action.Attack);

        //logica per verificare il successo dell'attacco (da implementare)
    }

    public void Idle()
    {
        //torna all'animazione "Idle"
        ChangeAnimation(Action.Idle);
    }
}