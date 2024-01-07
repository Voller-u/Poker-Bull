using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveSprite
{
    [MenuItem("Services/SaveSprite")]
    static void BeginSaveSprite()
    {
        string resourcesPath = "Assets/Resources/";//����Resources·��
        bool isSuccessSaved = false;
        foreach (Object obj in Selection.objects)
        {
            //ѡ��ͼƬ�ľ���·�����ҵ���"Assets/Game/Resources/Texture/Atlas/testUI.png"
            string selectionPath = AssetDatabase.GetAssetPath(obj);
            // �������ϼ���"Assets/Game/Resources/"
            if (selectionPath.StartsWith(resourcesPath))
            {
                string selectionExt = System.IO.Path.GetExtension(selectionPath);//�õ��ļ���׺.png
                if (selectionExt.Length == 0)
                {
                    continue;
                }

                // ��·��"Assets/Game/Resources/Texture/Atlas/testUI.png"�õ�·��"Texture/Atlas/testUI"
                string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);
                loadPath = loadPath.Substring(resourcesPath.Length);
                // ���ش��ļ��µ�������Դ
                Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
                if (sprites.Length > 0)
                {
                    // ���������ļ���
                    string outPath = Application.dataPath + "/Resources/";
                    System.IO.Directory.CreateDirectory(outPath);
                    foreach (Sprite sprite in sprites)
                    {
                        Debug.Log(sprite.name);
                        // ��������������
                        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                            (int)sprite.textureRect.y, (int)sprite.textureRect.width,
                            (int)sprite.textureRect.height);
                        tex.SetPixels(pixels);
                        tex.Apply();

                        // д���PNG�ļ�
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
