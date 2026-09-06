using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>展示单条日志调用堆栈的可视化窗口,由内置 Console 工具栏的「堆栈图」按钮打开</summary>
public class ALogStackGraphWindow : EditorWindow
{
    public const string StyleSheetPath = "Assets/Scripts/Common/Log/Editor/ALogStackGraph.uss";

    private const string ThemePreferenceKey = "AChen.ALogStackGraph.Theme";
    private const string LightTheme = "light";
    private const string DarkTheme = "dark";

    private static readonly List<string> s_filterChoices = new List<string>
    {
        "All",
        "Application",
        "Packages",
        "No Source",
    };

    private string m_title;
    private List<ALogFrame> m_frames;
    private ALogStackGraph m_graph;
    private Label m_errorTitle;
    private Label m_errorDetails;
    private VisualElement m_rootCausePanel;
    private Label m_rootCauseSignature;
    private Label m_rootCauseLocation;
    private Label m_countLabel;
    private Button m_expandButton;
    private Button m_themeButton;
    private bool m_allExpanded = true;
    private string m_theme;

    public static void Show(string title, List<ALogFrame> frames) {
        var window = GetWindow<ALogStackGraphWindow>(true, "Call Stack Graph");
        window.minSize = new Vector2(720f, 480f);
        window.m_title = title;
        window.m_frames = frames;
        window.Rebuild();
    }

    private void CreateGUI() {
        rootVisualElement.Clear();
        var style = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
        if (style != null)
        {
            rootVisualElement.styleSheets.Add(style);
        }
        rootVisualElement.AddToClassList("stack-graph-root");

        m_theme = LoadTheme();
        ApplyTheme();
        BuildToolbar();
        BuildErrorPanel();
        BuildRootCausePanel();

        var summary = new VisualElement();
        summary.AddToClassList("stack-summary");
        m_countLabel = new Label();
        m_countLabel.AddToClassList("stack-summary__count");
        summary.Add(m_countLabel);
        rootVisualElement.Add(summary);

        m_graph = new ALogStackGraph();
        m_graph.VisibleCountChanged += UpdateCount;
        var scroll = new ScrollView();
        scroll.AddToClassList("stack-scroll");
        scroll.Add(m_graph);
        rootVisualElement.Add(scroll);

        Rebuild();
    }

    private void BuildToolbar() {
        var toolbar = new VisualElement();
        toolbar.AddToClassList("stack-toolbar");

        var mark = new Label("≡");
        mark.AddToClassList("stack-toolbar__mark");
        toolbar.Add(mark);

        var title = new Label("Call Stack Graph");
        title.AddToClassList("stack-toolbar__title");
        toolbar.Add(title);

        var spacer = new VisualElement();
        spacer.AddToClassList("stack-toolbar__spacer");
        toolbar.Add(spacer);

        var copyButton = new Button(CopyStack) { text = "Copy" };
        copyButton.AddToClassList("stack-toolbar__button");
        toolbar.Add(copyButton);

        var filter = new DropdownField(s_filterChoices, 0);
        filter.AddToClassList("stack-toolbar__filter");
        filter.RegisterValueChangedCallback(evt => m_graph?.SetFilter(ToFilter(evt.newValue)));
        toolbar.Add(filter);

        m_expandButton = new Button(ToggleExpandAll);
        m_expandButton.AddToClassList("stack-toolbar__button");
        toolbar.Add(m_expandButton);

        m_themeButton = new Button(ToggleTheme);
        m_themeButton.AddToClassList("stack-toolbar__button");
        toolbar.Add(m_themeButton);

        rootVisualElement.Add(toolbar);
        UpdateToolbarText();
    }

    private void BuildErrorPanel() {
        var panel = new VisualElement();
        panel.AddToClassList("stack-error");

        var icon = new Label("×");
        icon.AddToClassList("stack-error__icon");
        panel.Add(icon);

        var content = new VisualElement();
        content.AddToClassList("stack-error__content");
        m_errorTitle = new Label();
        m_errorTitle.AddToClassList("stack-error__title");
        content.Add(m_errorTitle);
        m_errorDetails = new Label();
        m_errorDetails.AddToClassList("stack-error__details");
        content.Add(m_errorDetails);
        panel.Add(content);

        rootVisualElement.Add(panel);
    }

    private void BuildRootCausePanel() {
        m_rootCausePanel = new VisualElement();
        m_rootCausePanel.AddToClassList("stack-root-cause");

        var icon = new Label("!");
        icon.AddToClassList("stack-root-cause__icon");
        m_rootCausePanel.Add(icon);

        var content = new VisualElement();
        content.AddToClassList("stack-root-cause__content");
        var heading = new Label("Likely Root Cause");
        heading.AddToClassList("stack-root-cause__heading");
        content.Add(heading);
        m_rootCauseSignature = new Label();
        m_rootCauseSignature.AddToClassList("stack-root-cause__signature");
        content.Add(m_rootCauseSignature);
        m_rootCauseLocation = new Label();
        m_rootCauseLocation.AddToClassList("stack-root-cause__location");
        content.Add(m_rootCauseLocation);
        m_rootCausePanel.Add(content);

        rootVisualElement.Add(m_rootCausePanel);
    }

    private void Rebuild() {
        if (m_graph == null)
        {
            return;
        }

        SplitTitle(m_title, out string summary, out string details);
        m_errorTitle.text = summary;
        m_errorDetails.text = details;
        m_errorDetails.style.display = string.IsNullOrEmpty(details) ? DisplayStyle.None : DisplayStyle.Flex;

        ALogFrame rootCause = ALogStackGraph.FindRootCause(m_frames);
        m_rootCausePanel.style.display = rootCause == null ? DisplayStyle.None : DisplayStyle.Flex;
        if (rootCause != null)
        {
            m_rootCauseSignature.text = rootCause.Signature;
            m_rootCauseSignature.tooltip = rootCause.Signature;
            m_rootCauseLocation.text = rootCause.Location;
            m_rootCauseLocation.tooltip = rootCause.Location;
        }

        m_allExpanded = true;
        UpdateToolbarText();
        m_graph.SetFrames(m_frames);
    }

    public static void SplitTitle(string title, out string summary, out string details) {
        if (string.IsNullOrWhiteSpace(title))
        {
            summary = "No log selected";
            details = string.Empty;
            return;
        }
        int separator = title.IndexOf('：');
        int separatorLength = 1;
        if (separator < 0)
        {
            separator = title.IndexOf(": ", System.StringComparison.Ordinal);
            separatorLength = 2;
        }
        if (separator < 0)
        {
            summary = title.Trim();
            details = string.Empty;
            return;
        }
        summary = title.Substring(0, separator).Trim();
        details = title.Substring(separator + separatorLength).Trim();
    }

    public static string BuildCopyText(string title, IList<ALogFrame> frames) {
        var builder = new StringBuilder();
        builder.AppendLine(string.IsNullOrWhiteSpace(title) ? "No log selected" : title.Trim());
        if (frames == null)
        {
            return builder.ToString().TrimEnd();
        }
        for (int i = frames.Count - 1; i >= 0; i--)
        {
            int order = frames.Count - i;
            ALogFrame frame = frames[i];
            builder.Append(order).Append(". ").AppendLine(frame?.Signature ?? string.Empty);
            builder.Append("   ").AppendLine(frame?.Location ?? "<no source>");
        }
        return builder.ToString().TrimEnd();
    }

    private void CopyStack() {
        EditorGUIUtility.systemCopyBuffer = BuildCopyText(m_title, m_frames);
    }

    private void ToggleExpandAll() {
        m_allExpanded = !m_allExpanded;
        m_graph.SetAllExpanded(m_allExpanded);
        UpdateToolbarText();
    }

    private void ToggleTheme() {
        m_theme = m_theme == LightTheme ? DarkTheme : LightTheme;
        EditorPrefs.SetString(ThemePreferenceKey, m_theme);
        ApplyTheme();
        UpdateToolbarText();
    }

    private string LoadTheme() {
        string saved = EditorPrefs.GetString(ThemePreferenceKey, string.Empty);
        if (saved == LightTheme || saved == DarkTheme)
        {
            return saved;
        }
        return EditorGUIUtility.isProSkin ? DarkTheme : LightTheme;
    }

    private void ApplyTheme() {
        rootVisualElement.RemoveFromClassList("stack-theme--light");
        rootVisualElement.RemoveFromClassList("stack-theme--dark");
        rootVisualElement.AddToClassList(m_theme == LightTheme ? "stack-theme--light" : "stack-theme--dark");
    }

    private void UpdateToolbarText() {
        if (m_expandButton != null)
        {
            m_expandButton.text = m_allExpanded ? "Collapse All" : "Expand All";
        }
        if (m_themeButton != null)
        {
            m_themeButton.text = m_theme == LightTheme ? "Dark Theme" : "Light Theme";
        }
    }

    private void UpdateCount(int visible, int total) {
        if (m_countLabel != null)
        {
            m_countLabel.text = $"Showing {visible} / {total} frames";
        }
    }

    private static ALogFrameFilter ToFilter(string choice) {
        switch (choice)
        {
            case "Application":
                return ALogFrameFilter.Application;
            case "Packages":
                return ALogFrameFilter.Package;
            case "No Source":
                return ALogFrameFilter.NoSource;
            default:
                return ALogFrameFilter.All;
        }
    }
}
