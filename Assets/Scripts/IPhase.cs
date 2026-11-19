using System.Collections;

public interface IPhase
{
   public IEnumerator Tick(GameData gameData);
}
