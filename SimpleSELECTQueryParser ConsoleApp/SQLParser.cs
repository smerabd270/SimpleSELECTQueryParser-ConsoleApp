using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleSELECTQueryParser_ConsoleApp
{
    public class SQLParser
    {
        private string query;
        private string[] columns;
        private string table;
        private string whereClause;

        public SQLParser(string query)
        {
            this.query = query.Trim().ToLower();
        }
        public string Parse()
        {
            if (!ValidateStructure())
            {
                return "Invalid query structure.";
            }

            if (!ValidateColumns())
            {
                return "Invalid column specification. Columns must alternate between INTEGER and VARCHAR.";
            }

            if (!string.IsNullOrEmpty(whereClause) && !ValidateWhereClause())
            {
                return "Invalid WHERE clause conditions.";
            }

            return "Query is valid.";
        }

        private bool ValidateStructure()
        {
            // Regex to capture SELECT, columns, FROM, table, and optional WHERE clause
            var pattern = @"select\s+(.*?)\s+from\s+(\w+)(?:\s+where\s+(.*))?";
            var match = Regex.Match(query, pattern);

            if (!match.Success)
            {
                return false;
            }

            columns = match.Groups[1].Value.Split(',').Select(c => c.Trim()).ToArray();
            table = match.Groups[2].Value.Trim();
            whereClause = match.Groups[3].Success ? match.Groups[3].Value.Trim() : string.Empty;

            return true;
        }

        private bool ValidateColumns()
        {
            // Check if columns alternate between INTEGER and VARCHAR
            var expectedTypes = new[] { "integer", "varchar" };

            for (int i = 0; i < columns.Length; i++)
            {
                var expectedType = expectedTypes[i % 2];
                if (!ValidateColumn(columns[i], expectedType))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidateColumn(string column, string expectedType)
        {
            // For simplicity, assume all integer columns have "_id" and varchar columns don't
            if (expectedType == "integer")
            {
                return column.Contains("_id");
            }
            else if (expectedType == "varchar")
            {
                return !column.Contains("_id");
            }

            return false;
        }

        private bool ValidateWhereClause()
        {
            // Simple validation to check WHERE clause conditions
            var conditions = whereClause.Split(new[] { " and ", " or " }, StringSplitOptions.None);

            foreach (var condition in conditions)
            {
                var parts = condition.Split(new[] { '=', '<', '>', '!' }, 2);
                if (parts.Length != 2)
                {
                    return false;
                }

                var column = parts[0].Trim();
                var value = parts[1].Trim().Trim('\'');

                if (column.Contains("_id"))
                {
                    if (!int.TryParse(value, out _))
                    {
                        return false; // Integer column should have integer values
                    }
                }
                else
                {
                    if (int.TryParse(value, out _))
                    {
                        return false; // VARCHAR column should not have integer values
                    }
                }
            }

            return true;
        }

    }
}
