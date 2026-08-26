using System.Text; using System.Text.Json;
namespace EthernetLinkTester.Services;
public sealed class ReportService{
 public string ExportCsv<T>(IEnumerable<T> rows,string folder,string name){Directory.CreateDirectory(folder);var path=Path.Combine(folder,name+"_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+".csv");var p=typeof(T).GetProperties();var sb=new StringBuilder();sb.AppendLine(string.Join(";",p.Select(x=>x.Name)));foreach(var r in rows)sb.AppendLine(string.Join(";",p.Select(x=>(x.GetValue(r)?.ToString()??"").Replace(';',','))));File.WriteAllText(path,sb.ToString(),Encoding.UTF8);return path;}
 public string ExportJson<T>(IEnumerable<T> rows,string folder,string name){Directory.CreateDirectory(folder);var path=Path.Combine(folder,name+"_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+".json");File.WriteAllText(path,JsonSerializer.Serialize(rows,new JsonSerializerOptions{WriteIndented=true}));return path;}
}
