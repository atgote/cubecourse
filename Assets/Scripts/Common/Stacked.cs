// 2021.03.20 Tihonovschi Andrei
// Stacked component
// Allows objects to "stack"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacked : MonoBehaviour
{
    // double-linked list
    GameObject nextUp = null;
    GameObject nextDown = null;

    // get item at the bottom
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

    // get item at the top
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

    // unstack (slice) items
    public void Unstack(GameObject other)
    {
        if (nextDown == other)
        {
            nextDown = null;
        }
    }

    // stack items (or another stack of items)
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

    // upper element accessor
    public GameObject GetUpper()
    {
        return nextUp;
    }

    // lower element accessor
    public GameObject GetLower()
    {
        return nextDown;
    }
}
