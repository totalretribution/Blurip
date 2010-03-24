//BluRip - one click BluRay/m2ts to mkv converter
//Copyright (C) 2009-2010 _hawk_

//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation; either version 2
//of the License, or (at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

//Contact: hawk.ac@gmx.net

using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace BluRip
{
    public delegate void MsgHandler(string msg);

    public abstract class ExternalTool
    {
        public ExternalTool()
        {
            try
            {
                OnInfoMsg += new InfoEventHandler(EmptyEventHandler);
                OnLogMsg += new LogEventHandler(EmptyEventHandler);
            }
            catch (Exception)
            {
            }
        }

        ~ExternalTool()
        {
            try
            {
                Stop();
            }
            catch (Exception)
            {
            }
        }

        public bool IsAlive
        {
            get
            {
                try
                {
                    if (thread == null) return false;
                    else
                    {
                        return thread.IsAlive;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            
        }

        protected bool ok = false;
        protected int exitCode = -1;

        public bool Ok
        {
            get { return ok; }
        }

        public int ExitCode
        {
            get { return exitCode; }
        }

        public virtual void WaitForExit()
        {
            try
            {
                while (IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(5);
                }
            }
            catch (Exception)
            {
            }
        }

        public virtual void ToolThread()
        {
            Thread orThread = null;
            Thread or2Thread = null;
            ok = false;
            successfull = false;
            exitCode = -1;
            try
            {
                StartInfo();
                sb.Remove(0, sb.Length);
                pc = new Process();
                pc.StartInfo.FileName = Path; ;
                pc.StartInfo.Arguments = Parameter;

                Info("Command: " + Path + " " + Parameter);

                pc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.CreateNoWindow = true;
                pc.StartInfo.RedirectStandardOutput = true;
                pc.StartInfo.RedirectStandardError = true;

                if (!pc.Start())
                {
                    Info("Error starting " + Path);
                    return;
                }

                pc.PriorityClass = Priority;

                OutputReader or = new OutputReader(pc.StandardOutput, Log, sb);
                orThread = new Thread(or.Start);

                OutputReader or2 = new OutputReader(pc.StandardError, Log, sb);
                or2Thread = new Thread(or2.Start);

                orThread.Start();
                or2Thread.Start();

                orThread.Join();
                or2Thread.Join();

                pc.WaitForExit();
                Info("Return code: " + pc.ExitCode.ToString());
                exitCode = pc.ExitCode;
                pc.Close();
                pc = null;
                EndInfo();
                ok = true;
                if (StartProcessing())
                {                    
                    Process();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                try
                {
                    if (orThread != null)
                    {
                        orThread.Abort();
                        orThread = null;
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (or2Thread != null)
                    {
                        or2Thread.Abort();
                        or2Thread = null;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public virtual void Start()
        {
            try
            {
                thread = new Thread(ToolThread);
                thread.Start();
            }
            catch (Exception)
            {
            }
        }

        public virtual void Stop()
        {
            try
            {
                if (pc != null)
                {
                    pc.Kill();
                    pc.Close();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                pc = null;
            }
            try
            {
                if (thread != null)
                {
                    thread.Abort();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                thread = null;
            }
        }

        protected Process pc = null;
        protected Thread thread = null;
        protected string path = "";
        protected string parameter = "";
        protected ProcessPriorityClass priority = ProcessPriorityClass.Normal;

        public ProcessPriorityClass Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

        public class MsgEventArgs : EventArgs
        {
            private string message = "";

            public string Message
            {
                get { return message; }
            }

            internal MsgEventArgs(string msg)
            {
                message = msg;
            }
        }

        public delegate void InfoEventHandler(object sender, MsgEventArgs e);
        public delegate void LogEventHandler(object sender, MsgEventArgs e);
        public event InfoEventHandler OnInfoMsg;
        public event LogEventHandler OnLogMsg;

        private void EmptyEventHandler(object sender, EventArgs e)
        {
            // do nothing
        }

        protected void Info(string msg)
        {
            try
            {
                OnInfoMsg(null, new MsgEventArgs(msg));
            }
            catch (Exception)
            {
            }
        }

        protected void Log(string msg)
        {
            try
            {
                OnLogMsg(null, new MsgEventArgs(msg));
            }
            catch (Exception)
            {
            }
        }

        private StringBuilder sb = new StringBuilder();

        public string Output
        {
            get { return sb.ToString(); }
        }

        private class OutputReader
        {
            private StringBuilder sb = null;
            private StreamReader sr = null;
            private string text = "";
            public string Text { get { return text; } }
            private MsgHandler Message = null;

            public OutputReader(StreamReader sr, MsgHandler Message, StringBuilder sb)
            {
                try
                {
                    this.sr = sr;
                    this.Message = Message;
                    this.sb = sb;
                }
                catch (Exception)
                {
                }
            }

            public void Start()
            {
                try
                {
                    while ((text = sr.ReadLine()) != null)
                    {   
                        Message(text);
                        sb.Append(text.Trim() + "\r\n");
                    }
                    text = sr.ReadToEnd();
                    sb.Append(text.Trim() + "\r\n");
                    Message(text);
                }
                catch (Exception)
                {
                }
            }
        }

        protected virtual void StartInfo()
        {
        }

        protected virtual void EndInfo()
        {
        }

        protected virtual bool StartProcessing()
        {
            return false;
        }

        protected bool successfull = false;

        public bool Successfull
        {
            get { return successfull; }
        }

        protected virtual void Process()
        {
        }
    }
}