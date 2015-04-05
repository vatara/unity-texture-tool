using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextureToolUI : MonoBehaviour {
    public static TextureToolUI instance;

    public Material material;
    public MeshRenderer plane;

    void Awake() {
        instance = this;
    }

	void Update() {
        if (EventSystem.current.IsPointerOverGameObject() && ManipulateObject.instance.manipulating == -1) {
            ManipulateObject.instance.enabled = false;
        }
        else {
            ManipulateObject.instance.enabled = true;
        }

        plane.sharedMaterial = material;
	}

    public void OnExport() {
        string path = Path.GetDirectoryName(AlbedoUI.instance.albedoFilename);
        string filename = Path.GetFileNameWithoutExtension(AlbedoUI.instance.albedoFilename);

        SaveTextureToPng(material.mainTexture, path, filename + "_Albedo.png");
        SaveTextureToPng(material.GetTexture("_BumpMap"), path, filename + "_Normal.png");
        SaveTextureToPng(material.GetTexture("_ParallaxMap"), path, filename + "_Height.png");
        SaveTextureToPng(material.GetTexture("_OcclusionMap"), path, filename + "_Occlusion.png");
        SaveTextureToPng(material.GetTexture("_MetallicGlossMap"), path, filename + "_Metallic.png");
    }

    void SaveTextureToPng(Texture texture, string path, string filename) {
        var tex2D = texture as Texture2D;
        var bytes = tex2D.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(path, filename), bytes);
    }
}
