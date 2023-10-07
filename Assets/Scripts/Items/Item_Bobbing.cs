using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bobbing : MonoBehaviour
{
    private Transform visual;
    private Vector2 itemLocation;

    private float offset;

    private void Start()
    {
        itemLocation = transform.transform.position;
        visual = transform.transform;
        offset = Random.Range(0.0f, 2f);
        StartCoroutine(FloatingItem());
    }
    private IEnumerator FloatingItem()
    {
        while (true)
        {
            visual.position = new Vector2(0, Mathf.PingPong((Time.time + offset) * 0.1f, 0.2f) + 0.2f) + itemLocation;
            yield return null;
        }
    }
}
