using UnityEngine;


public class CameraHandler : MonoBehaviour
{
    public float[] bounds_x    = new float[] { -10f, 5f };
    public float[] bounds_y    = new float[] { -18f, -4f };
    
    public float PAN_SPEED        = 20f;
    public float ZOOM_SPEED_TOUCH = 0.1f;
    public float ZOOM_SPEED_MOUSE = 0.5f;

    private const float MIN_ZOOM = 6.0f;
    private const float MAX_ZOOM = 4.0f;

    private Camera cam;

    private Vector3 last_pan_position;
    private int     pan_finger_id; // Touch mode only

    private bool      was_zooming_last_frame; // Touch mode only
    private Vector2[] last_zoom_positions;    // Touch mode only

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if ( Input.touchSupported ) 
        {
            HandleTouch();
        } else 
        {
            HandleMouse();
        }
    }

    void HandleTouch()
    {
        switch ( Input.touchCount )
        {
            case 1: // Panning
                was_zooming_last_frame = false;

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch( 0 );
                if ( touch.phase == TouchPhase.Began )
                {
                    last_pan_position = touch.position;
                    pan_finger_id     = touch.fingerId;
                }
                else if ( touch.fingerId == pan_finger_id && touch.phase == TouchPhase.Moved )
                {
                    PanCamera( touch.position );
                }

                break;

            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch( 0 ).position, Input.GetTouch( 1 ).position };
                if ( !was_zooming_last_frame )
                {
                    last_zoom_positions    = newPositions;
                    was_zooming_last_frame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance( newPositions[0], newPositions[1] );
                    float oldDistance = Vector2.Distance( last_zoom_positions[0], last_zoom_positions[1] );
                    float offset      = newDistance - oldDistance;

                    ZoomCamera( offset, ZOOM_SPEED_TOUCH );

                    last_zoom_positions = newPositions;
                }

                break;

            default:
                was_zooming_last_frame = false;
                break;
        }
    }

    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            last_pan_position = Input.mousePosition;
        }
        else if ( Input.GetMouseButton( 0 ) )
        {
            PanCamera( Input.mousePosition );
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis( "Mouse ScrollWheel" );
        ZoomCamera( scroll, ZOOM_SPEED_MOUSE );
    }

    void PanCamera( Vector3 newPanPosition )
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint( last_pan_position - newPanPosition );
        Vector3 move   = new Vector3( offset.x * PAN_SPEED, offset.y * PAN_SPEED, 0.0f );

        // Perform the movement
        transform.Translate( move, Space.World );

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x              = Mathf.Clamp( transform.position.x, bounds_x[0], bounds_x[1] );
        pos.y              = Mathf.Clamp( transform.position.y, bounds_y[0], bounds_y[1] );
        transform.position = pos;

        // Cache the position
        last_pan_position = newPanPosition;
    }

    void ZoomCamera( float offset, float speed )
    {
        if ( offset == 0 )
            return;
        
        cam.orthographicSize = Mathf.Clamp( cam.orthographicSize - (offset * speed), MAX_ZOOM, MIN_ZOOM );
    }
}