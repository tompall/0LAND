using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    Camera maincamera;

    public Transform CameraParent;

    public Vector3 currentLocation;

    Explorer explorer;

    public delegate void LocationChange(Vector3 newLocation);
    public static event LocationChange OnLocationChange;

    private void OnEnable()
    {
        if (this.transform.parent.GetComponent<Explorer>())
        {
            explorer = this.transform.parent.GetComponent<Explorer>();
        }
        else
        {
            Debug.LogError("No Explorer script on parent or no parent");
        }
    }
    private void OnDisable()
    {
        
    }

    private void Start()
    {
        maincamera = GetComponent<Camera>();

        if(this.transform.parent.GetComponent<Explorer>())
        { 
            explorer = this.transform.parent.GetComponent<Explorer>();
        }else
        {
            Debug.LogError("No Explorer script on parent or no parent");
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            MouseLook();
        }
		if (Input.GetMouseButton (0))
        {
			Ray ray = maincamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				StopAllCoroutines ();
				StartCoroutine (ClickNavigation (hit.point));
			}
		}
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.ScrollWheel && Event.current.delta.y < 0)
        {
            maincamera.fieldOfView+=1f;
        }
        if (Event.current.type == EventType.ScrollWheel && Event.current.delta.y > 0)
        {
            maincamera.fieldOfView-=1f;
        }
    }

    IEnumerator ClickNavigation(Vector3 targetLocation)
    {
        while (Vector3.Distance(CameraParent.transform.position, targetLocation) > 1f)
        {
            CameraParent.transform.position = Vector3.Lerp(CameraParent.transform.position, targetLocation, Time.fixedDeltaTime * 1f);
            yield return null;
        }
    }

    void MouseLook()
    {
        CameraParent.Rotate(Vector3.up, Input.GetAxis("Horizontal")*Time.deltaTime);
    }
}