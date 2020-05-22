using Colorful;
using ExpenseTracker.Common;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.SQL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using Console = Colorful.Console;

namespace ExpenseTracker.ConsoleApp
{
    class Program
    {
        private static ExpenseTrackerContext _context = new ExpenseTrackerContext();

        static void CategoriesRefiller()
        {
            var categories = _context.Categories.ToList();

            var list = new List<Category>
            {
                new Category { Name = "Zakupy" },
                new Category { Name = "Opłaty" },
                new Category { Name = "Jedzenie" },
                new Category { Name = "Jedzenie-Praca" },
                new Category { Name = "Samochód" },
                new Category { Name = "Studia" },
            };

            var listToAdd = new List<Category>();

            foreach (var item in list)
            {
                if (!categories.Any(x => x.Name.ToLower() == item.Name.ToLower()))
                {
                    listToAdd.Add(item);
                }
            }

            if (listToAdd.Count == 0)
                return;

            _context.Categories.AddRange(listToAdd);
            _context.SaveChanges();
        }

        //static void NamesRefiller()
        //{
        //    var cat = _context.Categories.ToList();
        //    var names = _context.Names.ToList();

        //    var list = new List<Name>
        //    {
        //        new Name {  Value = "Jedzenie Praca" },
        //        new Name {  Value = "Paliwo" },
        //        new Name {  Value = "ViaTOll" },
        //        new Name {  Value = "Kredyt" },
        //        new Name {  Value = "Internet" },
        //        new Name {  Value = "Allegro Raty" },
        //        new Name {  Value = "Play Abonament" },
        //        new Name {  Value = "Studia Czesne" },
        //    };

        //    var listToAdd = new List<Name>();

        //    foreach (var item in list)
        //    {
        //        if (!names.Any(x => x.Value.ToLower() == item.Value.ToLower()))
        //        {
        //            listToAdd.Add(item);
        //        }
        //    }

        //    if (listToAdd.Count == 0)
        //        return;

        //    _context.Names.AddRange(listToAdd);
        //    _context.SaveChanges();
        //}

        static void Main()
        {
            //CategoriesRefiller();


            Logger.Log<Program>($"Starting system ...");

            //try
            //{
            //    var y = 2 - 2;
            //    var x = 2 / y;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Log(ex);
            //    Logger.Log<Program>(ex);
            //    Logger.Log(ex, true);
            //    Logger.Log<Program>(ex, true);
            //}
            
            for (int i = 0; i < 5; i++)
            {
                var level = (i % 2 == 0) ? LogLevel.INFO : LogLevel.ERROR;

                Logger.Log($"Starting system ... {Guid.NewGuid().ToString()}", level);

                Thread.Sleep(1000);
            }


            //while (true)
            //{
            //    var colors = new List<Color> { Color.Blue, Color.BlueViolet, Color.AliceBlue, Color.CadetBlue, 
            //        Color.CornflowerBlue, Color.SlateBlue, Color.SteelBlue, Color.RoyalBlue };
            //    var r = new Random();
            //    var styleSheet = new StyleSheet(Color.White);

            //    var c = colors[r.Next(0, colors.Count)];

            //    styleSheet.AddStyle("TESTOWA WIDOMOSC", c);

            //    Console.WriteLineStyled($"TESTOWA WIDOMOSC - {c.ToString()}", styleSheet);

            //    Thread.Sleep(500);
            //}



            //var logger = new LogTests();
            //Log.Information($"App started ... {DateTime.Now.ToShortDateString()}");
            ////logger.Start();

            //var colors = new string[] { "red", "black", "orange", "yellow" };
            //Log.Information($"Colors: {colors}");
        }
    }
}
