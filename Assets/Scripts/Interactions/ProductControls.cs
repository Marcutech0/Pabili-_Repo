using UnityEngine;

public class ProductControls : MonoBehaviour
{
    [Header("Required References")]
    public ProductData productData;
    private Collider2D _coll;
    private Vector3 _startDragPos;
    private Vector3 _previousDragPos;
    private bool _isDragging = false;

    [Header("Drag Settings")]
    public float rotationLerpSpeed = 1f;
    public float swayAmplitude = 3f;
    public float swayFrequency = 3f;

    private float swayTimer = 0f;
    private float targetAngle = 0f;
    private ProductDisplay _display;

    void Start()
    {
        _coll = GetComponent<Collider2D>();
        _startDragPos = transform.position;
        _display = GetComponent<ProductDisplay>();

        if (productData != null)
        {
            productData.OnStockChanged += UpdateDisplay;
            // Initialize with current stock count
            UpdateDisplay();
        }
    }

    void OnDestroy()
    {
        if (productData != null)
        {
            productData.OnStockChanged -= UpdateDisplay;
            productData.spawnedCount--;
        }
    }

    void Update()
    {
        HandleDragInput();
    }

    void HandleDragInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryStartDrag();
        }

        if (_isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                ContinueDrag();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }
    }

    void TryStartDrag()
    {
        if (productData == null || productData.productStock <= 0) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit == _coll)
        {
            _isDragging = true;
            _startDragPos = transform.position;
            _previousDragPos = GetMousePositionInWorldSpace();
            swayTimer = 0f;
            transform.SetParent(null);
            _display?.UpdateStack(1);
        }
    }

    void ContinueDrag()
    {
        Vector3 currentMousePos = GetMousePositionInWorldSpace();
        transform.position = currentMousePos;

        Vector3 dragDirection = currentMousePos - _previousDragPos;
        if (dragDirection.sqrMagnitude > 0.001f)
        {
            targetAngle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;
        }

        swayTimer += Time.deltaTime * swayFrequency;
        float swayOffset = Mathf.Sin(swayTimer) * swayAmplitude;
        float finalAngle = targetAngle + swayOffset;

        float currentZ = transform.eulerAngles.z;
        float smoothedZ = Mathf.LerpAngle(currentZ, finalAngle, Time.deltaTime * rotationLerpSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, smoothedZ);

        _previousDragPos = currentMousePos;
    }

    void EndDrag()
    {
        _isDragging = false;
        _coll.enabled = false;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.2f);
        _coll.enabled = true;

        if (hit != null)
        {
            HandleDropZoneInteraction(hit);
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    void HandleDropZoneInteraction(Collider2D hit)
    {
        if (hit.GetComponent<ProductDropZone>() is ProductDropZone dropZone)
        {
            if (dropZone is CustomerDropZone)
            {
                productData.ModifyStock(-1);
            }
            dropZone.OnProductDrop(this);
            UpdateDisplay();
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.SetPositionAndRotation(_startDragPos, Quaternion.identity);
        UpdateDisplay();
    }

    public Vector3 GetStartPosition()
    {
        return _startDragPos;
    }

    void UpdateDisplay()
    {
        _display?.UpdateStack(productData.productStock);
    }

    Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}