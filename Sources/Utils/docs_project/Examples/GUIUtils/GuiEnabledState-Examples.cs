﻿// Kerbal Development tools - Examples
// Author: igor.zavoychinskiy@gmail.com
// This software is distributed under Public domain license.

using KSPDev.GUIUtils;
using UnityEngine;

namespace Examples {

public class GuiEnabledStateDemo1 {
  #region GuiEnabledStateDemo1_OnGUI
  void OnGUI() {
    GUI.enabled = true;
    using (new GuiEnabledState(false)) {
      // All the controls in this block will become disabled.
      GUILayout.Button("Disabled button 1");
      GUILayout.Button("Disabled button 2");
    }
    // From here the enabled state is True again.
    GUILayout.Button("Enabled button 3");
  }
  #endregion
}

}  // namespace
