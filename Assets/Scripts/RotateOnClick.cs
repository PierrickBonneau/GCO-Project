using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    public enum CURRENTCOLOR
    {
        RED,
        BLU,
        GREEN,
        YELLOW
    };
    [SerializeField] AnimationCurve curve;
    bool isRotating = false;
    CURRENTCOLOR curentColor = CURRENTCOLOR.RED;
    float rotationTimer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isRotating = true;
                rotationTimer = 0;
            }
        }
        if (isRotating && rotationTimer <= curve.length)
        {
            rotationTimer += Time.deltaTime;
            transform.rotation.SetEulerAngles(transform.rotation.x, transform.rotation.y, transform.rotation.z + (curve.Evaluate(rotationTimer) * Time.deltaTime));
            if (rotationTimer > curve.length)
            {
                isRotating = false;
                switch (curentColor)
                {
                    case (CURRENTCOLOR.RED):
                        curentColor = CURRENTCOLOR.YELLOW;
                        break;
                    case (CURRENTCOLOR.BLU):
                        curentColor = CURRENTCOLOR.RED;
                        break;
                    case (CURRENTCOLOR.GREEN):
                        curentColor = CURRENTCOLOR.BLU;
                        break;
                    case (CURRENTCOLOR.YELLOW):
                        curentColor = CURRENTCOLOR.GREEN;
                        break;
                }
            }
        }
    }
}
