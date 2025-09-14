using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TradingJournal.Core.Logic.Manager
{
    public class ResponsiveLayoutManager
    {
        private readonly Form _hostForm;
        private readonly Dictionary<Control, ControlLayoutInfo> _controlLayouts = new Dictionary<Control, ControlLayoutInfo>();
        private FormWindowStateExtended _currentState = FormWindowStateExtended.Normal;

        private readonly List<Action> _normalStateActions = new List<Action>();
        private readonly List<Action> _maximizedStateActions = new List<Action>();

        // THE FIX: Change Panel to the more generic Control type.
        private class ControlLayoutInfo
        {
            public Control NormalParent { get; set; }
            public Point NormalLocation { get; set; }
            public Size NormalSize { get; set; }

            public Control MaximizedParent { get; set; }
            public Point MaximizedLocation { get; set; }
            public Size MaximizedSize { get; set; }
        }

        public ResponsiveLayoutManager(Form hostForm)
        {
            _hostForm = hostForm;
        }

        // THE FIX: Change Panel to Control in the method signature.
        public void RegisterControl(Control control, Control normalParent, Control maximizedParent, Point maximizedLocation, Size maximizedSize)
        {
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
            if (state == FormWindowStateExtended.Normal) _normalStateActions.Add(action);
            else _maximizedStateActions.Add(action);
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;

            _hostForm.SuspendLayout();

            foreach (var entry in _controlLayouts)
            {
                var control = entry.Key;
                var layoutInfo = entry.Value;

                if (newState == FormWindowStateExtended.Maximized)
                {
                    // Check if the parent needs to change before re-parenting
                    if (control.Parent != layoutInfo.MaximizedParent)
                    {
                        layoutInfo.MaximizedParent.Controls.Add(control);
                    }
                    control.Location = layoutInfo.MaximizedLocation;
                    control.Size = layoutInfo.MaximizedSize;
                }
                else
                {
                    if (control.Parent != layoutInfo.NormalParent)
                    {
                        layoutInfo.NormalParent.Controls.Add(control);
                    }
                    control.Location = layoutInfo.NormalLocation;
                    control.Size = layoutInfo.NormalSize;
                }
            }

            if (newState == FormWindowStateExtended.Maximized)
            {
                foreach (var action in _maximizedStateActions) action.Invoke();
            }
            else
            {
                foreach (var action in _normalStateActions) action.Invoke();
            }

            ToggleParentPanelVisibility(newState);
            _hostForm.ResumeLayout(true);
        }

        private void ToggleParentPanelVisibility(FormWindowStateExtended state)
        {
            // This logic now correctly handles any Control, including Panels.
            var allParents = _controlLayouts.Values
                .Select(info => info.NormalParent)
                .Concat(_controlLayouts.Values.Select(info => info.MaximizedParent))
                .Where(p => p is Panel) // We only need to toggle visibility of Panels
                .Distinct();

            foreach (var parent in allParents)
            {
                bool isNormalParent = _controlLayouts.Values.Any(info => info.NormalParent == parent);
                bool isMaximizedParent = _controlLayouts.Values.Any(info => info.MaximizedParent == parent);

                if (state == FormWindowStateExtended.Normal)
                {
                    parent.Visible = isNormalParent;
                }
                else // Maximized
                {
                    parent.Visible = isMaximizedParent;
                }
            }
        }
    }
}
