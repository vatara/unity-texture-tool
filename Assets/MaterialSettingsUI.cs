using UnityEngine;
using UnityEngine.UI;

public class MaterialSettingsUI : TextureConversion {
    public static MaterialSettingsUI instance;

    public Transform child;
    public Button albedoColorButton;

    void Awake() {
        instance = this;
        child = transform.GetChild(0);
        Hide();
    }

    public override void Hide() {
        //child.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void Show() {
        GetComponentInParent<HideAllTextureConversions>().Hide();

        child.gameObject.SetActive(true);
        gameObject.SetActive(true);
        albedoColorButton.GetComponent<Image>().color = TextureToolUI.instance.material.color;
    }

    public void OnColorButtonClick() {
        ColorPickerUI.instance.Show();
        ColorPickerUI.instance.SetColor(TextureToolUI.instance.material.color);
    }

    public void SetColor(Color color) {
        TextureToolUI.instance.material.color = color;
        albedoColorButton.GetComponent<Image>().color = TextureToolUI.instance.material.color;
    }

    public void OnNormalSliderChange(Slider slider) {
        TextureToolUI.instance.material.SetFloat("_BumpScale", slider.value);
    }

    public void OnHeightSliderChange(Slider slider) {
        TextureToolUI.instance.material.SetFloat("_Parallax", slider.value);
    }

    public void OnOcclusionSliderChange(Slider slider) {
        TextureToolUI.instance.material.SetFloat("_OcclusionStrength", slider.value);
    }
}
