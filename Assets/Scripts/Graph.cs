using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] GameObject pointPrefab;
    [SerializeField, Range(2, 100)] int resolution;
    private Vector3 position = Vector3.zero;
    private float scale = .2f;
    private float xRange = 1;
    // Start is called before the first frame update
    void Start()
    {
        float step = (xRange * 2 - scale) / (resolution - 1);
        for (int i = 0; i < resolution; i++)
        {
            Transform point = Instantiate(pointPrefab.transform);
            position.x = i * step + scale * 0.5f - xRange;
            position.y = position.x * position.x;
            point.localPosition = position;
            point.localScale = Vector3.one * scale;
            point.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
