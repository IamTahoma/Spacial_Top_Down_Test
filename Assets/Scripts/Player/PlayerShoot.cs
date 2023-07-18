using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject laser;
    [SerializeField] int shootAmmount;
    [SerializeField] List<GameObject> shootList;

    private void Start()
    {
        for (int i = 0; i < shootAmmount; i++)
        {
            GameObject laserObject = Instantiate(laser, this.transform);
            laserObject.SetActive(false);
            shootList.Add(laserObject);
        }
    }

    public GameObject Shooting()
    {
        for (int i = 0; i < shootList.Count; i++)
        {
            if (!shootList[i].activeInHierarchy)
            {
                shootList[i].SetActive(true);
                return shootList[i];
            }

        }
        return null;
    }
}
