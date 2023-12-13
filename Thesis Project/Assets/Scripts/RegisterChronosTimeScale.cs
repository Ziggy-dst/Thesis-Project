using Chronos;
using DG.Tweening;

public static class RegisterChronosTimeScale
{
    public static void RegisterChronosTimeline(this Tween tween, Timeline timeline)
    {
        tween.OnUpdate(()=> tween.timeScale = timeline.timeScale);
    }
}