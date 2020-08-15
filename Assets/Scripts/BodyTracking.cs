using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTracking : MonoBehaviour
{
    public Transform pointBody;

    private Transform[] joints;
    private Vector3[] jointPos;

    private void Start()
    {
        pointBody = GameObject.FindGameObjectWithTag("PointBody").transform;

        if (pointBody != null)
        {
            ActivatePointBody();
        }
    }

    private void ActivatePointBody()
    {
        int children = pointBody.childCount;

        joints = new Transform[children];
        jointPos = new Vector3[children];

        for (int i = 0; i < joints.Length; i++)
        {
            joints[i] = pointBody.GetChild(i);
            joints[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (pointBody == null)
        {
            Debug.LogError("NO POINT BODY");
            pointBody = GameObject.FindGameObjectWithTag("PointBody").transform;

            if (pointBody != null)
            {
                ActivatePointBody();
            }
            
        }
    }

    public void UpdateSkeleton(string inputdata)
    {
        GetValues(inputdata);

        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].localPosition = jointPos[i];
        }
    }

    private void GetValues(string inputdata)
    {
        string[] jointData = inputdata.Split(')');//should provide (x, y, z 32 joints

        for (int i = 0; i < jointData.Length; i++)
        {
            string s = jointData[i];
            s = s.Remove('(');

            string[] values = s.Split(',');

            jointPos[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
    }
}
