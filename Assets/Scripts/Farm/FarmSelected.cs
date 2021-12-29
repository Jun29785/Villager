using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmSelected : MonoBehaviour
{
    public FarmSelect Villager;
    public FarmSelect Crop;

    private void Start()
    {
        Villager = null;
        Crop = null;
    }

    private void LateUpdate()
    {
        Child();
    }

    void Child()
    {
        Villager = null;
        Crop = null;
        if (this.transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var k = transform.GetChild(i).GetComponent<FarmSelect>().Key;
                if (k > 10000 && k < 20000 && Villager == null)
                {
                    Villager = transform.GetChild(i).GetComponent<FarmSelect>();
                }
                else if (k > 30000 && k < 40000 && Crop == null)
                {
                    Crop = transform.GetChild(i).GetComponent<FarmSelect>();
                }
            }
        }
    }

    public void ResetChild()
    {
        for (int i = 0; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
