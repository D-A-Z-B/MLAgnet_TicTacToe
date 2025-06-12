using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    [SerializeField] private GameObject xPrefab;
    [SerializeField] private GameObject oPrefab;
    [SerializeField] private GameCanvas gameCanvas;

    [SerializeField] private List<Slot> trmList;
    private List<TurnEnum> board;

    private List<GameObject> spawnedObjectList = new List<GameObject>();

    private bool isGameOver = false;

    private void Awake()
    {
        board = new List<TurnEnum>();

        for (int i = 0; i < trmList.Count; ++i)
        {
            board.Add(TurnEnum.NONE);
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        bool draw = true;
        for (int i = 0; i < 9; i++)
        {
            if (CanPut(i))
            {
                draw = false;
                break;
            }
        }

        if (draw)
        {
            gameCanvas.Open();
            gameCanvas.SetWinnerText("Draw");
        }
    }

    public void Put(int index, TurnEnum turnEnum)
    {
        board[index] = turnEnum;


        if (turnEnum == TurnEnum.AI)
        {
            spawnedObjectList.Add(Instantiate(xPrefab, trmList[index].transform.position, Quaternion.identity));
        }
        else if (turnEnum == TurnEnum.PLAYER)
        {
            spawnedObjectList.Add(Instantiate(oPrefab, trmList[index].transform.position, Quaternion.identity));
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < board.Count; ++i)
        {
            board[i] = TurnEnum.NONE;
        }

        foreach (GameObject obj in spawnedObjectList)
        {
            Destroy(obj.gameObject);
        }
    }

    public bool CanPut(int index)
    {
        if (board[index] == TurnEnum.NONE)
        {
            return true;
        }
        return false;
    }

    public TurnEnum CheckWin()
    {
        int[][] winPatterns = new int[][]
        {
            new int[] {0, 1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},

            new int[] {0, 3, 6},
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},

            new int[] {0, 4, 8},
            new int[] {2, 4, 6}
        };

        foreach (var pattern in winPatterns)
        {
            TurnEnum a = board[pattern[0]];
            TurnEnum b = board[pattern[1]];
            TurnEnum c = board[pattern[2]];

            if (a != TurnEnum.NONE && a == b && b == c)
            {
                gameCanvas.Open();
                gameCanvas.SetWinnerText(a.ToString());

                isGameOver = true;

                return a;
            }
        }

        return TurnEnum.NONE;
    }
}
