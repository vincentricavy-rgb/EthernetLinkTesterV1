using EthernetLinkTester.Models;
namespace EthernetLinkTester.Services;
public sealed class Rfc2544Service(IpTestService ip){
 public async Task<List<Rfc2544Row>> Run(string host,int port,double lineRateMbps,int[] sizes,IProgress<string>? progress=null,CancellationToken ct=default){var rows=new List<Rfc2544Row>();foreach(var size in sizes){progress?.Report($"RFC2544 {size} octets : recherche throughput...");double lo=1,hi=lineRateMbps,best=0,bestLoss=100,bestJit=0;for(int i=0;i<7;i++){double mid=(lo+hi)/2;var r=await ip.UdpEcho(host,port,mid,TimeSpan.FromSeconds(2),Math.Max(64,size-42),ct);if(r.loss<=0.1){best=mid;bestLoss=r.loss;bestJit=r.jitter;lo=mid;}else hi=mid;}var p=await ip.PingStats(host,12,800);long btb=best>0?(long)(best*1e6/8*0.05/size):0;rows.Add(new(size,lineRateMbps,best,p.avg,bestLoss,btb,bestLoss<=0.1?"PASS":"FAIL"));}return rows;}
}
