using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using Model;
using BLL;
using Utility;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using System.Drawing.Imaging;
using com.google.zxing.common;
using com.google.zxing;
using com.google.zxing.qrcode;

namespace Printer
{
    public partial class MainForm : Form
    {
        #region 全局变量
        public static UsbDevice MyUsbDevice;
        //设置USB的VendorID 和 Product ID  
        public static UsbDeviceFinder MyUsbFinder = new UsbDeviceFinder(0x28E9, 0x0289);
        public static ErrorCode ec = ErrorCode.None;
        public static UsbEndpointWriter writer;

        public string configPath = System.Windows.Forms.Application.ExecutablePath + ".config";

        private int printType = 1;
        /// <summary>
        /// 打印格式，1为只放大突出数值，2为全部放大
        /// </summary>
        public int PrintType
        {
            get
            {
                return printType;
            }
        }

        private string firmName = "";
        /// <summary>
        /// 公司名称，用于打印是加LOGO
        /// </summary>
        public string FirmName
        {
            get
            {
                return firmName;
            }
        }

        private string appShortTitle = "";
        /// <summary>
        /// 系统名称，用于打印是加LOGO
        /// </summary>
        public string AppShortTitle
        {
            get
            {
                return appShortTitle;
            }
        }
        int pieceIdForPrint = 0;
        int maxCountForPrint = 0;
        int defaultCountForPrint = 0;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //找到并打开USB设备  
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                if (MyUsbDevice == null)
                {
                    //throw new Exception("Device Not Found");
                    ShowTipInfo("未发现打印机,请检查是否已连接好打印机！",Color.Red);
                }
                else
                {
                    ShowTipInfo("打印机就绪", Color.Green);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            bindAppConfigValues();
        }

        #region AppConfig相关
        private void bindAppConfigValues()
        {
            firmName = Function.GetAppConfig("FirmName");
            appShortTitle = Function.GetAppConfig("AppShortTitle");
            printType = int.Parse(Function.GetAppConfig("PrintType"));
        }
        #endregion

        #region 加油单打印操作相关

        private bool tryPrint(BillPrinter bill)
        {
            bool re = false;
            try
            {
                if (PrinterHelper.TxOpenPrinter(1, 0))
                {
                    int status = PrinterHelper.TxGetStatus();
                    if (status == 16)
                    {
                        string printText = "";
                        ShowTipInfo(string.Format("正在打印加油单：{0}-{1}-{2}-{3}-{4}", bill.Id, bill.DriverName, bill.OilerName, bill.OilName, bill.Volume), Color.YellowGreen);
                        //无故障情况下才执行打印
                        PrinterHelper.TxInit();
                        if (printType == 2)
                        {
                            printText = getPrintTextByStepForBill(bill, 1);
                            PrinterHelper.TxResetFont();
                            PrinterHelper.TxOutputString(printText);
                            printText = getPrintTextByStepForBill(bill, 2);
                            PrinterHelper.TxDoFunction(1, 1, 1);//放大打印
                            PrinterHelper.TxOutputStringLn(printText);
                            printText = getPrintTextByStepForBill(bill, 3);
                            PrinterHelper.TxResetFont();
                            PrinterHelper.TxOutputString(printText);
                            printText = getPrintTextByStepForBill(bill, 4);
                            PrinterHelper.TxDoFunction(1, 1, 1);//放大打印
                            PrinterHelper.TxOutputStringLn(printText);
                            printText = getPrintTextByStepForBill(bill, 5);
                            PrinterHelper.TxResetFont();
                            PrinterHelper.TxOutputStringLn(printText);
                        }
                        else
                        {
                            printText = getPrintTextForBill(bill);
                            PrinterHelper.TxDoFunction(1, 0, 1);//放大打印，只放大纵向
                            PrinterHelper.TxOutputStringLn(printText);
                        }
                        PrinterHelper.TxDoFunction(10, 30, 0);//走纸30毫米
                        Thread.Sleep(1000);
                        bool isSuccess = PrinterHelper.CheckIsPrintSuccess();
                        if (isSuccess)
                        {
                            re = true;
                            ShowTipInfo(string.Format("加油单打印成功：{0}-{1}-{2}-{3}-{4}", bill.Id, bill.DriverName, bill.OilerName, bill.OilName, bill.Volume), Color.Green);
                        }
                        else
                        {
                            ShowTipInfo("打印失败，有可能是打印机内纸不够、打印机断电或其他异常，请确保打印机接上电源并且其内有足够的纸，然后执行一次关闭打印机后再打开打印机。", Color.Red);
                        }
                    }
                    else if (status == 56)
                    {
                        ShowTipInfo("检测到打印机内没有纸，如果有纸，请执行一次关闭打印机后再打开打印机。", Color.Red);
                    }
                    else
                    {
                        ShowTipInfo("打印机繁忙或异常，请尝试执行一次关闭打印机后再打开打印机，可能能解决问题。", Color.Red);
                    }
                }
                else
                {
                    ShowTipInfo("无法连接打印机，请确保打印机电源打开并且正常连接到电脑", Color.Red);
                }
            }
            catch (Exception ex) {
                re = false;
                ShowTipInfo(string.Format("打印时出现异常：{0}",ex.ToString()), Color.Red);
            }
            finally
            {
                PrinterHelper.TxClosePrinter();
            }
            return re;
        }

        /// <summary>
        /// 一次把一个加油单完整打印出来
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        private string getPrintTextForBill(BillPrinter bill)
        {
            string reText = string.Format(@"　　　　　　加油单
单　　号:{0}
车辆编号:{1}
VIN　　 :{2}
油　　品:{3}　　　　　　　{6}
加油量　:{4}　　　　　　　　　{7}
加油时间:{5}", bill.Id, bill.CarNumber, bill.CarVin,bill.OilName, bill.Volume, bill.Time,bill.OilName, bill.Volume);
            reText += string.Format(@"
-------------------------------
　　　{0}
-------------------------------", firmName);
            return reText;
        }

        /// <summary>
        /// 分5个步骤把一个加油单打出来，额外打印加大字号的油品及加油量
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private string getPrintTextByStepForBill(BillPrinter bill,int step)
        {
            string reText = "";
            switch (step)
            {
                case 1:
                    //不带回车、正常打印
                    reText = string.Format(@"　　　　　　加油单
单　　号:{0}
车辆编号:{1}
VIN　　 :{2}
油　　品:{3}　　　　　", bill.Id, bill.CarNumber, bill.CarVin, bill.OilName);
                    break;
                case 2:
                    //带回车、放大打印
                    reText = bill.OilName.ToString();
                    break;
                case 3:
                    //不带回车、正常打印
                    reText = string.Format(@"加油量　:{0}　　　　　　　　", bill.Volume);
                    break;
                case 4:
                    //带回车、放大打印
                    reText = bill.Volume.ToString();
                    break;
                case 5:
                    reText = string.Format(@"加油时间:{0}", bill.Time);
                    reText += string.Format(@"
-------------------------------
　　　{0}
-------------------------------", firmName);
                    break;
            }
            return reText;
        }
        #endregion

        #region 按钮事件相关
        private void btnImportContent_Click(object sender, EventArgs e)
        {
            txtContent.Text = Clipboard.GetText().TrimEnd('\n');
            if (txtContent.Text == "")
            {
                ShowTipInfo("试件内容不能为空！", Color.Red);
            }
            else {
                ShowTipInfo("试件内容导入成功，请确认是否有误！", Color.Green);
            }
            
            //setPieceContentByClipboard(clipText);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (bytes.Length == 1)
            {
                MessageBox.Show("请先生成二维码，再打印！");
                return;
            }
            decimal count = nudCount.Value;
            for (int i = 0; i < count; i++)
            {
                if (Print())
                {
                    ShowTipInfo("打印成功");
                }
                else
                {
                    ShowTipInfo("打印出错");
                }
            }
        }
        #endregion

        #region 辅助函数相关
        private void reStartProcess()
        {
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Application.Exit();
        }

        private void keepListCount(ListBox curList, int targetCount)
        {
            if (curList.Items.Count > targetCount)
            {
                int waitClearCount = curList.Items.Count - targetCount;
                for (int i = 0; i < waitClearCount; i++)
                {
                    curList.Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 根据粘贴板内容设置试件内容
        /// 要求粘贴板内容最后一行有试件ID，试件最大打印数量及默认打印数量，三者用逗号分隔
        /// 除最后一行外，其他内容作为试件内容打印出来
        /// </summary>
        /// <param name="clipText">粘贴板内容</param>
        private void setPieceContentByClipboard(string clipText)
        {
            txtContent.Text = "";
            int splitIndex = clipText.LastIndexOf('\n');
            if (splitIndex < 0)
            {
                MessageBox.Show("试件内容不合法，无法打印！", "提示");
                return;
            }
            string lastText = clipText.Substring(splitIndex + 1);
            string[] lastTexts = lastText.Split(',');
            if (lastTexts.Length != 3)
            {
                MessageBox.Show("试件内容不合法，无法打印！", "提示");
                return;
            }
            if (!int.TryParse(lastTexts[0], out pieceIdForPrint))
            {
                MessageBox.Show("试件内容不合法，无法打印！", "提示");
                return;
            }
            if (!int.TryParse(lastTexts[1], out maxCountForPrint))
            {
                MessageBox.Show("试件内容不合法，无法打印！", "提示");
                return;
            }
            if (!int.TryParse(lastTexts[2], out defaultCountForPrint))
            {
                MessageBox.Show("试件内容不合法，无法打印！", "提示");
                return;
            }
            if (!(pieceIdForPrint > 0 && maxCountForPrint > 0 && defaultCountForPrint > 0 && maxCountForPrint >= defaultCountForPrint))
            {
                MessageBox.Show("试件内容不合法，无法打印！", "提示");
                return;
            }
            string content = clipText.Substring(0, splitIndex);
            txtContent.Text = content;
            nudCount.Maximum = maxCountForPrint;
            nudCount.Value = defaultCountForPrint;
        }

        #endregion

        #region 菜单事件相关
        private void tsmiContactUs_Click(object sender, EventArgs e)
        {
            ContactUs contactUs = new ContactUs();
            contactUs.ShowDialog();
        }

        private void tsmiAboutSoft_Click(object sender, EventArgs e)
        {
            AboutSoft aboutSoft = new AboutSoft();
            aboutSoft.ShowDialog();
        }

        private void tsmiDataBaseConfig_Click(object sender, EventArgs e)
        {
            DataBaseConfig dataBaseConfig = new DataBaseConfig();
            if (dataBaseConfig.ShowDialog() == DialogResult.OK)
            {
                string newDbConnectionString = dataBaseConfig.DbConnectionString;
                dataBaseConfig.Dispose();
                dataBaseConfig = null;
                if (!DBHelper.ConnectionTest(newDbConnectionString))
                {
                    MessageBox.Show("测试连接失败,请输入正确的配置信息！", "提示");
                }
                else
                {
                    try
                    {
                        Function.ModifyConnectionString(configPath, "ConnectionString", newDbConnectionString);
                        if (MessageBox.Show("新的服务器配置需要重新启动程序才能生效，确定要重启吗？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            reStartProcess();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("未能成功修改配置，请联系M&T HORIZON！");
                    }
                }
            }
        }
        #endregion

        #region 信息提示相关
        public void AddTipInfo(string text, params Color[] c)
        {
            if (rtbTipInfo.Lines.Length > 0)
            {
                rtbTipInfo.AppendText("\r\n");
            }
            if (c.Length > 0)
            {
                rtbTipInfo.SelectionColor = c[0];
            }
            rtbTipInfo.SelectedText = text;
            rtbTipInfo.ScrollToCaret();
        }
        public void clearTipInfo()
        {
            rtbTipInfo.Clear();
        }
        public void ShowTipInfo(string text, params Color[] c)
        {
            this.lbStatusBottom.Text = text;
            clearTipInfo();
            if (c.Length > 0)
            {
                rtbTipInfo.SelectionColor = c[0];
            }
            rtbTipInfo.SelectedText = text;
            rtbTipInfo.SelectionAlignment = HorizontalAlignment.Left;
        }
        #endregion
      
        //生成大二维码
        byte[] bytes = { 0 };
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtContent.Text.Trim() == "") {
                MessageBox.Show("试件内容为空，请先导入试件内容!");
                return;
            }
            byte temp = 0;
            int j = 0;
            int start = 0;
            byte Y;
            //string content = "http://www.sznewbest.com";
            string content = txtContent.Text;
            //H&W:384
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, 368, 368);
            Bitmap bitmap = toBitmap(byteMatrix);

            int newWidth = bitmap.Width;
            int newHeight = bitmap.Height;
            byte[] result = new byte[newWidth * newHeight / 8];
            Rectangle rt = new Rectangle(0, 0, newWidth, newHeight);
            BitmapData dt = bitmap.LockBits(rt, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)(dt.Scan0);
                for (int w = 0; w < newHeight; w++)
                {
                    for (j = 0; j < newWidth; j++)
                    {
                        Y = (byte)(0.299 * ptr[2] + 0.578 * ptr[1] + 0.114 * ptr[0]);//公式是最重要的
                        ptr += 3;
                        temp = (byte)((temp << 1) | (byte)(Y > 128 ? 0 : 1));
                        if (j % 8 == 7)
                        {
                            result[start] = temp;
                            start++;
                            temp = 0;
                        }
                    }
                    ptr += dt.Stride - newWidth * 3;
                }
            }
            bitmap.UnlockBits(dt);
            int aHeight = 24 - (newHeight) % 24;//aHeight == 24
            byte[] add = new byte[aHeight * 46];//add[24 * 48]
            byte[] nresult = new byte[newWidth * (newHeight) / 8 + aHeight * 46];
            Buffer.BlockCopy(result, 0, nresult, 0, result.Length);
            Buffer.BlockCopy(add, 0, nresult, result.Length, add.Length);
            bytes = new byte[((newWidth) / 8 + 4) * ((newHeight) + aHeight)];// 打印数组

            byte[] bytehead = new byte[4];// 每行打印头
            bytehead[0] = (byte)0x1f;
            bytehead[1] = (byte)0x10;
            bytehead[2] = (byte)((newWidth) / 8);
            bytehead[3] = (byte)0x00;

            for (int index = 0; index < (newHeight) + aHeight; index++)
            {
                Buffer.BlockCopy(bytehead, 0, bytes, index * 50, 4);
                Buffer.BlockCopy(nresult, index * 46, bytes,
                        index * 50 + 4, 46);

            }
            pictureBox1.Image = bitmap;
        }

        public static Bitmap toBitmap(ByteMatrix matrix)
        {
            int width = matrix.Width;
            int height = matrix.Height;
            Bitmap bmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? ColorTranslator.FromHtml("0xFF000000") : ColorTranslator.FromHtml("0xFFFFFFFF"));
                }
            }
            return bmap;
        }
        byte[] sbytes = { 0 };
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtContent.Text.Trim() == "")
            {
                MessageBox.Show("试件内容为空，请先导入试件内容!");
                return;
            }
            byte temp = 0;
            int j = 0;
            int start = 0;
            byte Y;
            //string content = "http://www.sznewbest.com";
            string content = txtContent.Text;
            //H&W:384
            ByteMatrix byteMatrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, 208, 208);
            Bitmap bitmap = toBitmap(byteMatrix);

            int newWidth = bitmap.Width;
            int newHeight = bitmap.Height;
            byte[] result = new byte[newWidth * newHeight / 8];
            Rectangle rt = new Rectangle(0, 0, newWidth, newHeight);
            BitmapData dt = bitmap.LockBits(rt, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)(dt.Scan0);
                for (int w = 0; w < newHeight; w++)
                {
                    for (j = 0; j < newWidth; j++)
                    {
                        Y = (byte)(0.299 * ptr[2] + 0.578 * ptr[1] + 0.114 * ptr[0]);//公式是最重要的
                        ptr += 3;
                        temp = (byte)((temp << 1) | (byte)(Y > 128 ? 0 : 1));
                        if (j % 8 == 7)
                        {
                            result[start] = temp;
                            start++;
                            temp = 0;
                        }
                    }
                    ptr += dt.Stride - newWidth * 3;
                }
            }
            bitmap.UnlockBits(dt);
            int aHeight = 24 - (newHeight) % 24;//aHeight == 24
            byte[] add = new byte[aHeight * 26];//add[24 * 48]
            byte[] nresult = new byte[newWidth * (newHeight) / 8 + aHeight * 26];
            Buffer.BlockCopy(result, 0, nresult, 0, result.Length);
            Buffer.BlockCopy(add, 0, nresult, result.Length, add.Length);
            sbytes = new byte[((newWidth) / 8 + 4) * ((newHeight) + aHeight)];// 打印数组

            byte[] bytehead = new byte[4];// 每行打印头
            bytehead[0] = (byte)0x1f;
            bytehead[1] = (byte)0x10;
            bytehead[2] = (byte)((newWidth) / 8);
            bytehead[3] = (byte)0x00;

            for (int index = 0; index < (newHeight) + aHeight; index++)
            {
                Buffer.BlockCopy(bytehead, 0, sbytes, index * 30, 4);
                Buffer.BlockCopy(nresult, index * 26, sbytes,
                        index * 30 + 4, 26);

            }
            pictureBox1.Image = bitmap;
        }

        public UsbEndpointWriter getWriter()
        {
            if (writer == null)
            {
                if (MyUsbDevice == null)
                {
                    //throw new Exception("Device Not Found");
                    MessageBox.Show("未连接上打印机");
                }
                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is a "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.
                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);
                    // Claim interface #0.
                    wholeUsbDevice.ClaimInterface(0);
                }
                writer = MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep03);
            }
            return writer;

        }

        public bool Print()
        {

            writer = getWriter();
            int bytesWritten;
            bool isSuccess = true;
            try
            {
                ec = writer.Write(bytes, 2000, out bytesWritten);
                ec = writer.Write(new byte[] { 0x0a }, 2000, out bytesWritten);
                ec = writer.Write(new byte[] { 0x0a }, 2000, out bytesWritten);
                /*
                for (int i = 0; i < 2; i++)
                {
                    ec = writer.Write(new byte[] { 0x0a }, 2000, out bytesWritten);
                }*/
                //ec = writer.Write(new byte[] { 0x0a }, 2000, out bytesWritten);

                if (ec.ToString() == "Success")
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                return isSuccess;
            }
            finally
            {

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sbytes.Length == 1)
            {
                MessageBox.Show("请先生成二维码，再打印！");
                return;
            }
            decimal count = nudCount.Value;
            for (int i = 0; i < count; i++)
            {
                if (PrintSmall())
                {
                    ShowTipInfo("打印成功",Color.Green);
                }
                else
                {
                    ShowTipInfo("打印出错",Color.Green);
                }
            }
        }

        public bool PrintSmall()
        {

            writer = getWriter();
            int bytesWritten;
            bool isSuccess = true;
            int count = 1;
            count =(int) nudCount.Value;
            try
            {
                ec = writer.Write(sbytes, 2000, out bytesWritten);
                for (int i = 0; i < 8; i++)
                {
                    ec = writer.Write(new byte[] { 0x0a }, 2000, out bytesWritten);
                }
                //ec = writer.Write(new byte[] { 0x0a }, 2000, out bytesWritten);

                if (ec.ToString() == "Success")
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                return isSuccess;
            }
            finally
            {

            }

        }
    }
}
