namespace HslCommunication.Robot.ABB
{
    using HslCommunication;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public class ABBWebApiClient : NetworkWebApiBase, IRobotNet
    {
        public ABBWebApiClient(string ipAddress) : base(ipAddress)
        {
        }

        public ABBWebApiClient(string ipAddress, int port) : base(ipAddress, port)
        {
        }

        public ABBWebApiClient(string ipAddress, int port, string name, string password) : base(ipAddress, port, name, password)
        {
        }

        private OperateResult<string> AnalysisClassAttribute(string content, string[] atts)
        {
            JObject obj2 = new JObject();
            for (int i = 0; i < atts.Length; i++)
            {
                Match match = Regex.Match(content, "<span class=\"" + atts[i] + "\">[^<]*");
                if (!match.Success)
                {
                    return new OperateResult<string>(content);
                }
                obj2.Add(atts[i], new JValue(match.Value.Substring(15 + atts[i].Length)));
            }
            return OperateResult.CreateSuccessResult<string>(obj2.ToString());
        }

        private OperateResult<string> AnalysisRobotTarget(string content)
        {
            string[] atts = new string[] { "x", "y", "z", "q1", "q2", "q3" };
            return this.AnalysisClassAttribute(content, atts);
        }

        private OperateResult<string> AnalysisSystem(string content)
        {
            string[] atts = new string[] { "major", "minor", "build", "title", "type", "description", "date", "mctimestamp", "name", "sysid", "starttm" };
            return this.AnalysisClassAttribute(content, atts);
        }

        [HslMqttApi(Description="Get the current control state. The Content attribute is the control information of the robot")]
        public OperateResult<string> GetCtrlState()
        {
            OperateResult<string> result = this.ReadString("url=/rw/panel/ctrlstate");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"ctrlstate\">[^<]+");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x18));
        }

        [HslMqttApi(Description="Gets the current error state. The Content attribute is the state information of the robot")]
        public OperateResult<string> GetErrorState()
        {
            OperateResult<string> result = this.ReadString("url=/rw/motionsystem/errorstate");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"err-state\">[^<]+");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x18));
        }

        [HslMqttApi(Description="Gets the input IO2 of the current robot's native")]
        public OperateResult<string> GetIO2In()
        {
            OperateResult<string> result = this.ReadString("url=/rw/iosystem/devices/BK5250");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"indata\">[^<]*");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x15));
        }

        [HslMqttApi(Description="Gets the output IO2 of the current robot's native")]
        public OperateResult<string> GetIO2Out()
        {
            OperateResult<string> result = this.ReadString("url=/rw/iosystem/devices/BK5250");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"outdata\">[^<]*");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x16));
        }

        [HslMqttApi(Description="Gets the input IO of the current robot's native")]
        public OperateResult<string> GetIOIn()
        {
            OperateResult<string> result = this.ReadString("url=/rw/iosystem/devices/D652_10");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"indata\">[^<]*");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x15));
        }

        [HslMqttApi(Description="Gets the output IO of the current robot's native")]
        public OperateResult<string> GetIOOut()
        {
            OperateResult<string> result = this.ReadString("url=/rw/iosystem/devices/D652_10");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"outdata\">[^<]*");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x16));
        }

        [HslMqttApi(Description="Get the physical node information of the current robot and return the joint information in json format")]
        public OperateResult<string> GetJointTarget()
        {
            OperateResult<string> result = this.ReadString("url=/rw/motionsystem/mechunits/ROB_1/jointtarget");
            if (!result.IsSuccess)
            {
                return result;
            }
            MatchCollection matchs = Regex.Matches(result.Content, "<span class=\"rax[^<]*");
            if (matchs.Count != 6)
            {
                return new OperateResult<string>(result.Content);
            }
            double[] o = new double[6];
            for (int i = 0; i < matchs.Count; i++)
            {
                if (matchs[i].Length > 0x11)
                {
                    o[i] = double.Parse(matchs[i].Value.Substring(20));
                }
            }
            return OperateResult.CreateSuccessResult<string>(JArray.FromObject(o).ToString(Formatting.None, new JsonConverter[0]));
        }

        [HslMqttApi(Description="Gets the log record for the current robot, which is 10 by default")]
        public OperateResult<string> GetLog([Optional, DefaultParameterValue(10)] int logCount)
        {
            OperateResult<string> result = this.ReadString("url=/rw/elog/0?lang=zh&amp;resource=title");
            if (!result.IsSuccess)
            {
                return result;
            }
            MatchCollection matchs = Regex.Matches(result.Content, "<li class=\"elog-message-li\" title=\"/rw/elog/0/[0-9]+\">[\\S\\s]+?</li>");
            JArray array = new JArray();
            for (int i = 0; i < matchs.Count; i++)
            {
                if (i >= logCount)
                {
                    break;
                }
                Match match = Regex.Match(matchs[i].Value, "[0-9]+\"");
                JObject item = new JObject();
                char[] trimChars = new char[] { '"' };
                item["id"] = match.Value.TrimEnd(trimChars);
                foreach (XElement element in XElement.Parse(matchs[i].Value).Elements("span"))
                {
                    item[element.Attribute("class").Value] = element.Value;
                }
                array.Add(item);
            }
            return OperateResult.CreateSuccessResult<string>(array.ToString());
        }

        [HslMqttApi(Description="Gets the current working mode of the robot")]
        public OperateResult<string> GetOperationMode()
        {
            OperateResult<string> result = this.ReadString("url=/rw/panel/opmode");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"opmode\">[^<]*");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x15));
        }

        [HslMqttApi(Description="Get the current program running status of the current robot")]
        public OperateResult<string> GetRapidExecution()
        {
            OperateResult<string> result = this.ReadString("url=/rw/rapid/execution");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<li class=\"rap-execution\"[\\S\\s]+?</li>");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            JObject obj2 = new JObject();
            foreach (XElement element in XElement.Parse(match.Value).Elements("span"))
            {
                obj2[element.Attribute("class").Value] = element.Value;
            }
            return OperateResult.CreateSuccessResult<string>(obj2.ToString());
        }

        [HslMqttApi(Description="Get the task list of the current robot")]
        public OperateResult<string> GetRapidTasks()
        {
            OperateResult<string> result = this.ReadString("url=/rw/rapid/tasks");
            if (!result.IsSuccess)
            {
                return result;
            }
            MatchCollection matchs = Regex.Matches(result.Content, "<li class=\"rap-task-li\" [\\S\\s]+?</li>");
            JArray array = new JArray();
            for (int i = 0; i < matchs.Count; i++)
            {
                JObject item = new JObject();
                foreach (XElement element in XElement.Parse(matchs[i].Value).Elements("span"))
                {
                    item[element.Attribute("class").Value] = element.Value;
                }
                array.Add(item);
            }
            return OperateResult.CreateSuccessResult<string>(array.ToString());
        }

        [HslMqttApi(Description="Get the current robot's system information, version number, unique ID and other information")]
        public OperateResult<string> GetRobotTarget()
        {
            OperateResult<string> result = this.ReadString("url=/rw/motionsystem/mechunits/ROB_1/robtarget");
            if (!result.IsSuccess)
            {
                return result;
            }
            return this.AnalysisRobotTarget(result.Content);
        }

        public static List<string> GetSelectStrings()
        {
            return new List<string> { 
                "ErrorState",
                "jointtarget",
                "PhysicalJoints",
                "SpeedRatio",
                "OperationMode",
                "CtrlState",
                "ioin",
                "ioout",
                "io2in",
                "io2out",
                "log",
                "system",
                "robtarget",
                "ServoEnable",
                "RapidExecution",
                "RapidTasks"
            };
        }

        [HslMqttApi(Description="Get the current robot servo enable state")]
        public OperateResult<string> GetServoEnable()
        {
            OperateResult<string> result = this.ReadString("url=/rw/iosystem/signals/Local/DRV_1/DRV1K1");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<li class=\"ios-signal\"[\\S\\s]+?</li>");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            JObject obj2 = new JObject();
            foreach (XElement element in XElement.Parse(match.Value).Elements("span"))
            {
                obj2[element.Attribute("class").Value] = element.Value;
            }
            return OperateResult.CreateSuccessResult<string>(obj2.ToString());
        }

        [HslMqttApi(Description="Get the speed matching information of the current robot")]
        public OperateResult<string> GetSpeedRatio()
        {
            OperateResult<string> result = this.ReadString("url=/rw/panel/speedratio");
            if (!result.IsSuccess)
            {
                return result;
            }
            Match match = Regex.Match(result.Content, "<span class=\"speedratio\">[^<]*");
            if (!match.Success)
            {
                return new OperateResult<string>(result.Content);
            }
            return OperateResult.CreateSuccessResult<string>(match.Value.Substring(0x19));
        }

        [HslMqttApi(Description="Get the current robot's system information, version number, unique ID and other information")]
        public OperateResult<string> GetSystem()
        {
            OperateResult<string> result = this.ReadString("url=/rw/system");
            if (!result.IsSuccess)
            {
                return result;
            }
            return this.AnalysisSystem(result.Content);
        }

        [HslMqttApi(ApiTopic="ReadRobotByte", Description="Read the other side of the data information, usually designed for the GET method information.If you start with url=, you are using native address access")]
        public override OperateResult<byte[]> Read(string address)
        {
            return base.Read(address);
        }

        protected override OperateResult<string> ReadByAddress(string address)
        {
            if (address.ToUpper() == "ErrorState".ToUpper())
            {
                return this.GetErrorState();
            }
            if (address.ToUpper() == "jointtarget".ToUpper())
            {
                return this.GetJointTarget();
            }
            if (address.ToUpper() == "PhysicalJoints".ToUpper())
            {
                return this.GetJointTarget();
            }
            if (address.ToUpper() == "SpeedRatio".ToUpper())
            {
                return this.GetSpeedRatio();
            }
            if (address.ToUpper() == "OperationMode".ToUpper())
            {
                return this.GetOperationMode();
            }
            if (address.ToUpper() == "CtrlState".ToUpper())
            {
                return this.GetCtrlState();
            }
            if (address.ToUpper() == "ioin".ToUpper())
            {
                return this.GetIOIn();
            }
            if (address.ToUpper() == "ioout".ToUpper())
            {
                return this.GetIOOut();
            }
            if (address.ToUpper() == "io2in".ToUpper())
            {
                return this.GetIO2In();
            }
            if (address.ToUpper() == "io2out".ToUpper())
            {
                return this.GetIO2Out();
            }
            if (address.ToUpper().StartsWith("log".ToUpper()))
            {
                int num;
                if ((address.Length > 3) && int.TryParse(address.Substring(3), out num))
                {
                    return this.GetLog(num);
                }
                return this.GetLog(10);
            }
            if (address.ToUpper() == "system".ToUpper())
            {
                return this.GetSystem();
            }
            if (address.ToUpper() == "robtarget".ToUpper())
            {
                return this.GetRobotTarget();
            }
            if (address.ToUpper() == "ServoEnable".ToUpper())
            {
                return this.GetServoEnable();
            }
            if (address.ToUpper() == "RapidExecution".ToUpper())
            {
                return this.GetRapidExecution();
            }
            if (address.ToUpper() == "RapidTasks".ToUpper())
            {
                return this.GetRapidTasks();
            }
            return base.ReadByAddress(address);
        }

        [HslMqttApi(ApiTopic="ReadRobotString", Description="The string data information that reads the other party information, usually designed for the GET method information.If you start with url=, you are using native address access")]
        public override OperateResult<string> ReadString(string address)
        {
            return base.ReadString(address);
        }

        public override string ToString()
        {
            return string.Format("ABBWebApiClient[{0}:{1}]", base.IpAddress, base.Port);
        }

        [HslMqttApi(ApiTopic="WriteRobotByte", Description="Using POST to request data information from the other party, we need to start with url= to indicate that we are using native address access")]
        public override OperateResult Write(string address, byte[] value)
        {
            return base.Write(address, value);
        }

        [HslMqttApi(ApiTopic="WriteRobotString", Description="Using POST to request data information from the other party, we need to start with url= to indicate that we are using native address access")]
        public override OperateResult Write(string address, string value)
        {
            return base.Write(address, value);
        }
    }
}

