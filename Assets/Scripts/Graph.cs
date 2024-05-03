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
        Vector3 position = Vector3.zero;
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(functionName);
        float step = (xRange * 2 - scale) / (resolution - 1);
        for (int u = 0; u < resolution; u++)
        {
            position.x = u * step + scale * 0.5f - xRange;
            for (int v = 0; v < resolution; v++)
            {
                position.z = v * step + scale * 0.5f - xRange;
                points[u * resolution + v].position = f(position.x, position.z, Time.time);
            }
        }
    }
}