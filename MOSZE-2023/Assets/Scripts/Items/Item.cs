using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Az Item class felel a fejlesztések végbemeneteléért, a fejlesztsek ebből vannak származnak.
public abstract class Item: MonoBehaviour
{
    //Egy funkciója van ami az összes ebből származtatott gyerek classban felül van írva.
    public abstract void Upgrade();
}
