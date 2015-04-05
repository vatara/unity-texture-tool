using UnityEngine;
using UnityEngine.UI;

public class HeightToNormal : TextureConversion {
    public static HeightToNormal instance;

    public Transform child;
    public Shader shader;
    Material material;

    Texture source;
    RenderTexture renderTexture;
    Texture2D texture;

    public Slider offsetSlider;

    void Awake() {
        instance = this;
        child = transform.GetChild(0);
        material = new Material(shader);

        Hide();
    }

    void Update() {
        if (source == null)
            return;

        Graphics.Blit(source, renderTexture, material);

        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        //HeightUI.instance.SetHeightTexture(texture);
    }

    public override void Show() {
        GetComponentInParent<HideAllTextureConversions>().Hide();
        child.gameObject.SetActive(true);
        gameObject.SetActive(true);

        source = AlbedoUI.instance.image.mainTexture;
        renderTexture = new RenderTexture(source.width, source.height, 24);
        texture = new Texture2D(renderTexture.width, renderTexture.height);

        NormalUI.instance.SetTexture(texture);

        offsetSlider.value = material.GetFloat("_Offset");
    }

    public override void Hide() {
        //child.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnOffsetSliderChanged(Slider slider) {
        material.SetFloat("_Offset", slider.value);
    }
}
