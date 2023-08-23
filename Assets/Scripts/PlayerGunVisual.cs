using System;
using UnityEngine;

public class PlayerGunVisual : MonoBehaviour
{
    public event Action OnShoot;
    private void OnShootInvoke() 
    {
        OnShoot?.Invoke();
    }
}
