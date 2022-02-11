using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{



    public GameObject position0;
    public GameObject position1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(transform.position.x<position0.transform.position.x)
        //{
        //    Vector2 AIPosition = transform.position;
        //    AIPosition.x += (1 * Time.deltaTime);
        //    transform.position = AIPosition;

        //}
        //else
        //{
        //    Vector2 AIPosition = transform.position;
        //    AIPosition.x -= (1 * Time.deltaTime);
        //    transform.position = AIPosition;
        //}
        ////using vectors to move up and down
        //if(transform.position.y<position0.transform.position.y)
        //{
        //    transform.position += (Vector3)Vector2.up * 1 * Time.deltaTime;
        //}
        //else 
        //{
        //    transform.position -= (Vector3)Vector2.up * 1 * Time.deltaTime;
        //}


        //direction from A to B via movement=B-A
        Vector2 directionToPos0 = (position0.transform.position-transform.position);
        directionToPos0.Normalize();
        transform.position += (Vector3) directionToPos0 * 1 * Time.deltaTime;




    }
}
