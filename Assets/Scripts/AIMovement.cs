using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 1.5f;
    public float noticeWaypoint = 0;
    public float minGoalDistance = 0.05f;
    public float chaseDistanace = 3f;
    public int wayPointIndex = 0;
    public List<GameObject> wayPoints;
    public GameObject wayPointPrefab;

    private void Start()
    {
        NewWayPoint();
        NewWayPoint();
        NewWayPoint();


    }


    public bool NoticePlayer()
    {
        return Vector2.Distance(transform.position, player.position) < chaseDistanace;     
         
    }
    public void WaypointUpdate()
    {
        if (Vector2.Distance(transform.position, wayPoints[wayPointIndex].transform.position) < minGoalDistance)
        {
            RemoveCurrentWayPoint();
            wayPointIndex++;
            if (wayPointIndex >= wayPoints.Count)
            {
                wayPointIndex = 0;
            }
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
    public void WayPointsMovement()
    {
        AIMoveTowards(wayPoints[wayPointIndex].transform);
        WaypointUpdate();
    }
    public void NewWayPoint()
    {
        float x = Random.Range(-5f, 5f); 
        float y = Random.Range(-5f, 5f);

        GameObject newPoint =Instantiate(wayPointPrefab,new Vector2(x,y), Quaternion.identity);
        wayPoints.Add(newPoint);
    }
    public void RemoveCurrentWayPoint()
    {
        GameObject current = wayPoints[wayPointIndex];
        wayPoints.Remove(current);
        Destroy(current);
    }
    public void findClosestWayPoint()
    {
        int nearestIndex = 0;
        float currentNearest = float.PositiveInfinity;
        for (int i = 0; i < wayPoints.Count; i++)
        {
            float test = Vector2.Distance(transform.position, wayPoints[i].transform.position);
            if (currentNearest > test)
            {
                nearestIndex=i;
                currentNearest = test;
            }
        }
        wayPointIndex = nearestIndex;
    }
}









//void Update()
//{
//    if (Vector2.Distance(transform.position, player.position) < chaseDistanace)
//    {
//        noticePlayer = true;

//        AIMoveTowards(player);
//    }
//    else if (!noticePlayer)
//    {
//        //patrol
//        if (noticeWaypoint == 0)
//        {
//            AIMoveTowards(waypoint1.transform);
//            if (Vector2.Distance(transform.position, waypoint1.transform.position) < 0.1f)
//            {
//                noticeWaypoint = 1;
//            }

//        }
//        else if (noticeWaypoint == 1)
//        {
//            AIMoveTowards(waypoint2.transform);
//            if (Vector2.Distance(transform.position, waypoint2.transform.position) < 0.1f)
//            {
//                noticeWaypoint = 0;
//            }

//        }


//    }

//    else
//    {
//        //check which way point is close
//        if (Vector2.Distance(transform.position, waypoint1.transform.position) < Vector2.Distance(transform.position, waypoint2.transform.position))
//        {

//            AIMoveTowards(waypoint1.transform);

//        }
//        else
//        {
//            AIMoveTowards(waypoint2.transform);
//        }

//        noticePlayer = false;

//    }







//}



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