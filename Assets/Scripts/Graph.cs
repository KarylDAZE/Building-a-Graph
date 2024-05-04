using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] GameObject pointPrefab;
    [SerializeField, Range(2, 100)] int resolution;
    [SerializeField] FunctionLibrary.FunctionName functionName;
    Transform[] points;
    private float scale = .1f;
    private float xRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        points = new Transform[resolution * resolution];
        for (int i = 0; i < resolution * resolution; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab.transform);
            point.localScale = Vector3.one * scale;
            point.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(functionName);
        float step = 2f / resolution;
        float v = 0.5f * step - xRange;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - xRange;
            }
            float u = (x + 0.5f) * step - xRange;
            points[i].position = f(u, v, Time.time);
        }
    }
}