using UnityEngine;
using System;
using System.Runtime.ExceptionServices;

public class TreeRoots : MonoBehaviour
{
    public GameObject segment;
    public GameObject start;
    public GameObject end;
    public float rand_box_size = 3f;
    public int num_vines = 3;
    public int SEGMENT_COUNT = 50;
    public bool scale = true;

    // Update is called once per frame
    void Start()
    {

        
    }

    public void Grow()
    {
        Vector3 last_pos;
        float dist_to_start = 0f;
        float max_dist = 0f;


        max_dist = Vector3.Distance(start.transform.position, end.transform.position);

        for (int i = 0; i < num_vines; i++)
        {
            Vector3 p1 = GetRandomVector3(start.transform.position, (start.transform.position + end.transform.position) / 2);
            Vector3 p2 = GetRandomVector3((start.transform.position + end.transform.position) / 2, end.transform.position);
            last_pos = start.transform.position;

            for (int j = 1; j <= SEGMENT_COUNT; j++)
            {
                float t = j / (float)SEGMENT_COUNT;

                Vector3 pos = CalculateCubicBezierPoint(t, start.transform.position, p1, p2, end.transform.position);

                if (scale)
                {
                    dist_to_start = Vector3.Distance(start.transform.position, pos);
                    Vector3 newScale = new Vector3(1, 1, 1);
                    newScale.x = newScale.z = (1.0f - 1.0f * dist_to_start / max_dist);
                    newScale.y = Mathf.Max((0.2f * dist_to_start / max_dist), 0.4f);
                    segment.transform.localScale = newScale;
                }
                Instantiate(segment, pos, Quaternion.FromToRotation(Vector3.up, pos - last_pos));

                last_pos = pos;
            }
        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    public Vector3 GetRandomVector3(Vector3 p1, Vector3 p2)
    {
        float max_x = Mathf.Max(p1.x, p2.x) + rand_box_size;
        float max_y = Mathf.Max(p1.y, p2.y) + rand_box_size;
        float max_z = Mathf.Max(p1.z, p2.z) + rand_box_size;
        float min_x = Mathf.Min(p1.x, p2.x) - rand_box_size;
        float min_y = Mathf.Min(p1.y, p2.y) - rand_box_size;
        float min_z = Mathf.Min(p1.z, p2.z) - rand_box_size;

        float new_x = (float)GetRandomNumber(min_x, max_x);
        float new_y = (float)GetRandomNumber(min_y, max_y);
        float new_z = (float)GetRandomNumber(min_z, max_z);

        return new Vector3(new_x, new_y, new_z);
    }

    public double GetRandomNumber(double minimum, double maximum)
    {
        System.Random random = new();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
}
