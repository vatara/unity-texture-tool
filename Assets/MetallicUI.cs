using UnityEngine;
using UnityEngine.UI;

public class MetallicUI : TextureTypeUI {
    public static MetallicUI instance;

    public void Awake() {
        instance = this;
    }

    public override void SetTexture(Texture2D texture) {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        image.sprite = Sprite.Create(texture, rect, Vector2.zero);
        TextureToolUI.instance.material.SetTexture("_MetallicGlossMap", texture);
    }
}
