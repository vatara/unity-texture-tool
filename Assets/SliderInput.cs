using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class SliderInput : MonoBehaviour {
    public Slider slider;
    InputField inputField;

    void Awake() {
        inputField = GetComponent<InputField>();
    }

    void Start() {
        OnSliderChange();
    }

    public void OnSliderChange() {
        inputField.text = slider.value.ToString();
    }

    public void OnInputChange() {
        float value = 0;
        float.TryParse(inputField.text, out value);
        slider.value = value;
    }
}
