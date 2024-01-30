// Decompiled with JetBrains decompiler
// Type: PDFtoWORD.Program
// Assembly: PDFtoWORD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 04E2525B-49CD-437A-AAAB-5C0C310B7016
// Assembly location: E:\АРХИВ\БГУ АРХИВ\PDFtoWORD.exe

using SautinSoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PDFtoWORD
{
  internal class Program
  {
    private static void showInfo(int countFiles)
    {
      Console.WriteLine("Эта программа преобразует PDF-файлы в docx");
      Console.WriteLine("Поместите файлы PDF рядом с исполняемым файлом");
      Console.WriteLine("Найдено файлов: " + countFiles.ToString());
      Console.WriteLine();
      Console.WriteLine("Комманды: ");
      Console.WriteLine("Обновить список файлов: \tupdate");
      Console.WriteLine("Показать список файлов: \tshow");
      Console.WriteLine("Очистить консоль: \t\tclear");
      Console.WriteLine("Преобразовать файлы: \t\tconvert");
    }

    private static void Main(string[] args)
    {
      string currentDirectory = Directory.GetCurrentDirectory();
      int num = 0;
      do
      {
        string[] files = Directory.GetFiles(currentDirectory, "*.pdf");
        PdfFocus pdfFocus1 = new PdfFocus();
        Program.showInfo(((IEnumerable<string>) files).Count<string>());
        switch (Console.ReadLine())
        {
          case "update":
            Console.Clear();
            break;
          case "show":
            Console.Clear();
            foreach (string str in files)
              Console.WriteLine(str);
            Console.WriteLine();
            break;
          case "clear":
            Console.Clear();
            break;
          case "convert":
            foreach (string str in files)
            {
              PdfFocus pdfFocus2 = new PdfFocus();
              pdfFocus2.OpenPdf(str);
              if (pdfFocus2.PageCount > 0)
                pdfFocus2.ToWord(str.Replace(".pdf", ".docx"));
            }
            Console.Clear();
            Console.WriteLine("Всё готово.");
            num = 1;
            break;
          default:
            Console.Clear();
            Console.WriteLine("Неизвестная команда. Попробуй ещё раз");
            Console.WriteLine();
            break;
        }
      }
      while (num == 0);
    }
  }
}
