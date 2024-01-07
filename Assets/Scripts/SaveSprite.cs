using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveSprite
{
    [MenuItem("Services/SaveSprite")]
    static void BeginSaveSprite()
    {
        string resourcesPath = "Assets/Resources/";//存入Resources路径
        bool isSuccessSaved = false;
        foreach (Object obj in Selection.objects)
        {
            //选择图片的绝对路径，我的是"Assets/Game/Resources/Texture/Atlas/testUI.png"
            string selectionPath = AssetDatabase.GetAssetPath(obj);
            // 必须最上级是"Assets/Game/Resources/"
            if (selectionPath.StartsWith(resourcesPath))
            {
                string selectionExt = System.IO.Path.GetExtension(selectionPath);//拿到文件后缀.png
                if (selectionExt.Length == 0)
                {
                    continue;
                }

                // 从路径"Assets/Game/Resources/Texture/Atlas/testUI.png"得到路径"Texture/Atlas/testUI"
                string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);
                loadPath = loadPath.Substring(resourcesPath.Length);
                // 加载此文件下的所有资源
                Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
                if (sprites.Length > 0)
                {
                    // 创建导出文件夹
                    string outPath = Application.dataPath + "/Resources/";
                    System.IO.Directory.CreateDirectory(outPath);
                    foreach (Sprite sprite in sprites)
                    {
                        Debug.Log(sprite.name);
                        // 创建单独的纹理
                        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                            (int)sprite.textureRect.y, (int)sprite.textureRect.width,
                            (int)sprite.textureRect.height);
                        tex.SetPixels(pixels);
                        tex.Apply();

                        // 写入成PNG文件
                        System.IO.File.WriteAllBytes(outPath + "/" + sprite.name + ".png", tex.EncodeToPNG());
                    }
                    isSuccessSaved = true;
                }
            }
        }
        if (isSuccessSaved)
        {
            Debug.Log("SaveSprite Successed!");
        }
        else
        {
            Debug.LogError("SaveSprite Failed!");
        }
    }
}
