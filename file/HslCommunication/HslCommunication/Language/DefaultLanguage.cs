namespace HslCommunication.Language
{
    using HslCommunication;
    using System;

    public class DefaultLanguage
    {
        public virtual string AllenBradley04
        {
            get
            {
                return "它没有正确生成或匹配标记不存在。";
            }
        }

        public virtual string AllenBradley05
        {
            get
            {
                return "引用的特定项（通常是实例）无法找到。";
            }
        }

        public virtual string AllenBradley06
        {
            get
            {
                return "请求的数据量不适合响应缓冲区。 发生了部分数据传输。";
            }
        }

        public virtual string AllenBradley0A
        {
            get
            {
                return "尝试处理其中一个属性时发生错误。";
            }
        }

        public virtual string AllenBradley13
        {
            get
            {
                return "命令中没有提供足够的命令数据/参数来执行所请求的服务。";
            }
        }

        public virtual string AllenBradley1C
        {
            get
            {
                return "与属性计数相比，提供的属性数量不足。";
            }
        }

        public virtual string AllenBradley1E
        {
            get
            {
                return "此服务中的服务请求出错。";
            }
        }

        public virtual string AllenBradley20
        {
            get
            {
                return "命令中参数的数据类型与实际参数的数据类型不一致。";
            }
        }

        public virtual string AllenBradley26
        {
            get
            {
                return "IOI字长与处理的IOI数量不匹配。";
            }
        }

        public virtual string AllenBradleySessionStatus00
        {
            get
            {
                return "成功";
            }
        }

        public virtual string AllenBradleySessionStatus01
        {
            get
            {
                return "发件人发出无效或不受支持的封装命令。";
            }
        }

        public virtual string AllenBradleySessionStatus02
        {
            get
            {
                return "接收器中的内存资源不足以处理命令。 这不是一个应用程序错误。 相反，只有在封装层无法获得所需内存资源的情况下才会导致此问题。";
            }
        }

        public virtual string AllenBradleySessionStatus03
        {
            get
            {
                return "封装消息的数据部分中的数据形成不良或不正确。";
            }
        }

        public virtual string AllenBradleySessionStatus64
        {
            get
            {
                return "向目标发送封装消息时，始发者使用了无效的会话句柄。";
            }
        }

        public virtual string AllenBradleySessionStatus65
        {
            get
            {
                return "目标收到一个无效长度的信息。";
            }
        }

        public virtual string AllenBradleySessionStatus69
        {
            get
            {
                return "不支持的封装协议修订。";
            }
        }

        public virtual string AttemptConnectServer
        {
            get
            {
                return "正在尝试第{0}次连接服务器";
            }
        }

        public virtual string AuthorizationFailed
        {
            get
            {
                return ("系统授权失败，需要使用激活码授权，谢谢支持。设备激活数：" + Authorization.iahsduiwikaskfhishfdi.ToString());
            }
        }

        public virtual string BugSubmit
        {
            get
            {
                return "Bug提交";
            }
        }

        public virtual string CheckDataTimeout
        {
            get
            {
                return "等待检查数据时，发生了超时，超时时间为：";
            }
        }

        public virtual string ClientDisableLogin
        {
            get
            {
                return "客户端 [ {0} ] 不被信任，禁止登录";
            }
        }

        public virtual string ClientOfflineInfo
        {
            get
            {
                return "客户端 [ {0} ] 下线";
            }
        }

        public virtual string ClientOnlineInfo
        {
            get
            {
                return "客户端 [ {0} ] 上线";
            }
        }

        public virtual string Close
        {
            get
            {
                return "关闭";
            }
        }

        public virtual string CommandHeadCodeCheckFailed
        {
            get
            {
                return "命令头校验失败";
            }
        }

        public virtual string CommandLengthCheckFailed
        {
            get
            {
                return "命令长度检查失败";
            }
        }

        public virtual string ConnectedFailed
        {
            get
            {
                return "连接失败：";
            }
        }

        public virtual string ConnectedSuccess
        {
            get
            {
                return "连接成功！";
            }
        }

        public virtual string ConnectFailedAndWait
        {
            get
            {
                return "连接断开，等待{0}秒后重新连接";
            }
        }

        public virtual string ConnectingServer
        {
            get
            {
                return "正在连接服务器...";
            }
        }

        public virtual string ConnectionIsNotAvailable
        {
            get
            {
                return "当前的连接不可用";
            }
        }

        public virtual string ConnectServerSuccess
        {
            get
            {
                return "连接服务器成功";
            }
        }

        public virtual string ConnectTimeout
        {
            get
            {
                return "连接失败，超时时间为 {0}";
            }
        }

        public virtual string DataLengthIsNotEnough
        {
            get
            {
                return "接收的数据长度不足，应该值:{0},实际值:{1}";
            }
        }

        public virtual string DataSourceFormatError
        {
            get
            {
                return "数据源格式不正确";
            }
        }

        public virtual string DataTransformError
        {
            get
            {
                return "数据转换失败，源数据：";
            }
        }

        public virtual string DeviceCurrentIsLoginForbidden
        {
            get
            {
                return "当前设备的id禁止登录";
            }
        }

        public virtual string DeviceCurrentIsLoginRepeat
        {
            get
            {
                return "当前设备的id重复登录";
            }
        }

        public virtual string DLTAddressCannotMoreThan12
        {
            get
            {
                return "地址信息长度不能大于12";
            }
        }

        public virtual string DLTAddressCannotNull
        {
            get
            {
                return "地址信息不能为空或是长度为0";
            }
        }

        public virtual string DLTAddressMatchFailed
        {
            get
            {
                return "地址格式配对失败，请检查是否是少于12个字，且都是0-9或A的数字组成的地址";
            }
        }

        public virtual string DLTErrorInfoBit0
        {
            get
            {
                return "其他错误";
            }
        }

        public virtual string DLTErrorInfoBit1
        {
            get
            {
                return "无请求数据";
            }
        }

        public virtual string DLTErrorInfoBit2
        {
            get
            {
                return "密码错/未授权";
            }
        }

        public virtual string DLTErrorInfoBit3
        {
            get
            {
                return "通信速率不能更改";
            }
        }

        public virtual string DLTErrorInfoBit4
        {
            get
            {
                return "年时区数超";
            }
        }

        public virtual string DLTErrorInfoBit5
        {
            get
            {
                return "日时段数超";
            }
        }

        public virtual string DLTErrorInfoBit6
        {
            get
            {
                return "费率数超";
            }
        }

        public virtual string DLTErrorInfoBit7
        {
            get
            {
                return "保留";
            }
        }

        public virtual string DLTErrorWriteReadCheckFailed
        {
            get
            {
                return "校验写入之后和之前的数据是否一致失败";
            }
        }

        public virtual string ErrorCode
        {
            get
            {
                return "错误代号";
            }
        }

        public virtual string ExceptionCustomer
        {
            get
            {
                return "用户自定义方法出错：";
            }
        }

        public virtual string ExceptionMessage
        {
            get
            {
                return "错误信息：";
            }
        }

        public virtual string ExceptionSource
        {
            get
            {
                return "错误源：";
            }
        }

        public virtual string ExceptionStackTrace
        {
            get
            {
                return "错误堆栈：";
            }
        }

        public virtual string ExceptionTargetSite
        {
            get
            {
                return "错误方法：";
            }
        }

        public virtual string ExceptionType
        {
            get
            {
                return "错误类型：";
            }
        }

        public virtual string FatekStatus02
        {
            get
            {
                return "不合法数值";
            }
        }

        public virtual string FatekStatus03
        {
            get
            {
                return "禁止写入";
            }
        }

        public virtual string FatekStatus04
        {
            get
            {
                return "不合法的命令码";
            }
        }

        public virtual string FatekStatus05
        {
            get
            {
                return "不能激活(下RUN命令但Ladder Checksum不合)";
            }
        }

        public virtual string FatekStatus06
        {
            get
            {
                return "不能激活(下RUN命令但PLC ID≠ Ladder ID)";
            }
        }

        public virtual string FatekStatus07
        {
            get
            {
                return "不能激活（下RUN命令但程序语法错误）";
            }
        }

        public virtual string FatekStatus09
        {
            get
            {
                return "不能激活（下RUN命令，但Ladder之程序指令PLC无法执行）";
            }
        }

        public virtual string FatekStatus10
        {
            get
            {
                return "不合法的地址";
            }
        }

        public virtual string FileDeleteFailed
        {
            get
            {
                return "文件删除异常";
            }
        }

        public virtual string FileDeleteSuccess
        {
            get
            {
                return "文件删除成功";
            }
        }

        public virtual string FileDownloadFailed
        {
            get
            {
                return "文件下载异常";
            }
        }

        public virtual string FileDownloadSuccess
        {
            get
            {
                return "文件下载成功";
            }
        }

        public virtual string FileLoadFailed
        {
            get
            {
                return "文件加载失败";
            }
        }

        public virtual string FileNotExist
        {
            get
            {
                return "文件不存在";
            }
        }

        public virtual string FilePathCreateFailed
        {
            get
            {
                return "文件夹路径创建失败：";
            }
        }

        public virtual string FileReadFromNetFailed
        {
            get
            {
                return "从网络读取文件异常";
            }
        }

        public virtual string FileReceiveFailed
        {
            get
            {
                return "确认文件接收异常";
            }
        }

        public virtual string FileRemoteNotExist
        {
            get
            {
                return "对方文件不存在，无法接收！";
            }
        }

        public virtual string FileSaveFailed
        {
            get
            {
                return "文件存储失败";
            }
        }

        public virtual string FileSendClientFailed
        {
            get
            {
                return "文件发送的时候发生了异常";
            }
        }

        public virtual string FileUploadFailed
        {
            get
            {
                return "文件上传异常";
            }
        }

        public virtual string FileUploadSuccess
        {
            get
            {
                return "文件上传成功";
            }
        }

        public virtual string FileWriteToNetFailed
        {
            get
            {
                return "文件写入网络异常";
            }
        }

        public virtual string FujiSpbStatus01
        {
            get
            {
                return "对ROM进行了写入";
            }
        }

        public virtual string FujiSpbStatus02
        {
            get
            {
                return "接收了未定义的命令或无法处理的命令";
            }
        }

        public virtual string FujiSpbStatus03
        {
            get
            {
                return "数据部分有矛盾（参数异常）";
            }
        }

        public virtual string FujiSpbStatus04
        {
            get
            {
                return "由于收到了其他编程器的传送联锁，因此无法处理";
            }
        }

        public virtual string FujiSpbStatus05
        {
            get
            {
                return "模块序号不正确";
            }
        }

        public virtual string FujiSpbStatus06
        {
            get
            {
                return "检索项目未找到";
            }
        }

        public virtual string FujiSpbStatus07
        {
            get
            {
                return "指定了超出模块范围的地址（写入时）";
            }
        }

        public virtual string FujiSpbStatus09
        {
            get
            {
                return "由于故障程序无法执行（RUN）";
            }
        }

        public virtual string FujiSpbStatus0C
        {
            get
            {
                return "密码不一致";
            }
        }

        public virtual string GeSRTPAddressCannotBeZero
        {
            get
            {
                return "当前的地址起始地址不能为0，需要从1开始";
            }
        }

        public virtual string GeSRTPNotSupportBitReadWrite
        {
            get
            {
                return "当前的地址数据不支持位单位的读写操作";
            }
        }

        public virtual string GeSRTPNotSupportByteReadWrite
        {
            get
            {
                return "当前的地址数据不支持字节单位的读写操作，只能使用字单位读写";
            }
        }

        public virtual string GeSRTPWriteLengthMustBeEven
        {
            get
            {
                return "当前的地址写入的数据长度必须是偶数";
            }
        }

        public virtual string GetClientIpAddressFailed
        {
            get
            {
                return "客户端IP地址获取失败";
            }
        }

        public virtual string InsufficientPrivileges
        {
            get
            {
                return "当前的方法接口或类，只对商业授权用户开放，权限不足，感谢支持。如果需要商业授权，联系QQ200962190，微信：13516702732，Email:hsl200909@163.com";
            }
        }

        public virtual string IpAddressError
        {
            get
            {
                return "Ip地址输入异常，格式不正确";
            }
        }

        public virtual string KeyenceSR2000Error00
        {
            get
            {
                return "接收未定义的命令";
            }
        }

        public virtual string KeyenceSR2000Error01
        {
            get
            {
                return "命令格式不匹配。（参数的数量有误）";
            }
        }

        public virtual string KeyenceSR2000Error02
        {
            get
            {
                return "超出参数1的设置范围";
            }
        }

        public virtual string KeyenceSR2000Error03
        {
            get
            {
                return "超出参数2的设置范围";
            }
        }

        public virtual string KeyenceSR2000Error04
        {
            get
            {
                return "在HEX（十六进制）码中未设置参数2";
            }
        }

        public virtual string KeyenceSR2000Error05
        {
            get
            {
                return "参数2属于HEX（十六进制）码，但是超出了设置范围";
            }
        }

        public virtual string KeyenceSR2000Error10
        {
            get
            {
                return "预设数据内存在两个以上的！预设数据有误";
            }
        }

        public virtual string KeyenceSR2000Error11
        {
            get
            {
                return "区域指定数据有误";
            }
        }

        public virtual string KeyenceSR2000Error12
        {
            get
            {
                return "指定文件不存在";
            }
        }

        public virtual string KeyenceSR2000Error13
        {
            get
            {
                return "超出了%Tmm-LON, bb命令的mm的设置范围";
            }
        }

        public virtual string KeyenceSR2000Error14
        {
            get
            {
                return "用%Tmm-KEYENCE命令无法确认通信";
            }
        }

        public virtual string KeyenceSR2000Error20
        {
            get
            {
                return "在当前的模式下不允许执行此命令（执行错误）";
            }
        }

        public virtual string KeyenceSR2000Error21
        {
            get
            {
                return "缓冲区已满，不能执行命令";
            }
        }

        public virtual string KeyenceSR2000Error22
        {
            get
            {
                return "加载或保存参数时发生错误";
            }
        }

        public virtual string KeyenceSR2000Error23
        {
            get
            {
                return "由于正在连接 AutoID Netwoerk Navigator, 因此不能接收 RS-232C 发送的命令";
            }
        }

        public virtual string KeyenceSR2000Error99
        {
            get
            {
                return "如果觉得SR-2000系列有异常，请联系基恩士公司";
            }
        }

        public virtual string KeyIsExistAlready
        {
            get
            {
                return "当前的关键字已经存在";
            }
        }

        public virtual string KeyIsNotAllowedNull
        {
            get
            {
                return "关键字不允许为空";
            }
        }

        public virtual string KeyIsNotExist
        {
            get
            {
                return "当前订阅的关键字不存在";
            }
        }

        public virtual string LogNetAbandon
        {
            get
            {
                return "放弃";
            }
        }

        public virtual string LogNetAll
        {
            get
            {
                return "全部";
            }
        }

        public virtual string LogNetDebug
        {
            get
            {
                return "调试";
            }
        }

        public virtual string LogNetError
        {
            get
            {
                return "错误";
            }
        }

        public virtual string LogNetFatal
        {
            get
            {
                return "致命";
            }
        }

        public virtual string LogNetInfo
        {
            get
            {
                return "信息";
            }
        }

        public virtual string LogNetWarn
        {
            get
            {
                return "警告";
            }
        }

        public virtual string MailSendTail
        {
            get
            {
                return "邮件服务系统自动发出，请勿回复！";
            }
        }

        public virtual string MailServerCenter
        {
            get
            {
                return "邮件发送系统";
            }
        }

        public virtual string MelsecCurrentTypeNotSupportedBitOperate
        {
            get
            {
                return "当前的类型不支持位读写";
            }
        }

        public virtual string MelsecCurrentTypeNotSupportedWordOperate
        {
            get
            {
                return "当前的类型不支持字读写";
            }
        }

        public virtual string MelsecError02
        {
            get
            {
                return "“读/写”(入/出)软元件的指定范围不正确。";
            }
        }

        public virtual string MelsecError51
        {
            get
            {
                return "在使用随机访问缓冲存储器的通讯时，由外部设备指定的起始地址设置在 0-6143 的范围之外。解决方法:检查及纠正指定的起始地址。";
            }
        }

        public virtual string MelsecError52
        {
            get
            {
                return "1. 在使用随机访问缓冲存储器的通讯时，由外部设备指定的起始地址+数据字数的计数(读时取决于设置)超出了 0-6143 的范围。\r\n2. 指定字数计数(文本)的数据不能用一个帧发送。(数据长度数值和通讯的文本总数不在允许的范围之内。)";
            }
        }

        public virtual string MelsecError54
        {
            get
            {
                return "当通过 GX Developer 在[操作设置]-[通讯数据代码]中选择“ASCII码通讯”时，则接收来自外部设备的、不能转换为二进制代码的ASCII 码。";
            }
        }

        public virtual string MelsecError55
        {
            get
            {
                return "当不能通过 GX Developer(无检查标记)来设置[操作设置]-[无法在运行时间内写入]时，如 PLCCPU 处于运行状态，则外部设备请求写入数据。 ";
            }
        }

        public virtual string MelsecError56
        {
            get
            {
                return "从外部进行的软元件指定不正确。";
            }
        }

        public virtual string MelsecError58
        {
            get
            {
                return "1. 由外部设备指定的命令起始地址(起始软元件号和起始步号)可设置在指定范围外。\r\n2. 为扩展文件寄存器指定的块号不存在。\r\n3. 不能指定文件寄存器(R)。\r\n4. 为位软元件的命令指定字软元件。\r\n5. 位软元件的起始号由某一个数值指定，此数值不是字软元件命令中16 的倍数。";
            }
        }

        public virtual string MelsecError59
        {
            get
            {
                return "不能指定扩展文件的寄存器。";
            }
        }

        public virtual string MelsecErrorC04D
        {
            get
            {
                return "在以太网模块通过自动开放 UDP端口通讯或无序固定缓冲存储器通讯接收的信息中，应用领域中指定的数据长度不正确。";
            }
        }

        public virtual string MelsecErrorC050
        {
            get
            {
                return "当在以太网模块中进行 ASCII 代码通讯的操作设置时，接收不能转化为二进制代码的 ASCII 代码数据。";
            }
        }

        public virtual string MelsecErrorC051_54
        {
            get
            {
                return "读/写点的数目在允许范围之外。";
            }
        }

        public virtual string MelsecErrorC055
        {
            get
            {
                return "文件数据读/写点的数目在允许范围之外。";
            }
        }

        public virtual string MelsecErrorC056
        {
            get
            {
                return "读/写请求超过了最大地址。";
            }
        }

        public virtual string MelsecErrorC057
        {
            get
            {
                return "请求数据的长度与字符区域(部分文本)的数据计数不匹配。";
            }
        }

        public virtual string MelsecErrorC058
        {
            get
            {
                return "在经过 ASCII 二进制转换后，请求数据的长度与字符区域( 部分文本)的数据计数不相符。";
            }
        }

        public virtual string MelsecErrorC059
        {
            get
            {
                return "命令和子命令的指定不正确。";
            }
        }

        public virtual string MelsecErrorC05A_B
        {
            get
            {
                return "以太网模块不能对指定软元件进行读出和写入";
            }
        }

        public virtual string MelsecErrorC05C
        {
            get
            {
                return "请求内容不正确。 ( 以位为单元请求读 / 写至字软元件。)";
            }
        }

        public virtual string MelsecErrorC05D
        {
            get
            {
                return "不执行监视注册。";
            }
        }

        public virtual string MelsecErrorC05E
        {
            get
            {
                return "以太网模块和 PLC CPU 之间的通讯时问超过了 CPU 监视定时器的时间。";
            }
        }

        public virtual string MelsecErrorC05F
        {
            get
            {
                return "目标 PLC 上不能执行请求。";
            }
        }

        public virtual string MelsecErrorC060
        {
            get
            {
                return "请求内容不正确。 ( 对位软元件等指定了不正确的数据。) ";
            }
        }

        public virtual string MelsecErrorC061
        {
            get
            {
                return "请求数据的长度与字符区域(部分文本)中的数据数目不相符。 ";
            }
        }

        public virtual string MelsecErrorC062
        {
            get
            {
                return "禁止在线更正时，通过 MC 协议远程 I/O 站执行( QnA兼容 3E 帧或4E 帧)写入操作。";
            }
        }

        public virtual string MelsecErrorC070
        {
            get
            {
                return "不能为目标站指定软元件存储器的范围";
            }
        }

        public virtual string MelsecErrorC072
        {
            get
            {
                return "请求内容不正确。 ( 以位为单元请求调写至字软元件。) ";
            }
        }

        public virtual string MelsecErrorC074
        {
            get
            {
                return "目标 PLC 不执行请求。需要纠正网络号和 PC 号。";
            }
        }

        public virtual string MelsecFxAckNagative
        {
            get
            {
                return "PLC反馈的数据无效";
            }
        }

        public virtual string MelsecFxAckWrong
        {
            get
            {
                return "PLC反馈信号错误：";
            }
        }

        public virtual string MelsecFxCrcCheckFailed
        {
            get
            {
                return "PLC反馈报文的和校验失败！";
            }
        }

        public virtual string MelsecFxReceiveZero
        {
            get
            {
                return "接收的数据长度为0";
            }
        }

        public virtual string MelsecPleaseReferToManualDocument
        {
            get
            {
                return "请查看三菱的通讯手册来查看报警的具体信息";
            }
        }

        public virtual string MelsecReadBitInfo
        {
            get
            {
                return "读取位变量数组只能针对位软元件，如果读取字软元件，请调用Read方法";
            }
        }

        public virtual string MessageTip
        {
            get
            {
                return "消息提示：";
            }
        }

        public virtual string ModbusAddressMustMoreThanOne
        {
            get
            {
                return "地址值在起始地址为1的情况下，必须大于1";
            }
        }

        public virtual string ModbusAsciiFormatCheckFailed
        {
            get
            {
                return "Modbus的ascii指令检查失败，不是modbus-ascii报文";
            }
        }

        public virtual string ModbusBitIndexOverstep
        {
            get
            {
                return "位访问的索引越界，应该在0-15之间";
            }
        }

        public virtual string ModbusCRCCheckFailed
        {
            get
            {
                return "Modbus的CRC校验检查失败";
            }
        }

        public virtual string ModbusLRCCheckFailed
        {
            get
            {
                return "Modbus的LRC校验检查失败";
            }
        }

        public virtual string ModbusMatchFailed
        {
            get
            {
                return "不是标准的modbus协议";
            }
        }

        public virtual string ModbusTcpFunctionCodeNotSupport
        {
            get
            {
                return "不支持的功能码";
            }
        }

        public virtual string ModbusTcpFunctionCodeOverBound
        {
            get
            {
                return "读取的数据越界";
            }
        }

        public virtual string ModbusTcpFunctionCodeQuantityOver
        {
            get
            {
                return "读取长度超过最大值";
            }
        }

        public virtual string ModbusTcpFunctionCodeReadWriteException
        {
            get
            {
                return "读写异常";
            }
        }

        public virtual string ModbusTcpReadCoilException
        {
            get
            {
                return "读取线圈异常";
            }
        }

        public virtual string ModbusTcpReadRegisterException
        {
            get
            {
                return "读取寄存器异常";
            }
        }

        public virtual string ModbusTcpWriteCoilException
        {
            get
            {
                return "写入线圈异常";
            }
        }

        public virtual string ModbusTcpWriteRegisterException
        {
            get
            {
                return "写入寄存器异常";
            }
        }

        public virtual string MQTTDataTooLong
        {
            get
            {
                return "当前的数据长度超出了协议的限制";
            }
        }

        public virtual string MQTTStatus01
        {
            get
            {
                return "不可请求的协议版本";
            }
        }

        public virtual string MQTTStatus02
        {
            get
            {
                return "当前的Id被拒绝";
            }
        }

        public virtual string MQTTStatus03
        {
            get
            {
                return "服务器不可用";
            }
        }

        public virtual string MQTTStatus04
        {
            get
            {
                return "错误的用户名或是密码";
            }
        }

        public virtual string MQTTStatus05
        {
            get
            {
                return "当前无授权";
            }
        }

        public virtual string NetClientAccountTimeout
        {
            get
            {
                return "等待账户验证超时：";
            }
        }

        public virtual string NetClientAliasFailed
        {
            get
            {
                return "客户端的别名接收失败：";
            }
        }

        public virtual string NetClientBreak
        {
            get
            {
                return "异常掉线";
            }
        }

        public virtual string NetClientFull
        {
            get
            {
                return "服务器承载上限，收到超出的请求连接。";
            }
        }

        public virtual string NetClientLoginFailed
        {
            get
            {
                return "客户端登录中错误：";
            }
        }

        public virtual string NetClientOffline
        {
            get
            {
                return "下线";
            }
        }

        public virtual string NetClientOnline
        {
            get
            {
                return "上线";
            }
        }

        public virtual string NetEngineClose
        {
            get
            {
                return "关闭引擎";
            }
        }

        public virtual string NetEngineStart
        {
            get
            {
                return "启动引擎";
            }
        }

        public virtual string NetHeartCheckFailed
        {
            get
            {
                return "心跳验证异常：";
            }
        }

        public virtual string NetHeartCheckTimeout
        {
            get
            {
                return "心跳验证超时，强制下线：";
            }
        }

        public virtual string NotSupportedDataType
        {
            get
            {
                return "输入的类型不支持，请重新输入";
            }
        }

        public virtual string NotSupportedFunction
        {
            get
            {
                return "当前的功能逻辑不支持，或是当前的功能没有实现";
            }
        }

        public virtual string OmronAddressMustBeZeroToFifteen
        {
            get
            {
                return "输入的位地址只能在0-15之间";
            }
        }

        public virtual string OmronReceiveDataError
        {
            get
            {
                return "数据接收异常";
            }
        }

        public virtual string OmronStatus0
        {
            get
            {
                return "通讯正常";
            }
        }

        public virtual string OmronStatus1
        {
            get
            {
                return "消息头不是FINS";
            }
        }

        public virtual string OmronStatus2
        {
            get
            {
                return "数据长度太长";
            }
        }

        public virtual string OmronStatus20
        {
            get
            {
                return "超过连接上限";
            }
        }

        public virtual string OmronStatus21
        {
            get
            {
                return "指定的节点已经处于连接中";
            }
        }

        public virtual string OmronStatus22
        {
            get
            {
                return "尝试去连接一个受保护的网络节点，该节点还未配置到PLC中";
            }
        }

        public virtual string OmronStatus23
        {
            get
            {
                return "当前客户端的网络节点超过正常范围";
            }
        }

        public virtual string OmronStatus24
        {
            get
            {
                return "当前客户端的网络节点已经被使用";
            }
        }

        public virtual string OmronStatus25
        {
            get
            {
                return "所有的网络节点已经被使用";
            }
        }

        public virtual string OmronStatus3
        {
            get
            {
                return "该命令不支持";
            }
        }

        public virtual string PanasonicAddressBitStartMulti16
        {
            get
            {
                return "位写入的起始地址需要为16的倍数，示例：R0.0, R2.0, L3.0, Y4.0";
            }
        }

        public virtual string PanasonicAddressParameterCannotBeNull
        {
            get
            {
                return "地址参数不允许为空";
            }
        }

        public virtual string PanasonicBoolLengthMulti16
        {
            get
            {
                return "批量bool写入的数据长度需要为16的倍数，否则无法写入";
            }
        }

        public virtual string PanasonicMewStatus20
        {
            get
            {
                return "错误未知";
            }
        }

        public virtual string PanasonicMewStatus21
        {
            get
            {
                return "NACK错误，远程单元无法被正确识别，或者发生了数据错误。";
            }
        }

        public virtual string PanasonicMewStatus22
        {
            get
            {
                return "WACK 错误:用于远程单元的接收缓冲区已满。";
            }
        }

        public virtual string PanasonicMewStatus23
        {
            get
            {
                return "多重端口错误:远程单元编号(01 至 16)设置与本地单元重复。";
            }
        }

        public virtual string PanasonicMewStatus24
        {
            get
            {
                return "传输格式错误:试图发送不符合传输格式的数据，或者某一帧数据溢出或发生了数据错误。";
            }
        }

        public virtual string PanasonicMewStatus25
        {
            get
            {
                return "硬件错误:传输系统硬件停止操作。";
            }
        }

        public virtual string PanasonicMewStatus26
        {
            get
            {
                return "单元号错误:远程单元的编号设置超出 01 至 63 的范围。";
            }
        }

        public virtual string PanasonicMewStatus27
        {
            get
            {
                return "不支持错误:接收方数据帧溢出. 试图在不同的模块之间发送不同帧长度的数据。";
            }
        }

        public virtual string PanasonicMewStatus28
        {
            get
            {
                return "无应答错误:远程单元不存在. (超时)。";
            }
        }

        public virtual string PanasonicMewStatus29
        {
            get
            {
                return "缓冲区关闭错误:试图发送或接收处于关闭状态的缓冲区。";
            }
        }

        public virtual string PanasonicMewStatus30
        {
            get
            {
                return "超时错误:持续处于传输禁止状态。";
            }
        }

        public virtual string PanasonicMewStatus40
        {
            get
            {
                return "BCC 错误:在指令数据中发生传输错误。";
            }
        }

        public virtual string PanasonicMewStatus41
        {
            get
            {
                return "格式错误:所发送的指令信息不符合传输格式。";
            }
        }

        public virtual string PanasonicMewStatus42
        {
            get
            {
                return "不支持错误:发送了一个未被支持的指令。向未被支持的目标站发送了指令。";
            }
        }

        public virtual string PanasonicMewStatus43
        {
            get
            {
                return "处理步骤错误:在处于传输请求信息挂起时,发送了其他指令。";
            }
        }

        public virtual string PanasonicMewStatus50
        {
            get
            {
                return "链接设置错误:设置了实际不存在的链接编号。";
            }
        }

        public virtual string PanasonicMewStatus51
        {
            get
            {
                return "同时操作错误:当向其他单元发出指令时,本地单元的传输缓冲区已满。";
            }
        }

        public virtual string PanasonicMewStatus52
        {
            get
            {
                return "传输禁止错误:无法向其他单元传输。";
            }
        }

        public virtual string PanasonicMewStatus53
        {
            get
            {
                return "忙错误:在接收到指令时,正在处理其他指令。";
            }
        }

        public virtual string PanasonicMewStatus60
        {
            get
            {
                return "参数错误:在指令中包含有无法使用的代码,或者代码没有附带区域指定参数(X, Y, D), 等以外。";
            }
        }

        public virtual string PanasonicMewStatus61
        {
            get
            {
                return "数据错误:触点编号,区域编号,数据代码格式(BCD,hex,等)上溢出, 下溢出以及区域指定错误。";
            }
        }

        public virtual string PanasonicMewStatus62
        {
            get
            {
                return "寄存器错误:过多记录数据在未记录状态下的操作（监控记录、跟踪记录等。)。";
            }
        }

        public virtual string PanasonicMewStatus63
        {
            get
            {
                return "PLC 模式错误:当一条指令发出时，运行模式不能够对指令进行处理。";
            }
        }

        public virtual string PanasonicMewStatus65
        {
            get
            {
                return "保护错误:在存储保护状态下执行写操作到程序区域或系统寄存器。";
            }
        }

        public virtual string PanasonicMewStatus66
        {
            get
            {
                return "地址错误:地址（程序地址、绝对地址等）数据编码形式（BCD、hex 等）、上溢、下溢或指定范围错误。";
            }
        }

        public virtual string PanasonicMewStatus67
        {
            get
            {
                return "丢失数据错误:要读的数据不存在。（读取没有写入注释寄存区的数据。。";
            }
        }

        public virtual string PanasonicReceiveLengthMustLargerThan9
        {
            get
            {
                return "接收数据长度必须大于9";
            }
        }

        public virtual string PasswordCheckFailed
        {
            get
            {
                return "密码验证失败";
            }
        }

        public virtual string Receive
        {
            get
            {
                return "接收";
            }
        }

        public virtual string ReceiveDataLengthTooShort
        {
            get
            {
                return "接收的数据长度太短：";
            }
        }

        public virtual string ReceiveDataTimeout
        {
            get
            {
                return "接收数据超时：";
            }
        }

        public virtual string ReConnectServerAfterTenSeconds
        {
            get
            {
                return "在10秒后重新连接服务器";
            }
        }

        public virtual string ReConnectServerSuccess
        {
            get
            {
                return "重连服务器成功";
            }
        }

        public virtual string RemoteClosedConnection
        {
            get
            {
                return "远程关闭了连接";
            }
        }

        public virtual string SAMAddressStartWrong
        {
            get
            {
                return "SAM的字符串地址标识错误。";
            }
        }

        public virtual string SAMHeadCheckFailed
        {
            get
            {
                return "SAM的数据帧头检查失败。";
            }
        }

        public virtual string SAMLengthCheckFailed
        {
            get
            {
                return "SAM的数据长度检查失败。";
            }
        }

        public virtual string SAMReceiveLengthMustLargerThan8
        {
            get
            {
                return "接收数据长度小于8，必须大于8";
            }
        }

        public virtual string SAMStatus10
        {
            get
            {
                return "接收数据校验和错";
            }
        }

        public virtual string SAMStatus11
        {
            get
            {
                return "接收数据长度错";
            }
        }

        public virtual string SAMStatus21
        {
            get
            {
                return "接收数据命令错";
            }
        }

        public virtual string SAMStatus23
        {
            get
            {
                return "越权操作";
            }
        }

        public virtual string SAMStatus24
        {
            get
            {
                return "无法识别的错误";
            }
        }

        public virtual string SAMStatus31
        {
            get
            {
                return "证/卡认证 SAM 失败";
            }
        }

        public virtual string SAMStatus32
        {
            get
            {
                return "SAM 认证证/卡失败";
            }
        }

        public virtual string SAMStatus33
        {
            get
            {
                return "信息验证错误";
            }
        }

        public virtual string SAMStatus40
        {
            get
            {
                return "无法识别的卡类型";
            }
        }

        public virtual string SAMStatus41
        {
            get
            {
                return "读证/卡操作失败";
            }
        }

        public virtual string SAMStatus47
        {
            get
            {
                return "取随机数失败";
            }
        }

        public virtual string SAMStatus60
        {
            get
            {
                return "SAM 自检失败";
            }
        }

        public virtual string SAMStatus66
        {
            get
            {
                return "SAM 未经授权";
            }
        }

        public virtual string SAMStatus80
        {
            get
            {
                return "寻找证/卡失败";
            }
        }

        public virtual string SAMStatus81
        {
            get
            {
                return "选取证/卡失败";
            }
        }

        public virtual string SAMStatus90
        {
            get
            {
                return "操作成功";
            }
        }

        public virtual string SAMStatus91
        {
            get
            {
                return "证/卡中此项无内容";
            }
        }

        public virtual string SAMStatus9F
        {
            get
            {
                return "寻找证/卡成功";
            }
        }

        public virtual string SAMSumCheckFailed
        {
            get
            {
                return "SAM的数据校验和检查失败。";
            }
        }

        public virtual string Send
        {
            get
            {
                return "发送";
            }
        }

        public virtual string ServerFileCheckFailed
        {
            get
            {
                return "服务器确认文件失败，请重新上传";
            }
        }

        public virtual string SiemensDataLengthCheckFailed
        {
            get
            {
                return "数据块长度校验失败，请检查是否开启put/get以及关闭db块优化";
            }
        }

        public virtual string SiemensDBAddressNotAllowedLargerThan255
        {
            get
            {
                return "DB块数据无法大于255";
            }
        }

        public virtual string SiemensFWError
        {
            get
            {
                return "发生了异常，具体信息查找Fetch/Write协议文档";
            }
        }

        public virtual string SiemensReadLengthCannotLargerThan19
        {
            get
            {
                return "读取的数组数量不允许大于19";
            }
        }

        public virtual string SiemensReadLengthMustBeEvenNumber
        {
            get
            {
                return "读取的数据长度必须为偶数";
            }
        }

        public virtual string SiemensReadLengthOverPlcAssign
        {
            get
            {
                return "读取的数据范围超出了PLC的设定";
            }
        }

        public virtual string SiemensWriteError
        {
            get
            {
                return "写入数据异常，代号为：";
            }
        }

        public virtual string SocketAcceptCallbackException
        {
            get
            {
                return "异步接受传入的连接尝试";
            }
        }

        public virtual string SocketContentReceiveException
        {
            get
            {
                return "内容数据接收异常：";
            }
        }

        public virtual string SocketContentRemoteReceiveException
        {
            get
            {
                return "对方内容数据接收异常：";
            }
        }

        public virtual string SocketEndReceiveException
        {
            get
            {
                return "异步数据结束接收指令头出错";
            }
        }

        public virtual string SocketEndSendException
        {
            get
            {
                return "异步数据结束挂起发送出错";
            }
        }

        public virtual string SocketHeadReceiveException
        {
            get
            {
                return "指令头接收异常：";
            }
        }

        public virtual string SocketIOException
        {
            get
            {
                return "套接字传送数据异常：";
            }
        }

        public virtual string SocketReAcceptCallbackException
        {
            get
            {
                return "重新异步接受传入的连接尝试";
            }
        }

        public virtual string SocketReceiveException
        {
            get
            {
                return "异步数据发送出错:";
            }
        }

        public virtual string SocketRemoteCloseException
        {
            get
            {
                return "远程主机强迫关闭了一个现有的连接";
            }
        }

        public virtual string SocketSendAsyncException
        {
            get
            {
                return "异步数据发送出错:";
            }
        }

        public virtual string SocketSendException
        {
            get
            {
                return "同步数据发送异常：";
            }
        }

        public virtual string SoftWare
        {
            get
            {
                return "软件：";
            }
        }

        public virtual string SuccessText
        {
            get
            {
                return "成功";
            }
        }

        public virtual string SystemInstallOperater
        {
            get
            {
                return "安装新系统：IP为";
            }
        }

        public virtual string SystemUpdateOperater
        {
            get
            {
                return "更新新系统：IP为";
            }
        }

        public virtual string TextDescription
        {
            get
            {
                return "文本描述";
            }
        }

        public virtual string Time
        {
            get
            {
                return "时间：";
            }
        }

        public virtual string TimeDescriptionDay
        {
            get
            {
                return " 天";
            }
        }

        public virtual string TimeDescriptionHour
        {
            get
            {
                return " 小时";
            }
        }

        public virtual string TimeDescriptionMinute
        {
            get
            {
                return " 分钟";
            }
        }

        public virtual string TimeDescriptionSecond
        {
            get
            {
                return " 秒";
            }
        }

        public virtual string TokenCheckFailed
        {
            get
            {
                return "接收验证令牌不一致";
            }
        }

        public virtual string TokenCheckTimeout
        {
            get
            {
                return "接收验证超时:";
            }
        }

        public virtual string TwoParametersLengthIsNotSame
        {
            get
            {
                return "两个参数的个数不一致";
            }
        }

        public virtual string UnknownError
        {
            get
            {
                return "未知错误";
            }
        }

        public virtual string YokogawaLinkError01
        {
            get
            {
                return "CPU编号超出1到4的范围";
            }
        }

        public virtual string YokogawaLinkError02
        {
            get
            {
                return "该命令不存在或该命令不可执行。";
            }
        }

        public virtual string YokogawaLinkError03
        {
            get
            {
                return "设备地址不存在，或以字为单位错误地指定了继电器进行读写访问。";
            }
        }

        public virtual string YokogawaLinkError04
        {
            get
            {
                return "超出设置范围的值：1. 0和1以外的字符用于位设置。 2.字设置超出了有效范围0000至FFFF。 3.命令中指定的起始位置（例如“加载/保存”）超出有效地址范围。";
            }
        }

        public virtual string YokogawaLinkError05
        {
            get
            {
                return "数据计数超出范围：1.指定的位数，字数等超出了规格范围。 2.指定的数据计数和设备参数计数等不匹配。";
            }
        }

        public virtual string YokogawaLinkError06
        {
            get
            {
                return "尝试执行监视而未指定监视命令（BRS，WRS）";
            }
        }

        public virtual string YokogawaLinkError07
        {
            get
            {
                return "不是BASIC CPU";
            }
        }

        public virtual string YokogawaLinkError08
        {
            get
            {
                return "由于上述原因以外的其他原因，参数无效。";
            }
        }

        public virtual string YokogawaLinkError41
        {
            get
            {
                return "通讯过程中发生错误";
            }
        }

        public virtual string YokogawaLinkError42
        {
            get
            {
                return "校验和的值不同。 （位省略或更改字符）";
            }
        }

        public virtual string YokogawaLinkError43
        {
            get
            {
                return "接收到的数据量超过规定值。";
            }
        }

        public virtual string YokogawaLinkError44
        {
            get
            {
                return "接收字符时超时：1.未接收到结束字符或ETX。 2.超时时间为5秒";
            }
        }

        public virtual string YokogawaLinkError51
        {
            get
            {
                return "超时错误：1.由于诸如CPU电源故障之类的原因，没有从CPU返回过程结束响应。（超时）2.顺序CPU硬件故障。 3.顺序CPU不接受命令。 4.顺序CPU服务时间不足";
            }
        }

        public virtual string YokogawaLinkError52
        {
            get
            {
                return "CPU在处理期间检测到错误。";
            }
        }

        public virtual string YokogawaLinkErrorF1
        {
            get
            {
                return "内部错误：1.在执行Load（PLD）或Save（PSV）命令以外的命令期间，发出了Cancel（PLC）命令。 2.检测到内部错误。";
            }
        }
    }
}

