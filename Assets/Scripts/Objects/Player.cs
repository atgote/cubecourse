// 2021.03.20 Tihonovschi Andrei
// Player class implementation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Player is "destructible" - i.e. it may be blocked by a block or "burned" by fire
public class Player : MonoBehaviour, IDestructible
{
    // Player speed settings
    [SerializeField] float speedX = 1.0f;
    [SerializeField] float speedZ = 1.0f;

    // Player aliveness state
    private bool alive = true;
    // is player to be promoted to the next level?
    private bool promote = false;

    private void Start()
    {
        alive = true;
        promote = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (!alive)
        {
            Game.Finish();
        }
        else if (promote)
        {
            promote = false;
            Game.NextLevel();
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        // interact with colliders
        IInteractable interactable = c.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            Vector3 direction = c.GetContact(0).normal;
            interactable.Interact(gameObject, direction);
        }
    }

    void MovePlayer()
    {
        // only if "alive"
        if (!alive)
        {
            return;
        }

        // Calculate movement
        float deltaX = Time.deltaTime * speedX;
        float deltaZ = - Time.deltaTime * speedZ * Input.GetAxis("Horizontal");

        transform.Translate(deltaX, 0, deltaZ);

        Vector3 pos = transform.position;

        // Ensure player does not leave the game road
        if (pos.z > 2.0f)
        {
            pos.z = 2.0f;
            transform.position = pos;
        }
        else if (pos.z < -2.0f)
        {
            pos.z = -2.0f;
            transform.position = pos;
        }

        // ensure the "tower" to move all cubes at once
        // get the lowest stack cube
        Stacked sc = gameObject.GetComponent<Stacked>();
        GameObject o = sc.GetLast();
        pos.y = o.transform.position.y;
        // for all stacked objects
        while (o) 
        {
            o.transform.position = pos;
            pos.y += 1.0f;
            o = o.GetComponent<Stacked>().GetUpper();
        }
    }

    // Implement IDestructible
    public void Damage(float amount)
    {
        Damage(amount, DamageType.VIRTUAL);
    }

    public void Damage(float amount, DamageType type)
    {
        // if receiving damage - just set alive to false
        if (alive)
        {
            alive = false;
        }
    }

    // Promote to next level
    public void Promote()
    {
        promote = true;
    }
}
