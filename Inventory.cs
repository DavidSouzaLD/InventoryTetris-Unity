using System;
using UnityEngine;

public static class InventorySettings
{
    /// <summary>
    /// Size that each slot has.
    /// </summary>
    public static readonly Vector2Int tileSize = new(96, 96);

    /// <summary>
    /// Tile scale for external changes. Do not touch.
    /// </summary>
    public static readonly float tileScale = 1f;

    /// <summary>
    /// Speed ​​at which the item will return to its target.
    /// </summary>
    public static readonly float rotationAnimationSpeed = 30f;
}

public partial class Inventory : MonoBehaviour
{
    /// <summary>
    /// List of data for each item in the game.
    /// </summary>
    [Header("Settings")]
    public ItemData[] itemsData;

    /// <summary>
    /// Prefab used to instantiate new items.
    /// </summary>
    public Item itemPrefab;

    /// <summary>
    /// Returns the InventoryGrid the mouse is currently on.
    /// </summary>
    public InventoryGrid gridOnMouse { get; set; }

    /// <summary>
    /// Dynamic list that has all inventory grids automatically when starting the game.
    /// </summary>
    public InventoryGrid[] grids { get; private set; }

    /// <summary>
    /// Currently selected item.
    /// </summary>
    public Item selectedItem { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        grids = GameObject.FindObjectsOfType<InventoryGrid>();
    }

    /// <summary>
    /// Selects the item and turns everything necessary to select it and make room for another item on and off
    /// </summary>
    /// <param name="item">Item to be selected.</param>
    public void SelectItem(Item item)
    {
        ClearItemReferences(item);
        selectedItem = item;
        selectedItem.rectTransform.SetParent(transform);
        selectedItem.rectTransform.SetAsLastSibling();
    }

    /// <summary>
    /// Deselects the current item.
    /// </summary>
    private void DeselectItem()
    {
        selectedItem = null;
    }

    /// <summary>
    /// Add an item to the inventory dynamically by looking for where the item might fit.
    /// </summary>
    /// <param name="itemData">Data of the item that will be added to the inventory.</param>
    public void AddItem(ItemData itemData)
    {
        for (int g = 0; g < grids.Length; g++)
        {
            for (int y = 0; y < grids[g].gridSize.y; y++)
            {
                for (int x = 0; x < grids[g].gridSize.x; x++)
                {
                    Vector2Int tilePosition = new Vector2Int(x, y);

                    for (int r = 0; r < 2; r++)
                    {
                        if (r == 0)
                        {
                            if (!ExistsItem(tilePosition, grids[g], itemData.size.width, itemData.size.height))
                            {
                                Item newItem = Instantiate(itemPrefab);
                                newItem.rectTransform = newItem.GetComponent<RectTransform>();
                                newItem.rectTransform.SetParent(grids[g].rectTransform);
                                newItem.rectTransform.sizeDelta = new Vector2(
                                    itemData.size.width * InventorySettings.tileSize.x,
                                    itemData.size.height * InventorySettings.tileSize.y
                                );

                                newItem.indexPosition = tilePosition;
                                newItem.inventory = this;

                                for (int xx = 0; xx < itemData.size.width; xx++)
                                {
                                    for (int yy = 0; yy < itemData.size.height; yy++)
                                    {
                                        int tileX = tilePosition.x + xx;
                                        int tileY = tilePosition.y + yy;

                                        grids[g].items[tileX, tileY] = newItem;
                                        grids[g].items[tileX, tileY].data = itemData;
                                    }
                                }

                                newItem.rectTransform.localPosition = IndexToInventoryPosition(newItem);
                                newItem.inventoryGrid = grids[g];
                                return;
                            }
                        }

                        if (r == 1)
                        {
                            if (!ExistsItem(tilePosition, grids[g], itemData.size.height, itemData.size.width))
                            {
                                Item newItem = Instantiate(itemPrefab);
                                newItem.Rotate();

                                newItem.rectTransform = newItem.GetComponent<RectTransform>();
                                newItem.rectTransform.SetParent(grids[g].rectTransform);
                                newItem.rectTransform.sizeDelta = new Vector2(
                                    itemData.size.width * InventorySettings.tileSize.x,
                                    itemData.size.height * InventorySettings.tileSize.y
                                );

                                newItem.indexPosition = tilePosition;
                                newItem.inventory = this;

                                for (int xx = 0; xx < itemData.size.height; xx++)
                                {
                                    for (int yy = 0; yy < itemData.size.width; yy++)
                                    {
                                        int tileX = tilePosition.x + xx;
                                        int tileY = tilePosition.y + yy;

                                        grids[g].items[tileX, tileY] = newItem;
                                        grids[g].items[tileX, tileY].data = itemData;
                                    }
                                }

                                newItem.rectTransform.localPosition = IndexToInventoryPosition(newItem);
                                newItem.inventoryGrid = grids[g];

                                return;
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("(Inventory) Not enough slots found to add the item!");
    }

    /// <summary>
    /// Remove item from inventory completely.
    /// </summary>
    /// <param name="item">Item to be removed.</param>
    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            ClearItemReferences(item);
            Destroy(item.gameObject);
        }
    }

    /// <summary>
    /// Moves an item to a new position, first checking if the item is outside the grid or if there is an item in the desired slot.
    /// </summary>
    /// <param name="tilePosition">Position that will be checked as the new position of the item in the inventory.</param>
    /// <param name="item">Item that will be moved to the new position.</param>
    /// <param name="deselectItemInEnd">Boolean that indicates whether the object will be deselected when it finishes moving.</param>
    public void MoveItem(Item item, bool deselectItemInEnd = true)
    {
        Vector2Int tilePosition = GetTileAtMouseCoords();

        if (ReachedBoundary(tilePosition, gridOnMouse, item.correctedSize.width, item.correctedSize.height))
        {
            return;
        }

        if (ExistsItem(tilePosition, gridOnMouse, item.correctedSize.width, item.correctedSize.height))
        {
            return;
        }

        item.indexPosition = tilePosition;
        item.rectTransform.SetParent(gridOnMouse.rectTransform);

        for (int x = 0; x < item.correctedSize.width; x++)
        {
            for (int y = 0; y < item.correctedSize.height; y++)
            {
                int tileX = item.indexPosition.x + x;
                int tileY = item.indexPosition.y + y;

                gridOnMouse.items[tileX, tileY] = item;
            }
        }

        item.rectTransform.localPosition = IndexToInventoryPosition(item);
        item.inventoryGrid = gridOnMouse;

        if (deselectItemInEnd)
        {
            DeselectItem();
        }
    }

    /// <summary>
    /// Swaps the selected item with the item overlaid by the mouse.
    /// </summary>
    /// <param name="overlapItem">Overlapping item.</param>
    public void SwapItem(Item overlapItem)
    {
        if (!ReachedBoundary(overlapItem.indexPosition, gridOnMouse, selectedItem.correctedSize.width, selectedItem.correctedSize.height))
        {
            SelectItem(overlapItem);
            MoveItem(selectedItem, false);
        }
    }

    /// <summary>
    /// Clears the item position references in the inventory.
    /// </summary>
    /// <param name="item">Item to have references removed.</param>
    public void ClearItemReferences(Item item)
    {
        for (int x = 0; x < item.correctedSize.width; x++)
        {
            for (int y = 0; y < item.correctedSize.height; y++)
            {
                int tileX = item.indexPosition.x + x;
                int tileY = item.indexPosition.y + y;

                item.inventoryGrid.items[tileX, tileY] = null;
            }
        }
    }

    /// <summary>
    /// Checks if there is an item in the indicated position.
    /// </summary>
    /// <param name="tilePosition">Position to be checked.</param>
    /// <param name="width">Item width</param>
    /// <param name="height">Item height</param>
    /// <param name="grid">Grid in which the verification should occur.</param>
    /// <returns></returns>
    public bool ExistsItem(Vector2Int tilePosition, InventoryGrid grid, int width = 1, int height = 1)
    {
        if (ReachedBoundary(tilePosition, grid, width, height))
        {
            return true;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int tileX = tilePosition.x + x;
                int tileY = tilePosition.y + y;

                if (grid.items[tileX, tileY] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Checks whether the indicated position is outside the inventory limit.
    /// </summary>
    /// <param name="tilePosition">Position to be checked.</param>
    /// <param name="width">Item width.</param>
    /// <param name="height">Item height.</param>
    /// <param name="gridReference">Grid in which the verification should occur.</param>
    /// <returns></returns>
    public bool ReachedBoundary(Vector2Int tilePosition, InventoryGrid gridReference, int width = 1, int height = 1)
    {
        if (tilePosition.x + width > gridReference.gridSize.x || tilePosition.x < 0)
        {
            return true;
        }

        if (tilePosition.y + height > gridReference.gridSize.y || tilePosition.y < 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the value of an array sequence at the inventory position.
    /// </summary>
    /// <param name="item">Item to be checked.</param>
    /// <returns></returns>
    public Vector3 IndexToInventoryPosition(Item item)
    {
        Vector3 inventorizedPosition =
            new()
            {
                x = item.indexPosition.x * InventorySettings.tileSize.x
                    + InventorySettings.tileSize.x * item.correctedSize.width / 2,
                y = -(item.indexPosition.y * InventorySettings.tileSize.y
                    + InventorySettings.tileSize.y * item.correctedSize.height / 2
                )
            };

        return inventorizedPosition;
    }

    /// <summary>
    /// Returns the screen of the matrix the mouse is on top of.
    /// </summary>
    /// <returns></returns>
    public Vector2Int GetTileAtMouseCoords()
    {
        if (gridOnMouse == null)
        {
            return Vector2Int.zero;
        }

        Vector2 gridPosition =
            new(
                Input.mousePosition.x - gridOnMouse.rectTransform.position.x,
                gridOnMouse.rectTransform.position.y - Input.mousePosition.y
            );

        Vector2Int tilePosition =
            new(
                (int)(gridPosition.x / (InventorySettings.tileSize.x * InventorySettings.tileScale)),
                (int)(gridPosition.y / (InventorySettings.tileSize.y * InventorySettings.tileScale))
            );

        return tilePosition;
    }


    /// <summary>
    /// Returns an item based on the mouse position.
    /// </summary>
    /// <returns></returns>
    public Item GetItemAtMouseCoords()
    {
        Vector2Int tilePosition = GetTileAtMouseCoords();

        if (!ReachedBoundary(tilePosition, gridOnMouse))
        {
            return GetItemFromTilePosition(tilePosition);
        }

        return null;
    }

    /// <summary>
    /// Returns an item based on the tile position.
    /// </summary>
    /// <param name="tilePosition">Tile position to check.</param>
    /// <returns></returns>
    public Item GetItemFromTilePosition(Vector2Int tilePosition)
    {
        return gridOnMouse.items[tilePosition.x, tilePosition.y];
    }
}