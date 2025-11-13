using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TradingJournal.Core.Logic.Manager
{
    /// <summary>
    /// Works with BOTH Form and UserControl.
    /// Host is any Control (Form, UserControl, Panel, etc.).
    /// </summary>
    public class ResponsiveLayoutManager
    {
        private readonly Control _host; // was Form
        private readonly Dictionary<Control, ControlLayoutInfo> _controlLayouts = new();
        private FormWindowStateExtended _currentState = FormWindowStateExtended.Normal;

        private readonly List<Action> _normalStateActions = new();
        private readonly List<Action> _maximizedStateActions = new();

        private class ControlLayoutInfo
        {
            public Control NormalParent { get; set; } = null!;
            public Point NormalLocation { get; set; }
            public Size NormalSize { get; set; }

            public Control MaximizedParent { get; set; } = null!;
            public Point MaximizedLocation { get; set; }
            public Size MaximizedSize { get; set; }
        }

        public ResponsiveLayoutManager(Control host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
        }

        public void RegisterControl(Control control, Control normalParent, Control maximizedParent,
                                    Point maximizedLocation, Size maximizedSize)
        {
            if (control is null) throw new ArgumentNullException(nameof(control));
            if (_controlLayouts.ContainsKey(control)) return;

            _controlLayouts[control] = new ControlLayoutInfo
            {
                NormalParent = normalParent,
                NormalLocation = control.Location,
                NormalSize = control.Size,
                MaximizedParent = maximizedParent,
                MaximizedLocation = maximizedLocation,
                MaximizedSize = maximizedSize
            };
        }

        public void RegisterStateAction(FormWindowStateExtended state, Action action)
        {
            if (action is null) return;
            if (state == FormWindowStateExtended.Normal) _normalStateActions.Add(action);
            else _maximizedStateActions.Add(action);
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;

            _host.SuspendLayout();

            foreach (var (control, li) in _controlLayouts)
            {
                if (newState == FormWindowStateExtended.Maximized)
                {
                    if (control.Parent != li.MaximizedParent)
                        li.MaximizedParent.Controls.Add(control);

                    control.Location = li.MaximizedLocation;
                    control.Size = li.MaximizedSize;
                }
                else
                {
                    if (control.Parent != li.NormalParent)
                        li.NormalParent.Controls.Add(control);

                    control.Location = li.NormalLocation;
                    control.Size = li.NormalSize;
                }
            }

            if (newState == FormWindowStateExtended.Maximized)
                foreach (var a in _maximizedStateActions) a();
            else
                foreach (var a in _normalStateActions) a();

            ToggleParentPanelVisibility(newState);

            _host.ResumeLayout(true);
        }

        private void ToggleParentPanelVisibility(FormWindowStateExtended state)
        {
            // Only toggle Panels (don’t hide random Controls).
            var parents = _controlLayouts.Values
                .Select(v => v.NormalParent).Concat(_controlLayouts.Values.Select(v => v.MaximizedParent))
                .OfType<Panel>()
                .Distinct()
                .ToList();

            foreach (var parent in parents)
            {
                // visible if it contains at least one child in the current state
                bool shouldBeVisible =
                    state == FormWindowStateExtended.Normal
                        ? _controlLayouts.Values.Any(v => v.NormalParent == parent)
                        : _controlLayouts.Values.Any(v => v.MaximizedParent == parent);

                parent.Visible = shouldBeVisible;
            }
        }
    }
}
