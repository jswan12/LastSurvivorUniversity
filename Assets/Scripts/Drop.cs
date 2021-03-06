﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    // the prefab of the active power up
    public PowerUp powerUp;

    public enum DropTypes
    {
        Health_Pack,
        Shield_Pack,
        Ammo_Pack,
    }

    /// <summary>
    /// When the player walks into this power up, this executes
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch(powerUp.name)
            {
                case DropTypes.Health_Pack:
                    HealthPack(other.gameObject); break;
                case DropTypes.Shield_Pack:
                    ShieldPack(other.gameObject); break;
                case DropTypes.Ammo_Pack:
                    AmmoPack(other.gameObject); break;
                default:
                    break;
            }
            
        }
    }

    void HealthPack(GameObject player)
    {
        // don't pick up the shield drop if the player can't use it
        if (!player.GetComponent<playerHealth>().CanApplyHealth())
            return;

        player.GetComponent<playerHealth>().AddHealth(powerUp.changeInHealth);
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("yessir");
    }

    void ShieldPack(GameObject player)
    {
        // don't pick up the shield drop if the player can't use it
        if (!player.GetComponent<playerHealth>().CanApplyShield())
            return;

        player.GetComponent<playerHealth>().AddShield(powerUp.changeInShield);
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("yessir");
    }

    void AmmoPack(GameObject player)
    {
        // don't pick up the ammo drop if the player has melee weapon out
        if (!player.GetComponent<Weapon>().CanApplyAmmo())
            return;

        player.GetComponent<Weapon>().loadout[player.GetComponent<Weapon>().currentIndex].IncreaseRemainingTotal(powerUp.changeInTotalAmmo);
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("yessir");
    }
}
