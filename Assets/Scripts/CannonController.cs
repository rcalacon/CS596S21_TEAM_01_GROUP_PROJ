using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    float aimHeight;
    public GameObject cannonBall;
    public float forceAmount;

    // Start is called before the first frame update
    void Start()
    {
        aimHeight = -20f;
        forceAmount = 2000f;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the Screen positions of the object
        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector3 mouseOnScreen = (Vector3)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        Vector3 cannonAim = new Vector3(mouseOnScreen.y * aimHeight, -angle, angle);
        Quaternion cannonRotation = Quaternion.Euler(cannonAim);
        Vector3 cannonTestAim = mouseOnScreen;
        cannonTestAim.z = -1;
        transform.rotation = cannonRotation;

        //Fire Cannon
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var firedCannonBall = Instantiate(cannonBall, transform.position, Quaternion.identity);
            firedCannonBall.GetComponent<Rigidbody>().AddForce((ray.direction) * forceAmount);
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
