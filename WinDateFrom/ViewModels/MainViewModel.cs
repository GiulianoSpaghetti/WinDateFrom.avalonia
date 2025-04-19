using Avalonia.Interactivity;
using DynamicData;
using System.IO;
using System;
using Avalonia.Controls;
using ReactiveUI;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinDateFrom.ViewModels;

public class MainViewModel : ViewModelBase
{

    internal Opzioni o;
    private readonly string PathName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"WinDateFrom");
    private readonly string FileName = "opzioni.json";
    public DateTimeOffset _data;
    public string ricorrenza = string.Empty;
    private string _nome = string.Empty;
    private string _risultato=string.Empty;
    private string _anniversario = string.Empty;
    private bool _calcolaEnable = true;
    public bool CalcolaEnable
    {
        get => _calcolaEnable;
        set => this.RaiseAndSetIfChanged(ref _calcolaEnable, value);
    }

    private bool _auguraVisible = false;
    public bool AuguraVisible
    {
        get => _auguraVisible;
        set => this.RaiseAndSetIfChanged(ref _auguraVisible, value);
    }
    public string Risultato
    {
        get => _risultato;
        set => this.RaiseAndSetIfChanged(ref _risultato, value);
    }
    public string Anniversario
    {
        get => _anniversario;
        set => this.RaiseAndSetIfChanged(ref _anniversario, value);
    }
    public string Nome
    {
        get => _nome;
        set => this.RaiseAndSetIfChanged(ref _nome, value);
    }
    public DateTimeOffset Data
    {
        get => _data;
        set => this.RaiseAndSetIfChanged(ref _data, value);
    }

    public MainViewModel()
    {
        o = new Opzioni();
        LeggiOpzioni();
        _data = new DateTimeOffset(new DateTime(o.year, o.month, o.day));
        _nome = o.Nome;
    }
    public void CaricaOpzioni()
    {
        LeggiOpzioni();

    }


    public void Calcola_Click()
    {
        _nome = _nome.Trim();
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - _data;
        if (differenza.Milliseconds < 0)
        {
            Risultato = "Invalid rvalue";
            return;
        }
        if (differenza.Days > 1)
        {
            if (d.Day == _data.Day)
                if (d.Month == _data.Month)
                {
                    Anniversario = "Is your anniversary";
                    ricorrenza = "anniversary";
                }
                else
                {
                    Anniversario = "Is your mesiversary";
                    ricorrenza = "mesiversary";
                }
        }
        if (_nome == "")
            Risultato = $"{differenza.Days} days are passed";
        else
            Risultato = $"You met {_nome} about {differenza.Days} days ago.";
        if (!SalvaOpzioni())
           Anniversario = "Impossibile salvare le opzioni";
        AuguraVisible = ricorrenza != "" && Nome != "";
        CalcolaEnable = false;
    }

    private void LeggiOpzioni()
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

    public bool SalvaOpzioni()
    {
        o.Nome = Nome;
        o.day = Data.Day;
        o.month = Data.Month;
        o.year = Data.Year;
        StreamWriter w = new StreamWriter(Path.Combine(PathName, FileName));
        w.Write(Newtonsoft.Json.JsonConvert.SerializeObject(o));
        w.Close();
        return true;
    }

}