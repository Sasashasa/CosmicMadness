public static class Stats
{
    public static int CurLevel;
    public static int BlueStars;
    public static int RedStars;
    
    public static void ResetAll()
    {
        CurLevel = 0;
        BlueStars = 0;
        RedStars = 0;
        
        Upgrades.ResetAll();
    }
}