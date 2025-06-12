using UnityEngine;

public enum TurnEnum
{
    NONE,
    AI,
    PLAYER
}

public class TurnManager : MonoBehaviour
{
    [SerializeField] private RenderManager renderManager;
    public TurnEnum CurrentTurn = TurnEnum.AI;

    public void EndTurn()
    {
        var winner = renderManager.CheckWin();

        if (winner == TurnEnum.AI)
        {
            CurrentTurn = TurnEnum.NONE;
            
            return;
        }
        else if (winner == TurnEnum.PLAYER)
        {
            CurrentTurn = TurnEnum.NONE;

            return;
        }
        
        if (CurrentTurn == TurnEnum.AI)
        {
            CurrentTurn = TurnEnum.PLAYER;
        }
        else
        {
            CurrentTurn = TurnEnum.AI;
        }
    }
}
