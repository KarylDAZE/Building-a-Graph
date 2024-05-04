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
        float v = scale * 0.5f - xRange;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(functionName);
        float step = (xRange * 2 - scale) / (resolution - 1);
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (resolution == x)
            {
                x = 0;
                z++;
                v = z * step + scale * 0.5f - xRange;
            }
            float u = x * step + scale * 0.5f - xRange;

            points[i].position = f(u, v, Time.time);
        }
    }
}