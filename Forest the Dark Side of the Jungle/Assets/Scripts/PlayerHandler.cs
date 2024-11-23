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
                UIManager.Instance.ShowDeadScreen(true);
                break;
            case "Win":
                UIManager.Instance.ShowWinScreen(true);
                break;
            default:
                break;
        }
    }
}
