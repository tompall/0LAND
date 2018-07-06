using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    Camera maincamera;

    public static Transform Target;
    public Transform CameraParent;

    private void Start()
    {
        maincamera = GetComponent<Camera>();
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