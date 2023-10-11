using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
public class AbilityManager : MonoBehaviour
{
    public GameObject ability_grey1;
    public GameObject ability_nongrey1;
    public TextMeshProUGUI abilityCooldownText1;
    public TextMeshProUGUI abilityDurationText1;
    public GameObject ability1;
    public Ability ability1Stats;

    public GameObject ability_grey2;
    public GameObject ability_nongrey2;
    public TextMeshProUGUI abilityCooldownText2;
    public TextMeshProUGUI abilityDurationText2;
    public GameObject ability2;
    public Ability ability2Stats;

    public GameObject ability_grey3;
    public GameObject ability_nongrey3;
    public TextMeshProUGUI abilityCooldownText3;
    public TextMeshProUGUI abilityDurationText3;
    public GameObject ability3;
    public Ability ability3Stats;
    
    public GameObject EquipedAbilitesSlot1;
    public GameObject EquipedAbilitesSlot2;
    public GameObject EquipedAbilitesSlot3;

    public GameObject shieldAbilityObject;
    public GameObject ArcticFreezeAbilityObject;
    public GameObject healAbilityObject;
    public GameObject teleportAbilityObject;
    public GameObject speedAbilityObject;
    public GameObject defenseAbilityObject;
    public GameObject MasochistAbilityObject;

    public GameObject lockedAbilities;
    public GameObject inventory;
    public int capacity;
    public int maxCapacity;

    public void Awake()
    {
    }

    public void setAbility1(GameObject abilityObject)
    {
        //instantiate shieldAbility object at icon1 position. 
        //If icon1 input is pressed down, call the shield ability from shield ability object\
        if (ability1 !=null)
        {
            Destroy(ability1);
        }
        
        ability1 = Instantiate(abilityObject, ability_grey1.transform.position, Quaternion.identity, transform);
        ability1Stats = ability1.GetComponent<Ability>();
        ability_grey1.GetComponent<Image>().sprite = ability1Stats.abilitySprite;
        ability_nongrey1.GetComponent<Image>().sprite = ability1Stats.abilitySprite;
        ability1Stats.abilityCooldownText = abilityCooldownText1;
        ability1Stats.abilityDurationText = abilityDurationText1;
        ability1Stats.abilityEquiped = true;
        print(ability1.GetComponent<Ability>().abilityName);
    }

    public void setAbility2(GameObject abilityObject)
    {   
        if (ability2 !=null)
        {
            Destroy(ability2);
        }
        ability2 = Instantiate(abilityObject, ability_grey2.transform.position, Quaternion.identity, transform);
        ability2Stats = ability2.GetComponent<Ability>();
        ability_grey2.GetComponent<Image>().sprite = ability2Stats.abilitySprite;
        ability_nongrey2.GetComponent<Image>().sprite = ability2Stats.abilitySprite;
        ability2Stats.abilityCooldownText = abilityCooldownText2;
        ability2Stats.abilityDurationText = abilityDurationText2;
        ability2Stats.abilityEquiped = true;
    }

    public void setAbility3(GameObject abilityObject)
    {   
        if (ability3 !=null)
        {
            Destroy(ability3);
        }
        ability3 = Instantiate(abilityObject, ability_grey3.transform.position, Quaternion.identity, transform);
        ability3Stats = ability3.GetComponent<Ability>();
        ability_grey3.GetComponent<Image>().sprite = ability3Stats.abilitySprite;
        ability_nongrey3.GetComponent<Image>().sprite = ability3Stats.abilitySprite;
        ability3Stats.abilityCooldownText = abilityCooldownText3;
        ability3Stats.abilityDurationText = abilityDurationText3;
        ability3Stats.abilityEquiped = true;
    }
    public void SetEquipedAbilites()
    {
        if( EquipedAbilitesSlot1.transform.childCount == 1)
        {
            setAbility1(EquipedAbilitesSlot1.transform.GetChild(0).gameObject);
        }
        if( EquipedAbilitesSlot2.transform.childCount == 1)
        {
            setAbility2(EquipedAbilitesSlot2.transform.GetChild(0).gameObject);
        }
        if(EquipedAbilitesSlot3.transform.childCount == 1)
        {
            setAbility3(EquipedAbilitesSlot3.transform.GetChild(0).gameObject);
        }
        
        ability_grey1.GetComponent<Image>().fillAmount=0f;
        ability_grey2.GetComponent<Image>().fillAmount=0f;
        ability_grey3.GetComponent<Image>().fillAmount=0f;
        abilityCooldownText1.text = "";
        abilityCooldownText2.text = "";
        abilityCooldownText3.text = "";
        abilityDurationText1.text = "";
        abilityDurationText2.text = "";
        abilityDurationText3.text = "";
        print("EquipedAbilitiesSet");
    }
    public void UseAbility1()   
    {
        if(ability1 !=null)
        {
            UseAbilityScriptsfunction(ability1, abilityCooldownText1, ability_grey1.GetComponent<Image>(), abilityDurationText1);
        }
        else
        {
            print("ability1 is null");
        }
    }

    public void UseAbility2()
    {
        if(ability2 !=null)
        {
            UseAbilityScriptsfunction(ability2, abilityCooldownText2, ability_grey2.GetComponent<Image>(), abilityDurationText2);
        }
        else
        {
            print("ability2 is null");
        }
        
    }

    public void UseAbility3()
    {
        if(ability3 !=null)
        {
            UseAbilityScriptsfunction(ability3, abilityCooldownText3, ability_grey3.GetComponent<Image>(), abilityDurationText3);
        }
        else
        {
            print("ability3 is null");
        }
    }

    public void UseAbilityScriptsfunction(GameObject AbilityObject, TextMeshProUGUI abilityCooldownText, Image abilityImageIcon, TextMeshProUGUI abilityDurationText)
    {
        
        if (AbilityObject.GetComponent<Ability>().abilityName == "ShieldAbility")
        {
            ShieldAbility shieldAbility = AbilityObject.GetComponent<ShieldAbility>();
            shieldAbility.UseAbility(abilityCooldownText, abilityImageIcon, abilityDurationText);
        } 
        else if(AbilityObject.GetComponent<Ability>().abilityName == "ArcticFreezeAbility")
        {
            ArcticFreezeAbility freezeAbility = AbilityObject.GetComponent<ArcticFreezeAbility>();
            freezeAbility.UseAbility(abilityCooldownText, abilityImageIcon);
        }
        else if(AbilityObject.GetComponent<Ability>().abilityName == "HealAbility")
        {
            HealAbility healAbility = AbilityObject.GetComponent<HealAbility>();
            healAbility.UseAbility(abilityCooldownText, abilityImageIcon);
        }
        else if(AbilityObject.GetComponent<Ability>().abilityName == "TeleportAbility")
        {
            TeleportAbility teleportAbility = AbilityObject.GetComponent<TeleportAbility>();
            teleportAbility.UseAbility(abilityCooldownText, abilityImageIcon);
        }
        else if(AbilityObject.GetComponent<Ability>().abilityName == "BolsterAbility")
        {
            DefenseAbility defenseAbility = AbilityObject.GetComponent<DefenseAbility>();
            defenseAbility.UseAbility(abilityCooldownText, abilityImageIcon, abilityDurationText);
        }
        else if(AbilityObject.GetComponent<Ability>().abilityName == "SpeedAbility")
        {
            SpeedAbility speedAbility = AbilityObject.GetComponent<SpeedAbility>();
            speedAbility.UseAbility(abilityCooldownText, abilityImageIcon, abilityDurationText);
        }
        else if(AbilityObject.GetComponent<Ability>().abilityName == "MasochistAbility")
        {
            MasochistAbility masochistAbility = AbilityObject.GetComponent<MasochistAbility>();
            masochistAbility.UseAbility(abilityCooldownText, abilityImageIcon, abilityDurationText);
        }
    }
    
    public void AddItem(GameObject item)
    {
        if(capacity != maxCapacity)
        {
            foreach (Transform child in inventory.transform)
            {
                if(child.childCount==0)
                {
                    item.transform.SetParent(child.transform);
                    print(item.transform.position);
                    item.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    capacity +=1;
                    Debug.Log("Item added to Inventory");
                    return;
                }
            }
        }
        else
        {
            Debug.Log("Inventory is full.");
        }
    }
}
