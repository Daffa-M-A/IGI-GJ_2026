using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance { get; private set; }

    [Header("Raycast Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    [Header("Drop Settings")]
    public Transform dropSpawnPoint;

    [Header("Grab/Push Settings")]
    public Transform holdPosition;
    public float grabLerpSpeed = 10f;

    private IInteractable currentInteractable;
    private Rigidbody currentlyGrabbedObject;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (currentlyGrabbedObject == null)
        {
            HandleRaycast();
            HandleInput();
        }
        else
        {
            HandleGrabbedObject();
        }
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            currentInteractable = hit.collider.GetComponent<IInteractable>();
        }
        else
        {
            currentInteractable = null;
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            InventoryManager.Instance.DropItem(dropSpawnPoint);
            return;
        }

        if (currentInteractable == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.OnInteractE();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            currentInteractable.OnInteractLMB();
        }
    }

    public void GrabPhysicsObject(Rigidbody rb)
    {
        currentlyGrabbedObject = rb;
        currentlyGrabbedObject.useGravity = false;
        currentlyGrabbedObject.linearDamping = 10f;
    }

    void HandleGrabbedObject()
    {
        Vector3 newPos = Vector3.Lerp(currentlyGrabbedObject.position, holdPosition.position, Time.deltaTime * grabLerpSpeed);
        currentlyGrabbedObject.MovePosition(newPos);

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentlyGrabbedObject.useGravity = true;
            currentlyGrabbedObject.linearDamping = 1f;
            currentlyGrabbedObject = null;
        }
    }
}