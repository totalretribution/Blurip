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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace BluRip
{
    public static class GlobalVars
    {
        public static List<string> videoTypes = new List<string>();
        public static List<string> ac3AudioTypes = new List<string>();
        public static List<string> dtsAudioTypes = new List<string>();
        public static List<string> ac3Bitrates = new List<string>();
        public static List<string> dtsBitrates = new List<string>();
        public static List<string> resizeMethods = new List<string>();

        public static List<string> profile = new List<string>();
        public static List<string> preset = new List<string>();
        public static List<string> tune = new List<string>();
        public static List<string> level = new List<string>();
        public static List<string> badapt = new List<string>();
        public static List<string> aqmode = new List<string>();

        static GlobalVars()
        {
            try
            {
                videoTypes.Add("h264/AVC");
                videoTypes.Add("VC-1");
                videoTypes.Add("MPEG2");
                videoTypes.Add("h264/AVC  (left eye)");

                ac3AudioTypes.Add("TrueHD/AC3");
                ac3AudioTypes.Add("AC3");
                ac3AudioTypes.Add("AC3 Surround");
                ac3AudioTypes.Add("AC3 EX");
                ac3AudioTypes.Add("E-AC3");
                ac3AudioTypes.Add("RAW/PCM"); // convert to ac3 by default

                dtsAudioTypes.Add("DTS");
                dtsAudioTypes.Add("DTS Master Audio");
                dtsAudioTypes.Add("DTS Express");
                dtsAudioTypes.Add("DTS Hi-Res");
                dtsAudioTypes.Add("DTS ES"); // have to check if needed
                dtsAudioTypes.Add("DTS-ES");

                dtsBitrates.Add("768");
                dtsBitrates.Add("1536");

                ac3Bitrates.Add("192");
                ac3Bitrates.Add("448");
                ac3Bitrates.Add("640");

                resizeMethods.Add("BicubicResize");
                resizeMethods.Add("BilinearResize");
                resizeMethods.Add("BlackmanResize");
                resizeMethods.Add("GaussResize");
                resizeMethods.Add("LanczosResize");
                resizeMethods.Add("Lanczos4Resize");
                resizeMethods.Add("PointResize");
                resizeMethods.Add("SincResize");
                resizeMethods.Add("Spline16Resize");
                resizeMethods.Add("Spline36Resize");
                resizeMethods.Add("Spline64Resize");

                profile.Add("Default");
                profile.Add("baseline");
                profile.Add("main");
                profile.Add("high");
                profile.Add("high10");

                preset.Add("Default");
                preset.Add("ultrafast");
                preset.Add("superfast");
                preset.Add("veryfast");
                preset.Add("faster");
                preset.Add("fast");
                preset.Add("medium");
                preset.Add("slow");
                preset.Add("slower");
                preset.Add("veryslow");
                preset.Add("placebo");

                tune.Add("Default");
                tune.Add("film");
                tune.Add("animation");
                tune.Add("grain");
                tune.Add("stillimage");
                tune.Add("psnr");
                tune.Add("ssim");

                level.Add("Default");
                level.Add("1");
                level.Add("1.1");
                level.Add("1.2");
                level.Add("1.3");
                level.Add("2");
                level.Add("2.1");
                level.Add("2.2");
                level.Add("3");
                level.Add("3.1");
                level.Add("3.2");
                level.Add("4");
                level.Add("4.1");
                level.Add("4.2");
                level.Add("5");
                level.Add("5.1");

                badapt.Add("Default");
                badapt.Add("0");
                badapt.Add("1");
                badapt.Add("2");

                aqmode.Add("Default");
                aqmode.Add("0");
                aqmode.Add("1");
                aqmode.Add("2");
            }
            catch (Exception)
            {
            }
        }        
    }
}