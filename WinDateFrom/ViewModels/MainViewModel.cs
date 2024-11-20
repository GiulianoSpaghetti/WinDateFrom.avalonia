using Avalonia.Interactivity;
using DynamicData;
using System.IO;
using System;
using Avalonia.Controls;

namespace WinDateFrom.ViewModels;

public class MainViewModel : ViewModelBase
{

    private static Opzioni o;
    public static readonly string PathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"WinDateFrom");
    public static readonly string FileName = "opzioni.json";
    public static void CaricaOpzioni()
    {
        LeggiOpzioni();
    }
    private static void LeggiOpzioni()
    {
        if (!Directory.Exists(PathName))
            Directory.CreateDirectory(PathName);
        StreamReader file;
        try
        {
            file = new StreamReader(Path.Combine(PathName, FileName));
        }
        catch (FileNotFoundException ex)
        {
            DateTime d = DateTime.Now;
            o = new Opzioni();
            o.Nome = "";
            o.day = d.Day;
            o.month = d.Month;
            o.year = d.Year;
            return;
        }
        string s = file.ReadToEnd();
        file.Close();
        try
        {
            o = Newtonsoft.Json.JsonConvert.DeserializeObject<Opzioni>(s);
        } catch (Newtonsoft.Json.JsonReaderException ex)
        {
            o = null;
        } catch (Newtonsoft.Json.JsonSerializationException ex)
        {
            o = null;
        }
        if (o == null)
        {
            DateTime d = DateTime.Now;
            o = new Opzioni();
            o.Nome = "";
            o.day = d.Day;
            o.month = d.Month;
            o.year = d.Year;
            return;
        }
    }

    public static bool SalvaOpzioni(String n, int d, int m, int y)
    {
        if (d < 0 || m < 0 || y < 0)
            return false;
        o.Nome = n;
        o.day = d;
        o.month = m;
        o.year = y;
        StreamWriter w = new StreamWriter(Path.Combine(PathName, FileName));
        w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(o));
        w.Close();
        return true;
    }

    public static String GetNome() { return o.Nome; }
    public static int GetGiorno() { return o.day; }
    public static int GetMese() { return o.month; }
    public static int GetAnno() { return o.year; }
}