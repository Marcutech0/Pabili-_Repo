using UnityEngine;

public class ProductControls : MonoBehaviour
{
    private Collider2D _coll;
    private Vector3 _startDragPos;
    private Vector3 _previousDragPos;
    private bool _isDragging = false;

    [Header("Product Data")]
    public ProductData productData;

    [Header("Sway Settings")]
    public float rotationLerpSpeed = 8f;
    public float swayAmplitude = 10f; // degrees
    public float swayFrequency = 8f;  // speed of sway

    private float swayTimer = 0f;
    private float targetAngle = 0f;

    void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

            if (hit != null && hit == _coll)
            {
                _startDragPos = transform.position;
                _previousDragPos = GetMousePositionInWorldSpace();
                _isDragging = true;
                swayTimer = 0f; // Reset sway
            }

            if (hit != null && hit == _coll)
            {
                _startDragPos = transform.position;
                _previousDragPos = GetMousePositionInWorldSpace();
                _isDragging = true;
                swayTimer = 0f;
                transform.SetParent(null);
            }
        }

        if (_isDragging && Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = GetMousePositionInWorldSpace();
            transform.position = currentMousePos;

            // Drag direction to target rotation angle
            Vector3 dragDirection = currentMousePos - _previousDragPos;
            if (dragDirection.sqrMagnitude > 0.001f)
            {
                targetAngle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;
            }

            // Sway wobble
            swayTimer += Time.deltaTime * swayFrequency;
            float swayOffset = Mathf.Sin(swayTimer) * swayAmplitude;

            float finalAngle = targetAngle + swayOffset;

            // Smooth Z-only rotation (no skew)
            float currentZ = transform.eulerAngles.z;
            float smoothedZ = Mathf.LerpAngle(currentZ, finalAngle, Time.deltaTime * rotationLerpSpeed);
            transform.rotation = Quaternion.Euler(0f, 0f, smoothedZ);

            _previousDragPos = currentMousePos;
        }

        if (_isDragging && Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _coll.enabled = false;

            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.2f);
            _coll.enabled = true;

            if (hit != null)
            {
                Debug.Log($"👀 Hit object: {hit.name}");

                Component[] all = hit.GetComponents<Component>();
                foreach (var comp in all)
                    Debug.Log("📌 Hit has component: " + comp.GetType());

                ProductDropZone dropZone = hit.GetComponent<ProductDropZone>() as ProductDropZone;

                if (dropZone != null)
                {
                    Debug.Log("📦 Dropped on a valid drop zone!");
                    dropZone.OnProductDrop(this);
                    return;
                }
                else
                {
                    Debug.LogWarning("⚠️ Drop zone component not found on hit object!");
                }
            }
            else
            {
                Debug.LogWarning("❌ No collider found at drop location: " + transform.position);
            }

            // Reset position and rotation
            transform.position = _startDragPos;
            transform.rotation = Quaternion.identity;
        }
    }

    public void ResetToStartPosition()
    {
        transform.position = _startDragPos;
        transform.rotation = Quaternion.identity;
    }

    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
