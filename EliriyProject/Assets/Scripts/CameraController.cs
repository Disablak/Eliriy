using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour // WORK only with orthographic camera mode
{
    [SerializeField] private Camera      camera    = null;
    [SerializeField] private BoxCollider borders   = null;
    [SerializeField] private Clickable   clickable = null;

    [SerializeField] private float pan_speed = 8.0f;
    [SerializeField] private float zoom_speed_touch = 0.5f;
    [SerializeField] private float zoom_speed_mouse = 2.0f;
    
    [SerializeField] private Vector2 zoom_bounds = new Vector2( 4.0f, 6.0f );
    [SerializeField] private bool enable_smooth_move = false;
    private Vector2 min_bounds = Vector2.zero;
    private Vector2 max_bounds = Vector2.zero;
    
    private Vector3 last_pan_position;
    private int pan_finger_id; // Touch mode only
    
    private bool was_zooming_last_frame; // Touch mode only
    private Vector2[] last_zoom_positions; // Touch mode only
    
    private Vector2 impulse_start_pos = Vector2.zero;
    private Vector2 impulse_finish_pos = Vector2.zero;

    private void calcBounds()
    {
        Vector2 position = borders.transform.position;
        Bounds bounds = borders.bounds;
        
        Vector2 cam_half_size = getHalfCameraSize();
        min_bounds = new Vector2(
            position.x - bounds.extents.x + cam_half_size.x,
            position.y - bounds.extents.y + cam_half_size.y
        );
        
        max_bounds = new Vector2(
            position.x + bounds.extents.x - cam_half_size.x,
            position.y + bounds.extents.y - cam_half_size.y
        );
    }

    private Vector2 getHalfCameraSize()
    {
        float screen_aspect = (float)Screen.width / (float)Screen.height;
        float cam_half_height = camera.orthographicSize;
        float cam_half_width = screen_aspect * cam_half_height;

        return new Vector2( cam_half_width, cam_half_height );
    }

    // Start is called before the first frame update
    void Start()
    {
        clickable.onBeginDrag += data => last_pan_position = data.position;
        clickable.onDrag += handleMouse;
        
        calcBounds();
    }

    // Update is called once per frame
    void onDrag( PointerEventData data )
    {
        handleMouse( data );
    }

    void handleTouch()
    {
        switch (Input.touchCount)
        {
            case 1:
                was_zooming_last_frame = false;
            
                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) 
                {
                    last_pan_position = touch.position;
                    pan_finger_id = touch.fingerId;
                } 
                else if (touch.fingerId == pan_finger_id && touch.phase == TouchPhase.Moved)
                {
                    panCamera(touch.position);
                }
                else if ( touch.phase == TouchPhase.Ended)
                {
                    if ( enable_smooth_move )
                        impulsePanCamera( true, touch );
                }
                break;
            
            case 2: // Zooming
                Vector2[] new_positions = new Vector2[]{Input.GetTouch(0).position, Input.GetTouch(1).position};
                if (!was_zooming_last_frame) {
                    last_zoom_positions = new_positions;
                    was_zooming_last_frame = true;
                } else {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float new_distance = Vector2.Distance(new_positions[0], new_positions[1]);
                    float old_distance = Vector2.Distance(last_zoom_positions[0], last_zoom_positions[1]);
                    float offset = new_distance - old_distance;
    
                    zoomCamera(offset, zoom_speed_touch);
    
                    last_zoom_positions = new_positions;
                }
                break;
            
            default: 
                was_zooming_last_frame = false;
                break;
        }
    }
    
    private void handleMouse( PointerEventData data ) {
        
        panCamera( data.position );
        return;
        if (Input.GetMouseButtonDown(0)) {
            last_pan_position = Input.mousePosition;
        } 
        else if (Input.GetMouseButton(0))
        {
            impulse_start_pos = Input.mousePosition;
            panCamera( Input.mousePosition );
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if ( enable_smooth_move )
                impulsePanCamera( false );
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoomCamera(scroll, zoom_speed_mouse);
    }
    
    void panCamera(Vector3 new_pan_position) {
        // Determine how much to move the camera
        Vector3 offset = camera.ScreenToViewportPoint(last_pan_position - new_pan_position);
        Vector3 move = new Vector3(offset.x * pan_speed, offset.y * pan_speed, 0.0f );
        
        // Perform the movement
        transform.Translate(move, Space.World);  
        
        // Ensure the camera remains within bounds.
        transform.position = clampPositions( transform.position );
    
        // Cache the position
        last_pan_position = new_pan_position;
    }

    private Vector3 clampPositions( Vector3 unclamp_pos )
    {
        Vector3 clamped_pos = unclamp_pos;
        clamped_pos.x = Mathf.Clamp( unclamp_pos.x, min_bounds.x, max_bounds.x );
        clamped_pos.y = Mathf.Clamp( unclamp_pos.y, min_bounds.y, max_bounds.y );

        return clamped_pos;
    }

    private void impulsePanCamera( bool is_touch, Touch touch = default )
    {
        StopAllCoroutines();
        StartCoroutine(waitOneFrame());
        IEnumerator waitOneFrame()
        {
            
            #if UNITY_EDITOR
            impulse_start_pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            yield return null;
            impulse_finish_pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            #endif
            
            Vector2 dir = (impulse_finish_pos - impulse_start_pos).normalized;
            if (is_touch)
                dir = touch.deltaPosition.normalized;
            
            Vector2 cur_pos = transform.position;
            Vector2 target_pos = (cur_pos - dir);
            target_pos = clampPositions(target_pos);
            
            float smooth = 15.0f;

            while (Vector2.Distance(cur_pos, target_pos) > 0.05f)
            {
                cur_pos = Vector2.Lerp(cur_pos, target_pos, smooth * Time.deltaTime);
                transform.position = new Vector3(cur_pos.x, cur_pos.y, transform.position.z);
                yield return null;
            }
        }
    }

    void zoomCamera(float offset, float speed)
    {
        if (offset.Equals(0.0f))
            return;

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - (offset * speed), zoom_bounds.x, zoom_bounds.y);
        calcBounds();
        transform.position = clampPositions( transform.position );
    }
}