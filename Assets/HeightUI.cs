using UnityEngine;
using UnityEngine.UI;

public class HeightUI : TextureTypeUI {
    public static HeightUI instance;

    protected void Awake() {
        instance = this;
    }

    public override void SetTexture(Texture2D texture) {
        TextureToolUI.instance.material.SetTexture("_ParallaxMap", texture);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    public void OnHeightToNormal() {
        HeightToNormal.instance.Show();
    }

    public void OnHeightToMetallic() {
        HeightToMetallic.instance.Show();
    }
}
