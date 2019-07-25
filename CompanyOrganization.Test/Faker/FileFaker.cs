using System.Collections.Generic;
using Faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyEmployeesBalancing.Test.Faker
{
    [TestClass]
    public static class FileFaker
    {
        public static IList<string> GetTeamsLines()
        {
            var lines = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lines.Add(string.Format("{0};{1}", CompanyFaker.Name(), NumberFaker.Number(5, 10)));
            }
            return lines;
        }

        public static IList<string> GetLinesWithInvalidValueTeams()
        {
            var lines = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lines.Add(string.Format("{0};{1}", CompanyFaker.Name(), NumberFaker.Number(5, 10)));
            }
            lines.Add(string.Format("{0};{1}", CompanyFaker.Name(), "A"));
            return lines;
        }
        public static IList<string> GetTeamsLinesWithEmptyLine()
        {
            var lines = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                AddNewTeamLine(lines);
            }
            lines.Add(string.Empty);

            AddNewTeamLine(lines);

            return lines;
        }

        private static void AddNewTeamLine(List<string> lines)
        {
            lines.Add(string.Format("{0};{1}",
                                    NameFaker.Name(),
                                    NumberFaker.Number(1, 5)));
        }

        public static IList<string> GetLinesWithOnlyName()
        {
            var lines = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                lines.Add(string.Format("{0}", CompanyFaker.Name()));
            }
            return lines;
        }

        public static IList<string> GetEmployeeLines()
        {
            var lines = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                lines.Add(
                    $"{NameFaker.Name()};{NumberFaker.Number(1, 5)};" +
                    $"{NumberFaker.Number(1970, 2000)};" +
                    $"{NumberFaker.Number(2000, 2005)};" +
                    $"{NumberFaker.Number(2006, 2010)}");
            }
            return lines;
        }

        public static IList<string> GetEmployeeLinesWithEmptyLine()
        {
            var lines = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                AddNewEmployeeLine(lines);
            }
            lines.Add(string.Empty);

            AddNewEmployeeLine(lines);

            return lines;
        }

        private static void AddNewEmployeeLine(List<string> lines)
        {
            lines.Add(
                $"{NameFaker.Name()};" +
                $"{NumberFaker.Number(1, 5)};" +
                $"{NumberFaker.Number(1970, 2000)};" +
                $"{NumberFaker.Number(2000, 2005)};" +
                $"{NumberFaker.Number(2006, 2010)}");
        }

        public static IList<string> GetLinesWithInvalidValueEmployee()
        {
            var lines = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                lines.Add(
                    $"{NameFaker.Name()};" +
                    $"{NumberFaker.Number(1, 5)};" +
                    $"{NumberFaker.Number(1970, 2000)};" +
                    $"{NumberFaker.Number(2000, 2005)};" +
                    $"{NumberFaker.Number(2006, 2010)}");
            }
            lines.Add(string.Format("{0};{1};{2};{3};{4}",
                                   NameFaker.Name(),
                                   "A",
                                   "B",
                                   "C",
                                   NumberFaker.Number(2011, 2018)));
            return lines;
        }
    }
}
