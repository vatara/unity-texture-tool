using UnityEngine;
using UnityEngine.EventSystems;

public class ColorPickerUI : MonoBehaviour, IPointerClickHandler {
    public static ColorPickerUI instance;

    void Awake() {
        instance = this;
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponentInChildren<HSVPicker>().onValueChanged.AddListener((color) => OnColorChange(color));
        Hide();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData click) {
        Hide();
    }

    public void SetColor(Color color) {
        GetComponentInChildren<HSVPicker>().AssignColor(color);
    }

    public void OnColorChange(Color color) {
        MaterialSettingsUI.instance.SetColor(color);
    }
}
