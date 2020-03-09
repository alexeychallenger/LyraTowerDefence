using UnityEngine;

public static class FilesUtil
{
    public static string LoadScript(string url)
    {
        TextAsset content = Resources.Load<TextAsset>(url);
        string luaText = "";
        try
        {
            luaText = content.text;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("loading file exception: " + ex.Message);
            Debug.LogError("url: " + url);
            return "";
        }

        if (luaText.Length == 0)
        {
            Debug.LogWarning("file not found " + url);
            return "";
        }
        return luaText;
    }
}
