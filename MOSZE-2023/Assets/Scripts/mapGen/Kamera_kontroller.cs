using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Ez a script felel a kameramozgásért. A kamera akkor mozog ha a játékos átmegy egy másik szobába.*/

public class Kamera_kontroller : MonoBehaviour
{
    //Ugyan úgy mint a szoba_irányítóba csinálunk egy sigeltont, hogy könnyen tudjuk kezelni a kamerát.
    public static Kamera_kontroller instance;

    //2 változónk lesz az egyik a kamera sebesség, a másik pedig a szoba aminek a koordinátáira kell állítanunk a kamerát.
    //Az aktuálszobát a Szoba classtól kapja. 
    public Room aktualSzoba;
    public float kameraSebesseg;

    //Ez a kamera instancet kelti életre és állitja be a kamera sebességet.
    void Awake()
    {
        instance = this;
        this.kameraSebesseg = 200f;
    }
    void Update()
    {
        kameraFrissites();
    }

    /* Minden frissitéskor megnézzük hogy a kamera koordinátája megegyezik-e azzal a szobának a koordinitájával
    amiben a játékos tartózkodik. Amennyiben eltérnek átállítjuk a kamerát az új szoba pozíciójára.
    Az akutalszoba == null arra kell hogy kiküszöböljünk bugokat.*/
    public void kameraFrissites()
    {
        if (aktualSzoba == null)
        {
            return;
        }

        Vector3 celPozicio = celPozKeres();
        if (celPozicio != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, celPozicio, Time.deltaTime * kameraSebesseg);
        }
        
    }

    //Ez a funkció határozza meg a szoba a célkoordinátát amire a kamerát allítani kell
    public Vector3 celPozKeres()
    {
        Vector3 celPoz = aktualSzoba.szobaKozepe();
        celPoz.z = transform.position.z;

        return celPoz;
    }
}
