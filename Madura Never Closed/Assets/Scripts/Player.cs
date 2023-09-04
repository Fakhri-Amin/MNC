using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IProductObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform productObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private ProductObject productObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Playe Instance!");
        }
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if (baseCounter != selectedCounter)
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

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        if (inputVector == Vector2.zero)
        {
            isWalking = false;
            return;
        }
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (GetComponent<ManaMovement>().GetPlayerManaAmount() < 1)
        {
            if (!canMove)
            {
                // Cannot move towards movedir

                // Attempt only x movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = (moveDir.x < -0.5f || moveDir.x > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

                if (canMove)
                {
                    // Can move only on the X
                    moveDir = moveDirX;
                }
                else
                {
                    // Cannot move only on the X

                    // Attempt only z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = (moveDir.z < -0.5f || moveDir.z > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                    if (canMove)
                    {
                        // Can move only on the z
                        moveDir = moveDirZ;
                    }
                    else
                    {
                        // Cannot move in any direction
                    }
                }
            }
        }
        if (GetComponent<ManaMovement>().GetPlayerManaAmount() > 0)
        {
            if (canMove)
            {
                transform.position += moveDir * moveDistance;
                GetComponent<ManaMovement>().DecreasePlayerMana();
            }
        }

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetProductObjectFollowTransform()
    {
        return productObjectHoldPoint;
    }

    public void SetProductObject(ProductObject productObject)
    {
        this.productObject = productObject;
        if (productObject != null) OnPickedSomething?.Invoke(this, EventArgs.Empty);
    }

    public ProductObject GetProductObject()
    {
        return productObject;
    }

    public void ClearProductObject()
    {
        productObject = null;
    }

    public bool HasProductObject()
    {
        return productObject != null;
    }
}
