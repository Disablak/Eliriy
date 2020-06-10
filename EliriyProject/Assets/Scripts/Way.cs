using UnityEngine;


public class Way : MonoBehaviour
{
    [SerializeField] private Transform[] way_points = null;
    
    private float way_length = 0.0f;

    public Transform[] getPoints() => way_points;
    
    // Start is called before the first frame update
    void Awake()
    {
        way_points = transform.getComponentAllChildrenArray<Transform>();
        way_length = getLength();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float getLength()
    {
        float total_lenght = 0.0f;
        
        for (int i = 0; i < way_points.Length - 1; i++)
        {
            Vector2 first_point = way_points[i].transform.localPosition;
            Vector2 second_point = way_points[i + 1].transform.localPosition;
            
            float distance = Vector2.Distance(first_point, second_point);
            total_lenght += distance;
        }

        return total_lenght;
    }
}

