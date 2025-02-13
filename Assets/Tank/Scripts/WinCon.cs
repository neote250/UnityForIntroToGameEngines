using TMPro;
using UnityEngine;

public class WinCon : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        GameManager.Instance.SetGameWin();
    }
}
