using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private int maxFrames = 0;

        private object drawLock = new object();

        public int cropTop = 0;
        public int cropBottom = 0;
        public int resizeY = 0;
        public int resizeX = 0;
        public int borderTop = 0;
        public int borderBottom = 0;
        public string errorStr = "";
        public bool error = false;
        public bool resize = false;
        public bool border = false;

        public AutoCrop()
        {
            InitializeComponent();
        }

        public AutoCrop(string filename, UserSettings settings)
        {
            InitializeComponent();
            try
            {
                this.settings = settings;
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
                error = true;
                errorStr = ex.Message;
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
            string tmp = "";
            tmp = "AutoCrop frame: " + progress.ToString() + "/" + nrFrames.ToString() + " - frame crop values: ";
            tmp += tmpTop.ToString() + "/" + tmpBottom.ToString() + " - global crop values: ";
            tmp += cropTop.ToString() + "/" + cropBottom.ToString();
            this.Text = tmp;
        }

        public void DoCrop()
        {
            try
            {
                cropBottom = int.MaxValue;
                cropTop = int.MaxValue;

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


                    this.Refresh();

                    for (int row = 0; row < bitmapCopy.Height; row++)
                    {
                        rowSum = RowSum(bitmap, row);
                        if (rowSum < blackValue)
                        {   
                            FillRow(bitmapCopy, row);
                            tmpTop++;
                            this.Refresh();
                        }
                        else
                        {                            
                            break;
                        }
                        UpdateStatusText(tmpTop, tmpBottom, cropTop, cropBottom, progress);
                    }
                    this.Refresh();
                    Thread.Sleep(1000);
                    for (int row = bitmapCopy.Height - 1; row >= 0; row--)
                    {
                        rowSum = RowSum(bitmap, row);
                        if (rowSum < blackValue)
                        {
                            FillRow(bitmapCopy, row);
                            tmpBottom++;
                            this.Refresh();
                        }
                        else
                        {
                            break;
                        }
                        UpdateStatusText(tmpTop, tmpBottom, cropTop, cropBottom, progress);
                    }
                    this.Refresh();
                    cropBottom = Math.Min(cropBottom, tmpBottom);
                    cropTop = Math.Min(cropTop, tmpTop);

                    UpdateStatusText(tmpTop, tmpBottom, cropTop, cropBottom, progress);
                    
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
                }

                bool mod8ok = false;

                // 1080p mode
                if (!settings.resize720p)
                {
                    // no resize, over/undercrop etc needed - should be default case
                    if (cropTop % 2 == 0 && cropBottom % 2 == 0 &&
                        (asc.VideoHeight - cropBottom - cropTop) % 16 == 0)
                    {
                        resize = false;
                        border = false;
                    }
                    else
                    {
                        if (cropTop % 2 == 0 && cropBottom % 2 == 0 &&
                        (asc.VideoHeight - cropBottom - cropTop) % 8 == 0)
                        {
                            mod8ok = false;
                        }

                        // undercrop % 8
                        if (settings.cropMode == 0)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                resize = false;
                                border = false;
                            }
                            else
                            {
                                if (cropBottom % 2 != 0) cropBottom--;
                                if (cropTop % 2 != 0) cropTop--;
                                int tmp = 0;
                                if (cropBottom > cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropBottom - cropTop) % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropTop -= 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropBottom -= 2;
                                        tmp = 0;
                                    }
                                }
                                resize = false;
                                border = false;
                            }
                        }
                        // undercrop % 16
                        else if (settings.cropMode == 1)
                        {
                            if (cropBottom % 2 != 0) cropBottom--;
                            if (cropTop % 2 != 0) cropTop--;
                            int tmp = 0;
                            if (cropBottom > cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropBottom - cropTop) % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropTop -= 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropBottom -= 2;
                                    tmp = 0;
                                }
                            }
                            resize = false;
                            border = false;
                        }
                        // overcrop % 8
                        else if (settings.cropMode == 2)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                resize = false;
                                border = false;
                            }
                            else
                            {
                                if (cropBottom % 2 != 0) cropBottom++;
                                if (cropTop % 2 != 0) cropTop++;
                                int tmp = 0;
                                if (cropBottom > cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropBottom - cropTop) % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropBottom += 2;
                                        tmp = 0;
                                    }
                                }
                                resize = false;
                                border = false;
                            }
                        }
                        // overcrop % 16
                        else if (settings.cropMode == 3)
                        {
                            if (cropBottom % 2 != 0) cropBottom++;
                            if (cropTop % 2 != 0) cropTop++;
                            int tmp = 0;
                            if (cropBottom > cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropBottom - cropTop) % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropBottom += 2;
                                    tmp = 0;
                                }
                            }
                            resize = false;
                            border = false;
                        }
                        // resize % 8
                        else if (settings.cropMode == 4)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                resize = false;
                                border = false;
                            }
                            else
                            {
                                if (cropBottom % 2 != 0) cropBottom--;
                                if (cropTop % 2 != 0) cropTop--;
                                resizeY = asc.VideoHeight - cropBottom - cropTop;
                                if (resizeY % 8 != 0)
                                {
                                    int tmp = resizeY % 8;
                                    if (tmp <= 4)
                                    {
                                        resizeY -= tmp;
                                    }
                                    else
                                    {
                                        resizeY += (8 - tmp);
                                    }
                                    resize = true;
                                    border = false;
                                }
                            }
                        }
                        // resize % 16
                        else if (settings.cropMode == 5)
                        {
                            if (cropBottom % 2 != 0) cropBottom--;
                            if (cropTop % 2 != 0) cropTop--;
                            resizeY = asc.VideoHeight - cropBottom - cropTop;
                            if (resizeY % 16 != 0)
                            {
                                int tmp = resizeY % 16;
                                if (tmp <= 8)
                                {
                                    resizeY -= tmp;
                                }
                                else
                                {
                                    resizeY += (16 - tmp);
                                }
                                resize = true;
                                border = false;
                            }
                        }
                        // addborder % 8
                        else if (settings.cropMode == 6)
                        {
                            if (mod8ok)
                            {
                                // nothing to be done
                                resize = false;
                                border = false;
                            }
                            else
                            {
                                border = false;
                                borderTop = 0;
                                borderBottom = 0;
                                if (cropBottom % 2 != 0) cropBottom--;
                                if (cropTop % 2 != 0) cropTop--;
                                int tmp = 0;
                                if (cropBottom > cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropBottom - cropTop + borderTop + borderBottom) % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        borderTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        borderBottom += 2;
                                        tmp = 0;
                                    }
                                    border = true;
                                }
                                resize = false;
                            }
                        }
                        // addborder % 16
                        else if (settings.cropMode == 7)
                        {
                            border = false;
                            borderTop = 0;
                            borderBottom = 0;
                            if (cropBottom % 2 != 0) cropBottom--;
                            if (cropTop % 2 != 0) cropTop--;
                            int tmp = 0;
                            if (cropBottom > cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropBottom - cropTop + borderTop + borderBottom) % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    borderTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    borderBottom += 2;
                                    tmp = 0;
                                }
                                border = true;
                            }
                            resize = false;
                        }
                    }
                    resizeX = asc.VideoWidth;
                }
                else
                {
                    if ((asc.VideoWidth / 3 * 2) % 16 == 0 && ((asc.VideoHeight - cropBottom - cropTop) / 3 * 2) % 16 == 0 &&
                        cropBottom % 2 == 0 && cropTop % 2 == 0)
                    {
                        resizeX = asc.VideoWidth / 3 * 2;
                        resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                        resize = true;
                        border = false;
                    }
                    else
                    {
                        if (cropTop % 2 == 0 && cropBottom % 2 == 0 &&
                        (asc.VideoHeight - cropBottom - cropTop) /3 * 2 % 8 == 0)
                        {
                            mod8ok = false;
                        }

                        // undercrop % 8
                        if (settings.cropMode == 0)
                        {
                            if (mod8ok)
                            {
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                resize = true;
                                border = false;
                            }
                            else
                            {
                                if (cropBottom % 2 != 0) cropBottom--;
                                if (cropTop % 2 != 0) cropTop--;
                                int tmp = 0;
                                if (cropBottom > cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropBottom - cropTop) /3 * 2 % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropTop -= 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropBottom -= 2;
                                        tmp = 0;
                                    }
                                }
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                resize = true;
                                border = false;
                            }
                        }
                        // undercrop % 16
                        else if (settings.cropMode == 1)
                        {
                            if (cropBottom % 2 != 0) cropBottom--;
                            if (cropTop % 2 != 0) cropTop--;
                            int tmp = 0;
                            if (cropBottom > cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropBottom - cropTop) / 3 * 2 % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropTop -= 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropBottom -= 2;
                                    tmp = 0;
                                }
                            }
                            resizeX = asc.VideoWidth / 3 * 2;
                            resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                            resize = true;
                            border = false;
                        }
                        // overcrop % 8
                        else if (settings.cropMode == 2)
                        {
                            if (mod8ok)
                            {
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                resize = true;
                                border = false;
                            }
                            else
                            {
                                if (cropBottom % 2 != 0) cropBottom++;
                                if (cropTop % 2 != 0) cropTop++;
                                int tmp = 0;
                                if (cropBottom > cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropBottom - cropTop) / 3 * 2 % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        cropTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        cropBottom += 2;
                                        tmp = 0;
                                    }
                                }
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                resize = true;
                                border = false;
                            }
                        }
                        // overcrop % 16
                        else if (settings.cropMode == 3)
                        {
                            if (cropBottom % 2 != 0) cropBottom++;
                            if (cropTop % 2 != 0) cropTop++;
                            int tmp = 0;
                            if (cropBottom > cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropBottom - cropTop) / 3 * 2 % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    cropTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    cropBottom += 2;
                                    tmp = 0;
                                }
                            }
                            resizeX = asc.VideoWidth / 3 * 2;
                            resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                            resize = true;
                            border = false;
                        }
                        // resize % 8
                        else if (settings.cropMode == 4)
                        {
                            if (mod8ok)
                            {
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                resize = true;
                                border = false;
                            }
                            else
                            {
                                if (cropBottom % 2 != 0) cropBottom--;
                                if (cropTop % 2 != 0) cropTop--;
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                if (resizeY % 8 != 0)
                                {
                                    int tmp = resizeY % 8;
                                    if (tmp <= 4)
                                    {
                                        resizeY -= tmp;
                                    }
                                    else
                                    {
                                        resizeY += (8 - tmp);
                                    }
                                    resize = true;
                                    border = false;
                                }
                            }
                        }
                        // resize % 16
                        else if (settings.cropMode == 5)
                        {
                            if (cropBottom % 2 != 0) cropBottom--;
                            if (cropTop % 2 != 0) cropTop--;
                            resizeX = asc.VideoWidth / 3 * 2;
                            resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                            if (resizeY % 16 != 0)
                            {
                                int tmp = resizeY % 16;
                                if (tmp <= 8)
                                {
                                    resizeY -= tmp;
                                }
                                else
                                {
                                    resizeY += (16 - tmp);
                                }
                                resize = true;
                                border = false;
                            }
                        }
                        // addborders % 8
                        else if (settings.cropMode == 6)
                        {
                            if (mod8ok)
                            {
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop) / 3 * 2;
                                resize = true;
                                border = false;
                            }
                            else
                            {
                                border = false;
                                resize = true;
                                borderTop = 0;
                                borderBottom = 0;
                                if (cropBottom % 2 != 0) cropBottom--;
                                if (cropTop % 2 != 0) cropTop--;
                                int tmp = 0;
                                if (cropBottom > cropTop) tmp = 1;
                                while ((asc.VideoHeight - cropBottom - cropTop + borderTop + borderBottom) / 3 * 2 % 8 != 0)
                                {
                                    if (tmp == 0)
                                    {
                                        borderTop += 2;
                                        tmp++;
                                    }
                                    else
                                    {
                                        borderBottom += 2;
                                        tmp = 0;
                                    }
                                    border = true;
                                }
                                resizeX = asc.VideoWidth / 3 * 2;
                                resizeY = (asc.VideoHeight - cropBottom - cropTop + borderTop + borderBottom) / 3 * 2;
                            }
                        }
                        // addborders % 16
                        else if (settings.cropMode == 7)
                        {
                            border = false;
                            resize = true;
                            borderTop = 0;
                            borderBottom = 0;
                            if (cropBottom % 2 != 0) cropBottom--;
                            if (cropTop % 2 != 0) cropTop--;
                            int tmp = 0;
                            if (cropBottom > cropTop) tmp = 1;
                            while ((asc.VideoHeight - cropBottom - cropTop + borderTop + borderBottom) / 3 * 2 % 16 != 0)
                            {
                                if (tmp == 0)
                                {
                                    borderTop += 2;
                                    tmp++;
                                }
                                else
                                {
                                    borderBottom += 2;
                                    tmp = 0;
                                }
                                border = true;
                            }
                            resizeX = asc.VideoWidth / 3 * 2;
                            resizeY = (asc.VideoHeight - cropBottom - cropTop + borderTop + borderBottom) / 3 * 2;
                        }
                    }
                }
                
                
                this.Close();
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
