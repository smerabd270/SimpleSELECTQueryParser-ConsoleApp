Regex Pattern Matching:

The query is first matched against a regex pattern to extract the SELECT, FROM, and optional WHERE clause.
The SELECT clause columns are split into an array of strings, and the table and whereClause are captured.
Column Validation:

The columns are checked to ensure they alternate between INTEGER and VARCHAR types.
A basic assumption is made that columns containing _id are INTEGER and others are VARCHAR.
WHERE Clause Validation:

The WHERE clause is split into individual conditions, and each condition is checked to ensure that the column's data type matches the expected value type (i.e., integer columns should have integer values, and varchar columns should have string values).
Execution:

The parser is tested with sample queries in the Main method.
This code will validate a simple SELECT SQL query, ensuring correct column data types and appropriate WHERE clause conditions.
And if we need we can make  the string query in pareser method as an inpit value