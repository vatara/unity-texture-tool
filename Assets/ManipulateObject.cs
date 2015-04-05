using UnityEngine;

public class ManipulateObject : MonoBehaviour {
    public static ManipulateObject instance;

    Vector3 initialPosition;
    Quaternion initialRotation;

    Vector3 startMousePosition;
    Vector3 startPosition;
    Quaternion startRotation;

    public int manipulating = -1;

    void Awake() {
        instance = this;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update() {
        if (manipulating == 0 && !Input.GetMouseButton(0)) {
            manipulating = -1;
        }
        if (manipulating == 1 && !Input.GetMouseButton(1)) {
            manipulating = -1;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            startMousePosition = Input.mousePosition;
            startPosition = transform.position;
            startRotation = transform.rotation;
            manipulating = Input.GetMouseButton(0) ? 0 : 1;
        }
        if (Input.GetMouseButton(0) && manipulating == 0) {
            var diff = Input.mousePosition - startMousePosition;
            transform.position = startPosition + diff * .01f;
        }
        if (Input.GetMouseButton(1) && manipulating == 1) {
            var diff = Input.mousePosition - startMousePosition;
            Vector3 euler = startRotation.eulerAngles;
            euler.x += diff.y;
            euler.y -= diff.x;
            transform.rotation = Quaternion.Euler(euler);
        }

        var zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0) {
            transform.position += new Vector3(0,0, zoom);
        }
    }

    public void Reset() {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
