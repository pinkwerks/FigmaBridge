using System;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FigmaManager : MonoBehaviour
{
    public string FigmaToken = "61012-08857851-40fd-48ca-9e4d-1e9c776d1a21";
    public string figmaRestURL = "https://api.figma.com/v1/files";
    public string figmaFileKey = "ks4xgTujRkiGdr2a1Lz9JP";
    public string figmaNodeId = "";

    private HttpClient client = null;

    async Task<string> GetFigmaRespose()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            string responseBody = await client.GetStringAsync($"{figmaRestURL}/{figmaFileKey}");

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

    void BuildDocument(QuickType.DocumentClass document)
    {
        var go = new GameObject();
        go.name = document.Name;

        foreach (var item in document.Children)
        {
            Build(item, go.transform);
        }
    }

    void Build(QuickType.Document document, Transform parent)
    {
        GameObject go = null;

        switch (document.Type)
        {
            case QuickType.NodeType.Boolean:
                break;
            case QuickType.NodeType.Canvas:
                go = BuildCanvas(document);
                break;
            case QuickType.NodeType.Component:
                break;
            case QuickType.NodeType.Document:
                break;
            case QuickType.NodeType.Ellipse:
                break;
            case QuickType.NodeType.Frame:
                go = BuildFrame(document);
                break;
            case QuickType.NodeType.Group:
                go = BuildGroup(document);
                break;
            case QuickType.NodeType.Instance:
                break;
            case QuickType.NodeType.Line:
                break;
            case QuickType.NodeType.Rectangle:
                go = BuildRectange(document);
                break;
            case QuickType.NodeType.RegularPolygon:
                go = BuildRegularPolygon(document);
                break;
            case QuickType.NodeType.Slice:
                break;
            case QuickType.NodeType.Star:
                break;
            case QuickType.NodeType.Text:
                go = BuildText(document);
                break;
            case QuickType.NodeType.Vector:
                break;
            default:
                break;
        }

        if (!go)
        {
            go = new GameObject(document.Name);
        }

        go.transform.SetParent(parent);

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

    private GameObject BuildRegularPolygon(QuickType.Document document)
    {
        var go = BuildBase(document);
        BuildFills(document, go);
        //go.AddComponent<MeshRenderer>();
        return go;
    }

    private GameObject BuildBase(QuickType.Document document)
    {
        var go = new GameObject(document.Name);
        return go;
    }

    private void SetupRect(QuickType.Document document, GameObject go)
    {
        var absoluteBoundingBox = document.AbsoluteBoundingBox;

        if (absoluteBoundingBox == null)
        {
            return;
        }

        var anchorMin = Vector2.zero;
        var anchorMax = Vector2.one;

        float width = absoluteBoundingBox.X;
        float height = absoluteBoundingBox.Y;

        if (document.Constraints != null)
        {
            switch (document.Constraints.Horizontal)
            {
                case QuickType.Horizontal.Center:
                    anchorMin.x = .5f;
                    anchorMax.x = .5f;
                    break;
                case QuickType.Horizontal.Left:
                    anchorMin.x = 0;
                    anchorMax.x = 0;
                    break;
                case QuickType.Horizontal.LeftRight:
                    break;
                case QuickType.Horizontal.Right:
                    anchorMin.x = 1;
                    anchorMax.x = 1;
                    break;
                case QuickType.Horizontal.Scale:
                    anchorMin.x = 0;
                    anchorMax.x = 1;
                    break;
                default:
                    break;
            }

            switch (document.Constraints.Vertical)
            {
                case QuickType.Vertical.Bottom:
                    anchorMin.y = 0;
                    anchorMax.y = 0;
                    break;
                case QuickType.Vertical.Center:
                    anchorMin.y = .5f;
                    anchorMax.y = .5f;
                    break;
                case QuickType.Vertical.Scale:
                    anchorMin.y = 0;
                    anchorMax.y = 1;
                    break;
                case QuickType.Vertical.Top:
                    anchorMin.y = 1;
                    anchorMax.y = 1;
                    break;
                case QuickType.Vertical.TopBottom:
                    break;
                default:
                    break;
            }
        }

        var rectTransform = go.AddComponent<RectTransform>();

        //rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        //rectTransform.anchoredPosition = new Vector2(absoluteBoundingBox.X, -absoluteBoundingBox.Y);
        //rectTransform.sizeDelta = new Vector2(absoluteBoundingBox.Width, absoluteBoundingBox.Height);

        //rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, absoluteBoundingBox.Width);
        //rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, absoluteBoundingBox.Height);

        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, absoluteBoundingBox.X, absoluteBoundingBox.Width);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, absoluteBoundingBox.Y, absoluteBoundingBox.Height);

        //rectTransform.position = new Vector3(absoluteBoundingBox.X, absoluteBoundingBox.Y, 0);
    }

    private void BuildFills(QuickType.Document document, GameObject go)
    {
        if (document.Fills == null)
        {
            return;
        }

        if (document.Fills.Length > 0)
        {
            if (document.Fills.Length > 1)
            {
                Debug.LogWarning($"More than one fill detected on {go.name}, using first.");
            }

            var image = go.AddComponent<UnityEngine.UI.Image>();

            if (document.Name == "**** INVITE PEOPLE LIST ****")
            {
                Debug.Log("");
            }

            var firstFill = document.Fills[0];
            var color = firstFill.Color;

            if (color == null)
            {
                // Fill type may be 'image'
                image.color = Color.cyan;
            }
            else
            {
                image.color = new Color(color.R, color.G, color.B, color.A);
            }

            image.enabled = firstFill.Visible;
        }
    }

    private GameObject BuildText(QuickType.Document document)
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

    private TextAlignmentOptions TextAlignment(QuickType.Document document)
    {
        TextAlignmentOptions tao = TextAlignmentOptions.TopLeft;

        switch (document.Style.TextAlignHorizontal)
        {
            case QuickType.TextAlignHorizontal.Center:
                switch (document.Style.TextAlignVertical)
                {
                    case QuickType.TextAlignVertical.Bottom:
                        tao = TextAlignmentOptions.Bottom;
                        break;
                    case QuickType.TextAlignVertical.Center:
                        tao = TextAlignmentOptions.Center;
                        break;
                    case QuickType.TextAlignVertical.Top:
                        tao = TextAlignmentOptions.Top;
                        break;
                    default:
                        break;
                }
                break;
            case QuickType.TextAlignHorizontal.Justified:
                break;
            case QuickType.TextAlignHorizontal.Left:
                switch (document.Style.TextAlignVertical)
                {
                    case QuickType.TextAlignVertical.Bottom:
                        tao = TextAlignmentOptions.BottomLeft;
                        break;
                    case QuickType.TextAlignVertical.Center:
                        tao = TextAlignmentOptions.Left;
                        break;
                    case QuickType.TextAlignVertical.Top:
                        tao = TextAlignmentOptions.TopLeft;
                        break;
                    default:
                        break;
                }
                tao = TextAlignmentOptions.Left;
                break;
            case QuickType.TextAlignHorizontal.Right:
                switch (document.Style.TextAlignVertical)
                {
                    case QuickType.TextAlignVertical.Bottom:
                        tao = TextAlignmentOptions.BottomRight;
                        break;
                    case QuickType.TextAlignVertical.Center:
                        tao = TextAlignmentOptions.Right;
                        break;
                    case QuickType.TextAlignVertical.Top:
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

    private GameObject BuildCanvas(QuickType.Document document)
    {
        var go = BuildBase(document);
        go.AddComponent<Canvas>();
        //var rt = go.GetComponent<RectTransform>();
        //rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 1920);
        //rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 1080);
        BuildBackground(document, go);
        return go;
    }

    private void BuildBackground(QuickType.Document document, GameObject go)
    {
        var image = go.AddComponent<UnityEngine.UI.Image>();

        image.color = new Color(
            document.BackgroundColor.R,
            document.BackgroundColor.G,
            document.BackgroundColor.B,
            document.BackgroundColor.A);

        image.enabled = false;
    }

    private GameObject BuildFrame(QuickType.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        BuildFills(document, go);
        return go;
    }

    private GameObject BuildGroup(QuickType.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        BuildFills(document, go);
        return go;
    }

    private GameObject BuildRectange(QuickType.Document document)
    {
        var go = BuildBase(document);
        SetupRect(document, go);
        BuildFills(document, go);
        return go;
    }

    [ContextMenu("Get Figma")]
    public async void GetFigma()
    {
        var first = EditorApplication.isPlayingOrWillChangePlaymode;
        client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Figma-Token", FigmaToken);
        var jsonRespose = await GetFigmaRespose();
        var fileResponse = QuickType.FileResponse.FromJson(jsonRespose);

        BuildDocument(fileResponse.Document);
    }

    private void OnEnable()
    {
        GetFigma();
    }
}
