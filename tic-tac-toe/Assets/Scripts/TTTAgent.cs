using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class TTTAgent : Agent
{
    [SerializeField] private RenderManager renderManager;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private TurnEnum myTurn;

    public override void CollectObservations(VectorSensor sensor)
    {
        for (int i = 0; i < 9; i++)
        {
            TurnEnum state = renderManager.CanPut(i) ? TurnEnum.NONE : TurnEnum.AI;
            sensor.AddObservation((int)state);
        }
        sensor.AddObservation(turnManager.CurrentTurn == myTurn ? 1 : 0);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];

        if (turnManager.CurrentTurn != myTurn)
        {
            AddReward(-0.01f);
            return;
        }

        if (renderManager.CanPut(action))
        {
            renderManager.Put(action, myTurn);

            var winner = renderManager.CheckWin();

            if (winner == myTurn)
            {
                AddReward(1.0f);
                turnManager.EndTurn();
                EndEpisode();
            }
            else if (winner != TurnEnum.NONE)
            {
                AddReward(-1.0f);
                turnManager.EndTurn();
                EndEpisode();

            }
            else
            {
                bool draw = true;
                for (int i = 0; i < 9; i++)
                {
                    if (renderManager.CanPut(i))
                    {
                        draw = false;
                        break;
                    }
                }
                if (draw)
                {
                    AddReward(-0.1f);
                    turnManager.EndTurn();
                    EndEpisode();
                }
                else
                {
                    turnManager.EndTurn();
                }
            }
        }
        else
        {
            AddReward(-0.2f);
            for (int i = 0; i < 9; i++)
            {
                if (renderManager.CanPut(i))
                {
                    renderManager.Put(i, myTurn);
                    break;
                }
            }
            turnManager.EndTurn();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.DiscreteActions.Array[0] = Random.Range(0, 9);
    }
}
