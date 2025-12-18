
namespace EndlessRunner
{
    public enum UiScreen
    {
        None,
        Loading,
        Splash,
        Gameplay,
        Gameover,
        QuitPopup,
    }

    public enum UiScreenShowBehaviour
    {
        HidePrevious,
        KeepPrevious
    }

    public enum GameState
    {
        Pause,
        Resume
    }

    public enum Lane
    {
        Left,
        Middle,
        Right,
    }

    public enum LevelBlocksType
    {
        BeachCity = 0,
        BeachGarden = 1,
        BeachSegment = 2,
        CityGarden = 3,
        CitySegment = 4,
        CrossingSegment = 5,
        GardenCity = 6,
        ParkPondFountain = 7,
        ParkSegment = 8,
        PondFountainSegment = 9,
        RiverPondFountain = 10,
        RiverSegment = 11,
        TunnelSegment = 12
    }
}
