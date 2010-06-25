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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BluRip
{
    public partial class AutoCrop : Form
    {
        private Bitmap bitmap = null;
        private Bitmap bitmapCopy = null;
        private AviSynthClip asc = null;
        private UserSettings settings = null;
        private CropInfo cropInfo = null;

        private int maxFrames = 0;

        private object drawLock = new object();
                
        private delegate void UpdateStatusDelegate(int tmpTop, int tmpBottom, int cropTop, int cropBottom, int progress);

        public AutoCrop()
        {
            InitializeComponent();
        }

        public AutoCrop(string filename, UserSettings settings, ref CropInfo cropInfo)
        {
            InitializeComponent();
            try
            {
                this.settings = settings;
                this.cropInfo = cropInfo;
                AviSynthScriptEnvironment asse = new AviSynthScriptEnvironment();
                asc = asse.OpenScriptFile(filename);

                if(asc.HasVideo)
                {
                    System.Drawing.Size s = new Size(asc.VideoWidth, asc.VideoHeight);
                    this.ClientSize = s;                    
                    maxFrames = asc.num_frames;
                    // remove last ~10 minutes
                    if (maxFrames > 30000)
                    {
                        maxFrames -= 15000;
                    }
                    int step = 0;
                    step = maxFrames / nrFrames;
                    Monitor.Enter(drawLock);
                    bitmap = ReadFrameBitmap(asc, step);
                    bitmapCopy = new Bitmap(bitmap);
                    Monitor.Exit(drawLock);
                }
            }
            catch (Exception ex)
            {
                cropInfo.error = true;
                cropInfo.errorStr = ex.Message;
            }
        }

        public Bitmap ReadFrameBitmap(AviSynthClip asc, int position)
        {
            Bitmap bmp = null;
            try
            {
                bmp = new Bitmap(asc.VideoWidth, asc.VideoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);
                try
                {
                    // Get the address of the first line.
                    IntPtr ptr = bmpData.Scan0;
                    // Read data
                    asc.ReadFrame(ptr, bmpData.Stride, position);
                }
                finally
                {
                    // Unlock the bits.
                    bmp.UnlockBits(bmpData);
                }
                bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                return bmp;
            }
            catch (Exception)
            {
                if(bmp != null) bmp.Dispose();
                throw;
            }
        }

        private int RowSum(Bitmap b, int row)
        {
            try
            {
                if (row < 0 || row > b.Height) return -1;
                int sum = 0;
                for (int x = 0; x < b.Width; x++)
                {
                    Color c = b.GetPixel(x, row);
                    sum += c.R + c.G + c.B;
                }
                return sum;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void FillRow(Bitmap b, int row)
        {
            try
            {
                Monitor.Enter(drawLock);
                for (int x = 0; x < b.Width; x++)
                {
                    b.SetPixel(x, row, Color.White);
                }
                Monitor.Exit(drawLock);
            }
            catch (Exception)
            {
            }
        }

        private int blackValue = 50000;
        private int nrFrames = 10;
        private bool drawOrig = false;
        private int rowSum = 0;

        public int NrFrames
        {
            get
            {
                return nrFrames;
            }
            set
            {
                nrFrames = value;
                int step = 0;
                step = maxFrames / nrFrames;
                Monitor.Enter(drawLock);
                bitmap = ReadFrameBitmap(asc, step);
                bitmapCopy = new Bitmap(bitmap);
                Monitor.Exit(drawLock);
            }
        }

        public int BlackValue
        {
            get
            {
                return blackValue;
            }
            set
            {
                blackValue = value;
            }
        }

        private void UpdateStatusText(int tmpTop, int tmpBottom, int cropTop, int cropBottom, int progress)
        {
            if (this.InvokeRequired) this.Invoke(new UpdateStatusDelegate(UpdateStatusText), tmpTop, tmpBottom, cropTop, cropBottom, progress);
            else
            {
                string tmp = "";
                tmp = "AutoCrop frame: " + progress.ToString() + "/" + nrFrames.ToString() + " - frame crop values: ";
                tmp += tmpTop.ToString() + "/" + tmpBottom.ToString() + " - global crop values: ";
                tmp += cropTop.ToString() + "/" + cropBottom.ToString();
                this.Text = tmp;
            }
        }

        public void DoCrop()
        {
            try
            {
                cropInfo.cropBottom = int.MaxValue;
                cropInfo.cropTop = int.MaxValue;

                int step = 0;
                step = maxFrames / nrFrames;
                int progress = 0;

                for (int frame = step; frame <= maxFrames; frame += step)
                {
                    int tmpTop = 0;
                    int tmpBottom = 0;
                    progress++;

                    Monitor.Enter(drawLock);
                    bitmap.Dispose();
                    bitmapCopy.Dispose();

                    bitmap = ReadFrameBitmap(asc, frame);
                    bitmapCopy = new Bitmap(bitmap);
                    Monitor.Exit(drawLock);

                    if (this.InvokeRequired) this.Invoke(new MethodInvoker(this.Refresh));
                    else this.Refresh();

                    for (int row = 0; row < bitmapCopy.Height; row++)
                    {
                        rowSum = RowSum(bitmap, row);
                        if (rowSum < blackValue)
                        {   
                            FillRow(bitmapCopy, row);
                            tmpTop++;
                            if (this.InvokeRequired) this.Invoke(new MethodInvoker(this.Refresh));
                            else this.Refresh();
                        }
                        else
                        {                            
                            break;
                        }
                        UpdateStatusText(tmpTop, tmpBottom, cropInfo.cropTop, cropInfo.cropBottom, progress);
                    }
                    if (this.InvokeRequired) this.Invoke(new MethodInvoker(this.Refresh));
                    else this.Refresh();
                    Thread.Sleep(1000);
                    for (int row = bitmapCopy.Height - 1; row >= 0; row--)
                    {
                        rowSum = RowSum(bitmap, row);
                        if (rowSum < blackValue)
                        {
                            FillRow(bitmapCopy, row);
                            tmpBottom++;
                            if (this.InvokeRequired) this.Invoke(new MethodInvoker(this.Refresh));
                            else this.Refresh();
                        }
                        else
                        {
                            break;
                        }
                        UpdateStatusText(tmpTop, tmpBottom, cropInfo.cropTop, cropInfo.cropBottom, progress);
                    }
                    if (this.InvokeRequired) this.Invoke(new MethodInvoker(this.Refresh));
                    else this.Refresh();
                    cropInfo.cropBottom = Math.Min(cropInfo.cropBottom, tmpBottom);
                    cropInfo.cropTop = Math.Min(cropInfo.cropTop, tmpTop);

                    UpdateStatusText(tmpTop, tmpBottom, cropInfo.cropTop, cropInfo.cropBottom, progress);
                    
                    for (int i = 0; i < 5; i++)
                    {
                        Monitor.Enter(drawLock);
                        drawOrig = true;
                        Monitor.Exit(drawLock);
                        this.Invalidate();
                        Thread.Sleep(500);
                        Monitor.Enter(drawLock);
                        drawOrig = false;
                        Monitor.Exit(drawLock);
                        this.Invalidate();
                        Thread.Sleep(500);
                    }
                    if (tmpTop == 0 && tmpBottom == 0)
                    {
                        break;
                    }
                }

                bool mod8ok = false;

                // 1080p mode
                if (!settings.resize720p)
                {
                    // no resize, over/undercrop etc needed - should be default case
                    if (cropInfo.cropTop % 2 == 0 && cropInfo.cropBottom % 2 == 0 &&
                        (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) % 16 == 0)
                    {
                        cropInfo.resize = false;
                        cropInfo.border = false;
                    }
                    else
                    {
                        if (cropInfo.cropTop % 2 == 0 && cropInfo.cropBottom % 2 == 0 &&
                        (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) % 8 == 0)
                        {
                            mod8ok = false;
                        }

                        // undercrop % 8
                        if (settings.cropMode == 0)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                cropInfo.resize = false;
                                cropInfo.border = false;
                            }
                            else
                            {
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                                int tmp = 0;
                                if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropInfo.cropTop -= 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropInfo.cropBottom -= 2;
                                        tmp = 0;
                                    }
                                }
                                cropInfo.resize = false;
                                cropInfo.border = false;
                            }
                        }
                        // undercrop % 16
                        else if (settings.cropMode == 1)
                        {
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                            int tmp = 0;
                            if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropInfo.cropTop -= 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropInfo.cropBottom -= 2;
                                    tmp = 0;
                                }
                            }
                            cropInfo.resize = false;
                            cropInfo.border = false;
                        }
                        // overcrop % 8
                        else if (settings.cropMode == 2)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                cropInfo.resize = false;
                                cropInfo.border = false;
                            }
                            else
                            {
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom++;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop++;
                                int tmp = 0;
                                if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropInfo.cropTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropInfo.cropBottom += 2;
                                        tmp = 0;
                                    }
                                }
                                cropInfo.resize = false;
                                cropInfo.border = false;
                            }
                        }
                        // overcrop % 16
                        else if (settings.cropMode == 3)
                        {
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom++;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop++;
                            int tmp = 0;
                            if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropInfo.cropTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropInfo.cropBottom += 2;
                                    tmp = 0;
                                }
                            }
                            cropInfo.resize = false;
                            cropInfo.border = false;
                        }
                        // resize % 8
                        else if (settings.cropMode == 4)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                cropInfo.resize = false;
                                cropInfo.border = false;
                            }
                            else
                            {
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                                cropInfo.resizeY = asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop;
                                if (cropInfo.resizeY % 8 != 0)
                                {
                                    int tmp = cropInfo.resizeY % 8;
                                    if (tmp <= 4)
                                    {
                                        cropInfo.resizeY -= tmp;
                                    }
                                    else
                                    {
                                        cropInfo.resizeY += (8 - tmp);
                                    }
                                    cropInfo.resize = true;
                                    cropInfo.border = false;
                                }
                            }
                        }
                        // resize % 16
                        else if (settings.cropMode == 5)
                        {
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                            cropInfo.resizeY = asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop;
                            if (cropInfo.resizeY % 16 != 0)
                            {
                                int tmp = cropInfo.resizeY % 16;
                                if (tmp <= 8)
                                {
                                    cropInfo.resizeY -= tmp;
                                }
                                else
                                {
                                    cropInfo.resizeY += (16 - tmp);
                                }
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                        }
                        // addborder % 8
                        else if (settings.cropMode == 6)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                cropInfo.resize = false;
                                cropInfo.border = false;
                            }
                            else
                            {
                                cropInfo.border = false;
                                cropInfo.borderTop = 0;
                                cropInfo.borderBottom = 0;
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                                int tmp = 0;
                                if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop + cropInfo.borderTop + cropInfo.borderBottom) % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropInfo.borderTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropInfo.borderBottom += 2;
                                        tmp = 0;
                                    }
                                    cropInfo.border = true;
                                }
                                cropInfo.resize = false;
                            }
                        }
                        // addborder % 16
                        else if (settings.cropMode == 7)
                        {
                            cropInfo.border = false;
                            cropInfo.borderTop = 0;
                            cropInfo.borderBottom = 0;
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                            int tmp = 0;
                            if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop + cropInfo.borderTop + cropInfo.borderBottom) % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropInfo.borderTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropInfo.borderBottom += 2;
                                    tmp = 0;
                                }
                                cropInfo.border = true;
                            }
                            cropInfo.resize = false;
                        }
                    }
                    cropInfo.resizeX = asc.VideoWidth;
                }
                else
                {
                    if ((asc.VideoWidth / 3 * 2) % 16 == 0 && ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2) % 16 == 0 &&
                        cropInfo.cropBottom % 2 == 0 && cropInfo.cropTop % 2 == 0)
                    {
                        cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                        cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                        cropInfo.resize = true;
                        cropInfo.border = false;
                    }
                    else
                    {
                        if (cropInfo.cropTop % 2 == 0 && cropInfo.cropBottom % 2 == 0 &&
                        (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2 % 8 == 0)
                        {
                            mod8ok = false;
                        }

                        // undercrop % 8
                        if (settings.cropMode == 0)
                        {
                            if (mod8ok)
                            {
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                            else
                            {
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                                int tmp = 0;
                                if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2 % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropInfo.cropTop -= 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropInfo.cropBottom -= 2;
                                        tmp = 0;
                                    }
                                }
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                        }
                        // undercrop % 16
                        else if (settings.cropMode == 1)
                        {
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                            int tmp = 0;
                            if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2 % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropInfo.cropTop -= 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropInfo.cropBottom -= 2;
                                    tmp = 0;
                                }
                            }
                            cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                            cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                            cropInfo.resize = true;
                            cropInfo.border = false;
                        }
                        // overcrop % 8
                        else if (settings.cropMode == 2)
                        {
                            if (mod8ok)
                            {
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                            else
                            {
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom++;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop++;
                                int tmp = 0;
                                if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2 % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropInfo.cropTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropInfo.cropBottom += 2;
                                        tmp = 0;
                                    }
                                }
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                        }
                        // overcrop % 16
                        else if (settings.cropMode == 3)
                        {
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom++;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop++;
                            int tmp = 0;
                            if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2 % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropInfo.cropTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropInfo.cropBottom += 2;
                                    tmp = 0;
                                }
                            }
                            cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                            cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                            cropInfo.resize = true;
                            cropInfo.border = false;
                        }
                        // resize % 8
                        else if (settings.cropMode == 4)
                        {
                            if (mod8ok)
                            {
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                            else
                            {
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                if (cropInfo.resizeY % 8 != 0)
                                {
                                    int tmp = cropInfo.resizeY % 8;
                                    if (tmp <= 4)
                                    {
                                        cropInfo.resizeY -= tmp;
                                    }
                                    else
                                    {
                                        cropInfo.resizeY += (8 - tmp);
                                    }
                                    cropInfo.resize = true;
                                    cropInfo.border = false;
                                }
                            }
                        }
                        // resize % 16
                        else if (settings.cropMode == 5)
                        {
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                            cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                            cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                            if (cropInfo.resizeY % 16 != 0)
                            {
                                int tmp = cropInfo.resizeY % 16;
                                if (tmp <= 8)
                                {
                                    cropInfo.resizeY -= tmp;
                                }
                                else
                                {
                                    cropInfo.resizeY += (16 - tmp);
                                }
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                        }
                        // addborders % 8
                        else if (settings.cropMode == 6)
                        {
                            if (mod8ok)
                            {
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop) / 3 * 2;
                                cropInfo.resize = true;
                                cropInfo.border = false;
                            }
                            else
                            {
                                cropInfo.border = false;
                                cropInfo.resize = true;
                                cropInfo.borderTop = 0;
                                cropInfo.borderBottom = 0;
                                if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                                if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                                int tmp = 0;
                                if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop + cropInfo.borderTop + cropInfo.borderBottom) / 3 * 2 % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropInfo.borderTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropInfo.borderBottom += 2;
                                        tmp = 0;
                                    }
                                    cropInfo.border = true;
                                }
                                cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                                cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop + cropInfo.borderTop + cropInfo.borderBottom) / 3 * 2;
                            }
                        }
                        // addborders % 16
                        else if (settings.cropMode == 7)
                        {
                            cropInfo.border = false;
                            cropInfo.resize = true;
                            cropInfo.borderTop = 0;
                            cropInfo.borderBottom = 0;
                            if (cropInfo.cropBottom % 2 != 0) cropInfo.cropBottom--;
                            if (cropInfo.cropTop % 2 != 0) cropInfo.cropTop--;
                            int tmp = 0;
                            if (cropInfo.cropBottom > cropInfo.cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop + cropInfo.borderTop + cropInfo.borderBottom) / 3 * 2 % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropInfo.borderTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropInfo.borderBottom += 2;
                                    tmp = 0;
                                }
                                cropInfo.border = true;
                            }
                            cropInfo.resizeX = asc.VideoWidth / 3 * 2;
                            cropInfo.resizeY = (asc.VideoHeight - cropInfo.cropBottom - cropInfo.cropTop + cropInfo.borderTop + cropInfo.borderBottom) / 3 * 2;
                        }
                    }
                }
                
                if(this.InvokeRequired) this.Invoke(new MethodInvoker(this.Close));
                else this.Close();
            }
            catch (Exception)
            {
            }
        }

        public void StartAutoCrop()
        {
            try
            {
                Thread t = new Thread(DoCrop);
                t.Start();
            }
            catch (Exception)
            {
            }
        }

        private void AutoCrop_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (bitmapCopy != null)
                {
                    Monitor.Enter(drawLock);
                    if (!drawOrig)
                    {
                        e.Graphics.DrawImage(bitmapCopy, new Point(0, 0));
                    }
                    else
                    {
                        e.Graphics.DrawImage(bitmap, new Point(0, 0));
                    }
                    Font font = new Font("Arial", 14.0f);
                    e.Graphics.DrawString("Row sum: " + rowSum.ToString(), font, Brushes.LightGreen, new PointF(10, 40));
                    font.Dispose();
                    Monitor.Exit(drawLock);
                }
            }
            catch (Exception)
            {
            }
        }

        private void AutoCrop_Shown(object sender, EventArgs e)
        {
            try
            {
                StartAutoCrop();
            }
            catch (Exception)
            {
            }
        }
    }
}
