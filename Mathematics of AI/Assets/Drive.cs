using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject Fuel;
    void Start()
    {

    }

    void CalculateAngle()
    {
        Vector3 tF = this.transform.up;
        Vector3 fD = Fuel.transform.position - this.transform.position;
        float dot = tF.x * fD.x + tF.y * fD.y;

        float angle = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));
        Debug.Log("Angle " + angle * Mathf.Rad2Deg);
        Debug.Log("Unity angle " + Vector3.Angle(tF, fD));

         Debug.DrawRay(this.transform.position, tF * 10, Color.blue, 2);
         Debug.DrawRay(this.transform.position, fD, Color.red, 2);

        this.transform.Rotate(0, 0, angle * Mathf.Rad2Deg);



    }
    
     Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;
        Vector3 crossProd = new Vector3(xMult, yMult, zMult);
        return crossProd;
    }
    
    
    
    void CalculateDistance()
    {
        Vector3 tP = this .transform.position;
        Vector3 fP = Fuel.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - fP.x, 2) + Mathf.Pow(tP.y - fP.y, 2));

        Debug.Log("Distance: " + distance);
    }


    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

       if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }
    
    }
}