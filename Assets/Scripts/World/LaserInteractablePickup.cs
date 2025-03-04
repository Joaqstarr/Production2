using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteractablePickup : PlayerInteractable
{
    protected override void PlayerInteractedWith()
    {
        base.PlayerInteractedWith();

        Destroy(gameObject);
    }
}
