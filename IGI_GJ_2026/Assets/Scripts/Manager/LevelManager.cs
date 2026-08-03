using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Settings")]
    public int maxFloor = 10;
    public int currentFloor = 10;
    public float maxTimePerFloor = 60f;
    private float currentTimer;
    private bool isTimerRunning = false;
    private bool hasFailedOnce = false;

    [Header("Unique Rooms")]
    public GameObject startRoom;
    public GameObject endRoom;

    [Header("Middle Rooms Pool")]
    public GameObject[] middleRoomsPool;
    private Queue<GameObject> availableMidRooms = new Queue<GameObject>();

    [Header("Active Rooms Tracker")]
    private GameObject roomAbove;
    private GameObject roomCurrent;
    private GameObject roomBelow;

    [Header("Player Reference")]
    public Transform playerTransform;
    public CharacterController playerController;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        foreach (var room in middleRoomsPool)
        {
            room.SetActive(false);
            availableMidRooms.Enqueue(room);
        }

        endRoom.SetActive(false);

        RebuildLevel(maxFloor);
        ResetAndStartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTimer -= Time.deltaTime;

            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdateTimerDisplay(currentTimer);
            }

            if (currentTimer <= 0)
            {
                ExecuteLoseCondition();
            }
        }
    }

    public void ResetAndStartTimer()
    {
        currentTimer = maxTimePerFloor;
        isTimerRunning = true;
        Debug.Log("Timer lantai dimulai!");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Masalah selesai. Timer dihentikan sementara.");
    }

    public void ResetTimer()
    {
        currentTimer = maxTimePerFloor;
        isTimerRunning = true;
    }

    void ExecuteLoseCondition()
    {
        isTimerRunning = false;

        if (UIManager.Instance != null) UIManager.Instance.ShowLoseScreen();

        if (!hasFailedOnce)
        {
            hasFailedOnce = true;
            currentFloor++;
            Debug.Log("LOSE CONDITION 1: Waktu Habis! Kembali ke Lantai: " + currentFloor);
        }
        else
        {
            hasFailedOnce = false;
            currentFloor = maxFloor;
            Debug.Log("LOSE CONDITION 2: Gagal Kedua! Reset ke Lantai Atas (Lantai 10).");
        }

        RebuildLevel(currentFloor);
        ResetAndStartTimer();
    }

    public void ExecuteWinCondition()
    {
        isTimerRunning = false;
        Debug.Log("WIN CONDITION TRIGGERED: Player berhasil kabur!");

        if (UIManager.Instance != null) UIManager.Instance.ShowWinScreen();

        playerController.enabled = false;
    }

    public void PlayerReachedEndTrigger(GameObject roomPassed)
    {
        hasFailedOnce = false;
        currentFloor--;
        Debug.Log("Lantai diselesaikan! Turun ke lantai: " + currentFloor);

        if (currentFloor <= 1)
        {
            Debug.Log("PLAYER MENCAPAI LANTAI DASAR!");
            isTimerRunning = false;
        }
        else
        {
            ShiftRoomsDown();
            ResetAndStartTimer();
        }

        roomPassed.GetComponentInChildren<RoomTrigger>().GetComponent<Collider>().enabled = true;
    }

    void ShiftRoomsDown()
    {
        if (roomAbove != null)
        {
            roomAbove.SetActive(false);
            if (roomAbove != startRoom && roomAbove != endRoom)
            {
                availableMidRooms.Enqueue(roomAbove);
            }
        }

        roomAbove = roomCurrent;
        roomCurrent = roomBelow;

        if (currentFloor == 2)
        {
            roomBelow = endRoom;
        }
        else
        {
            roomBelow = availableMidRooms.Dequeue();
        }

        roomBelow.SetActive(true);
        AlignBottomRoomToCurrent(roomCurrent, roomBelow);

        RoomData randomizerData = roomBelow.GetComponent<RoomData>();
        if (randomizerData.randomizer != null)
        {
            randomizerData.randomizer.GenerateObstaclesAndItems();
        }
    }

    void TimeOutPunishment()
    {
        isTimerRunning = false;

        if (!hasFailedOnce)
        {
            hasFailedOnce = true;
            currentFloor++;
            Debug.Log("Gagal! Kembali ke Lantai: " + currentFloor);
        }
        else
        {
            hasFailedOnce = false;
            currentFloor = maxFloor;
            Debug.Log("Gagal Kedua! Reset ke Lantai Atas (Lantai 10).");
        }

        RebuildLevel(currentFloor);
        ResetTimer();
    }

    void RebuildLevel(int targetFloor)
    {
        if (roomAbove != null) { roomAbove.SetActive(false); if (roomAbove != startRoom && roomAbove != endRoom) availableMidRooms.Enqueue(roomAbove); }
        if (roomCurrent != null) { roomCurrent.SetActive(false); if (roomCurrent != startRoom && roomCurrent != endRoom) availableMidRooms.Enqueue(roomCurrent); }
        if (roomBelow != null) { roomBelow.SetActive(false); if (roomBelow != startRoom && roomBelow != endRoom) availableMidRooms.Enqueue(roomBelow); }

        if (targetFloor == maxFloor) roomCurrent = startRoom;
        else roomCurrent = availableMidRooms.Dequeue();

        roomCurrent.transform.position = Vector3.zero;
        roomCurrent.SetActive(true);

        if (targetFloor - 1 == 1) roomBelow = endRoom;
        else roomBelow = availableMidRooms.Dequeue();

        roomBelow.SetActive(true);
        AlignBottomRoomToCurrent(roomCurrent, roomBelow);

        if (targetFloor < maxFloor)
        {
            if (targetFloor + 1 == maxFloor) roomAbove = startRoom;
            else roomAbove = availableMidRooms.Dequeue();

            roomAbove.SetActive(true);
            AlignTopRoomToCurrent(roomCurrent, roomAbove);
        }
        else
        {
            roomAbove = null;
        }

        playerController.enabled = false;
        playerTransform.position = roomCurrent.GetComponent<RoomData>().startPoint.position;
        playerTransform.rotation = roomCurrent.GetComponent<RoomData>().startPoint.rotation;
        playerController.enabled = true;
    }

    void AlignBottomRoomToCurrent(GameObject current, GameObject bottom)
    {
        Transform currentEnd = current.GetComponent<RoomData>().endPoint;
        Transform bottomStart = bottom.GetComponent<RoomData>().startPoint;

        bottom.transform.rotation = currentEnd.rotation;

        Vector3 posOffset = bottom.transform.position - bottomStart.position;

        bottom.transform.position = currentEnd.position + posOffset;
    }

    void AlignTopRoomToCurrent(GameObject current, GameObject top)
    {
        Transform currentStart = current.GetComponent<RoomData>().startPoint;
        Transform topEnd = top.GetComponent<RoomData>().endPoint;

        top.transform.rotation = currentStart.rotation;

        Vector3 posOffset = top.transform.position - topEnd.position;

        top.transform.position = currentStart.position + posOffset;
    }
}