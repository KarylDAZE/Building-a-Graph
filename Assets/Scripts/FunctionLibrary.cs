using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Mathf;
public static class FunctionLibrary
{
    public enum FunctionName { Zero, Wave, MultiWave, Ripple, Sphere, Torus }
    private static Function[] functions = { Zero, Wave, MultiWave, Ripple, Sphere, Torus };

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

    public static Vector3 Sphere(float u, float v, float t)
    {
        float r = 0.5f + 0.5f * Sin(PI * t);
        float s = Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p * r;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}