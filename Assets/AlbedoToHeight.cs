using UnityEngine;
using UnityEngine.UI;

public class AlbedoToHeight : TextureConversion {
    public static AlbedoToHeight instance;

    public Transform child;
    public Shader blurShader;
    public Shader shader;
    Material blurMaterial;
    Material material;

    Texture source;
    RenderTexture output;
    Texture2D texture;

    public Slider contrastSlider;
    public Slider offsetSlider;

    void Awake() {
        instance = this;
        child = transform.GetChild(0);
        blurMaterial = new Material(blurShader);
        material = new Material(shader);

        Hide();
    }

    void Update() {
        if (source == null)
            return;

        RenderTexture rt = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(source, rt, blurMaterial, 0);

        for (int i = 0; i < 1; i++) {
            Graphics.Blit(rt, rt, blurMaterial, 1);
            Graphics.Blit(rt, rt, blurMaterial, 1);
        }

        Graphics.Blit(rt, output, material);
        RenderTexture.ReleaseTemporary(rt);

        Rect rect = new Rect(0, 0, output.width, output.height);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();
    }

    public override void Show() {
        GetComponentInParent<HideAllTextureConversions>().Hide();
        child.gameObject.SetActive(true);
        gameObject.SetActive(true);

        source = AlbedoUI.instance.image.mainTexture;
        output = new RenderTexture(source.width, source.height, 24);
        texture = new Texture2D(output.width, output.height);

        HeightUI.instance.SetTexture(texture);

        contrastSlider.value = material.GetFloat("_Contrast");
        offsetSlider.value = material.GetFloat("_Offset");
        material.SetFloat("_Contrast", contrastSlider.value);
    }

    public override void Hide() {
        //child.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnBlurSizeSliderChanged(Slider slider) {
        blurMaterial.SetVector("_Parameter", new Vector4(slider.value, -slider.value, 0.0f, 0.0f));
    }

    public void OnContrastSliderChanged(Slider slider) {
        material.SetFloat("_Contrast", slider.value);
    }

    public void OnOffsetSliderChanged(Slider slider) {
        material.SetFloat("_Offset", slider.value);
    }
}
