using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeScreenManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject hpLogo;
    [SerializeField] private GameObject defLogo;
    [SerializeField] private GameObject dmgLogo;
    [SerializeField] private GameObject atkSpeedLogo;
    
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text defText;
    [SerializeField] private TMP_Text dmgText;
    [SerializeField] private TMP_Text atkSpeedText;

    [SerializeField] private GameObject indicator;
    private Vector3 offset = new Vector3();

    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private float healthIncrease = 10;
    [SerializeField] private float defenseIncrease = 1;
    [SerializeField] private float damageIncrease = 0.2f;
    [SerializeField] private float attackSpeedIncrease = 0.2f;

    public GameObject selectedObject;

    private UIBarScript _hpBarUI;

    private void Awake()
    {
        offset = indicator.transform.position - hpLogo.transform.position;
        ChangeSelection(0);

        _hpBarUI = GameObject.FindGameObjectWithTag("HpBarUI").GetComponent<UIBarScript>();
    }

    public void ChangeSelection(int selection)
    {
        switch (selection)
        {
            case 0:
                Select(hpLogo);
                descriptionText.text = "Increase max health by " + healthIncrease;
                break;
            case 1:
                Select(defLogo);
                descriptionText.text = "Increase Defense by " + defenseIncrease;
                break;
            case 2:
                Select(dmgLogo);
                descriptionText.text = "Increase damage multiplier by " + damageIncrease;
                break;
            case 3:
                Select(atkSpeedLogo);
                descriptionText.text = "Increase attack speed by " + attackSpeedIncrease;
                break;
            default:
                Debug.Log("Selection out of index");
                break;
        }
    }

    private void Select(GameObject objectToSelect)
    {
        indicator.GetComponent<UpgradeIndicatorScript>()
                 .ChangePosition(objectToSelect.transform.position + new Vector3(0, offset.y, 0));
    }

    public void UpgradeObject(int selection)
    {
        if (!selectedObject)
        {
            return;
        }

        ObjectWithStatsScript objectToUpgrade = selectedObject.GetComponent<ObjectWithStatsScript>();

        switch (selection)
        {
            case 0:
                objectToUpgrade.health += healthIncrease;
                objectToUpgrade.maxHealth += healthIncrease;
                objectToUpgrade.healthBar.GetComponent<HealthBarScript>().ChangeMaxHealth(objectToUpgrade.maxHealth);

                if (objectToUpgrade.gameObject.CompareTag("Player"))
                {
                    _hpBarUI.ChangeMaxValue(objectToUpgrade.maxHealth);
                    _hpBarUI.ChangeValue(objectToUpgrade.health);
                }
                break;
            case 1:
                objectToUpgrade.defense += defenseIncrease;
                break;
            case 2:
                objectToUpgrade.damageMultiplier += damageIncrease;
                break;
            case 3:
                objectToUpgrade.attackSpeed += attackSpeedIncrease;
                break;
            default:
                Debug.Log("Selection out of index");
                break;
        }
        
        GameStateControllerScript.Current.ChangeState(GameStateControllerScript.GameState.Game);
    }

    public void ShowStats()
    {
        ObjectWithStatsScript selectedObjectScript = selectedObject.GetComponent<ObjectWithStatsScript>();
        hpText.text = Math.Round(selectedObjectScript.maxHealth, 2).ToString();
        defText.text = Math.Round(selectedObjectScript.defense, 2).ToString();
        dmgText.text = Math.Round(selectedObjectScript.damageMultiplier, 2).ToString();
        atkSpeedText.text = Math.Round(selectedObjectScript.attackSpeed, 2).ToString();
    }
}