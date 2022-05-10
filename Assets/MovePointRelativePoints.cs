using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointRelativePoints : MonoBehaviour
{
    [SerializeField] private GameObject m_renderObject;
    [SerializeField] private Vector3 m_origin;
    private List<Vector3> m_startingPoints = new List<Vector3>();

    private List<Vector2> m_angles = new List<Vector2>();

    [SerializeField] private Vector2 m_rotateSpeed;

    private List<GameObject> m_pointsObjects = new List<GameObject>();

    [SerializeField] private Vector2 m_randomSize;
    [SerializeField] private Vector2 m_randomAngles;
    [SerializeField] private Vector3 m_randomOffsets;


    private void Start()
    {
        GameObject origin = Instantiate(m_renderObject);
        //m_pointsObjects.Add(origin);
        origin.transform.position = m_origin;
        origin.name = "Patricle Manager";
        m_pointsObjects.Add(origin);
        m_startingPoints.Add(m_origin);
        m_angles.Add(new Vector2(Random.Range(-m_randomAngles.x, m_randomAngles.x), Random.Range(-m_randomAngles.y, m_randomAngles.y)));

        for (int i = 0; i < Random.Range(100, 200); i++)
        {
            m_startingPoints.Add(new Vector3(Random.Range(-m_randomOffsets.x, m_randomOffsets.x ), Random.Range(-m_randomOffsets.y, m_randomOffsets.y), Random.Range(-m_randomOffsets.z, m_randomOffsets.z)));
            GameObject point = Instantiate(m_renderObject);
            float size = Random.Range(m_randomSize.x, m_randomSize.y);
            point.transform.localScale = new Vector3(size,size,size);
            point.transform.position = m_startingPoints[i];
            m_pointsObjects.Add(point);
            m_angles.Add(new Vector2(Random.Range(-m_randomAngles.x, m_randomAngles.x), Random.Range(-m_randomAngles.y, m_randomAngles.y)));

        }

    }


    private Vector3 MoveRelativeFromPoint(Vector3 relativeTo, Vector3 point,Vector2 angle)
    {
        point.x = (Mathf.Abs(relativeTo.x - point.x) * Mathf.Cos(angle.x * Mathf.Deg2Rad) * Mathf.Cos(angle.y * Mathf.Deg2Rad));
        point.z = Mathf.Abs(relativeTo.z - point.z) * Mathf.Sin(angle.x * Mathf.Deg2Rad);
        point.y = Mathf.Abs(relativeTo.y - point.y) * Mathf.Sin(angle.y * Mathf.Deg2Rad);


        return point;
    }
    void Update()
    {

        for(int i = 1; i < m_startingPoints.Count; i++)
        {
            m_angles[i] += m_rotateSpeed;
            if (m_angles[i].x > 360)
            {
                m_angles[i] = new Vector2(0,m_angles[i].y);
            }else if(m_angles[i].y > 360)
            {
                m_angles[i] = new Vector2(m_angles[i].x,0);
            }
            Vector3 startPoint = m_startingPoints[i];
            //Vector3 relativePoint = m_pointsObjects[Random.Range(0, m_pointsObjects.Count)].transform.position;
            m_pointsObjects[i].transform.position = MoveRelativeFromPoint(m_pointsObjects[0].transform.position,startPoint, m_angles[i]);
        }

        //m_pointsObjects[1].transform.position = (MoveRelativeFromPoint(m_pointsObjects[0].transform.position,m_startingPoint, m_angles));

       Vector3 boxPosition = m_pointsObjects[0].transform.position;
        Debug.DrawLine(new Vector3(-boxPosition.x, -boxPosition.y, -boxPosition.z), new Vector3(boxPosition.x, -boxPosition.y, -boxPosition.z), Color.green);
        Debug.DrawLine(new Vector3(-boxPosition.x, -boxPosition.y, -boxPosition.z), new Vector3(-boxPosition.x, boxPosition.y, -boxPosition.z), Color.green);
        Debug.DrawLine(new Vector3(-boxPosition.x, -boxPosition.y, -boxPosition.z), new Vector3(-boxPosition.x,-boxPosition.y, boxPosition.z), Color.green);


        Debug.DrawLine(new Vector3(boxPosition.x, boxPosition.y, boxPosition.z), new Vector3(-boxPosition.x, boxPosition.y, boxPosition.z), Color.green);
        Debug.DrawLine(new Vector3(boxPosition.x, boxPosition.y, boxPosition.z), new Vector3(boxPosition.x, -boxPosition.y, boxPosition.z), Color.green);
        Debug.DrawLine(new Vector3(boxPosition.x, boxPosition.y, boxPosition.z), new Vector3(boxPosition.x, boxPosition.y, -boxPosition.z), Color.green);

        Debug.DrawLine(new Vector3(-boxPosition.x, boxPosition.y, boxPosition.z), new Vector3(boxPosition.x, boxPosition.y, boxPosition.z), Color.green);

    }
}
