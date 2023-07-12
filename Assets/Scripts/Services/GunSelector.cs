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

    public Gun GetGun(Transform owner)
    {
        int index = Random.Range(0, FreeGuns.Count);     
        GameObject prefab = gunPrefabs[FreeGuns[index]];
        FreeGuns.RemoveAt(index);

        Gun result = Instantiate(prefab, owner, false).GetComponent<Gun>();
        return result;
    }
}