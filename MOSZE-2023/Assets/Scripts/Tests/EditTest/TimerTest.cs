using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Tests
{
    [Test]
    public void TestTimer()
    {
        // Beállítjuk 600ra a timeot ami 10perccel egyenlő
        Timer timer = new Timer();
        timer.timer = 600f;

        //Meghívjuk a függvényeket
        var min = timer.GetMinutes();
        var sec = timer.GetSeconds();

        //Vizsgáljuk jól működnek-e
        Assert.AreEqual(10f, min);
        Assert.AreEqual(600f, sec);

        //Ha a StartTimer működik akkor vissza kell állítania 0-ra a timert, ezt vizsgáljuk
        timer.StartTimer();
        Assert.AreEqual(0, timer.timer);
    }
    [Test]
    public void TestGame()
    {
        Game game = new Game();
        game.enemies = new List<GameObject>();

        // Az enemies listához elemeket adunk
        GameObject enemy1 = new GameObject();
        game.enemies.Add(enemy1);
        GameObject enemy2 = new GameObject();
        game.enemies.Add(enemy2);
        GameObject enemy3 = new GameObject();
        game.enemies.Add(enemy3);

        //Meghívjuk a függvéynt ami egyet kiálaszt a listából
        var asd = game.GetEnemy();

        // Ellenőrizzük hogy a függvény által visszaadott érték egyenlő e a lista valamely elemével
        if (asd == game.enemies[0])
        {
            Assert.AreEqual(game.enemies[0], asd);
        }
        else if (asd == game.enemies[1])
        {
            Assert.AreEqual(game.enemies[1], asd);
        }
        else if (asd == game.enemies[2])
        {
            Assert.AreEqual(game.enemies[2], asd);
        }
        else
        {
            Assert.AreEqual(game.enemies[0], asd);
        } 
    }
    [Test]
    public void TestPlayer()
    {
        //player healtet 4-re állítjuk, a maxHp alapból 5
        Player player = new Player();
        player.health = 4;
        player.attackSpeedBuff = 4;

        //Meghívjuk a heal függvényt, és megnézzük növekedett e a health
        player.Heal();
        Assert.AreEqual(5, player.health);

        //Itt megpróbáljuk megegyszer növelni aminek nem szabad sikerrel járnia, hisz a health nem lehet nagyobb mint a maxHp
        player.Heal();
        Assert.AreEqual(5, player.health);

        //Növeljük egyel a maxHp-t
        player.SetHp(1);
        Assert.AreEqual(6, player.maxHp);

        //Megnézzük hogy a getAtatckSpeedBuff helyes értéket ad-e
        Assert.AreEqual(0.8f, player.getAttackSpeedBuff());

        //Damage függvény tesztelése, szükség vagy egy RigidBody változóra
        GameObject rb = new GameObject();
        player.health = 5;
        player.Damage(2,rb);
        Assert.AreEqual(3, player.health);
    }
}
