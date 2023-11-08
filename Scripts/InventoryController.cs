using System;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class InventoryController : MonoBehaviour
{
    public Inventory inventory { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Check if mouse is inside a any grid.
            if (!inventory.ReachedBoundary(inventory.GetSlotAtMouseCoords(), inventory.gridOnMouse))
            {
                if (inventory.selectedItem)
                {
                    Item oldSelectedItem = inventory.selectedItem;
                    Item overlapItem = inventory.GetItemAtMouseCoords();

                    if (overlapItem != null)
                    {
                        inventory.SwapItem(overlapItem, oldSelectedItem);
                    }
                    else
                    {
                        inventory.MoveItem(oldSelectedItem);
                    }
                }
                else
                {
                    SelectItemWithMouse();
                }
            }
        }

        // Remove an item from the inventory
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RemoveItemWithMouse();
        }

        // Generates a random item in the inventory
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.AddItem(inventory.itemsData[UnityEngine.Random.Range(0, inventory.itemsData.Length)]);
        }

        if (inventory.selectedItem != null)
        {
            MoveSelectedItemToMouse();

            if (Input.GetKeyDown(KeyCode.R))
            {
                inventory.selectedItem.Rotate();
            }
        }
    }

    /// <summary>
    /// Select a new item in the inventory.
    /// </summary>
    private void SelectItemWithMouse()
    {
        Item item = inventory.GetItemAtMouseCoords();

        if (item != null)
        {
            inventory.SelectItem(item);
        }
    }

    /// <summary>
    /// Removes the item from the inventory that the mouse is hovering over.
    /// </summary>
    private void RemoveItemWithMouse()
    {
        Item item = inventory.GetItemAtMouseCoords();

        if (item != null)
        {
            inventory.RemoveItem(item);
        }
    }

    /// <summary>
    /// Moves the currently selected object to the mouse.
    /// </summary>
    private void MoveSelectedItemToMouse()
    {
        inventory.selectedItem.rectTransform.position = new Vector3(
                Input.mousePosition.x
                    + ((inventory.selectedItem.correctedSize.width * InventorySettings.slotSize.x) / 2)
                    - InventorySettings.slotSize.x / 2,
                Input.mousePosition.y
                    - ((inventory.selectedItem.correctedSize.height * InventorySettings.slotSize.y) / 2)
                    + InventorySettings.slotSize.y / 2,
                Input.mousePosition.z
            );
    }
}