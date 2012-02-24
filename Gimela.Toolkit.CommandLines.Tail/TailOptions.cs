﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Gimela.Toolkit.CommandLines.Tail
{
	internal static class TailOptions
	{
		public static readonly ReadOnlyCollection<string> RetryOptions;
		public static readonly ReadOnlyCollection<string> FollowOptions;
		public static readonly ReadOnlyCollection<string> FollowRetryOptions;
		public static readonly ReadOnlyCollection<string> OutputLinesOptions;
		public static readonly ReadOnlyCollection<string> SleepIntervalOptions;
		public static readonly ReadOnlyCollection<string> HelpOptions;
		public static readonly ReadOnlyCollection<string> VersionOptions;

		public static readonly IDictionary<TailOptionType, ICollection<string>> Options;

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static TailOptions()
		{
			RetryOptions = new ReadOnlyCollection<string>(new string[] { "r", "retry" });
			FollowOptions = new ReadOnlyCollection<string>(new string[] { "f", "follow" });
			FollowRetryOptions = new ReadOnlyCollection<string>(new string[] { "F" });
			OutputLinesOptions = new ReadOnlyCollection<string>(new string[] { "n", "lines" });
			SleepIntervalOptions = new ReadOnlyCollection<string>(new string[] { "s", "sleep-interval" });
			HelpOptions = new ReadOnlyCollection<string>(new string[] { "h", "help" });
			VersionOptions = new ReadOnlyCollection<string>(new string[] { "v", "version" });

			Options = new Dictionary<TailOptionType, ICollection<string>>();
			Options.Add(TailOptionType.Retry, RetryOptions);
			Options.Add(TailOptionType.Follow, FollowOptions);
			Options.Add(TailOptionType.FollowRetry, FollowRetryOptions);
			Options.Add(TailOptionType.OutputLines, OutputLinesOptions);
			Options.Add(TailOptionType.SleepInterval, SleepIntervalOptions);
			Options.Add(TailOptionType.Help, HelpOptions);
			Options.Add(TailOptionType.Version, VersionOptions);
		}

		public static List<string> GetSingleOptions()
		{
			List<string> singleOptionList = new List<string>();

			singleOptionList.AddRange(TailOptions.RetryOptions);
			singleOptionList.AddRange(TailOptions.HelpOptions);
			singleOptionList.AddRange(TailOptions.VersionOptions);

			return singleOptionList;
		}

		#region Usage

		public const string Version = @"Tail v1.0";
		public static readonly string Usage = string.Format(CultureInfo.CurrentCulture, @"
NAME

	tail - output the last part of a file

SYNOPSIS

	tail [OPTION]... [FILE]

DESCRIPTION

	Print the last 10 lines of the FILE to standard output.

OPTIONS

	-f, --follow[=name]
	{0}{0}Output appended data as the file grows; -f, --follow, and 
	{0}{0}--follow=name are equivalent.
	-r, --retry
	{0}{0}Keep trying to open a file even if it is inaccessible when tail
	{0}{0}starts or if it becomes inaccessible later; useful when 
	{0}{0}following by name, i.e., with --follow=name.
	-F same as --follow=name --retry
	-n, --lines=N
	{0}{0}Output the last N lines, instead of the last 10.
	-s, --sleep-interval=S
	{0}{0}With -f, sleep for approximately S seconds (default 1) between
	{0}{0}iterations.
	-h, --help 
	{0}{0}Display this help and exit.
	-v, --version
	{0}{0}Output version information and exit.

AUTHOR

	Written by Chundong Gao.

REPORTING BUGS

	Report bugs to <gaochundong@gmail.com>.

COPYRIGHT

	Copyright (C) 2012 Chundong Gao, All Rights Reserved.
", @" ");

		#endregion

		public static TailOptionType GetOptionType(string option)
		{
			TailOptionType optionType = TailOptionType.None;

			foreach (var pair in Options)
			{
				foreach (var item in pair.Value)
				{
					if (item == option)
					{
						optionType = pair.Key;
						break;
					}
				}
			}

			return optionType;
		}
	}
}
