using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test
{
    [Test]
    public void TestTimer()
    {
        GameObject timerObject = new GameObject();
        Timer timer = timerObject.AddComponent<Timer>();
        timer.timer = 600f;

        var min = timer.GetMinutes();
        var sec = timer.GetSeconds();

        Assert.AreEqual(10f, min);
        Assert.AreEqual(600f, sec);

        timer.StartTimer();
        Assert.AreEqual(0, timer.timer);

        GameObject.Destroy(timerObject);

    }
}
