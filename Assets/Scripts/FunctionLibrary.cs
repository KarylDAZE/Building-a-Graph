using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Mathf;
public static class FunctionLibrary
{
    public enum FunctionName { Zero, Wave, MultiWave, Ripple }
    private static Function[] functions = { Zero, Wave, MultiWave, Ripple };

    public delegate Vector3 Function(float u, float v, float t);
    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static Vector3 Zero(float x, float z, float t)
    {
        return Vector3.zero;
    }

    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + t));
        p.y += Sin(2f * PI * (u + t)) * 0.5f;
        p.y += Sin(4f * PI * (v + t)) * 0.25f;
        p.y /= (1 + 0.5f + 0.25f);
        p.z = v;
        return p;

    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p;
        float d = Sqrt(u * u + v * v);
        p.x = u;
        p.y = Sin(PI * (4f * d - t)) / (1f + 10f * d);
        p.z = v;
        return p;
    }

}