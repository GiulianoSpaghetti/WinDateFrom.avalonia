using System;
using WixSharp;

namespace windatefrom_setup
{
    internal class Program
    {
        static void Main()
        {
            var project = new Project("WinDateFrom.Avalonia",
                              new Dir(@"[ProgramFiles64Folder]\\WinDateFrom.Avalonia",
                                  new DirFiles(@"*.*"),
                                  new Dir("runtimes",
                                      new Dir("win-x64",
                                            new Dir("native",
                                                new File("runtimes\\win-x64\\native\\av_libglesv2.dll"),
                                                new File("runtimes\\win-x64\\native\\libHarfBuzzSharp.dll"),
                                                new File("runtimes\\win-x64\\native\\libSkiaSharp.dll")
                                            )
                                        )
                                 )
                        ),
                        new Dir(@"%ProgramMenu%",
                         new ExeFileShortcut("WinDateFrom.Avalonia", "[ProgramFiles64Folder]\\WinDateFrom.Avalonia\\WinDateFrom.Desktop.exe", "") { WorkingDirectory = "[INSTALLDIR]" }
                      )//,
                       //new Property("ALLUSERS","0")
            );

            project.GUID = new Guid("A2941143-09E9-45AD-8017-0DB4C98D80D2");
            project.Version = new Version("0.8.9");
            project.Platform = Platform.x64;
            project.SourceBaseDir = "F:\\source\\WinDateFrom.avalonia\\WinDateFrom.Desktop\\bin\\Release\\net8.0";
            project.LicenceFile = "LICENSE.rtf";
            project.OutDir = "f:\\";
            project.ControlPanelInfo.Manufacturer = "Giulio Sorrentino";
            project.ControlPanelInfo.Name = "CBriscola.Avalonia";
            project.ControlPanelInfo.HelpLink = "https://github.com/numerunix/WinDateFrom.Avalonia/issues";
            project.Description = "Un semplice programma per calcolare quanto tempo è passato da un incontro.";
            //            project.Properties.SetValue("ALLUSERS", 0);
            project.BuildMsi();
        }
    }
}