namespace MazeWorld
{
    public enum MazeWorldObjectType
    {
        None,
        Creature,
        Thing
    }
    
    [System.Serializable]
    public enum MazeWorldCreatures
    {
        None,
        Player,
        Ghost,
        OozeyGreenMug,
        OozeyRedMug,
        MazeGuardian,
        MazeGoblin,
        MazeVampire,
        MouthlessLady,
        RatFromKelgar,
        Minator
    }

    public enum MazeWorldObjects
    {
        None,
        Key
    }
}