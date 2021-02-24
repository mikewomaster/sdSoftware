namespace HslCommunication.Core.Address
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ModbusAddress : DeviceAddressBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Function>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Station>k__BackingField;

        public ModbusAddress()
        {
            this.Station = -1;
            this.Function = -1;
            base.Address = 0;
        }

        public ModbusAddress(string address)
        {
            this.Station = -1;
            this.Function = -1;
            base.Address = 0;
            this.Parse(address);
        }

        public ModbusAddress(string address, byte function)
        {
            this.Station = -1;
            this.Function = function;
            base.Address = 0;
            this.Parse(address);
        }

        public ModbusAddress(string address, byte station, byte function)
        {
            this.Station = -1;
            this.Function = function;
            this.Station = station;
            base.Address = 0;
            this.Parse(address);
        }

        public ModbusAddress AddressAdd()
        {
            return this.AddressAdd(1);
        }

        public ModbusAddress AddressAdd(int value)
        {
            return new ModbusAddress { 
                Station = this.Station,
                Function = this.Function,
                Address = (ushort) (base.Address + value)
            };
        }

        public override void Parse(string address)
        {
            if (address.IndexOf(';') < 0)
            {
                base.Address = ushort.Parse(address);
            }
            else
            {
                char[] separator = new char[] { ';' };
                string[] strArray = address.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if ((strArray[i][0] == 's') || (strArray[i][0] == 'S'))
                    {
                        this.Station = byte.Parse(strArray[i].Substring(2));
                    }
                    else if ((strArray[i][0] == 'x') || (strArray[i][0] == 'X'))
                    {
                        this.Function = byte.Parse(strArray[i].Substring(2));
                    }
                    else
                    {
                        base.Address = ushort.Parse(strArray[i]);
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (this.Station >= 0)
            {
                builder.Append("s=" + this.Station.ToString() + ";");
            }
            if (this.Function >= 1)
            {
                builder.Append("x=" + this.Function.ToString() + ";");
            }
            builder.Append(base.Address.ToString());
            return builder.ToString();
        }

        public int Function { get; set; }

        public int Station { get; set; }
    }
}

