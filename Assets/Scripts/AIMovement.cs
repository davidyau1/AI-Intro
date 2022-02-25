using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;
    public GameObject waypoint1;
    public GameObject waypoint2;
    public float speed = 1.5f;
    public bool noticePlayer = false;
    public float noticeWaypoint = 0;
    public float minGoalDistance = 0.05f;
    public float chaseDistanace = 3f;
    
    
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < chaseDistanace)
        {
            noticePlayer = true;

            AIMoveTowards(player);
        }
        else if (!noticePlayer)
        {
            //patrol
            if (noticeWaypoint == 0)
            {
                AIMoveTowards(waypoint1.transform);
                if (Vector2.Distance(transform.position, waypoint1.transform.position) < 0.1f)
                {
                    noticeWaypoint = 1;
                }

            }
            else if (noticeWaypoint == 1)
            {
                AIMoveTowards(waypoint2.transform);
                if (Vector2.Distance(transform.position, waypoint2.transform.position) < 0.1f)
                {
                    noticeWaypoint = 0;
                }

            }


        }

        else
        {
            //check which way point is close
            if (Vector2.Distance(transform.position, waypoint1.transform.position) < Vector2.Distance(transform.position, waypoint2.transform.position))
            {

                AIMoveTowards(waypoint1.transform);

            }
            else
            {
                AIMoveTowards(waypoint2.transform);
            }

            noticePlayer = false;

        }

       

     
     
      

    }
    public void AIMoveTowards(Transform goal)
    {

        if (Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }


    }
}













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