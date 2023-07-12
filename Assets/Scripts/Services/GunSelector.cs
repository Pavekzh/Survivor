using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] gunPrefabs;

    private System.Collections.Generic.List<int> freeGuns;

    private System.Collections.Generic.List<int> FreeGuns
    {
        get
        {
            if(freeGuns == null)
            {
                freeGuns = new System.Collections.Generic.List<int>();
                for(int i = 0; i< gunPrefabs.Length; i++)
                {
                    freeGuns.Add(i);
                }
            }
            return freeGuns;
        }
    }

    public GameObject GetGun()
    {
        int index = Random.Range(0, FreeGuns.Count);     
        GameObject prefab = gunPrefabs[FreeGuns[index]];
        FreeGuns.RemoveAt(index);

        return prefab;
    }
}