using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceToMousPos : MonoBehaviour
{
    [SerializeField] GameObject ending;
    bool followMouse = false;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        PlayerPrefs.SetInt("CheminParfaitDone", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
                followMouse = true;
        }
        if(followMouse)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            gameObject.transform.position = hit.point;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        followMouse = false;
        transform.position = startPos;
        Debug.Log("Hit");
        if (collision.gameObject == ending)
        {
            PlayerPrefs.SetInt("CheminParfaitDone", 1);
        }
    }
}
