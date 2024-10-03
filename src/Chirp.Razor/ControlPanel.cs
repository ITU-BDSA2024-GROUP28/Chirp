namespace SimpleDB;

public class ControlPanel
{
    public int pageCounter;
    // Function thet gets input from the buttons to determine what page the user is on
    // Consider if this is necessary when there is a pageCounter
    public static int determinePage()
    {
        return pageCounter;
    }

    // Function that returns the cheeps based on the current page
    public static int[] determineCheeps(int page)
    {
        // Sort the cheeps by recent posts
        // Group them by 20 (maybe add possebility to customise amount of cheeps shown)
    }
}