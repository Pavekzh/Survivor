using UnityEngine;

public class TargetDesignator:MonoBehaviour
{
    private Character[] playersArray;

    private Character[] players
    {
        get
        {
            if (playersArray == null)
                playersArray = new Character[FusionConnect.PlayersCount];

            return playersArray;
        }
    }
    private int lastAdded = 0;

    public void AddPlayer(Character character)
    {
        players[lastAdded] = character;
        lastAdded++;
    }

    public void RemovePlayer()
    {
        lastAdded--;
    }

    public bool GetTarget(Vector3 position,out Character target)
    {
        float minDistance = ((Vector2)players[0].transform.position - (Vector2)position).magnitude;
        int nearestIndex = -1;

        if (players[0].IsAlive)
            nearestIndex = 0;

        for(int i = 1; i < lastAdded; i++)
        {
            float distance = ((Vector2)players[i].transform.position - (Vector2)position).magnitude;
            if ((minDistance > distance || nearestIndex == -1) && players[i].IsAlive)
            {
                minDistance = distance;
                nearestIndex = i;
            }

        }

        if (nearestIndex != -1)
        {            
            target = players[nearestIndex];
            return true;
        }
        else
        {
            target = null;
            return false;
        }

    }

    public float GetNearest(Vector3 position, out Character target)
    {
        float minDistance = ((Vector2)players[0].transform.position - (Vector2)position).magnitude;
        int nearestIndex = 0;

        for (int i = 1; i < lastAdded; i++)
        {
            float distance = ((Vector2)players[i].transform.position - (Vector2)position).magnitude;
            if (minDistance > distance)
            {
                minDistance = distance;
                nearestIndex = i;
            }

        }

        target = players[nearestIndex];
        return minDistance;
    }
}