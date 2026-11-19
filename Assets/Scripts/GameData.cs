using System.Collections.Generic;

public class GameData
{
    public List<int> expButtonList = new List<int>();
    public List<int> actButtonList = new List<int>(); 
    private bool isPlaying = false;
    private bool extendedGame = false;

    public void setIsPlaying(bool b)
    {
        isPlaying = b;
    }   

    public bool getIsPlaying()
    {
        return isPlaying;
    }  
    
    public void setExtendedGame(bool b)
    {
        extendedGame = b;
    }   

    public bool getExtendedGame()
    {
        return extendedGame;
    }  
}
