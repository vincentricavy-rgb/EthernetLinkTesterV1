namespace EthernetLinkTester.Models;
public record QuickResult(double MinMs,double AvgMs,double MaxMs,double JitterMs,double LossPct,double TcpMbps,double UdpMbps,int Mtu);
public record PortResult(int Port,string Protocol,string Status,double ResponseMs);
public record VlanResult(int SentVid,int? ReceivedVid,int Pcp,string Status,int Sent,int Received,double LossPct,string Detail);
public record Rfc2544Row(int FrameSize,double OfferedMbps,double ThroughputMbps,double LatencyMs,double LossPct,long BackToBack,string Status);
public record Y1564Service(string Name,int VlanId,int Pcp,double CirMbps,double EirMbps,double MaxFtdMs,double MaxFdvMs,double MaxFlrPct);
public record Y1564Result(string Service,double RateMbps,double FtdMs,double FdvMs,double FlrPct,string Phase,string Status);
public record HistoryRow(DateTime Time,string Test,string Target,string Summary,string Status);
