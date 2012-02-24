﻿/*
 * [The "BSD Licence"]
 * Copyright (c) 2011-2012 Chundong Gao
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. The name of the author may not be used to endorse or promote products
 *    derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Gimela.Toolkit.CommandLines.Foundation;
using Gimela.Toolkit.CommandLines.Tail;

namespace Gimela.Toolkit.CommandLines.TailUI
{
  public partial class MainWindow : Window
  {
    private const string TailCommand = @"Tail";
    private const string CancelCommand = @"Cancel";

    private TailCommandLine tail = null;

    public MainWindow()
    {
      InitializeComponent();
    }

    private void OnOpenFileButtonClick(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
      dlg.Filter = "Log files (*.log)|*.log|Text documents (*.txt)|*.txt|All files (*.*)|*.*"; 
      dlg.FilterIndex = 3;

      Nullable<bool> result = dlg.ShowDialog();
      if (result == true)
      {
        tbFileName.Text = dlg.FileName;
        OnTailButtonClick(sender, new RoutedEventArgs());
      }
    }

    private void OnTailButtonClick(object sender, RoutedEventArgs e)
    {
      if (btnTail.Content.ToString() == TailCommand)
      {
        if (string.IsNullOrEmpty(tbFileName.Text))
        {
          MessageBox.Show(this, "Please specify a file for tailing.", this.Title,
            MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
          return;
        }

        btnTail.Content = CancelCommand;
        tbFileName.IsEnabled = false;
        btnOpenFile.IsEnabled = false;
        tbFileData.Document.Blocks.Clear();

        try
        {
          string[] args = new string[] { 
            @"-F",
            tbFileName.Text
          };

          tail = new TailCommandLine(args);
          tail.CommandLineException += new EventHandler<CommandLineExceptionEventArgs>(OnCommandLineException);
          tail.CommandLineDataChanged += new EventHandler<CommandLineDataChangedEventArgs>(OnCommandLineDataChanged);

          ThreadPool.QueueUserWorkItem((WaitCallback)TailExecuter, tail);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, ex.Message, this.Title,
            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
      }
      else
      {
        btnTail.Content = TailCommand;
        tbFileName.IsEnabled = true;
        btnOpenFile.IsEnabled = true;

        try
        {
          if (tail != null)
          {
            tail.CommandLineException -= new EventHandler<CommandLineExceptionEventArgs>(OnCommandLineException);
            tail.CommandLineDataChanged -= new EventHandler<CommandLineDataChangedEventArgs>(OnCommandLineDataChanged);
            tail.Terminate();
            tail.Dispose();
            tail = null;
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, ex.Message, this.Title,
            MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
      }
    }

    private void TailExecuter(object state)
    {
      TailCommandLine tcl = (TailCommandLine)state;
      tcl.Execute();

      while (tail != null && tail.IsExecuting) ;
    }

    private void OnCommandLineException(object sender, CommandLineExceptionEventArgs e)
    {
      this.Dispatcher.Invoke(DispatcherPriority.Normal,
        new Action(() =>
        {
          tbFileData.AppendText(e.Exception.Message);
          tbFileData.ScrollToEnd();
          OnTailButtonClick(sender, new RoutedEventArgs());
        }));
    }

    private void OnCommandLineDataChanged(object sender, CommandLineDataChangedEventArgs e)
    {
      this.Dispatcher.Invoke(DispatcherPriority.Normal,
        new Action(() =>
        {
          tbFileData.AppendText(e.Data);
          tbFileData.ScrollToEnd();
        }));
    }
  }
}
