﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum ItemFlags
{
    INTERACTABLE = 1,
    LOOTABLE = 2
}

[System.Flags]
public enum ActivateFlags
{
    USE = 1
}

[RequireComponent(typeof(DatabaseItem))]
public class Item : MonoBehaviour {
    public int GUID;
    public string itemName;
    public Sprite icon;

    public ItemFlags flags;
    public ActivateFlags activateFlags;

    private static int itemCounter = 0;

    public void Awake()
    {
        itemCounter++;
        GUID = itemCounter;
        flags = ItemFlags.INTERACTABLE | ItemFlags.LOOTABLE;
        activateFlags = ActivateFlags.USE;
    }

    public bool OnActivate(GameObject player)
    {
        if (!FlagHelper.IsSet(flags, ItemFlags.INTERACTABLE)) return false;

        if (FlagHelper.IsSet(activateFlags, ActivateFlags.USE)) return OnUse();

        return true;
    }

    public bool OnUse()
    {
        Debug.Log("Just used");
        return true;
    }

    public bool WhileInteractionLook(GameObject player)
    {
        if (!FlagHelper.IsSet(flags, ItemFlags.INTERACTABLE)) return false;
        return true;
    }

    public bool OnInteraction(GameObject player)
    {
        if (!FlagHelper.IsSet(flags, ItemFlags.INTERACTABLE)) return false;

        if (FlagHelper.IsSet(flags, ItemFlags.LOOTABLE))
        {
            OnLootStart(player);
            LootHandler.OnLoot(this, player);
            OnLootEnd(player);
        }

        return true;
    }

    public int getEntry()
    {
        DatabaseItem dbItem = GetComponent<DatabaseItem>();
        if (!dbItem)
        {
            Debug.Log("ERROR: DB item could not be find Item.cs::getEntry() for " + itemName);
            return -1;
        }

        return dbItem.getId();
    }

    public void OnLootStart(GameObject player) { }
    public void OnLootEnd(GameObject player) { }
}