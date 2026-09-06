using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

public enum ALogFrameKind
{
    Application,
    Package,
    NoSource,
}

public enum ALogFrameFilter
{
    All,
    Application,
    Package,
    NoSource,
}

/// <summary>
/// 调用堆栈的可视化:把栈帧画成自上而下的调用时间线,
/// 最上方是最外层调用者,最下方是日志发生的位置。
/// </summary>
public class ALogStackGraph : VisualElement
{
    private readonly List<ALogFrame> m_frames = new List<ALogFrame>();
    private readonly HashSet<ALogFrame> m_collapsedFrames = new HashSet<ALogFrame>();
    private ALogFrameFilter m_filter;

    public event Action<int, int> VisibleCountChanged;

    public int VisibleCount { get; private set; }
    public int TotalCount => m_frames.Count;

    public ALogStackGraph() {
        AddToClassList("stack-graph");
    }

    public void SetFrames(List<ALogFrame> frames) {
        m_frames.Clear();
        if (frames != null)
        {
            m_frames.AddRange(frames);
        }
        m_collapsedFrames.Clear();
        Rebuild();
    }

    public void SetFilter(ALogFrameFilter filter) {
        if (m_filter == filter)
        {
            return;
        }
        m_filter = filter;
        Rebuild();
    }

    public void SetAllExpanded(bool expanded) {
        m_collapsedFrames.Clear();
        if (!expanded)
        {
            foreach (ALogFrame frame in m_frames)
            {
                m_collapsedFrames.Add(frame);
            }
        }
        Rebuild();
    }

    public static ALogFrameKind Classify(ALogFrame frame) {
        if (frame == null || string.IsNullOrEmpty(frame.FilePath))
        {
            return ALogFrameKind.NoSource;
        }
        string path = frame.FilePath.Replace('\\', '/');
        if (path.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
        {
            return ALogFrameKind.Application;
        }
        if (path.StartsWith("Packages/", StringComparison.OrdinalIgnoreCase)
            || path.StartsWith("Library/PackageCache/", StringComparison.OrdinalIgnoreCase))
        {
            return ALogFrameKind.Package;
        }
        return ALogFrameKind.NoSource;
    }

    public static bool MatchesFilter(ALogFrame frame, ALogFrameFilter filter) {
        if (filter == ALogFrameFilter.All)
        {
            return true;
        }
        switch (filter)
        {
            case ALogFrameFilter.Application:
                return Classify(frame) == ALogFrameKind.Application;
            case ALogFrameFilter.Package:
                return Classify(frame) == ALogFrameKind.Package;
            case ALogFrameFilter.NoSource:
                return Classify(frame) == ALogFrameKind.NoSource;
            default:
                return false;
        }
    }

    /// <summary>frames 保持 Unity 原始的由内向外顺序,越靠前越接近日志发生位置。</summary>
    public static ALogFrame FindRootCause(IList<ALogFrame> frames) {
        if (frames == null || frames.Count == 0)
        {
            return null;
        }
        foreach (ALogFrame frame in frames)
        {
            if (Classify(frame) == ALogFrameKind.Application)
            {
                return frame;
            }
        }
        foreach (ALogFrame frame in frames)
        {
            if (frame != null && frame.CanJump)
            {
                return frame;
            }
        }
        return frames[0];
    }

    private void Rebuild() {
        Clear();

        var visibleFrames = new List<ALogFrame>();
        for (int i = m_frames.Count - 1; i >= 0; i--)
        {
            if (MatchesFilter(m_frames[i], m_filter))
            {
                visibleFrames.Add(m_frames[i]);
            }
        }

        VisibleCount = visibleFrames.Count;
        VisibleCountChanged?.Invoke(VisibleCount, TotalCount);

        if (m_frames.Count == 0)
        {
            Add(CreateEmptyState("No stack information"));
            return;
        }
        if (visibleFrames.Count == 0)
        {
            Add(CreateEmptyState("No frames match the current filter"));
            return;
        }

        ALogFrame rootCause = FindRootCause(m_frames);
        for (int i = 0; i < visibleFrames.Count; i++)
        {
            ALogFrame frame = visibleFrames[i];
            int order = m_frames.Count - m_frames.IndexOf(frame);
            Add(CreateFrameRow(frame, order, i == 0, i == visibleFrames.Count - 1, ReferenceEquals(frame, rootCause)));
        }
    }

    private VisualElement CreateEmptyState(string text) {
        var empty = new Label(text);
        empty.AddToClassList("stack-graph-empty");
        return empty;
    }

    private VisualElement CreateFrameRow(ALogFrame frame, int order, bool isFirst, bool isLast, bool isRootCause) {
        var row = new VisualElement();
        row.AddToClassList("stack-frame");

        var timeline = new VisualElement();
        timeline.AddToClassList("stack-frame__timeline");
        if (isFirst)
        {
            timeline.AddToClassList("stack-frame__timeline--first");
        }
        if (isLast)
        {
            timeline.AddToClassList("stack-frame__timeline--last");
        }

        var rail = new VisualElement();
        rail.AddToClassList("stack-frame__rail");
        timeline.Add(rail);

        var marker = new Label(order.ToString());
        marker.AddToClassList("stack-frame__marker");
        if (isRootCause)
        {
            marker.AddToClassList("stack-frame__marker--root-cause");
        }
        timeline.Add(marker);
        row.Add(timeline);

        var card = new VisualElement();
        card.AddToClassList("stack-frame__card");
        if (isRootCause)
        {
            card.AddToClassList("stack-frame__card--root-cause");
        }

        var header = new VisualElement();
        header.AddToClassList("stack-frame__header");

        var signature = new Label(frame?.Signature ?? string.Empty);
        signature.tooltip = frame?.Signature;
        signature.AddToClassList("stack-frame__signature");
        header.Add(signature);

        var frameKind = Classify(frame);
        var kind = new Label(GetKindText(frameKind));
        kind.AddToClassList("stack-frame__kind");
        kind.AddToClassList(GetKindClass(frameKind));
        header.Add(kind);

        bool expanded = !m_collapsedFrames.Contains(frame);
        var toggle = new Button { text = expanded ? "Collapse" : "Expand", tooltip = expanded ? "Hide source location" : "Show source location" };
        toggle.AddToClassList("stack-frame__toggle");
        toggle.clicked += () => {
            if (m_collapsedFrames.Contains(frame))
            {
                m_collapsedFrames.Remove(frame);
            }
            else
            {
                m_collapsedFrames.Add(frame);
            }
            Rebuild();
        };
        header.Add(toggle);
        card.Add(header);

        if (expanded)
        {
            var details = new VisualElement();
            details.AddToClassList("stack-frame__details");

            var location = new Label(frame?.Location ?? "<no source>");
            location.tooltip = frame?.Location;
            location.AddToClassList("stack-frame__location");
            details.Add(location);

            if (frame != null && frame.CanJump)
            {
                var openButton = new Button(() => ALogSourceJump.Open(frame)) { text = "Open Source" };
                openButton.AddToClassList("stack-frame__open");
                details.Add(openButton);
            }
            card.Add(details);
        }

        row.Add(card);
        return row;
    }

    private static string GetKindText(ALogFrameKind kind) {
        switch (kind)
        {
            case ALogFrameKind.Application:
                return "Application";
            case ALogFrameKind.Package:
                return "Package";
            default:
                return "No Source";
        }
    }

    private static string GetKindClass(ALogFrameKind kind) {
        switch (kind)
        {
            case ALogFrameKind.Application:
                return "stack-frame__kind--application";
            case ALogFrameKind.Package:
                return "stack-frame__kind--package";
            default:
                return "stack-frame__kind--no-source";
        }
    }
}
