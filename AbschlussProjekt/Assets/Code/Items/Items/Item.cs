﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{

    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private ItemContainer container;
    [Range(2, 4)][SerializeField] private float itemSize = 3;
    private int itemID;

    [SerializeField] private int currentlyStacked;
    public int CurrentlyStacked {  get { return currentlyStacked; } set { currentlyStacked = (value > Container.StackingLimit) || (value < 1) ? currentlyStacked : value; } }
    public ItemContainer Container { get { return container; } set { container = value; Setup(); } }

    public void Start()
    {
        inventoryManager = AssetManager.Instance.GetManager<InventoryManager>();
        Setup();
        SetComponentValues();
    }

    public  void Setup()
    {
        SetSprite();
        SetID();
        SetCorrectSize();
        SetEditorName();
    }

    private void SetSprite()
    {
        if(Container !=null)
            GetComponent<SpriteRenderer>().sprite = Container.ItemIcon;
    }

    // Has no "real" purpose right now. But maybe we'll need it later :)
    private void SetID()
    {
        itemID = GetInstanceID();
    }

    private void SetCorrectSize()
    {
        transform.localScale = Vector3.one * itemSize;
    }

    private void SetEditorName()
    {
        if (Container != null)
            name = Container.name + " ID[" + itemID + "]";
    }

    private void SetComponentValues()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inventoryManager.PickUpItem(CurrentlyStacked, Container);
        //GameObject.Destroy(gameObject);
    }
}