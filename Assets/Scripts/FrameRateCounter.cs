using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    public enum playMode { FPS, MS }
    [SerializeField] playMode displayMode = playMode.FPS;

    [SerializeField, Range(0.1f, 2f)]
    private float sampleDuration = 1f;
    private int frames = 0;
    private float duration = 0, bestDuration = float.MaxValue, worstDuration = 0;
    void Start()
    {
        display = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        frames++;
        duration += Time.unscaledDeltaTime;
        bestDuration = bestDuration < Time.unscaledDeltaTime ? bestDuration : Time.unscaledDeltaTime;
        worstDuration = worstDuration > Time.unscaledDeltaTime ? worstDuration : Time.unscaledDeltaTime;
        if (duration > sampleDuration)
        {
            switch (displayMode)
            {
                case playMode.FPS:
                    display.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 1f / bestDuration, frames / duration, 1f / worstDuration);
                    break;
                case playMode.MS:
                    display.SetText(
                        "MS\n{0:1}\n{1:1}\n{2:1}",
                        1000f * bestDuration,
                        1000f * duration / frames,
                        1000f * worstDuration
                    );
                    break;
            }
            frames = 0;
            duration = 0;
            bestDuration = float.MaxValue;
            worstDuration = 0;
        }
    }
}