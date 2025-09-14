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

        private class ControlLayoutInfo
        {
            public Panel NormalParent { get; set; }
            public Point NormalLocation { get; set; }
            public Size NormalSize { get; set; }
            public Panel MaximizedParent { get; set; }
            public Point MaximizedLocation { get; set; }
            public Size MaximizedSize { get; set; }
        }

        public ResponsiveLayoutManager(Form hostForm)
        {
            _hostForm = hostForm;
        }

        public void RegisterControl(Control control, Panel normalParent, Panel maximizedParent, Point maximizedLocation, Size maximizedSize)
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
            if (state == FormWindowStateExtended.Normal)
            {
                _normalStateActions.Add(action);
            }
            else
            {
                _maximizedStateActions.Add(action);
            }
        }

        public void SetWindowState(FormWindowStateExtended newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;

            _hostForm.SuspendLayout();

            // *** THIS IS THE CORRECTED LOOP ***
            foreach (var entry in _controlLayouts)
            {
                var control = entry.Key;
                var layoutInfo = entry.Value;

                if (newState == FormWindowStateExtended.Maximized)
                {
                    // Switch to Maximized State
                    layoutInfo.MaximizedParent.Controls.Add(control); // Re-parent the control
                    control.Location = layoutInfo.MaximizedLocation;
                    control.Size = layoutInfo.MaximizedSize;
                }
                else
                {
                    // Switch back to Normal State
                    layoutInfo.NormalParent.Controls.Add(control); // Re-parent the control
                    control.Location = layoutInfo.NormalLocation;
                    control.Size = layoutInfo.NormalSize;
                }
            }

            // Execute custom actions for the new state
            if (newState == FormWindowStateExtended.Maximized)
            {
                foreach (var action in _maximizedStateActions)
                {
                    action.Invoke();
                }
            }
            else
            {
                foreach (var action in _normalStateActions)
                {
                    action.Invoke();
                }
            }

            ToggleParentPanelVisibility(newState);
            _hostForm.ResumeLayout(true);
        }

        private void ToggleParentPanelVisibility(FormWindowStateExtended state)
        {
            var allPanels = _controlLayouts.Values.Select(info => info.NormalParent).Concat(_controlLayouts.Values.Select(info => info.MaximizedParent)).Distinct();
            foreach (var panel in allPanels)
            {
                bool isNormalParent = _controlLayouts.Values.Any(info => info.NormalParent == panel);
                bool isMaximizedParent = _controlLayouts.Values.Any(info => info.MaximizedParent == panel);
                if (state == FormWindowStateExtended.Normal)
                {
                    panel.Visible = isNormalParent;
                }
                else // Maximized
                {
                    panel.Visible = isMaximizedParent;
                }
            }
        }
    }
}
