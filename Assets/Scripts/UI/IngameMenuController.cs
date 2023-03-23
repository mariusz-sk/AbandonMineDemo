using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.UI
{
    public class IngameMenuController : MonoBehaviour
    {
        [SerializeField]
        private InventoryMenuScreen inventoryMenuScreen;

        private void Start()
        {
            if (inventoryMenuScreen != null)
            {
                inventoryMenuScreen.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I) && inventoryMenuScreen != null)
            {
                if (inventoryMenuScreen.IsVisible)
                    inventoryMenuScreen.HideScreen();
                else
                    inventoryMenuScreen.ShowScreen();
            }
        }
    }
}
