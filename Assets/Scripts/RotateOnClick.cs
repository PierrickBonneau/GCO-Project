using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CURRENTCOLOR
{
    RED,
    BLU,
    GREEN,
    YELLOW
};
public class RotateOnClick : MonoBehaviour
{
    
    [SerializeField] AnimationCurve curve;
    bool isRotating = false;
    [HideInInspector] public CURRENTCOLOR curentColor = CURRENTCOLOR.RED;
    float rotationTimer = 0;
    Vector3 startingRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRotating && PlayerPrefs.GetInt("RayonVertWon", 0) == 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isRotating = true;
                rotationTimer = 0;
                startingRotation = transform.rotation.eulerAngles;
            }
        }
        if (isRotating && rotationTimer <= curve.length)
        {
            rotationTimer += Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, startingRotation.z + curve.Evaluate(rotationTimer) * - 90);
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
                Debug.Log(curentColor);
            }
        }
    }
}
