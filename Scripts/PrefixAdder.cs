using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefixAdder : MonoBehaviour
{
    [SerializeField]
    private string _prefix;

    public void AddPrefixes()
    {
        gameObject.name = _prefix + "_" + gameObject.name;
        IterateThroughChildren(transform);
    }
    public void IterateThroughChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Do something with the child
            Debug.Log("Found child: " + child.name);
            child.name = _prefix + "_" + child.name;
            // Recursively iterate through this child's children
            if (child.childCount > 0)
            {
                IterateThroughChildren(child);
            }
        }
    }
}
