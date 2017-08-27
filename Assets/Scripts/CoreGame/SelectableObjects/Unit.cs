using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : SelectableObject, IMoveable {

    public float Speed
    {
        get { return speed; }
        protected set { speed = value; }
    }
    public int Health
    {
        get { return health; }
        protected set { health = value; }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
        protected set { maxHealth = value; }
    }
    
    private float speed;
    private int health;
    private int maxHealth;

    private MoveComponent moveComponent;

    private void Start ()
    {
        GetComponents();
	}

    protected override void GetComponents()
    {
        base.GetComponents();
        moveComponent = gameObject.AddComponent<MoveComponent>();
    }

    public virtual bool Move(MoveComponent.MovementDirection direction)
    {
        return moveComponent.Move(this, direction);
    }

    public void StopMoving()
    {
        moveComponent.StopMoving();
    }

    public virtual void FinishedMoving() { }
}
