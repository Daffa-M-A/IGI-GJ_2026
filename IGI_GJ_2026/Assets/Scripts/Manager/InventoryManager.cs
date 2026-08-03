using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory Data (2 Slots)")]
    public int totalCoins = 0;

    public string[] itemNames = new string[2] { "", "" };
    public GameObject[] itemObjects = new GameObject[2] { null, null };
    public int activeSlot = 0; 

    [Header("FPS Viewmodel Settings")]
    public Transform handTransform;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwitchSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwitchSlot(1);
        }
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        Debug.Log("[INVENTORY] Koin bertambah! Total: " + totalCoins);
    }

    public void EquipItem(string itemName, GameObject actualObject)
    {
        int targetSlot = -1;

        if (string.IsNullOrEmpty(itemNames[0])) targetSlot = 0;
        else if (string.IsNullOrEmpty(itemNames[1])) targetSlot = 1;
        else
        {
            targetSlot = activeSlot;
            if (itemObjects[targetSlot] != null)
            {
                Destroy(itemObjects[targetSlot]);
            }
            Debug.Log($"[INVENTORY] Slot penuh! Menimpa item di Slot {targetSlot + 1}");
        }

        itemNames[targetSlot] = itemName;
        itemObjects[targetSlot] = actualObject;

        if (targetSlot == activeSlot)
        {
            ActivateVisual(actualObject);
        }
        else
        {
            DeactivateVisual(actualObject);
        }

        Debug.Log($"[INVENTORY] {itemName} disimpan di Slot {targetSlot + 1}");
    }

    void SwitchSlot(int newSlot)
    {
        if (activeSlot == newSlot) return;

        if (itemObjects[activeSlot] != null)
        {
            DeactivateVisual(itemObjects[activeSlot]);
        }

        activeSlot = newSlot;
        Debug.Log($"[INVENTORY] Beralih ke Slot {activeSlot + 1}");

        if (itemObjects[activeSlot] != null)
        {
            ActivateVisual(itemObjects[activeSlot]);
        }
    }

    void ActivateVisual(GameObject obj)
    {
        obj.SetActive(true);
        obj.transform.SetParent(handTransform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = true;
        if (obj.TryGetComponent<Collider>(out Collider col)) col.enabled = false;
        obj.layer = 0;
    }

    void DeactivateVisual(GameObject obj)
    {
        obj.SetActive(false);
    }

    public string currentItem => itemNames[activeSlot];

    public void RemoveItem()
    {
        if (itemObjects[activeSlot] != null)
        {
            Destroy(itemObjects[activeSlot]);
        }
        itemNames[activeSlot] = "";
        itemObjects[activeSlot] = null;
        Debug.Log($"[INVENTORY] Slot {activeSlot + 1} dikosongkan.");
    }

    public void DropItem(Transform dropPosition)
    {
        if (dropPosition == null)
        {
            Debug.LogError("[DROP GAGAL] Titik Drop belum diisi di PlayerInteract!");
            return;
        }

        GameObject objToDrop = itemObjects[activeSlot];
        if (objToDrop == null)
        {
            Debug.LogWarning($"[DROP GAGAL] Slot {activeSlot + 1} kosong.");
            return;
        }

        objToDrop.SetActive(true);
        objToDrop.transform.SetParent(null);
        objToDrop.transform.position = dropPosition.position;
        objToDrop.transform.rotation = dropPosition.rotation;

        if (objToDrop.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.isKinematic = false;
        if (objToDrop.TryGetComponent<Collider>(out Collider col)) col.enabled = true;
        objToDrop.layer = LayerMask.NameToLayer("Interactable");

        Debug.Log($"[INVENTORY] Membuang {itemNames[activeSlot]} dari Slot {activeSlot + 1}");

        itemNames[activeSlot] = "";
        itemObjects[activeSlot] = null;
    }
}