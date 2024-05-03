using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Mathf;
public static class FunctionLibrary
{
    public enum FunctionName { Zero, Wave, MultiWave, Ripple }
    private static Function[] functions = { Zero, Wave, MultiWave, Ripple };

    public delegate float Function(float x, float t);
    public static Function GetFunction(FunctionName name)
    {
        return functions[(int)name];
    }

    public static float Zero(float x, float t)
    {
        return 0;
    }

    public static float Wave(float x, float t)
    {
        return Sin(PI * (x + t));
    }

    public static float MultiWave(float x, float t)
    {
        float y = Sin(PI * (x + t));
        //Division requires a bit more work than multiplication
        //And constant expressions are already reduced to a single number by the compiler
        y += Sin(2f * PI * (x + t)) * 0.5f;
        return y * (2f / 3f);
    }

    public static float Ripple(float x, float t)
    {
        float d = Abs(x);
        float y = Sin(PI * (4f * d - t)) / (1f + 10f * d);
        return y;
    }

}