using UnityEngine;

[CreateAssetMenu(fileName = "New Info", menuName = "RPG/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Settings")]
    public string itemName;

    [Header("Infos")]
    [TextArea(3, 10)]
    public string[] itemInfo;
}
