using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{   
    public static Player Instance { get; set; }
    [SerializeField] 
    protected int maxHp = 5;
    public Camera cam;
    public GameObject currentWeapon;
    protected SpriteRenderer weaponSprite;
    protected SpriteRenderer firepointSprite;

    
    private void Awake() {
        Instance = this;
        firepointSprite = firepoint.GetComponent<SpriteRenderer>();
        weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        health = maxHp;
    }

    private void FixedUpdate() {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); 
        Aim(mousePos);
        firepointSprite.sprite = weaponSprite.sprite;
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y).normalized;
        MoveCharacter(movement);

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }
    private void OnTriggerStay2D(Collider2D other) {           
        if (other.gameObject.CompareTag("Weapons") && pickup == true) 
        {   
            if (Input.GetButton("Fire2"))
            {
                GameObject newWeapon = Instantiate(currentWeapon, firepoint.position + firepoint.up, new Quaternion(0,0,0,0));
                newWeapon.SetActive(true);
                Destroy(currentWeapon);
                currentWeapon = other.gameObject;
                other.gameObject.SetActive(false);
                Weapon w = (Weapon) other.GetComponent(typeof(Weapon));
                Instance.gun = w.GetGun();
                weaponSprite.sprite = currentWeapon.GetComponent<SpriteRenderer>().sprite;
                pickup = false;
                Invoke(nameof(PickupCooldown), 2f);
            }
        }
    }
    public void Heal() 
    {
        if (health < maxHp)
        {
            health++;
        }
    }
    public void SetHp(int i)
    {
        maxHp += i;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Upgrade")) 
        {        
            ((Item)other.GetComponent(typeof(Item))).Upgrade();
        }
    }
}

