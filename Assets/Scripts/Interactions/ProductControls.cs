using UnityEngine;

public class ProductControls : MonoBehaviour
{
    [Header("Required References")]
    public ProductData productData;
    public Collider2D productCollider;
    private Vector3 _originalPosition;
    private bool _isDragging = false;

    [Header("Drag Settings")]
    public float swayAmplitude = 3f;
    public float swayFrequency = 3f;
    [Range(0.1f, 2f)] public float rotationLerpSpeed = 1f;

    private float _swayTimer;
    private float _targetAngle;
    private ProductDisplay _display;

    void Start()
    {
        if (productCollider == null) productCollider = GetComponent<Collider2D>();
        _originalPosition = transform.position;
        _display = GetComponent<ProductDisplay>();

        if (productData != null)
        {
            productData.OnStockChanged += UpdateDisplay;
            UpdateDisplay();
        }
    }

    void OnDestroy()
    {
        if (productData != null)
        {
            productData.OnStockChanged -= UpdateDisplay;
        }
    }

    void Update()
    {
        if (_isDragging)
        {
            UpdateDragPosition();
            UpdateDragRotation();
        }
    }

    void OnMouseDown()
    {
        if (productData == null || productData.productStock <= 0) return;

        _isDragging = true;
        _swayTimer = 0f;
        transform.SetParent(null);
        productCollider.enabled = false;
    }

    void OnMouseUp()
    {
        if (!_isDragging) return;
        _isDragging = false;
        productCollider.enabled = true;

        HandleDrop();
    }

    private void UpdateDragPosition()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        transform.position = mousePos;
    }

    private void UpdateDragRotation()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        Vector3 moveDirection = mousePos - _originalPosition;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            _targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        }

        _swayTimer += Time.deltaTime * swayFrequency;
        float swayOffset = Mathf.Sin(_swayTimer) * swayAmplitude;
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, _targetAngle + swayOffset, Time.deltaTime * rotationLerpSpeed);

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void HandleDrop()
    {
        Collider2D dropZone = Physics2D.OverlapCircle(transform.position, 0.5f);

        if (dropZone != null && dropZone.TryGetComponent<ProductDropZone>(out var zone))
        {
            zone.OnProductDrop(this);

            if (zone is CustomerDropZone)
            {
                productData.ModifyStock(-1);
            }
        }
        else
        {
            ReturnToShelf();
        }
    }

    public void ReturnToShelf()
    {
        transform.position = _originalPosition;
        transform.rotation = Quaternion.identity;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (_display != null)
        {
            _display.UpdateStack(productData.productStock);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}