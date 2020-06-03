using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
        objectWidth = GetComponent<PolygonCollider2D>().bounds.extents.x;
        objectHeight = GetComponent<PolygonCollider2D>().bounds.size.y;
    }

    void LateUpdate()
    {
        Vector3 viewPosition = transform.position;
        viewPosition.x = Mathf.Clamp(viewPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPosition.y = Mathf.Clamp(viewPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPosition;
    }
    
}
