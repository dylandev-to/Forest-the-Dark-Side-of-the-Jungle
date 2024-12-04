using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Dead":
                UIManager.OnShowDeadScreen?.Invoke(true);
                break;
            case "Win":
                UIManager.OnShowWinScreen?.Invoke(true); ;
                break;
            default:
                break;
        }
    }

    

}