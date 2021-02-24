namespace HslCommunication.DTU
{
    using HslCommunication.Core.Net;
    using HslCommunication.ModBus;
    using HslCommunication.Profinet.AllenBradley;
    using HslCommunication.Profinet.Melsec;
    using HslCommunication.Profinet.Omron;
    using HslCommunication.Profinet.Siemens;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class DTUSettingType
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <DtuId>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <DtuType>k__BackingField = "ModbusRtuOverTcp";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <JsonParameter>k__BackingField = "{}";

        public virtual NetworkDeviceBase GetClient()
        {
            JObject obj2 = JObject.Parse(this.JsonParameter);
            if (this.DtuType == "ModbusRtuOverTcp")
            {
                return new ModbusRtuOverTcp("127.0.0.1", 0x1f6, obj2["Station"].Value<byte>()) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "ModbusTcpNet")
            {
                return new ModbusTcpNet("127.0.0.1", 0x1f6, obj2["Station"].Value<byte>()) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecMcNet")
            {
                return new MelsecMcNet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecMcAsciiNet")
            {
                return new MelsecMcAsciiNet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecA1ENet")
            {
                return new MelsecA1ENet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecA1EAsciiNet")
            {
                return new MelsecA1EAsciiNet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecA3CNet1OverTcp")
            {
                return new MelsecA3CNet1OverTcp("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecFxLinksOverTcp")
            {
                return new MelsecFxLinksOverTcp("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "MelsecFxSerialOverTcp")
            {
                return new MelsecFxSerialOverTcp("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "SiemensS7Net")
            {
                return new SiemensS7Net((SiemensPLCS) Enum.Parse(typeof(SiemensPLCS), obj2["SiemensPLCS"].Value<string>())) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "SiemensFetchWriteNet")
            {
                return new SiemensFetchWriteNet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "SiemensPPIOverTcp")
            {
                return new SiemensPPIOverTcp("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "OmronFinsNet")
            {
                return new OmronFinsNet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType == "OmronHostLinkOverTcp")
            {
                return new OmronHostLinkOverTcp("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
            }
            if (this.DtuType != "AllenBradleyNet")
            {
                throw new NotImplementedException();
            }
            return new AllenBradleyNet("127.0.0.1", 0x1388) { ConnectionId = this.DtuId };
        }

        public string DtuId { get; set; }

        public string DtuType { get; set; }

        public string JsonParameter { get; set; }
    }
}

