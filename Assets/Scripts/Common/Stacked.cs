using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacked : MonoBehaviour
{
    GameObject nextUp = null;
    GameObject nextDown = null;

    public GameObject GetLast()
    {
        GameObject last = null;
        GameObject next = gameObject;
        do
        {
            last = next;
            next = last.GetComponent<Stacked>().GetLower();
        }
        while(next != null);

        return last;
    }

    public GameObject GetFirst()
    {
        GameObject first = null;
        GameObject next = gameObject;
        do
        {
            first = next;
            next = first.GetComponent<Stacked>().GetUpper();
        }
        while(next != null);

        return first;
    }

    public void Unstack(GameObject other)
    {
        if (nextDown == other)
        {
            nextDown = null;
        }
    }

    public void Stack(GameObject other)
    {
        if (other == null)
            return;

        GameObject first1 = GetFirst();
        GameObject first2 = other.GetComponent<Stacked>().GetFirst();
        GameObject last = GetLast();

        if (first1 != first2) // protect from building recursive stack
        {
            last.GetComponent<Stacked>().nextDown = first2;
            first2.GetComponent<Stacked>().nextUp = last;
        }
    }

    public GameObject GetUpper()
    {
        return nextUp;
    }

    public GameObject GetLower()
    {
        return nextDown;
    }
}
