Tail & TailUI
=================

Tail is a real-time file monitor and viewer, is a Windows port of the UNIX 'tail -f' command.

Tail can display the last part of a large file quickly without loading the entire file. And it can be used to view the end of a growing file. Tail a file viewer only, it makes no changes to file.

TailUI is the GUI program of Tail.


Options
----------

**-f, --follow[=name]** 
Output appended data as the file grows; -f, --follow, and --follow=name are equivalent.

**-r, --retry** 
Keep trying to open a file even if it is inaccessible when tail starts or if it becomes inaccessible later; useful when following by name, i.e., with --follow=name.

**-F** same as --follow=name --retry

**-n, --lines=N**
Output the last N lines, instead of the last 10.

**-s, --sleep-interval=S**
With -f, sleep for approximately S seconds (default 1) between iterations.

**-h, --help**
Display this help and exit.

**-v, --version**
Output version information and exit.


Environment Requirement
-----------------------

Microsoft .NET Framework 4

Bug tracker
-----------

Have a bug? Please create an issue here on GitHub!

+ https://github.com/gaochundong/Tail/issues


Authors
-------

**Dennis Gao** 

+ http://about.me/gaochundong


Copyright and license
---------------------

Copyright (c) 2011-2012 Chundong Gao
All rights reserved.

Licensed under the BSD License.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
