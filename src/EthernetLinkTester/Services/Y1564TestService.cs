using EthernetLinkTester.Models;
namespace EthernetLinkTester.Services;
public sealed class Y1564TestService(IpTestService ip){
 public async Task<List<Y1564Result>> Run(string host,int port,IEnumerable<EthernetLinkTester.Models.Y1564Service> services,bool performance,TimeSpan duration,IProgress<string>? progress=null,CancellationToken ct=default){var res=new List<Y1564Result>();foreach(var s in services){var rates=performance?new[]{s.CirMbps}:new[]{s.CirMbps*.25,s.CirMbps*.5,s.CirMbps*.75,s.CirMbps,Math.Max(s.CirMbps,s.CirMbps+s.EirMbps)};foreach(var rate in rates){progress?.Report($"Y.1564 {s.Name} @ {rate:F1} Mbit/s");var u=await ip.UdpEcho(host,port,Math.Max(.1,rate),performance?duration:TimeSpan.FromSeconds(2),1200,ct);var p=await ip.PingStats(host,10,800);var st=(p.avg<=s.MaxFtdMs&&u.jitter<=s.MaxFdvMs&&u.loss<=s.MaxFlrPct)?"PASS":"FAIL";res.Add(new(s.Name,rate,p.avg,u.jitter,u.loss,performance?"Performance":"Configuration",st));}}return res;}
}
