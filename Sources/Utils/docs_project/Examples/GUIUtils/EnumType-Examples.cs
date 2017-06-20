﻿// Kerbal Development tools - Examples
// Author: igor.zavoychinskiy@gmail.com
// This software is distributed under Public domain license.

using KSPDev.LogUtils;
using KSPDev.GUIUtils;

namespace Examples {

#region EnumTypeDemo1
public class EnumTypeDemo1 : PartModule {
  enum MyEnum {
    One,
    Two,
    Three
  }
  
  // This message uses a enum type as a parameter.
  static readonly Message<EnumType<MyEnum>> msg1 = new Message<EnumType<MyEnum>>(
      "#EnumTypeDemo_msg1", defaultTemplate: "Enum value is: <<0[-ONE-/-TWO-/-THREE-]>>");

  // Depending on the current language in the system, this method will present the values in
  // different languages.
  void ShowDistance() {
    HostedDebugLog.Info(this, msg1.Format(MyEnum.One));
    // Prints: "Enum value is: -ONE-"
    HostedDebugLog.Info(this, msg1.Format(MyEnum.Two));
    // Prints: "Enum value is: -TWO-"
    HostedDebugLog.Info(this, msg1.Format(MyEnum.Three));
    // Prints: "Enum value is: -THREE-"

    // This will work due to implicit conversion to the enum type.
    var wrapper = new EnumType<MyEnum>(MyEnum.One);
    PrintEnum(wrapper);
    // Prints: "Value is: One"
  }

  void PrintEnum(MyEnum value) {
    HostedDebugLog.Info(this, "Value is: {0}", value);
  }
}
#endregion

}  // namespace