﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SoldierStatus : MonoBehaviour {

    private Soldier mySoldier;
    public Image nextSoldierDot;
    public Image soldierIcon;
    public Image healthBar;
    public Image deathMarker;
    public Text currentClipSize;
    public Image weaponImage;
    public bool alive = true;
    internal bool minimal { get; private set; }

    void Start()
    {
        mySoldier = transform.root.GetComponent<Soldier>();
    }

    public void UpdateStatus(Soldier mySoldier, Color teamColor)
    {
        if (nextSoldierDot != null)
        {
            if (TeamManager.instance.teamIndex == mySoldier.myTeam)
            {
                nextSoldierDot.color = teamColor;
                if (TeamManager.instance.allTeams[mySoldier.myTeam].allSoldiers[TeamManager.instance.allTeams[mySoldier.myTeam].soldierIndex] == transform.root.GetComponent<Soldier>())
                {
                    nextSoldierDot.gameObject.SetActive(true);
                }
            }
            else
            {
                nextSoldierDot.gameObject.SetActive(false);
            }
        }


        if (!soldierIcon.gameObject.activeInHierarchy)
            soldierIcon.gameObject.SetActive(true);

        if (mySoldier.health <= 0 || mySoldier.isDead)
        {
            deathMarker.gameObject.SetActive(true);
            alive = false;
        }
        else
        {
            deathMarker.gameObject.SetActive(false);
            alive = true;
        }

        soldierIcon.color = teamColor;
        if(currentClipSize != null)
        {
            currentClipSize.text = mySoldier.equippedWeapon.currentClip.ToString();
        }
        else
        {
            Debug.LogError("There is no currentclipsize text");
        }
        weaponImage.sprite = mySoldier.equippedWeapon.weaponSprite;

        float percent = (float)mySoldier.health / mySoldier.maxHealth;
        healthBar.fillAmount = percent;
    }

    public void ToggleMinimalism(bool toggle)
    {
        if (toggle)
        {
            currentClipSize.transform.parent.gameObject.SetActive(false);
            weaponImage.gameObject.SetActive(false);
            minimal = true;
        }
        else
        {
            currentClipSize.transform.parent.gameObject.SetActive(true);
            weaponImage.gameObject.SetActive(true);
            minimal = false;
        }

    }
}
