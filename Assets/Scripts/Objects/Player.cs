using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDestructible
{
    [SerializeField] float speedX = 1.0f;
    [SerializeField] float speedZ = 1.0f;

    private bool alive = true;
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
        IInteractable interactable = c.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            Vector3 direction = c.GetContact(0).normal;
            interactable.Interact(gameObject, direction);
        }
    }

    void MovePlayer()
    {
        if (!alive)
        {
            return;
        }

        float deltaX = Time.deltaTime * speedX;
        float deltaZ = - Time.deltaTime * speedZ * Input.GetAxis("Horizontal");

        transform.Translate(deltaX, 0, deltaZ);

        Vector3 pos = transform.position;

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

        Stacked sc = gameObject.GetComponent<Stacked>();
        GameObject o = sc.GetLast();
        pos.y = o.transform.position.y;

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
        if (alive)
        {
            alive = false;
        }
    }

    // Promote
    public void Promote()
    {
        promote = true;
    }
}
