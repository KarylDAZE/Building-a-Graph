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
    private Vector3 position = Vector3.zero;
    private float scale = .2f;
    private float xRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        float step = (xRange * 2 - scale) / (resolution - 1);
        points = new Transform[resolution * resolution];
        // for (int i = 0,x=0,z=0; i < resolution; i++,x++)
        // {
        //     if(resolution==x){
        //         x=0;
        //         z++;
        //     }
        //     Transform point = points[i] = Instantiate(pointPrefab.transform);
        //     position.x = x * step + scale * 0.5f - xRange;
        //     position.z=z * step + scale * 0.5f - xRange;
        //     point.localPosition = position;
        //     point.localScale = Vector3.one * scale;
        //     point.SetParent(transform, false);
        // }
        for (int x = 0; x < resolution; x++)
            for (int z = 0; z < resolution; z++)
            {
                Transform point = points[x * resolution + z] = Instantiate(pointPrefab.transform);
                position.x = x * step + scale * 0.5f - xRange;
                position.z = z * step + scale * 0.5f - xRange;
                point.localPosition = position;
                point.localScale = Vector3.one * scale;
                point.SetParent(transform, false);
            }
    }

    // Update is called once per frame
    void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(functionName);
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 pos = points[i].position;
            pos.y = f(pos.x, pos.z, Time.time);
            points[i].position = pos;
        }
    }
}
