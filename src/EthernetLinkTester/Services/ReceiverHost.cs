using System.Net; using System.Net.Sockets; using System.Diagnostics;
namespace EthernetLinkTester.Services;
public sealed class ReceiverHost : IAsyncDisposable {
 CancellationTokenSource? cts; TcpListener? tcp; UdpClient? udp; public bool Running=>cts!=null; public int Port{get;private set;}
 public Task StartAsync(int port, Action<string> log){ if(Running)return Task.CompletedTask; Port=port; cts=new(); tcp=new(IPAddress.Any,port); tcp.Start(); udp=new UdpClient(port); _=TcpLoop(cts.Token,log); _=UdpLoop(cts.Token,log); log($"Récepteur TCP/UDP actif sur {port}"); return Task.CompletedTask; }
 async Task TcpLoop(CancellationToken ct,Action<string> log){ while(!ct.IsCancellationRequested){ try{var c=await tcp!.AcceptTcpClientAsync(ct); _=Drain(c,ct,log);}catch(OperationCanceledException){break;}catch(Exception e){log("TCP RX: "+e.Message);} } }
 static async Task Drain(TcpClient c,CancellationToken ct,Action<string> log){ using(c){var s=c.GetStream(); var b=new byte[1024*1024]; long n=0; var sw=Stopwatch.StartNew(); try{for(;;){int r=await s.ReadAsync(b,ct);if(r==0)break;n+=r;}}catch{} sw.Stop(); if(sw.Elapsed.TotalSeconds>.05)log($"TCP reçu {(n*8/sw.Elapsed.TotalSeconds)/1e6:F1} Mbit/s");} }
 async Task UdpLoop(CancellationToken ct,Action<string> log){ while(!ct.IsCancellationRequested){ try{var r=await udp!.ReceiveAsync(ct); if(r.Buffer.Length>=4 && r.Buffer[0]==0x45 && r.Buffer[1]==0x4C && r.Buffer[2]==0x54) await udp.SendAsync(r.Buffer,r.Buffer.Length,r.RemoteEndPoint); }catch(OperationCanceledException){break;}catch(Exception e){log("UDP RX: "+e.Message);} } }
 public void Stop(){cts?.Cancel();tcp?.Stop();udp?.Dispose();cts?.Dispose();cts=null;tcp=null;udp=null;}
 public ValueTask DisposeAsync(){Stop();return ValueTask.CompletedTask;}
}
