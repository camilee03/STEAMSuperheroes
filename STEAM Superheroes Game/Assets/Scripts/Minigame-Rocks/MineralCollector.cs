using System.Collections;
using UnityEngine;

public class MineralCollector : MonoBehaviour
{
    public int hexsCollided;
    public MineralType mineralType;
    [SerializeField] MineralManager manager;
    bool destroyingMineral = false;

    public enum MineralType
    {
        Ilmenite, Anorthosite, MareBasalt, Helium, Paradot
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hexsCollided++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hexsCollided--;

        if (hexsCollided <= 1 && !destroyingMineral)
        {
            destroyingMineral = true;
            manager.AddMineral(mineralType.GetHashCode());
            Destroy(gameObject);
        }
    }


    // moon was magma and it solidified into all the stuff



    // Ilmenite (FeTiO3)
    // Mare Basalt (dark side of moon) - Pyroxene, Plagioclase (anorthisite), Olivine/Peridot 

    // Anorthosite (Silicon stuff) aka plagioclase feldspar (CaAl2Si2O8)
    // Mefic? Minerals (those)
    // Paradot or olivine
    // mining Helium? From sun rays. Solar wind radiation / space weathering - nanoscale helium 
    //try to mine in the light parts of the moon cuz space weathering only affects the top 50nm of the crust


    // 3 Levels: Dark side of moon (Basalt, Pyroxene, Plagioclase, Olivine), Light side (Helium), Both (Ilmenite)
}
