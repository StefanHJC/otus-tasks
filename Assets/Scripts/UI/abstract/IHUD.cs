namespace ShootEmUp
{
    public interface IHUD
    {
        GameplayButton StartButton { get; }
        GameplayButton ResumeButton { get; }
        GameplayButton PauseButton { get; }
        ScreenTextRenderer ScreenTextRenderer { get; }
    }
}