namespace SimpleDB;

public class ControlPanel
{
    public int pageCounter;
    public int[][] pagedCheeps;
    // Function thet gets input from the buttons to determine what page the user is on
    // Consider if this is necessary when there is a pageCounter
    public static int determinePage()
    {
        return pageCounter;
    }

    public static int[] determineCheeps(int pageNr)
    {
        return pagedCheeps[pageNr];
    }
    
    // Function that returns the cheeps based on the current page
    public static pagiseCheeps(int[] cheeps)
    {
        // Array of cheepId from the DB, sorted by recently posted
        int[] cheeps;
        int cheepCounter = 0;
        int pages = Math.Cieling(cheeps.Length / 20); // Get's the needed amount of pages
        int[pages][20] pagedCheeps;

        for (int i = 0; i < pages; i++) // Loop to go through the pages
        {
            int tempList[] = pagedCheeps[i];
            
            for (int j = 0; j < 20; j++) // Loop to insert the cheepId into the list
            {
                tempList[j] = allCheeps[cheepCounter];
                cheepCounter++;
            }
        }
        // Group them by 20 (maybe add possebility to customise amount of cheeps shown)
    }
}