using UnityEngine;

public class ProductControls : MonoBehaviour
{
    private Collider2D _coll;
    private Vector3 _startDragPos;
    private bool _isDragging = false;

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
                _isDragging = true;
            }
        }

        if (_isDragging && Input.GetMouseButton(0))
        {
            transform.position = GetMousePositionInWorldSpace();
        }

        if (_isDragging && Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _coll.enabled = false;

            Collider2D hit = Physics2D.OverlapPoint(transform.position);
            _coll.enabled = true;

            if (hit != null && hit.TryGetComponent(out ProductDropZone dropZone))
            {
                dropZone.OnProductDrop(this);
            }
            else
            {
                transform.position = _startDragPos;
            }
        }
    }
    public void ResetToStartPosition()
    {
        transform.position = _startDragPos;
    }

    private Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
