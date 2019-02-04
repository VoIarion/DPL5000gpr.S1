﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    public static Vector4 invisColor = Vector4.zero;
    public static Vector4 opaqueColor = new Vector4(0, 0, 0, 1);

    [SerializeField]
    private TextMeshProUGUI characterName, characterStats, characterSkillInfo;
    [SerializeField]
    private Image characterPortrait, characterPreview;
    [SerializeField]
    private Image[] characterBuffs;
    [SerializeField]
    private SkillInfo[] characterSkills;

    private Entity currentCharacter;

    public void DisplayCharacter(Entity character)
    {
        currentCharacter = character;

        characterName.text = character.Name;
        characterStats.text = character.ToString();

        characterPortrait.sprite = character.CharDataContainer.Portrait;
        //characterPreview.sprite = ???

        SetBuffImageVisibility();

        for (int i = 0; i < characterSkills.Length; i++)
        {
            if (i < character.EquippedCombatSkills.Length)
                characterSkills[i].SetUI(character.EquippedCombatSkills[i].SkillIcon, character.EquippedCombatSkills[i].SkillDescription, this);
            else
                characterSkills[i].SetUI(null, "", this);
        }
        
        for (int i = 0; i < character.currentCombatEffects.activeCombatEffectElements.Count; i++)
        {
            characterBuffs[i].sprite = character.currentCombatEffects.activeCombatEffectElements[i].CombatEffect.EffectSprite;
            characterBuffs[i].color = opaqueColor;
        }

    }

    private void SetBuffImageVisibility()
    {
        for (int i = 0; i < characterBuffs.Length; i++)
        {
            characterBuffs[i].color = invisColor;
        }
    }

    public void DisplaySkillInfo(string description)
    {
        if(description != "")
            characterSkillInfo.text = "Skill Description:\n"+ description;
    }

    public void ClearSkillInfo()
    {
        characterSkillInfo.text = "";
    }

}