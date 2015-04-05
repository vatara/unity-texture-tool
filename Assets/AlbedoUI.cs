using System.IO;
using UnityEngine;

public class AlbedoUI : TextureTypeUI {
    public static AlbedoUI instance;

    public string albedoFilename;

    protected void Awake() {
        instance = this;
    }

    protected override void OnFileSelected(string filename) {
        filename = filename.Replace('/', Path.DirectorySeparatorChar);
        albedoFilename = filename;
        StartCoroutine(LoadTexture(filename));
    }

    public void OnAlbedoToHeight() {
        AlbedoToHeight.instance.Show();
    }

    public override void SetTexture(Texture2D texture) {
        TextureToolUI.instance.material.mainTexture = texture;
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
