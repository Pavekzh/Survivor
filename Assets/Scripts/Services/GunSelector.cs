using UnityEngine;

public class GunSelector : MonoBehaviour
{
    [SerializeField] private GunSettings[] guns;

    private System.Collections.Generic.List<int> freeGuns;

    private System.Collections.Generic.List<int> FreeGuns
    {
        get
        {
            if(freeGuns == null)
            {
                freeGuns = new System.Collections.Generic.List<int>();
                for(int i = 0; i< guns.Length; i++)
                {
                    freeGuns.Add(i);
                }
            }
            return freeGuns;
        }
    }

    public GunSettings GetGun()
    {
        int index = Random.Range(0, FreeGuns.Count);     
        GunSettings gun = guns[FreeGuns[index]];
        FreeGuns.RemoveAt(index);

        return gun;
    }
}