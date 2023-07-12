using System;
using UnityEngine;
using Photon.Pun;

public class PlayerFactory:MonoBehaviour
{
    public virtual bool InstantiatePlayer(GameObject characterPrefab,GameObject weaponPrefab,out Character character,out Gun weapon)
    {
        character = PhotonNetwork.Instantiate(characterPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<Character>();
        weapon = PhotonNetwork.Instantiate(weaponPrefab.name, weaponPrefab.transform.position, Quaternion.identity).GetComponent<Gun>();

        if (character != null && weapon != null)
        {
            weapon.transform.parent = character.transform;
            return true;
        }
        else
            return false;
    }
}

