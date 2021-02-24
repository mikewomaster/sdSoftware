namespace HslCommunication.Language
{
    using HslCommunication;
    using System;

    public class English : DefaultLanguage
    {
        public override string AllenBradley04
        {
            get
            {
                return "The IOI could not be deciphered. Either it was not formed correctly or the match tag does not exist.";
            }
        }

        public override string AllenBradley05
        {
            get
            {
                return "The particular item referenced (usually instance) could not be found.";
            }
        }

        public override string AllenBradley06
        {
            get
            {
                return "The amount of data requested would not fit into the response buffer. Partial data transfer has occurred.";
            }
        }

        public override string AllenBradley0A
        {
            get
            {
                return "An error has occurred trying to process one of the attributes.";
            }
        }

        public override string AllenBradley13
        {
            get
            {
                return "Not enough command data / parameters were supplied in the command to execute the service requested.";
            }
        }

        public override string AllenBradley1C
        {
            get
            {
                return "An insufficient number of attributes were provided compared to the attribute count.";
            }
        }

        public override string AllenBradley1E
        {
            get
            {
                return "A service request in this service went wrong.";
            }
        }

        public override string AllenBradley20
        {
            get
            {
                return "The data type of the parameter in the command is inconsistent with the data type of the actual parameter.";
            }
        }

        public override string AllenBradley26
        {
            get
            {
                return "The IOI word length did not match the amount of IOI which was processed.";
            }
        }

        public override string AllenBradleySessionStatus00
        {
            get
            {
                return "success";
            }
        }

        public override string AllenBradleySessionStatus01
        {
            get
            {
                return "The sender issued an invalid or unsupported encapsulation command.";
            }
        }

        public override string AllenBradleySessionStatus02
        {
            get
            {
                return "Insufficient memory resources in the receiver to handle the command. This is not an application error. Instead, it only results if the encapsulation layer cannot obtain memory resources that it need.";
            }
        }

        public override string AllenBradleySessionStatus03
        {
            get
            {
                return "Poorly formed or incorrect data in the data portion of the encapsulation message.";
            }
        }

        public override string AllenBradleySessionStatus64
        {
            get
            {
                return "An originator used an invalid session handle when sending an encapsulation message.";
            }
        }

        public override string AllenBradleySessionStatus65
        {
            get
            {
                return "The target received a message of invalid length.";
            }
        }

        public override string AllenBradleySessionStatus69
        {
            get
            {
                return "Unsupported encapsulation protocol revision.";
            }
        }

        public override string AttemptConnectServer
        {
            get
            {
                return "Attempting to connect server {0} times";
            }
        }

        public override string AuthorizationFailed
        {
            get
            {
                return ("System authorization failed, need to use activation code authorization, thank you for your support. Active device number：" + Authorization.iahsduiwikaskfhishfdi.ToString());
            }
        }

        public override string BugSubmit
        {
            get
            {
                return "Bug submit";
            }
        }

        public override string CheckDataTimeout
        {
            get
            {
                return "When waiting to check the data, a timeout occurred. The timeout period is:";
            }
        }

        public override string ClientDisableLogin
        {
            get
            {
                return "Client [ {0} ] is not trusted, login forbidden";
            }
        }

        public override string ClientOfflineInfo
        {
            get
            {
                return "Client [ {0} ] Offline";
            }
        }

        public override string ClientOnlineInfo
        {
            get
            {
                return "Client [ {0} ] Online";
            }
        }

        public override string Close
        {
            get
            {
                return "Close";
            }
        }

        public override string CommandHeadCodeCheckFailed
        {
            get
            {
                return "Command header check failed";
            }
        }

        public override string CommandLengthCheckFailed
        {
            get
            {
                return "Command length check failed";
            }
        }

        public override string ConnectedFailed
        {
            get
            {
                return "Connected Failed: ";
            }
        }

        public override string ConnectedSuccess
        {
            get
            {
                return "Connect Success!";
            }
        }

        public override string ConnectFailedAndWait
        {
            get
            {
                return "Connection disconnected, wait {0} seconds to reconnect";
            }
        }

        public override string ConnectingServer
        {
            get
            {
                return "Connecting to Server...";
            }
        }

        public override string ConnectionIsNotAvailable
        {
            get
            {
                return "The current connection is not available";
            }
        }

        public override string ConnectServerSuccess
        {
            get
            {
                return "Connection Server succeeded";
            }
        }

        public override string ConnectTimeout
        {
            get
            {
                return "Connected Timeout: {0}";
            }
        }

        public override string DataLengthIsNotEnough
        {
            get
            {
                return "Receive length is not enough，Should:{0},Actual:{1}";
            }
        }

        public override string DataSourceFormatError
        {
            get
            {
                return "Data source format is incorrect";
            }
        }

        public override string DataTransformError
        {
            get
            {
                return "Data conversion failed, source data: ";
            }
        }

        public override string DeviceCurrentIsLoginForbidden
        {
            get
            {
                return "The ID of the current device prohibits login";
            }
        }

        public override string DeviceCurrentIsLoginRepeat
        {
            get
            {
                return "ID of the current device duplicate login";
            }
        }

        public override string DLTAddressCannotMoreThan12
        {
            get
            {
                return "Address information length cannot be greater than 12";
            }
        }

        public override string DLTAddressCannotNull
        {
            get
            {
                return "Address information cannot be empty or have a length of 0";
            }
        }

        public override string DLTAddressMatchFailed
        {
            get
            {
                return "Address format failed to match, please check whether it is less than 12 words, and are all addresses composed of 0-9 or A digits";
            }
        }

        public override string DLTErrorInfoBit0
        {
            get
            {
                return "Other errors";
            }
        }

        public override string DLTErrorInfoBit1
        {
            get
            {
                return "No data requested";
            }
        }

        public override string DLTErrorInfoBit2
        {
            get
            {
                return "Incorrect password / unauthorized";
            }
        }

        public override string DLTErrorInfoBit3
        {
            get
            {
                return "The communication rate cannot be changed";
            }
        }

        public override string DLTErrorInfoBit4
        {
            get
            {
                return "Annual time zone exceeded";
            }
        }

        public override string DLTErrorInfoBit5
        {
            get
            {
                return "Day time slot exceeded";
            }
        }

        public override string DLTErrorInfoBit6
        {
            get
            {
                return "Rates exceeded";
            }
        }

        public override string DLTErrorInfoBit7
        {
            get
            {
                return "Reserve";
            }
        }

        public override string DLTErrorWriteReadCheckFailed
        {
            get
            {
                return "Verify that the data after writing is consistent with the previous data fails";
            }
        }

        public override string ErrorCode
        {
            get
            {
                return "Error Code: ";
            }
        }

        public override string ExceptionCustomer
        {
            get
            {
                return "Error in user-defined method: ";
            }
        }

        public override string ExceptionMessage
        {
            get
            {
                return "Exception Info: ";
            }
        }

        public override string ExceptionSource
        {
            get
            {
                return "Exception Source：";
            }
        }

        public override string ExceptionStackTrace
        {
            get
            {
                return "Exception Stack: ";
            }
        }

        public override string ExceptionTargetSite
        {
            get
            {
                return "Exception Method: ";
            }
        }

        public override string ExceptionType
        {
            get
            {
                return "Exception Type：";
            }
        }

        public override string FatekStatus02
        {
            get
            {
                return "Illegal value";
            }
        }

        public override string FatekStatus03
        {
            get
            {
                return "Write disabled";
            }
        }

        public override string FatekStatus04
        {
            get
            {
                return "Invalid command code";
            }
        }

        public override string FatekStatus05
        {
            get
            {
                return "Cannot be activated (down RUN command but Ladder Checksum does not match)";
            }
        }

        public override string FatekStatus06
        {
            get
            {
                return "Cannot be activated (down RUN command but PLC ID ≠ Ladder ID)";
            }
        }

        public override string FatekStatus07
        {
            get
            {
                return "Cannot be activated (down RUN command but program syntax error)";
            }
        }

        public override string FatekStatus09
        {
            get
            {
                return "Cannot be activated (down RUN command, but the ladder program command PLC cannot be executed)";
            }
        }

        public override string FatekStatus10
        {
            get
            {
                return "Illegal address";
            }
        }

        public override string FileDeleteFailed
        {
            get
            {
                return "File Delete exception";
            }
        }

        public override string FileDeleteSuccess
        {
            get
            {
                return "File deletion succeeded";
            }
        }

        public override string FileDownloadFailed
        {
            get
            {
                return "File Download exception";
            }
        }

        public override string FileDownloadSuccess
        {
            get
            {
                return "File Download Successful";
            }
        }

        public override string FileLoadFailed
        {
            get
            {
                return "File load failed";
            }
        }

        public override string FileNotExist
        {
            get
            {
                return "File does not exist";
            }
        }

        public override string FilePathCreateFailed
        {
            get
            {
                return "Folder path creation failed: ";
            }
        }

        public override string FileReadFromNetFailed
        {
            get
            {
                return "Read file exceptions from the network";
            }
        }

        public override string FileReceiveFailed
        {
            get
            {
                return "Confirm File Receive exception";
            }
        }

        public override string FileRemoteNotExist
        {
            get
            {
                return "The other file does not exist, cannot receive!";
            }
        }

        public override string FileSaveFailed
        {
            get
            {
                return "File Store failed";
            }
        }

        public override string FileSendClientFailed
        {
            get
            {
                return "An exception occurred when the file was sent";
            }
        }

        public override string FileUploadFailed
        {
            get
            {
                return "File Upload exception";
            }
        }

        public override string FileUploadSuccess
        {
            get
            {
                return "File Upload Successful";
            }
        }

        public override string FileWriteToNetFailed
        {
            get
            {
                return "File Write Network exception";
            }
        }

        public override string FujiSpbStatus01
        {
            get
            {
                return "Write to the ROM";
            }
        }

        public override string FujiSpbStatus02
        {
            get
            {
                return "Received undefined commands or commands that could not be processed";
            }
        }

        public override string FujiSpbStatus03
        {
            get
            {
                return "There is a contradiction in the data part (parameter exception)";
            }
        }

        public override string FujiSpbStatus04
        {
            get
            {
                return "Unable to process due to transfer interlocks from other programmers";
            }
        }

        public override string FujiSpbStatus05
        {
            get
            {
                return "The module number is incorrect";
            }
        }

        public override string FujiSpbStatus06
        {
            get
            {
                return "Search item not found";
            }
        }

        public override string FujiSpbStatus07
        {
            get
            {
                return "An address that exceeds the module range (when writing) is specified";
            }
        }

        public override string FujiSpbStatus09
        {
            get
            {
                return "Unable to execute due to faulty program (RUN)";
            }
        }

        public override string FujiSpbStatus0C
        {
            get
            {
                return "Inconsistent password";
            }
        }

        public override string GeSRTPAddressCannotBeZero
        {
            get
            {
                return "The starting address of the current address cannot be 0, it needs to start from 1";
            }
        }

        public override string GeSRTPNotSupportBitReadWrite
        {
            get
            {
                return "The current address data does not support read and write operations in bit units";
            }
        }

        public override string GeSRTPNotSupportByteReadWrite
        {
            get
            {
                return "The current address data does not support read and write operations in byte units, and can only be read and written in word units";
            }
        }

        public override string GeSRTPWriteLengthMustBeEven
        {
            get
            {
                return "The length of the data written to the current address must be an even number";
            }
        }

        public override string GetClientIpAddressFailed
        {
            get
            {
                return "Client IP Address acquisition failed";
            }
        }

        public override string InsufficientPrivileges
        {
            get
            {
                return "The current method interface or class is only open to commercial authorized users with insufficient permissions. Thank you for your support. If you need commercial authorization, please contact QQ200962190, WeChat: 13516702732, Email: hsl200909@163.com";
            }
        }

        public override string IpAddressError
        {
            get
            {
                return "IP address input exception, format is incorrect";
            }
        }

        public override string KeyIsExistAlready
        {
            get
            {
                return "The current keyword already exists";
            }
        }

        public override string KeyIsNotAllowedNull
        {
            get
            {
                return "The keyword is not allowed to be empty";
            }
        }

        public override string KeyIsNotExist
        {
            get
            {
                return "The keyword for the current subscription does not exist";
            }
        }

        public override string LogNetAbandon
        {
            get
            {
                return "Abandon";
            }
        }

        public override string LogNetAll
        {
            get
            {
                return "All";
            }
        }

        public override string LogNetDebug
        {
            get
            {
                return "Debug";
            }
        }

        public override string LogNetError
        {
            get
            {
                return "Error";
            }
        }

        public override string LogNetFatal
        {
            get
            {
                return "Fatal";
            }
        }

        public override string LogNetInfo
        {
            get
            {
                return "Info";
            }
        }

        public override string LogNetWarn
        {
            get
            {
                return "Warn";
            }
        }

        public override string MailSendTail
        {
            get
            {
                return "Mail Service system issued automatically, do not reply";
            }
        }

        public override string MailServerCenter
        {
            get
            {
                return "Mail Center System";
            }
        }

        public override string MelsecCurrentTypeNotSupportedBitOperate
        {
            get
            {
                return "The current type does not support bit read and write";
            }
        }

        public override string MelsecCurrentTypeNotSupportedWordOperate
        {
            get
            {
                return "The current type does not support word read and write";
            }
        }

        public override string MelsecError02
        {
            get
            {
                return "The specified range of the \"read/write\" (in/out) device is incorrect.";
            }
        }

        public override string MelsecError51
        {
            get
            {
                return "When using random access buffer memory for communication, the start address specified by the external device is set outside the range of 0-6143. Solution: Check and correct the specified start address.";
            }
        }

        public override string MelsecError52
        {
            get
            {
                return "1. When using random access buffer memory for communication, the start address + data word count specified by the external device (depending on the setting when reading) is outside the range of 0-6143. \r\n2. Data of the specified word count (text) cannot be sent in one frame. (The data length value and the total text of the communication are not within the allowed range.)";
            }
        }

        public override string MelsecError54
        {
            get
            {
                return "When \"ASCII Communication\" is selected in [Operation Settings]-[Communication Data Code] via GX Developer, ASCII codes from external devices that cannot be converted to binary codes are received.";
            }
        }

        public override string MelsecError55
        {
            get
            {
                return "When [Operation Settings]-[Cannot Write in Run Time] cannot be set by GX Developer (No check mark), if the PLCCPU is in the running state, the external device requests to write data.";
            }
        }

        public override string MelsecError56
        {
            get
            {
                return "The device specified from the outside is incorrect.";
            }
        }

        public override string MelsecError58
        {
            get
            {
                return "1. The command start address (start device number and start step number) specified by the external device can be set outside the specified range.\r\n2. The block number specified for the extended file register does not exist.\r\n3. File register (R) cannot be specified.\r\n4. Specify the word device for the bit device command.\r\n5. The start number of the bit device is specified by a certain value. This value is not a multiple of 16 in the word device command.";
            }
        }

        public override string MelsecError59
        {
            get
            {
                return "The register of the extension file cannot be specified.";
            }
        }

        public override string MelsecErrorC04D
        {
            get
            {
                return "In the information received by the Ethernet module through automatic open UDP port communication or out-of-order fixed buffer communication, the data length specified in the application domain is incorrect.";
            }
        }

        public override string MelsecErrorC050
        {
            get
            {
                return "When the operation setting of ASCII code communication is performed in the Ethernet module, ASCII code data that cannot be converted into binary code is received.";
            }
        }

        public override string MelsecErrorC051_54
        {
            get
            {
                return "The number of read/write points is outside the allowable range.";
            }
        }

        public override string MelsecErrorC055
        {
            get
            {
                return "The number of file data read/write points is outside the allowable range.";
            }
        }

        public override string MelsecErrorC056
        {
            get
            {
                return "The read/write request exceeded the maximum address.";
            }
        }

        public override string MelsecErrorC057
        {
            get
            {
                return "The length of the requested data does not match the data count of the character area (partial text).";
            }
        }

        public override string MelsecErrorC058
        {
            get
            {
                return "After the ASCII binary conversion, the length of the requested data does not match the data count of the character area (partial text).";
            }
        }

        public override string MelsecErrorC059
        {
            get
            {
                return "The designation of commands and subcommands is incorrect.";
            }
        }

        public override string MelsecErrorC05A_B
        {
            get
            {
                return "The Ethernet module cannot read and write to the specified device.";
            }
        }

        public override string MelsecErrorC05C
        {
            get
            {
                return "The requested content is incorrect. (Request to read/write to word device in bits.)";
            }
        }

        public override string MelsecErrorC05D
        {
            get
            {
                return "Monitoring registration is not performed.";
            }
        }

        public override string MelsecErrorC05E
        {
            get
            {
                return "The communication time between the Ethernet module and the PLC CPU exceeds the time of the CPU watchdog timer.";
            }
        }

        public override string MelsecErrorC05F
        {
            get
            {
                return "The request cannot be executed on the target PLC.";
            }
        }

        public override string MelsecErrorC060
        {
            get
            {
                return "The requested content is incorrect. (Incorrect data is specified for the bit device, etc.)";
            }
        }

        public override string MelsecErrorC061
        {
            get
            {
                return "The length of the requested data does not match the number of data in the character area (partial text).";
            }
        }

        public override string MelsecErrorC062
        {
            get
            {
                return "When the online correction is prohibited, the remote protocol I/O station (QnA compatible 3E frame or 4E frame) write operation is performed by the MC protocol.";
            }
        }

        public override string MelsecErrorC070
        {
            get
            {
                return "Cannot specify the range of device memory for the target station";
            }
        }

        public override string MelsecErrorC072
        {
            get
            {
                return "The requested content is incorrect. (Request to write to word device in bit units.) ";
            }
        }

        public override string MelsecErrorC074
        {
            get
            {
                return "The target PLC does not execute the request. The network number and PC number need to be corrected.";
            }
        }

        public override string MelsecFxAckNagative
        {
            get
            {
                return "Invalid data from PLC feedback";
            }
        }

        public override string MelsecFxAckWrong
        {
            get
            {
                return "PLC Feedback Signal Error: ";
            }
        }

        public override string MelsecFxCrcCheckFailed
        {
            get
            {
                return "PLC Feedback message and check failed!";
            }
        }

        public override string MelsecFxReceiveZero
        {
            get
            {
                return "The received data length is 0";
            }
        }

        public override string MelsecPleaseReferToManualDocument
        {
            get
            {
                return "Please check Mitsubishi's communication manual for details of the alarm.";
            }
        }

        public override string MelsecReadBitInfo
        {
            get
            {
                return "The read bit variable array can only be used for bit soft elements, if you read the word soft component, call the Read method";
            }
        }

        public override string MessageTip
        {
            get
            {
                return "Message prompt:";
            }
        }

        public override string ModbusAddressMustMoreThanOne
        {
            get
            {
                return "The address value must be greater than 1 in the case where the start address is 1";
            }
        }

        public override string ModbusAsciiFormatCheckFailed
        {
            get
            {
                return "Modbus ASCII command check failed, not MODBUS-ASCII message";
            }
        }

        public override string ModbusBitIndexOverstep
        {
            get
            {
                return "The index of the bit access is out of range, it should be between 0-15";
            }
        }

        public override string ModbusCRCCheckFailed
        {
            get
            {
                return "The CRC checksum check failed for Modbus";
            }
        }

        public override string ModbusLRCCheckFailed
        {
            get
            {
                return "The LRC checksum check failed for Modbus";
            }
        }

        public override string ModbusMatchFailed
        {
            get
            {
                return "Not the standard Modbus protocol";
            }
        }

        public override string ModbusTcpFunctionCodeNotSupport
        {
            get
            {
                return "Unsupported function code";
            }
        }

        public override string ModbusTcpFunctionCodeOverBound
        {
            get
            {
                return "Data read out of bounds";
            }
        }

        public override string ModbusTcpFunctionCodeQuantityOver
        {
            get
            {
                return "Read length exceeds maximum value";
            }
        }

        public override string ModbusTcpFunctionCodeReadWriteException
        {
            get
            {
                return "Read and Write exceptions";
            }
        }

        public override string ModbusTcpReadCoilException
        {
            get
            {
                return "Read Coil anomalies";
            }
        }

        public override string ModbusTcpReadRegisterException
        {
            get
            {
                return "Read Register exception";
            }
        }

        public override string ModbusTcpWriteCoilException
        {
            get
            {
                return "Write Coil exception";
            }
        }

        public override string ModbusTcpWriteRegisterException
        {
            get
            {
                return "Write Register exception";
            }
        }

        public override string MQTTDataTooLong
        {
            get
            {
                return "The current data length exceeds the limit of the agreement";
            }
        }

        public override string MQTTStatus01
        {
            get
            {
                return "unacceptable protocol version";
            }
        }

        public override string MQTTStatus02
        {
            get
            {
                return "identifier rejected";
            }
        }

        public override string MQTTStatus03
        {
            get
            {
                return "server unavailable";
            }
        }

        public override string MQTTStatus04
        {
            get
            {
                return "bad user name or password";
            }
        }

        public override string MQTTStatus05
        {
            get
            {
                return "not authorized";
            }
        }

        public override string NetClientAccountTimeout
        {
            get
            {
                return "Wait for account check timeout：";
            }
        }

        public override string NetClientAliasFailed
        {
            get
            {
                return "Client's alias receive failed: ";
            }
        }

        public override string NetClientBreak
        {
            get
            {
                return "Abnormal offline";
            }
        }

        public override string NetClientFull
        {
            get
            {
                return "The server hosts the upper limit and receives an exceeded request connection.";
            }
        }

        public override string NetClientLoginFailed
        {
            get
            {
                return "Error in Client logon: ";
            }
        }

        public override string NetClientOffline
        {
            get
            {
                return "Offline";
            }
        }

        public override string NetClientOnline
        {
            get
            {
                return "Online";
            }
        }

        public override string NetEngineClose
        {
            get
            {
                return "Shutting down the engine";
            }
        }

        public override string NetEngineStart
        {
            get
            {
                return "Start engine";
            }
        }

        public override string NetHeartCheckFailed
        {
            get
            {
                return "Heartbeat Validation exception: ";
            }
        }

        public override string NetHeartCheckTimeout
        {
            get
            {
                return "Heartbeat verification timeout, force offline: ";
            }
        }

        public override string NotSupportedDataType
        {
            get
            {
                return "Unsupported DataType, input again";
            }
        }

        public override string NotSupportedFunction
        {
            get
            {
                return "The current feature logic does not support";
            }
        }

        public override string OmronAddressMustBeZeroToFifteen
        {
            get
            {
                return "The bit address entered can only be between 0-15";
            }
        }

        public override string OmronReceiveDataError
        {
            get
            {
                return "Data Receive exception";
            }
        }

        public override string OmronStatus0
        {
            get
            {
                return "Communication is normal.";
            }
        }

        public override string OmronStatus1
        {
            get
            {
                return "The message header is not fins";
            }
        }

        public override string OmronStatus2
        {
            get
            {
                return "Data length too long";
            }
        }

        public override string OmronStatus20
        {
            get
            {
                return "Exceeding connection limit";
            }
        }

        public override string OmronStatus21
        {
            get
            {
                return "The specified node is already in the connection";
            }
        }

        public override string OmronStatus22
        {
            get
            {
                return "Attempt to connect to a protected network node that is not yet configured in the PLC";
            }
        }

        public override string OmronStatus23
        {
            get
            {
                return "The current client's network node exceeds the normal range";
            }
        }

        public override string OmronStatus24
        {
            get
            {
                return "The current client's network node is already in use";
            }
        }

        public override string OmronStatus25
        {
            get
            {
                return "All network nodes are already in use";
            }
        }

        public override string OmronStatus3
        {
            get
            {
                return "This command does not support";
            }
        }

        public override string PanasonicAddressBitStartMulti16
        {
            get
            {
                return "The starting address for bit writing needs to be a multiple of 16, for example: R0.0, R2.0, L3.0, Y4.0";
            }
        }

        public override string PanasonicAddressParameterCannotBeNull
        {
            get
            {
                return "Address parameter is not allowed to be empty";
            }
        }

        public override string PanasonicBoolLengthMulti16
        {
            get
            {
                return "The data length written in batch bool needs to be a multiple of 16, otherwise it cannot be written";
            }
        }

        public override string PanasonicMewStatus20
        {
            get
            {
                return "Error unknown";
            }
        }

        public override string PanasonicMewStatus21
        {
            get
            {
                return "Nack error, the remote unit could not be correctly identified, or a data error occurred.";
            }
        }

        public override string PanasonicMewStatus22
        {
            get
            {
                return "WACK Error: The receive buffer for the remote unit is full.";
            }
        }

        public override string PanasonicMewStatus23
        {
            get
            {
                return "Multiple port error: The remote unit number (01 to 16) is set to repeat with the local unit.";
            }
        }

        public override string PanasonicMewStatus24
        {
            get
            {
                return "Transport format error: An attempt was made to send data that does not conform to the transport format, or a frame data overflow or a data error occurred.";
            }
        }

        public override string PanasonicMewStatus25
        {
            get
            {
                return "Hardware error: Transport system hardware stopped operation.";
            }
        }

        public override string PanasonicMewStatus26
        {
            get
            {
                return "Unit Number error: The remote unit's numbering setting exceeds the range of 01 to 63.";
            }
        }

        public override string PanasonicMewStatus27
        {
            get
            {
                return "Error not supported: Receiver data frame overflow. An attempt was made to send data of different frame lengths between different modules.";
            }
        }

        public override string PanasonicMewStatus28
        {
            get
            {
                return "No answer error: The remote unit does not exist. (timeout).";
            }
        }

        public override string PanasonicMewStatus29
        {
            get
            {
                return "Buffer Close error: An attempt was made to send or receive a buffer that is in a closed state.";
            }
        }

        public override string PanasonicMewStatus30
        {
            get
            {
                return "Timeout error: Persisted in transport forbidden State.";
            }
        }

        public override string PanasonicMewStatus40
        {
            get
            {
                return "BCC Error: A transmission error occurred in the instruction data.";
            }
        }

        public override string PanasonicMewStatus41
        {
            get
            {
                return "Malformed: The sent instruction information does not conform to the transmission format.";
            }
        }

        public override string PanasonicMewStatus42
        {
            get
            {
                return "Error not supported: An unsupported instruction was sent. An instruction was sent to a target station that was not supported.";
            }
        }

        public override string PanasonicMewStatus43
        {
            get
            {
                return "Processing Step Error: Additional instructions were sent when the transfer request information was suspended.";
            }
        }

        public override string PanasonicMewStatus50
        {
            get
            {
                return "Link Settings Error: A link number that does not actually exist is set.";
            }
        }

        public override string PanasonicMewStatus51
        {
            get
            {
                return "Simultaneous operation error: When issuing instructions to other units, the transmit buffer for the local unit is full.";
            }
        }

        public override string PanasonicMewStatus52
        {
            get
            {
                return "Transport suppression Error: Unable to transfer to other units.";
            }
        }

        public override string PanasonicMewStatus53
        {
            get
            {
                return "Busy error: Other instructions are being processed when the command is received.";
            }
        }

        public override string PanasonicMewStatus60
        {
            get
            {
                return "Parameter error: Contains code that cannot be used in the directive, or the code does not have a zone specified parameter (X, Y, D), and so on.";
            }
        }

        public override string PanasonicMewStatus61
        {
            get
            {
                return "Data error: Contact number, area number, Data code format (BCD,HEX, etc.) overflow, overflow, and area specified error.";
            }
        }

        public override string PanasonicMewStatus62
        {
            get
            {
                return "Register ERROR: Excessive logging of data in an unregistered state of operations (Monitoring records, tracking records, etc.). )。";
            }
        }

        public override string PanasonicMewStatus63
        {
            get
            {
                return "PLC mode error: When an instruction is issued, the run mode is not able to process the instruction.";
            }
        }

        public override string PanasonicMewStatus65
        {
            get
            {
                return "Protection Error: Performs a write operation to the program area or system register in the storage protection state.";
            }
        }

        public override string PanasonicMewStatus66
        {
            get
            {
                return "Address Error: Address (program address, absolute address, etc.) Data encoding form (BCD, hex, etc.), overflow, underflow, or specified range error.";
            }
        }

        public override string PanasonicMewStatus67
        {
            get
            {
                return "Missing data error: The data to be read does not exist. (reads data that is not written to the comment register.)";
            }
        }

        public override string PanasonicReceiveLengthMustLargerThan9
        {
            get
            {
                return "The received data length must be greater than 9";
            }
        }

        public override string PasswordCheckFailed
        {
            get
            {
                return "Password validation failed";
            }
        }

        public override string Receive
        {
            get
            {
                return "Receive";
            }
        }

        public override string ReceiveDataLengthTooShort
        {
            get
            {
                return "Receive length is too short: ";
            }
        }

        public override string ReceiveDataTimeout
        {
            get
            {
                return "Receive timeout: ";
            }
        }

        public override string ReConnectServerAfterTenSeconds
        {
            get
            {
                return "Reconnect the server after 10 seconds";
            }
        }

        public override string ReConnectServerSuccess
        {
            get
            {
                return "Re-connect server succeeded";
            }
        }

        public override string RemoteClosedConnection
        {
            get
            {
                return "Remote shutdown of connection";
            }
        }

        public override string SAMAddressStartWrong
        {
            get
            {
                return "SAM string address identification error.";
            }
        }

        public override string SAMHeadCheckFailed
        {
            get
            {
                return "Data frame header check failed for SAM。";
            }
        }

        public override string SAMLengthCheckFailed
        {
            get
            {
                return "Data length header check failed for SAM。";
            }
        }

        public override string SAMReceiveLengthMustLargerThan8
        {
            get
            {
                return "Received data length is less than 8, must be greater than 8";
            }
        }

        public override string SAMStatus10
        {
            get
            {
                return "Received data checksum error";
            }
        }

        public override string SAMStatus11
        {
            get
            {
                return "Received data length error";
            }
        }

        public override string SAMStatus21
        {
            get
            {
                return "Receive data command error";
            }
        }

        public override string SAMStatus23
        {
            get
            {
                return "Unauthorized operation";
            }
        }

        public override string SAMStatus24
        {
            get
            {
                return "Unrecognized error";
            }
        }

        public override string SAMStatus31
        {
            get
            {
                return "Card authentication SAM failed";
            }
        }

        public override string SAMStatus32
        {
            get
            {
                return "SAM certificate / card failed";
            }
        }

        public override string SAMStatus33
        {
            get
            {
                return "Information validation error";
            }
        }

        public override string SAMStatus40
        {
            get
            {
                return "Unrecognized card type";
            }
        }

        public override string SAMStatus41
        {
            get
            {
                return "ID / card operation failed";
            }
        }

        public override string SAMStatus47
        {
            get
            {
                return "Random number failed";
            }
        }

        public override string SAMStatus60
        {
            get
            {
                return "SAM Self-test failed";
            }
        }

        public override string SAMStatus66
        {
            get
            {
                return "SAM unauthorized";
            }
        }

        public override string SAMStatus80
        {
            get
            {
                return "Failed to find card";
            }
        }

        public override string SAMStatus81
        {
            get
            {
                return "Select card failed";
            }
        }

        public override string SAMStatus90
        {
            get
            {
                return "Successful operation";
            }
        }

        public override string SAMStatus91
        {
            get
            {
                return "No content in the card";
            }
        }

        public override string SAMStatus9F
        {
            get
            {
                return "Find card success";
            }
        }

        public override string SAMSumCheckFailed
        {
            get
            {
                return "SAM's data checksum check failed.";
            }
        }

        public override string Send
        {
            get
            {
                return "Send";
            }
        }

        public override string ServerFileCheckFailed
        {
            get
            {
                return "Server confirmed file failed, please re-upload";
            }
        }

        public override string SiemensDataLengthCheckFailed
        {
            get
            {
                return "Block length checksum failed, please check if Put/get is turned on and DB block optimization is turned off";
            }
        }

        public override string SiemensDBAddressNotAllowedLargerThan255
        {
            get
            {
                return "DB block data cannot be greater than 255";
            }
        }

        public override string SiemensFWError
        {
            get
            {
                return "An exception occurred, the specific information to find the Fetch/write protocol document";
            }
        }

        public override string SiemensReadLengthCannotLargerThan19
        {
            get
            {
                return "The number of arrays read does not allow greater than 19";
            }
        }

        public override string SiemensReadLengthMustBeEvenNumber
        {
            get
            {
                return "The length of the data read must be an even number";
            }
        }

        public override string SiemensReadLengthOverPlcAssign
        {
            get
            {
                return "The range of data read exceeds the setting of the PLC";
            }
        }

        public override string SiemensWriteError
        {
            get
            {
                return "Writes the data exception, the code name is: ";
            }
        }

        public override string SocketAcceptCallbackException
        {
            get
            {
                return "Asynchronously accepts an incoming connection attempt: ";
            }
        }

        public override string SocketContentReceiveException
        {
            get
            {
                return "Content Data Receive exception: ";
            }
        }

        public override string SocketContentRemoteReceiveException
        {
            get
            {
                return "Recipient content Data Receive exception: ";
            }
        }

        public override string SocketEndReceiveException
        {
            get
            {
                return "Asynchronous data end receive instruction header error";
            }
        }

        public override string SocketEndSendException
        {
            get
            {
                return "Asynchronous data end callback send Error";
            }
        }

        public override string SocketHeadReceiveException
        {
            get
            {
                return "Command header receive exception: ";
            }
        }

        public override string SocketIOException
        {
            get
            {
                return "Socket transport error: ";
            }
        }

        public override string SocketReAcceptCallbackException
        {
            get
            {
                return "To re-accept incoming connection attempts asynchronously";
            }
        }

        public override string SocketReceiveException
        {
            get
            {
                return "Asynchronous Data send Error: ";
            }
        }

        public override string SocketRemoteCloseException
        {
            get
            {
                return "An existing connection was forcibly closed by the remote host";
            }
        }

        public override string SocketSendAsyncException
        {
            get
            {
                return "Asynchronous Data send Error: ";
            }
        }

        public override string SocketSendException
        {
            get
            {
                return "Synchronous Data Send exception: ";
            }
        }

        public override string SoftWare
        {
            get
            {
                return "Software:";
            }
        }

        public override string SuccessText
        {
            get
            {
                return "Success";
            }
        }

        public override string SystemInstallOperater
        {
            get
            {
                return "Install new software: ip address is";
            }
        }

        public override string SystemUpdateOperater
        {
            get
            {
                return "Update software: ip address is";
            }
        }

        public override string TextDescription
        {
            get
            {
                return "Description: ";
            }
        }

        public override string Time
        {
            get
            {
                return "Time:";
            }
        }

        public override string TimeDescriptionDay
        {
            get
            {
                return " Day";
            }
        }

        public override string TimeDescriptionHour
        {
            get
            {
                return " Hour";
            }
        }

        public override string TimeDescriptionMinute
        {
            get
            {
                return " Minute";
            }
        }

        public override string TimeDescriptionSecond
        {
            get
            {
                return " Second";
            }
        }

        public override string TokenCheckFailed
        {
            get
            {
                return "Receive authentication token inconsistency";
            }
        }

        public override string TokenCheckTimeout
        {
            get
            {
                return "Receive authentication timeout: ";
            }
        }

        public override string TwoParametersLengthIsNotSame
        {
            get
            {
                return "Two Parameter Length is not same";
            }
        }

        public override string UnknownError
        {
            get
            {
                return "Unknown Error";
            }
        }

        public override string YokogawaLinkError01
        {
            get
            {
                return " The CPU number is outside the range of 1 to 4";
            }
        }

        public override string YokogawaLinkError02
        {
            get
            {
                return "The command does not exist or the command is not executable.";
            }
        }

        public override string YokogawaLinkError03
        {
            get
            {
                return "The device name does not exist or A relay device is incorrectly specified for read/write access in word units.";
            }
        }

        public override string YokogawaLinkError04
        {
            get
            {
                return "Value outside the setting range: 1. Characters other than 0 and 1 are used for bit setting. 2. Word setting is out of the valid range of 0000 to FFFF. 3. The specified starting position in a command, such as Load/Save, is out of the valid address range.";
            }
        }

        public override string YokogawaLinkError05
        {
            get
            {
                return "Data count out of range: 1. The specified bit count, word count, etc. exceeded the specifications range. 2. The specified data count and the device parameter count, etc. do not match.";
            }
        }

        public override string YokogawaLinkError06
        {
            get
            {
                return "Attempted to execute monitoring without having specified a monitor command( BRS, WRS)";
            }
        }

        public override string YokogawaLinkError07
        {
            get
            {
                return "Not a BASIC CPU";
            }
        }

        public override string YokogawaLinkError08
        {
            get
            {
                return "A parameter is invalid for a reason other than those given above.";
            }
        }

        public override string YokogawaLinkError41
        {
            get
            {
                return "An error has occurred during communication";
            }
        }

        public override string YokogawaLinkError42
        {
            get
            {
                return "Value of checksum differs. (Bit omitted or changed characters)";
            }
        }

        public override string YokogawaLinkError43
        {
            get
            {
                return "The amount of data received exceeded stipulated value.";
            }
        }

        public override string YokogawaLinkError44
        {
            get
            {
                return "Timeout while receiving characters: 1. No End character or ETX was received. 2. Timeout duration is 5 seconds";
            }
        }

        public override string YokogawaLinkError51
        {
            get
            {
                return "Timeout error: 1. No end-of-process response is returned from the CPU for reasons such as CPU power failure.(timeout) 2. Sequence CPU hardware failure. 3. Sequence CPU is not accepting commands. 4. Insufficient sequence CPU service time";
            }
        }

        public override string YokogawaLinkError52
        {
            get
            {
                return "The CPU has detected an error during processing. ";
            }
        }

        public override string YokogawaLinkErrorF1
        {
            get
            {
                return "Internal error: 1. A Cancel (PLC) command was issued during execution of a command other than a Load( PLD) or Save( PSV) command. 2. An internal error was detected.";
            }
        }
    }
}

