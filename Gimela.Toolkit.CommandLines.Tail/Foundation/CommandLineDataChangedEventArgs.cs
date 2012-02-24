﻿using System;
using System.Globalization;

namespace Gimela.Toolkit.CommandLines.Foundation
{
  public class CommandLineDataChangedEventArgs : EventArgs
  {
    public string Data { get; set; }

    public CommandLineDataChangedEventArgs(string data)
    {
      Data = data;
    }

    public override string ToString()
    {
      return string.Format(CultureInfo.CurrentCulture, "Data : {0}", Data);
    }
  }
}
