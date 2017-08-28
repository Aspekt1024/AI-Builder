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

    protected MoveComponent moveComponent;

    private void Start ()
    {
        GetComponents();
	}

    protected override void GetComponents()
    {
        base.GetComponents();
        moveComponent = gameObject.AddComponent<MoveComponent>();
    }

    public virtual bool MoveForward()
    {
        return moveComponent.MoveForward(this);
    }

    public virtual bool MoveBackward()
    {
        return moveComponent.MoveBackward(this);
    }

    public virtual bool TurnTowards(MoveComponent.Direction direction)
    {
        return moveComponent.TurnTowards(this, direction);
    }

    public void StopMoving()
    {
        moveComponent.StopMovement();
    }

    public virtual void FinishedAction() { }
}
