using System;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyPlayerSpawned;
    public static event EventHandler OnAnyPlayerPickedSometing;
    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
        OnAnyPlayerPickedSometing = null;
    }

    public static Player LocalInstance { get; private set; }


    public event EventHandler OnPlayerPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    public int PlayerTeam { get; set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotatSpeed = 10f;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private LayerMask collisionsLayerMask;
    [SerializeField] private Transform objectHandlePoint;
    [SerializeField] private List<Vector3> spawnPositionList;
    [SerializeField] private PlayerVisual playerVisual;

    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;
    private Vector3 lastInteractDirection;
    private bool isWalking = false;



    private void Start()
    { 
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromClientID(OwnerClientId);
        playerVisual.SetPlayerColor(KitchenGameMultiplayer.Instance.GetPlayerColor(playerData.ColorID));
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }

        if (IsClient)
        {
            GameManager.Instance.SetConnectedPlayer(this);
        }
        
        transform.position = spawnPositionList[KitchenGameMultiplayer.Instance.GetPlayerDataIndexFromClientID(OwnerClientId)];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;
        }
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == OwnerClientId && HasKitchenObject())
        {
            KitchenObject.DestroyKitchenObject(GetKitchenObject());
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            if (selectedCounter != null)
            {
                selectedCounter.InteractAlternate(this);
            }
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            if (selectedCounter != null)
            {
                selectedCounter.Interact(this);
            }
        }
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        //HandleMovementServerAuth();
        HandleMovement();
        HandleInteractions();
    }

    #region HandleMovementServer

    private void HandleMovementServerAuth()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        HandleMovementServerRpc(inputVector);
    }

    [ServerRpc(RequireOwnership = false)]
    private void HandleMovementServerRpc(Vector2 inputVector)
    {
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDirection, moveDistance);

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotatSpeed);
    }
    #endregion
    
    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        //float playerHeight = 2f;
        float playerRadius = 0.7f;
        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * playerRadius,
            moveDirection, Quaternion.identity, moveDistance, collisionsLayerMask);

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotatSpeed);
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out var baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { SelectedCounter = selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return objectHandlePoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPlayerPickedSomething?.Invoke(this, EventArgs.Empty);
            OnAnyPlayerPickedSometing?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}
