using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

//A Játékost irányitó class, a character classból származik
public class Player : Character
{   
    /*A playerhez singletont használunk hogy minden class könnyn tudjon rá hivatkozni.
    MaxHP változó a maximum elérhtő életet tárolja ami alapból 5.
    A currentweapon a játékosnál elhelyezett fegyver.
    weaponSprite az előbb említett fegyver spriteja.
    Minden caracter rendelkezik firepointtal, ez annak a spriteja.*/
    public static Player Instance { get; set; }
    public int maxHp = 5;
    public GameObject currentWeapon = null;
    protected SpriteRenderer weaponSprite;
    protected SpriteRenderer firepointSprite;
    
    //Játékos megjelenésekor elmentjük a firepontSpriteot, és beállitjuk az aktuális életet a max életre.
    private void Awake() {
        Instance = this; 
        firepointSprite = firepoint.GetChild(0).GetComponent<SpriteRenderer>();
        health = maxHp;
    }

    /*Ez az update felel az inputok bekéréséért.
    Folyton nézzük van e egétmozgás, kattintás vagy billentyű parancs, és ezek alapján irányitjuk a karakter.*/
    private void FixedUpdate() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Aim(mousePos);
        if (Instance.gun != null)
        {
            firepointSprite.sprite = weaponSprite.sprite;
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y).normalized;
        MoveCharacter(movement);

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    /*Ha érintkezünk egy fegyverrel, ezen funkció által képesek vagyunk azt felvenni.
    Ha van már nálunk akkor fegyver, akkor azt leteszi a földre.*/
    private void OnTriggerStay2D(Collider2D other) {           
        if (other.gameObject.CompareTag("Weapons") && pickup == true) 
        {   
            if (Input.GetButton("Fire2"))
            {
                if (currentWeapon != null)
                {
                    GameObject newWeapon = Instantiate(currentWeapon, firepoint.position + firepoint.up, Quaternion.identity);
                    newWeapon.SetActive(true);
                    Destroy(currentWeapon);                   
                }
                currentWeapon = other.gameObject;
                weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
                other.gameObject.SetActive(false);
                Weapon w = (Weapon) other.GetComponent(typeof(Weapon));
                Instance.gun = w.GetGun();
                weaponSprite.sprite = currentWeapon.GetComponent<SpriteRenderer>().sprite;
                pickup = false;
                Invoke(nameof(PickupCooldown), 2f);
            }
        }
    }

    //Játékos gyógyítása
    public void Heal() 
    {
        if (health < maxHp)
        {
            health++;
        }
    }

    //Maximum élet növelése, 10-nél több nem lehet.
    public void SetHp(int i)
    {   
        if (maxHp <= 10)
        {
            maxHp += i;
        }
    }

    //Upgrade tárgyak felvétele automatikusan történik, a BuffManageren keresztül a UI is frissül.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Upgrade")) 
        {        
            ((Item)other.GetComponent(typeof(Item))).Upgrade();
            BuffManager.Instance.setBuffs();
        }
    }

    //A játlkos halálát kezeli, amennyiben lefut a játék véget ér.
    public override void killCharacter(GameObject chara)
    {
        Destroy(chara);
        Time.timeScale = 0f;
        Game.Instance.lose.SetActive(true);
    }

    //MaxHp getter.
    public int getMaxHp() {
        return maxHp;
    }

    //Aktuális élet getter.
    public int getHealth(){
        return health;
    }
}

