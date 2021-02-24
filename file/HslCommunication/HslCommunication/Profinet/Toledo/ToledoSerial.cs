namespace HslCommunication.Profinet.Toledo
{
    using HslCommunication.LogNet;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.Ports;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ToledoSerial
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <BaudRate>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <PortName>k__BackingField;
        private ILogNet logNet;
        private SerialPort serialPort = new SerialPort();

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event ToledoStandardDataReceivedDelegate OnToledoStandardDataReceived;

        public ToledoSerial()
        {
            this.serialPort.RtsEnable = true;
            this.serialPort.DataReceived += new SerialDataReceivedEventHandler(this.SerialPort_DataReceived);
        }

        public void Close()
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Close();
            }
        }

        public bool IsOpen()
        {
            return this.serialPort.IsOpen;
        }

        public void Open()
        {
            if (!this.serialPort.IsOpen)
            {
                this.serialPort.Open();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> list = new List<byte>();
            byte[] buffer = new byte[0x400];
            while (true)
            {
                Thread.Sleep(20);
                if (this.serialPort.BytesToRead < 1)
                {
                    if (list.Count != 0)
                    {
                        if (this.OnToledoStandardDataReceived != null)
                        {
                            ToledoStandardDataReceivedDelegate onToledoStandardDataReceived = this.OnToledoStandardDataReceived;
                            onToledoStandardDataReceived(this, new ToledoStandardData(list.ToArray()));
                            return;
                        }
                        else
                        {
                            ToledoStandardDataReceivedDelegate expressionStack_BE_0 = this.OnToledoStandardDataReceived;
                        }
                    }
                    return;
                }
                try
                {
                    int length = this.serialPort.Read(buffer, 0, Math.Min(this.serialPort.BytesToRead, buffer.Length));
                    byte[] destinationArray = new byte[length];
                    Array.Copy(buffer, 0, destinationArray, 0, length);
                    list.AddRange(destinationArray);
                }
                catch (Exception exception)
                {
                    ILogNet logNet;
                    if (this.logNet != null)
                    {
                        logNet = this.logNet;
                        goto Label_0086;
                    }
                    else
                    {
                        ILogNet expressionStack_83_0 = this.logNet;
                    }
                    return;
                Label_0086:
                    logNet.WriteException(this.ToString(), "SerialPort_DataReceived", exception);
                    return;
                }
            }
        }

        public void SerialPortInni(Action<SerialPort> initi)
        {
            if (!this.serialPort.IsOpen)
            {
                this.serialPort.PortName = "COM5";
                this.serialPort.BaudRate = 0x2580;
                this.serialPort.DataBits = 8;
                this.serialPort.StopBits = StopBits.One;
                this.serialPort.Parity = Parity.None;
                initi(this.serialPort);
                this.PortName = this.serialPort.PortName;
                this.BaudRate = this.serialPort.BaudRate;
            }
        }

        public void SerialPortInni(string portName)
        {
            this.SerialPortInni(portName, 0x2580);
        }

        public void SerialPortInni(string portName, int baudRate)
        {
            this.SerialPortInni(portName, baudRate, 8, StopBits.One, Parity.None);
        }

        public void SerialPortInni(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            if (!this.serialPort.IsOpen)
            {
                this.serialPort.PortName = portName;
                this.serialPort.BaudRate = baudRate;
                this.serialPort.DataBits = dataBits;
                this.serialPort.StopBits = stopBits;
                this.serialPort.Parity = parity;
                this.PortName = this.serialPort.PortName;
                this.BaudRate = this.serialPort.BaudRate;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public int BaudRate { get; private set; }

        public ILogNet LogNet
        {
            get
            {
                return this.logNet;
            }
            set
            {
                this.logNet = value;
            }
        }

        public string PortName { get; private set; }

        public bool RtsEnable
        {
            get
            {
                return this.serialPort.RtsEnable;
            }
            set
            {
                this.serialPort.RtsEnable = value;
            }
        }

        public delegate void ToledoStandardDataReceivedDelegate(object sender, ToledoStandardData toledoStandardData);
    }
}

