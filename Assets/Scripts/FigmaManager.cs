using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FigmaManager : MonoBehaviour
{
    public string FigmaToken = "61012-08857851-40fd-48ca-9e4d-1e9c776d1a21";
    public string figmaFileKey = "ks4xgTujRkiGdr2a1Lz9JP";
    public string figmaNodeId = "";
    public string FigmaRendersFolder = "FigmaRenders";
    public float ImageScale = 1;

    string figmaBaseURL = "https://api.figma.com/v1";
    HttpClient client = null;
    bool throttled = false;
    Dictionary<string, Image> imagesById = new Dictionary<string, Image>();
    Dictionary<string, string> urlsById = new Dictionary<string, string>();

    async Task<string> GetImageJson(string id)
    {
        if (throttled)
        {
            Debug.LogWarning("Currently throttled - wait awhile...");
            return null;
        }

        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            var url = $"{figmaBaseURL}/images/{figmaFileKey}?ids={id}&scale={ImageScale}";
            string responseBody = await client.GetStringAsync(url);
            responseBody = Uri.UnescapeDataString(responseBody);
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            if (e.Message.StartsWith("429"))
            {
                throttled = true;
            }
            Debug.Log($"Exception Message :{e.Message} ");
        }

        return null;
    }

    async Task<string> GetFile()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            string responseBody = await client.GetStringAsync($"{figmaBaseURL}/files/{figmaFileKey}");

            responseBody = Uri.UnescapeDataString(responseBody);

            Debug.Log(responseBody);
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            Debug.Log("\nException Caught!");
            Debug.Log($"Message :{e.Message} ");
        }

        return null;
    }

    void BuildDocument(Figma.DocumentClass document)
    {
        var go = new GameObject();
        go.name = document.Name;

        // make the game object hierarchy

        foreach (var item in document.Children)
        {
            Build(item, go.transform);
        }
    }

    void Build(Figma.Document document, Transform parent)
    {
        GameObject go = null;

        switch (document.Type)
        {
            case Figma.NodeType.Boolean:
                break;
            case Figma.NodeType.Canvas:
                go = BuildCanvas(document);
                break;
            case Figma.NodeType.Component:
                break;
            case Figma.NodeType.Document:
                break;
            case Figma.NodeType.Ellipse:
                break;
            case Figma.NodeType.Frame:
                go = BuildFrame(document);
                break;
            case Figma.NodeType.Group:
                go = BuildGroup(document);
                break;
            case Figma.NodeType.Instance:
                break;
            case Figma.NodeType.Line:
                break;
            case Figma.NodeType.Rectangle:
                go = BuildRectange(document);
                break;
            case Figma.NodeType.RegularPolygon:
                go = BuildRegularPolygon(document);
                break;
            case Figma.NodeType.Slice:
                break;
            case Figma.NodeType.Star:
                break;
            case Figma.NodeType.Text:
                go = BuildText(document);
                break;
            case Figma.NodeType.Vector:
                go = BuildVector(document);
                break;
            default:
                break;
        }

        if (!go && document.Type != Figma.NodeType.Vector)
        {
            go = new GameObject();
        }

        go.transform.SetParent(parent, true);

        if (document.Children != null)
        {
            foreach (var item in document.Children)
            {
                Build(item, go?.transform);
            }
        }

        // Prevent top-level "Page N" from being turned off
        if (document.AbsoluteBoundingBox == null)
        {
            go.SetActive(true);
        }

        go.SetActive(document.Visible);
    }

    private GameObject BuildVector(Figma.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        var image = BuildFills(document, go);
        imagesById.Add(document.Id, image);
        return go;
    }

    private string SaveAndConfigureImporter(string id, Texture2D texture2d)
    {
        // save to assets

        var bytes = texture2d.EncodeToPNG();
        var rendersPath = $"{Application.dataPath}/{FigmaRendersFolder}";
        Directory.CreateDirectory(rendersPath);
        var filename = id.Replace(":", "_");
        filename = $"{filename}.png";
        File.WriteAllBytes($"{rendersPath}/{filename}", bytes);

        AssetDatabase.Refresh();

        // update importer settings

        var assetFilePathname = $"Assets/{FigmaRendersFolder}/{filename}";
        var importer = TextureImporter.GetAtPath(assetFilePathname) as TextureImporter;
        if (importer == null)
        {
            return null;
        }
        importer.textureType = TextureImporterType.Sprite;
        importer.mipmapEnabled = true;
        AssetDatabase.ImportAsset(importer.assetPath, ImportAssetOptions.ForceUpdate);
        return assetFilePathname;
    }

    private GameObject BuildRegularPolygon(Figma.Document document)
    {
        var go = BuildBase(document);
        BuildFills(document, go);
        return go;
    }

    private GameObject BuildBase(Figma.Document document)
    {
        var go = new GameObject(document.Name);
        return go;
    }

    private void SetupRect(Figma.Document document, GameObject go)
    {
        var absoluteBoundingBox = document.AbsoluteBoundingBox;

        if (absoluteBoundingBox == null)
        {
            return;
        }

        var anchorMin = Vector2.zero;
        var anchorMax = Vector2.one;

        if (document.Constraints != null)
        {
            switch (document.Constraints.Horizontal)
            {
                case Figma.Horizontal.Center:
                    anchorMin.x = .5f;
                    anchorMax.x = .5f;
                    break;
                case Figma.Horizontal.Left:
                    anchorMin.x = 0;
                    anchorMax.x = 0;
                    break;
                case Figma.Horizontal.LeftRight:
                    break;
                case Figma.Horizontal.Right:
                    anchorMin.x = 1;
                    anchorMax.x = 1;
                    break;
                case Figma.Horizontal.Scale:
                    anchorMin.x = 0;
                    anchorMax.x = 1;
                    break;
                default:
                    break;
            }

            switch (document.Constraints.Vertical)
            {
                case Figma.Vertical.Bottom:
                    anchorMin.y = 0;
                    anchorMax.y = 0;
                    break;
                case Figma.Vertical.Center:
                    anchorMin.y = .5f;
                    anchorMax.y = .5f;
                    break;
                case Figma.Vertical.Scale:
                    anchorMin.y = 0;
                    anchorMax.y = 1;
                    break;
                case Figma.Vertical.Top:
                    anchorMin.y = 1;
                    anchorMax.y = 1;
                    break;
                case Figma.Vertical.TopBottom:
                    break;
                default:
                    break;
            }
        }

        var rectTransform = go.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, absoluteBoundingBox.X, absoluteBoundingBox.Width);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, absoluteBoundingBox.Y, absoluteBoundingBox.Height);
    }

    private Image BuildFills(Figma.Document document, GameObject go)
    {
        if (document.Fills == null)
        {
            return null;
        }

        if (document.Fills.Length > 0)
        {
            if (document.Fills.Length > 1)
            {
                Debug.LogWarning($"More than one fill detected on {go.name}, using first.");
            }

            var image = go.AddComponent<Image>();

            var firstFill = document.Fills[0];
            var color = firstFill.Color;

            if (color != null)
            {
                image.color = new Color(color.R, color.G, color.B, color.A);
            }

            if (firstFill.Type != Figma.FillType.Solid)
            {
                imagesById.Add(document.Id, image);
            }

            image.enabled = firstFill.Visible;

            return image;
        }

        return null;
    }
    private GameObject BuildText(Figma.Document document)
    {
        var go = BuildBase(document);

        SetupRect(document, go);

        var tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = document.Characters;
        tmp.fontSize = document.Style.FontSize;
        tmp.alignment = TextAlignment(document);

        if (document.Fills.Length > 0)
        {
            if (document.Fills.Length > 1)
            {
                Debug.LogWarning($"More than one fill detected on {go.name}, using first.");
            }

            var firstFill = document.Fills[0];
            var color = firstFill.Color;

            tmp.color = new Color(color.R, color.G, color.B, color.A);

            tmp.enabled = firstFill.Visible;
        }
        return go;
    }

    private TextAlignmentOptions TextAlignment(Figma.Document document)
    {
        TextAlignmentOptions tao = TextAlignmentOptions.TopLeft;

        switch (document.Style.TextAlignHorizontal)
        {
            case Figma.TextAlignHorizontal.Center:
                switch (document.Style.TextAlignVertical)
                {
                    case Figma.TextAlignVertical.Bottom:
                        tao = TextAlignmentOptions.Bottom;
                        break;
                    case Figma.TextAlignVertical.Center:
                        tao = TextAlignmentOptions.Center;
                        break;
                    case Figma.TextAlignVertical.Top:
                        tao = TextAlignmentOptions.Top;
                        break;
                    default:
                        break;
                }
                break;

            case Figma.TextAlignHorizontal.Justified:
                break;

            case Figma.TextAlignHorizontal.Left:
                switch (document.Style.TextAlignVertical)
                {
                    case Figma.TextAlignVertical.Bottom:
                        tao = TextAlignmentOptions.BottomLeft;
                        break;
                    case Figma.TextAlignVertical.Center:
                        tao = TextAlignmentOptions.Left;
                        break;
                    case Figma.TextAlignVertical.Top:
                        tao = TextAlignmentOptions.TopLeft;
                        break;
                    default:
                        break;
                }
                tao = TextAlignmentOptions.Left;
                break;

            case Figma.TextAlignHorizontal.Right:
                switch (document.Style.TextAlignVertical)
                {
                    case Figma.TextAlignVertical.Bottom:
                        tao = TextAlignmentOptions.BottomRight;
                        break;
                    case Figma.TextAlignVertical.Center:
                        tao = TextAlignmentOptions.Right;
                        break;
                    case Figma.TextAlignVertical.Top:
                        tao = TextAlignmentOptions.TopRight;
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }

        return tao;
    }

    private GameObject BuildCanvas(Figma.Document document)
    {
        var go = BuildBase(document);
        go.AddComponent<Canvas>();
        BuildBackground(document, go);
        return go;
    }

    private void BuildBackground(Figma.Document document, GameObject go)
    {
        var image = go.AddComponent<UnityEngine.UI.Image>();

        image.color = new Color(
            document.BackgroundColor.R,
            document.BackgroundColor.G,
            document.BackgroundColor.B,
            document.BackgroundColor.A);

        image.enabled = false;
    }

    private GameObject BuildFrame(Figma.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        BuildFills(document, go);
        return go;
    }

    private GameObject BuildGroup(Figma.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        BuildFills(document, go);
        return go;
    }

    private GameObject BuildRectange(Figma.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        BuildFills(document, go);
        return go;
    }

    [ContextMenu("Get Figma")]
    public async void GetFigma()
    {
        throttled = false;
        imagesById.Clear();
        urlsById.Clear();

        if (client == null)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Figma-Token", FigmaToken);
        }

        var fileJson = await GetFile();
        var fileResponse = Figma.FileResponse.FromJson(fileJson);

        BuildDocument(fileResponse.Document);

        // make a request to render images and get the urls

        string[] ids = new string[imagesById.Count];
        imagesById.Keys.CopyTo(ids, 0);

        var jointIds = String.Join(",", ids);
        var imageJson = await GetImageJson(jointIds);
        var imageResponse = Figma.ImageResponse.FromJson(imageJson);

        // clean out nulls from our image response

        foreach (var item in imageResponse.Images)
        {
            if (item.Value != null)
            {
                urlsById.Add(item.Key, item.Value);
            }
        }

        // get a Texture2D for each of the image urls
        // make a sprite out of it
        // assign it back to the image components

        foreach (var item in urlsById)
        {
            var texture2d = await GetRemoteTexture(item.Value);
            var assetPath = SaveAndConfigureImporter(item.Key, texture2d);
            var sprite = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite)) as Sprite;
            imagesById[item.Key].sprite = sprite;
        }
    }

    [ContextMenu("test")]
    private void Test()
    {
        Debug.Log($"{Application.dataPath}");
    }

    private void OnEnable()
    {
        GetFigma();
    }

    public static async Task<Texture2D> GetRemoteTexture(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false)
            {
                await Task.Delay(1000 / 30);//30 hertz
            }

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log($"{ www.error }, URL:{ www.url }");
                return null;
            }
            else
            {
                return DownloadHandlerTexture.GetContent(www);
            }
        }
    }

    private static string GetGameObjectPath(Transform transform)
    {
        string path = transform.name;
        while (transform.parent != null)
        {
            transform = transform.parent;
            path = transform.name + "/" + path;
        }
        return path;
    }
}
