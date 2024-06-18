using System;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] GameObject pointPrefab;
    [SerializeField, Range(2, 100)] int resolution;
    [SerializeField] FunctionLibrary.FunctionName functionName, transitionFunctionName;
    [SerializeField, Min(0)] float funtionDuration = 5;
    [SerializeField, Min(0)] float transitionDuration = 1;
    private FunctionLibrary.Function function;
    private bool isTransitioning = false;
    private Transform[] points;
    private float scale = .1f;
    private float xRange = 1;
    private float duration = 0;

    void Start()
    {
        function = FunctionLibrary.GetFunction(functionName);
        points = new Transform[resolution * resolution];
        scale = 2f / resolution;
        for (int i = 0; i < resolution * resolution; i++)
        {
            Transform point = points[i] = Instantiate(pointPrefab.transform);
            point.localScale = Vector3.one * scale;
            point.SetParent(transform, false);
        }
    }

    void Update()
    {
        duration += Time.deltaTime;
        if (duration > funtionDuration)
        {
            duration -= funtionDuration;
            transitionFunctionName = functionName;
            functionName = FunctionLibrary.GetNextFunction(functionName);
            function = FunctionLibrary.GetFunction(functionName);
            isTransitioning = true;
        }
        if (isTransitioning)
        {
            UpdateFunctionTransition();
            if (duration > transitionDuration)
            {
                isTransitioning = false;
            }
        }
        else
            UpdateFunction();
    }

    void UpdateFunction()
    {
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
            points[i].position = function(u, v, Time.time);
        }
    }

    void UpdateFunctionTransition()
    {
        FunctionLibrary.Function
            from = FunctionLibrary.GetFunction(transitionFunctionName),
            to = FunctionLibrary.GetFunction(functionName);
        float progress = duration / transitionDuration;
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
            points[i].position = FunctionLibrary.Morph(u, v, Time.time, from, to, progress);
        }
    }
}