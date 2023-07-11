using System.Collections.Generic;
using UnityEngine;


public class ScoreCounter:MonoBehaviour
{
    public Dictionary<string, ScoreRecord> Records { get; private set; } = new Dictionary<string, ScoreRecord>();

    public class ScoreRecord
    {
        public int Kills;
        public float Damage;
    }

    public void AddKill(string sender)
    {
        ScoreRecord record;
        if (Records.TryGetValue(sender, out record))
            record.Kills++;
        else
            Records.Add(sender, new ScoreRecord() { Kills = 1 });
    }

    public void AddDamage(float damage,string sender)
    {
        ScoreRecord record;
        if (Records.TryGetValue(sender, out record))
            record.Damage += damage;
        else
            Records.Add(sender, new ScoreRecord() { Damage = damage });
    }

}

