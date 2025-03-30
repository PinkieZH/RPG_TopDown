using UnityEngine;

public class Items : MonoBehaviour, IInteractable
{
    [Header("Items Data")]
    public ItemData itemData;

    [Header("Custom Settings (Optional)")]
    [Tooltip("Laissez vide pour utiliser le nom d�fini dans le ItemData")]
    public string customName;
    public void Read()
    {
        if (itemData == null)
        {
            Debug.LogError("ItemData not assigned to NPC: " + gameObject.name);
            return;
        }

        // v�rif si ObjectsManager existe et d�marrer le texte
        if (ObjectsManager.Instance != null)
        {
            //Utiliser le nom personnalis� s'il est d�fini sinon celui du ItemData
            string objectName = string.IsNullOrEmpty(customName) ? itemData.itemName : customName;
            ObjectsManager.Instance.StartText(objectName, itemData.itemInfo);
        }
        else
        {
            Debug.LogError("ObjectsManager not found in scene !");
        }
    }
    public void GetInteractionPrompt()
    {

    }
    
    public void Interact()
    {

    }
}
