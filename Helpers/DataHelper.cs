using Drim.Interfaces.MVC.Connection;
using Drim.Interfaces.MVC.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Drim.Interfaces.MVC.Helpers
{
    public static class DataHelper
    {


        public static string LimpiarCaracteresEspeciales(string input)
        {
            // Define una expresión regular para buscar caracteres especiales ocultos
            string patron = @"[^\x20-\x7E]";

            // Reemplaza los caracteres especiales con una cadena vacía
            string resultado = Regex.Replace(input, patron, string.Empty);

            return resultado;
        }



        public static string ConvertValueByTypeData(this string value, string typeData, string formatdate, out object valueObjet)
        {
            string result = string.Empty;
            valueObjet = null;

            List<string> formatsData = new List<string>();
            formatsData.Add("yyyyMMdd");
            formatsData.Add("yyyyMMddHHmmss");
            formatsData.Add("yyyy-MM-dd HH:mm:ss");
            formatsData.Add("yyyy/MM/dd HH:mm:ss");
            formatsData.Add("ddMMyyyy");
            formatsData.Add("ddMMyyyyHHmmss");
            formatsData.Add("dd-MM-yyyy HH:mm:ss");
            formatsData.Add("dd/MM/yyyy");
            formatsData.Add("dd/MM/yyyy HH:mm:ss");


            List<string> formatsTime = new List<string>();
            formatsData.Add("HHmmss");
            formatsData.Add("HH:mm:ss");

            if (formatsData.Contains(value))
            {
                formatdate = value;
                value = DateTime.Now.ToString(formatdate);
            }

            if (formatsTime.Contains(value))
            {
                
                formatdate = value;
                value = DateTime.Now.ToString(formatdate);
            }



            try
            {
                switch (typeData.ToLower().Trim())
                {
                    case "int32":
                    case "int":
                        valueObjet = Convert.ToInt32(value);
                        break;
                    case "int64":
                    case "bigint":
                    case "long":
                        valueObjet = Convert.ToInt64(value);
                        break;
                    case "int16":
                    case "smallint":
                    case "short":
                        valueObjet = Convert.ToInt16(value);
                        break;
                    case "decimal":
                    case "numeric":
                        valueObjet = Convert.ToDecimal(value);
                        break;
                    case "double":
                    case "float":
                        valueObjet = Convert.ToDouble(value);
                        break;
                    case "bit":
                    case "bool":
                    case "boolean":
                        valueObjet = Convert.ToBoolean(value);
                        break;
                    case "char":
                    case "nchar":
                        valueObjet = Convert.ToChar(value);
                        break;
                    case "string":
                    case "nvarchar":
                    case "varchar":
                        valueObjet = Convert.ToString(value);
                        break;
                    case "datetime":

                        if (!string.IsNullOrEmpty(formatdate))
                        {

                            valueObjet = DateTime.ParseExact(value, formatdate, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            var datetimeStr = value.Split(new List<char> { ':', '_', '.', '/', '-' }.ToArray());
                            if (datetimeStr.Length >= 6)
                            {
                                valueObjet = new DateTime(int.Parse(datetimeStr[0]), int.Parse(datetimeStr[1]), int.Parse(datetimeStr[2]), int.Parse(datetimeStr[3]), int.Parse(datetimeStr[4]), int.Parse(datetimeStr[5]));
                            }
                            else if (value.Length >= 12)
                            {
                                valueObjet = new DateTime(int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 4)), int.Parse(value.Substring(4, 6)), int.Parse(value.Substring(6, 8)), int.Parse(value.Substring(8, 10)), int.Parse(value.Substring(10, 12)));

                            }
                        }

                        break;
                    case "time":

                        if (!string.IsNullOrEmpty(formatdate))
                        {
                            valueObjet = TimeSpan.ParseExact(value, formatdate, System.Globalization.CultureInfo.InvariantCulture);
                        }
                        {
                            var timeStr = value.Split(new List<char> { ':', '_', '.' }.ToArray());
                            if (timeStr.Length > 0)
                            {
                                valueObjet = new TimeSpan(int.Parse(timeStr[0]), int.Parse(timeStr[1]), int.Parse(timeStr[2]));
                            }
                            else if (value.Length >= 7)
                            {
                                valueObjet = new TimeSpan(int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 4)), int.Parse(value.Substring(4, 6)));
                            }
                        }
                        break;
                    default:
                        valueObjet = Convert.ToString(value);
                        break;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string NormalizeTypeData(this string value)
        {
            string result = string.Empty;

            switch (value.ToLower().Trim())
            {
                case "int32":
                case "int":
                    result = "int";
                    break;
                case "int64":
                case "bigint":
                case "long":
                    result = "bigint";
                    break;
                case "int16":
                case "smallint":
                case "short":
                    result = "smallint";
                    break;
                case "double":
                case "float":
                case "decimal":
                case "numeric":
                    result = "decimal";
                    break;
                case "bit":
                case "bool":
                case "boolean":
                    result = "bit";
                    break;
                case "char":
                case "nchar":
                    result = "char";
                    break;
                case "string":
                case "nvarchar":
                case "varchar":
                    result = "nvarchar";
                    break;
                case "datetime":
                    result = "datetime";
                    break;
                case "date":
                    result = "date";
                    break;
                default:
                    result = "nvarchar";
                    break;
            }


            return result;
        }


        public static object GetValueDefault(this string typeData)
        {

            object valueObjet = null;

            switch (typeData.ToLower().Trim())
            {
                case "int32":
                case "int":
                    valueObjet = 1;
                    break;
                case "int64":
                case "bigint":
                case "long":
                    valueObjet = 1;
                    break;
                case "int16":
                case "smallint":
                case "short":
                    valueObjet = 1;
                    break;
                case "decimal":
                case "numeric":
                    valueObjet = 1.0M;
                    break;
                case "double":
                case "float":
                    valueObjet = 1.0M;
                    break;
                case "bit":
                case "bool":
                case "boolean":
                    valueObjet = false;
                    break;
                case "char":
                case "nchar":
                    valueObjet = '1';
                    break;
                case "string":
                case "nvarchar":
                case "varchar":
                    valueObjet = "";
                    break;
                case "datetime":
                case "date":
                    valueObjet = DateTime.Now;
                    break;
                case "time":
                    valueObjet = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    break;
                default:
                    valueObjet = "";
                    break;
            }


            return valueObjet;
        }



        public static bool ValidaCadena(string cadena)
        {
            bool result = true;
            var partesOperacion = cadena.Split(new List<char> { '+', '-', '*', '/' }.ToArray());

            foreach (var operacion in partesOperacion)
            {
                if (!(operacion.StartsWith("<") || operacion.StartsWith("[")))
                    return false;

                if (!(operacion.EndsWith(">") || operacion.EndsWith("]")))
                    return false;

                if (operacion.StartsWith("<"))
                {
                    if (!operacion.EndsWith(">"))
                        return false;

                    if (operacion.Split('<').Count() > 2 || operacion.Split('>').Count() > 2 || operacion.Split('[').Count() > 1 || operacion.Split(']').Count() > 1)
                        return false;
                }

                if (operacion.StartsWith("["))
                {
                    if (!operacion.EndsWith("]"))
                        return false;

                    if (operacion.Split('<').Count() > 1 || operacion.Split('>').Count() > 1 || operacion.Split('[').Count() > 2 || operacion.Split(']').Count() > 2)
                        return false;
                }
            }

            return result;
        }


        private static bool dentroCorchetes(string cadena, int index)
        {
            int openBrackets = 0;
            for (int i = index - 1; i >= 0; i--)
            {
                if (cadena[i] == '[')
                {
                    if (openBrackets == 0)
                        return true;

                    openBrackets--;
                }
                else if (cadena[i] == ']')
                {
                    openBrackets++;
                }
            }
            return false;
        }

        private static bool dentroAngulares(string cadena, int index)
        {
            int openAngulares = 0;
            for (int i = index - 1; i >= 0; i--)
            {
                if (cadena[i] == '<')
                {
                    if (openAngulares == 0)
                        return true;

                    openAngulares--;
                }
                else if (cadena[i] == '>')
                {
                    openAngulares++;
                }
            }
            return false;
        }



        public static string GetTipoData(this string value)
        {
            if (int.TryParse(value, out _))
            {
                return "int";
            }
            else if (double.TryParse(value, out _))
            {
                return "double";
            }
            else if (bool.TryParse(value, out _))
            {
                return "bit";
            }
            else if (DateTime.TryParse(value, out _))
            {
                return "datetime";
            }
            else if (Regex.IsMatch(value, @"^\d{4}-\d{2}-\d{2}$"))
            {
                return "datetime";
            }
            else
            {
                return "nvarchar";
            }
        }

        public static string GetVariableByIndex(this int index)
        {

            switch (index)
            {
                case 22:
                    return "a";
                case 21:
                    return "b";
                case 20:
                    return "c";
                case 19:
                    return "d";
                case 18:
                    return "e";
                case 17:
                    return "f";
                case 16:
                    return "g";
                case 15:
                    return "h";
                case 14:
                    return "i";
                case 13:
                    return "j";
                case 12:
                    return "k";
                case 11:
                    return "l";
                case 10:
                    return "m";
                case 9:
                    return "n";
                case 8:
                    return "o";
                case 7:
                    return "p";
                case 6:
                    return "q";
                case 5:
                    return "r";
                case 4:
                    return "s";
                case 3:
                    return "w";
                case 2:
                    return "x";
                case 1:
                    return "y";
                case 0:
                    return "z";
                default:
                    return "a";
            }


        }


        public static object ConvertData(string value, string targetType)
        {
            Type type = Type.GetType(targetType, throwOnError: true);

            if (type == typeof(string))
            {
                // Si el tipo de dato es string, simplemente devuelve el valor de entrada
                return value;
            }
            else
            {
                // Limpiar la cadena de entrada eliminando caracteres no numéricos
                string cleanedValue = Regex.Replace(value, @"[^0-9\.,]", "");

                if (type == typeof(int) || type == typeof(double) || type == typeof(decimal) || type == typeof(float))
                {
                    // Convertir el valor numérico al tipo de dato especificado
                    return Convert.ChangeType(cleanedValue, type);
                }
                else if (type == typeof(DateTime))
                {
                    // Intentar analizar el valor como fecha con diferentes formatos
                    string[] dateFormats = { "dd/MM/yyyy", "dd-MM-yyyy", "ddMMyyyy", "ddMMyyyyHHmmss", "yyyyMMdd", "yyyy-MM-dd" };
                    DateTime result;

                    if (DateTime.TryParseExact(cleanedValue, dateFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid date format.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid targetType specified.");
                }
            }
        }


        public static List<Interfaces_Procesos_Steps_Parametros> FilterByIds(string expression, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            // Extracción de los ids entre corchetes usando LINQ y expresiones regulares
            var ids = System.Text.RegularExpressions.Regex.Matches(expression, @"\[(\d+)\]")
                                                       .Cast<System.Text.RegularExpressions.Match>()
                                                       .Select(match => long.Parse(match.Groups[1].Value))
                                                       .ToList();

            // Filtrar la lista de parametros utilizando los ids extraídos
            var filteredParametros = parametros.Where(p => ids.Contains(p.ParametroId)).ToList();

            return filteredParametros;
        }

        public static string GetDescripcionValueParametroStep(this string valueparametro, List<Interfaces_Procesos_Steps_Parametros> parametros)
        {
            var operaciones = ExtractOperations(valueparametro);
            int index = 0;
            string result = string.Empty;

            var param = FilterByIds(valueparametro, parametros);

            foreach (var par in param)
            {
                result += "[Step" + par.Step.Orden + "]." + par.NombreParametro + (operaciones != null && operaciones.Any() && operaciones.Count() > index ? operaciones[index] : "");

                index++;
            }



            return result;
        }

        public static string[] ExtractOperations(string expression)
        {
            // Extracción de los caracteres de tipo operación utilizando LINQ y expresiones regulares
            var operations = expression.Where(c => char.IsSymbol(c) && !char.IsWhiteSpace(c))
                                       .Select(c => c.ToString())
                                       .Distinct()
                                       .ToArray();

            return operations;
        }


        public static string ModifySqlQuery(string sqlQuery)
        {
            // Eliminar la cláusula WHERE
            int whereIndex = sqlQuery.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase);
            if (whereIndex >= 0)
            {
                sqlQuery = sqlQuery.Substring(0, whereIndex);
            }

            // Agregar TOP 1 al principio de la consulta
            sqlQuery = $"SELECT TOP 1 {sqlQuery.Substring("SELECT".Length)}";

            return sqlQuery;
        }

        public static DataTable GetStoredProcedureParameters(string connectionString, string storedProcedureName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"
                SELECT 
                    PARAMETER_NAME, DATA_TYPE 
                FROM INFORMATION_SCHEMA.PARAMETERS
                WHERE 
                    SPECIFIC_NAME = @storedProcedureName
                    AND ORDINAL_POSITION > 0
                ORDER BY ORDINAL_POSITION";

                command.Parameters.AddWithValue("@storedProcedureName", storedProcedureName);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable parameterTable = new DataTable();
                adapter.Fill(parameterTable);

                return parameterTable;
            }
        }





        public static string GetParametrosQuerySqlOut(string query, CommandType type, string Db, long StepId, List<Interfaces_Procesos_Steps_Parametros> parametrosIn, out List<Interfaces_Procesos_Steps_Parametros> parametrosOut)
        {
            parametrosOut = new List<Interfaces_Procesos_Steps_Parametros>();
            short? index = 0;

            string result = string.Empty;

            List<string> Tablas = new List<string>();

            if (type == CommandType.Text)
            {
                Tablas = GetTables(query);
            }
            else
            {
                var _query = GetQueryStoreProcedure(query, Db.GetDB().GetConectionString());
                Tablas = GetTables(_query);
            }


            try
            {

                using (SqlConnection connection = new SqlConnection(Db.GetDB().GetConectionString()))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = @"" + query;
                    command.CommandType = type;
                    foreach (var parametros in parametrosIn)
                    {
                        object _value = null;
                        _value = parametros.TypeDataParametro.GetValueDefault();
                        command.Parameters.AddWithValue(parametros.NombreParametro, _value);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable tablaResult = new DataTable();
                    adapter.Fill(tablaResult);


                    foreach (DataColumn column in tablaResult.Columns)
                    {

                        var columninfo = GetColumnTypeFromInformationSchema(Tablas, column.ColumnName, Db.GetDB().GetConectionString());

                        parametrosOut.Add(new Interfaces_Procesos_Steps_Parametros()
                        {
                            TipoParametros = "out",
                            NombreParametro = column.ColumnName,
                            TypeDataParametro = column.DataType.Name.ToLower().NormalizeTypeData(),
                            StepId = StepId,
                            Value_IndexParametros = index,
                            IsKey = bool.Parse(columninfo.Split("|")[1]),
                            LineaArchivo = bool.Parse(columninfo.Split("|")[1]) ? (short?)1 : (short?)2
                        });

                        index++;
                    }


                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }



        public static List<ColumnInfo> GetColumnsWithVariables(string query, string Db)
        {
            List<ColumnInfo> columnas = new List<ColumnInfo>();

            query = CleanText(query);

            if (query.ToUpper().IndexOf("UPDATE") != -1)
            {
                columnas = GetColumnsWithVariablesFromUpdateClause(query, Db);
            }
            if (query.ToUpper().IndexOf("INSERT") != -1)
            {
                columnas = GetColumnsWithVariablesFromInsertClause(query, Db);
            }

            columnas.AddRange(GetColumnsWithVariablesFromWhereClause(query, Db));

            return columnas.Distinct().ToList();
        }



        public static List<ColumnInfo> GetColumnsWithVariablesFromWhereClause(string query, string Db)
        {
            List<ColumnInfo> columnas = new List<ColumnInfo>();
            var tablas = GetTables(query);

            // Encontrar la parte del WHERE de la consulta
            int whereIndex = query.IndexOf("WHERE", StringComparison.OrdinalIgnoreCase);
            if (whereIndex >= 0)
            {
                string whereClause = query.Substring(whereIndex + 5).Trim();

                // Separar las condiciones del WHERE por el operador 'and'
                string[] condiciones = whereClause.Split(new string[] { " AND ", " OR " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string condicion in condiciones)
                {
                    string[] partes = condicion.Split(new string[] { "=", "IN", "BETWEEN" }, StringSplitOptions.RemoveEmptyEntries);

                    if (partes.Length >= 2)
                    {
                        string columna = partes[0].Trim().Replace("(", "").Replace(")", "").Replace("[", "]").Replace(" ", "");
                        string variable = partes[1].Trim().Replace("(", "").Replace(")", "").Replace("[", "]").Replace(" ", "");

                        if (variable.StartsWith("@"))
                        {

                            var tipoDato = GetColumnTypeFromInformationSchema(tablas, columna, Db.GetDB().GetConectionString());

                            columnas.Add(new ColumnInfo() { ColumnName = columna, Variable = variable, TipoDato = tipoDato.Split("|")[0], IsKey = Boolean.Parse(tipoDato.Split("|")[1]) });
                        }
                    }
                }
            }

            return columnas;
        }

        public static List<ColumnInfo> GetColumnsWithVariablesFromUpdateClause(string query, string Db)
        {
            List<ColumnInfo> columnas = new List<ColumnInfo>();
            var tablas = GetTables(query);

            int setIndex = query.ToUpper().IndexOf("SET", StringComparison.OrdinalIgnoreCase);

            int whereIndex = query.ToUpper().IndexOf("WHERE", StringComparison.OrdinalIgnoreCase);

            if (setIndex >= 0)
            {
                string whereClause = whereIndex >= 0 ? query.Substring(setIndex + 3, (whereIndex - (setIndex + 3))).Trim() : query.Substring(setIndex + 3).Trim();


                string[] condiciones = whereClause.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string condicion in condiciones)
                {
                    string[] partes = condicion.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                    if (partes.Length >= 2)
                    {
                        string columna = partes[0].Trim().Replace("(", "").Replace(")", "").Replace("[", "]").Replace(" ", "");
                        string variable = partes[1].Trim().Replace("(", "").Replace(")", "").Replace("[", "]").Replace(" ", "");

                        if (variable.StartsWith("@"))
                        {

                            var tipoDato = GetColumnTypeFromInformationSchema(tablas, columna, Db.GetDB().GetConectionString());

                            columnas.Add(new ColumnInfo() { ColumnName = columna, Variable = variable, TipoDato = tipoDato.Split("|")[0], IsKey = Boolean.Parse(tipoDato.Split("|")[1]) });
                        }
                    }
                }
            }

            return columnas;
        }


        public static List<ColumnInfo> GetColumnsWithVariablesFromInsertClause(string query, string Db)
        {
            List<ColumnInfo> columnasInfo = new List<ColumnInfo>();
            List<string> variables = new List<string>();
            List<string> columnas = new List<string>();
            var tablas = GetTables(query);

            int setIndex = query.ToUpper().IndexOf("VALUES", StringComparison.OrdinalIgnoreCase);

            int whereIndex = query.ToUpper().IndexOf(")", StringComparison.OrdinalIgnoreCase);

            if (setIndex >= 0)
            {
                string whereClause = whereIndex >= 0 ? query.Substring(setIndex + 6, (whereIndex - (setIndex + 6))).Trim() : query.Substring(setIndex + 6).Trim();
                variables = whereClause.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            setIndex = query.ToUpper().IndexOf("(", StringComparison.OrdinalIgnoreCase);

            whereIndex = query.ToUpper().IndexOf(")", StringComparison.OrdinalIgnoreCase);

            if (setIndex >= 0)
            {
                string whereClause = whereIndex >= 0 ? query.Substring(setIndex + 1, (whereIndex - (setIndex + 1))).Trim() : query.Substring(setIndex + 1).Trim();
                columnas = whereClause.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            int index = 0;
            foreach (var columna in columnas)
            {
                var tipoDato = GetColumnTypeFromInformationSchema(tablas, columna, Db.GetDB().GetConectionString());
                columnasInfo.Add(new ColumnInfo() { ColumnName = columna, Variable = variables[index], TipoDato = tipoDato.Split("|")[0], IsKey = Boolean.Parse(tipoDato.Split("|")[1]) });
                index++;
            }


            return columnasInfo;
        }



        static string CleanText(string input)
        {
            string cleaned = input.ToUpper().Replace("[", "").Replace("]", "");

            cleaned = cleaned.Replace(".", " ").Replace("DBO", " ");

            // Reemplaza múltiples espacios en blanco con un solo espacio
            cleaned = Regex.Replace(cleaned, @"\s+", " ");

            // Elimina saltos de línea y espacios en blanco al principio y al final
            cleaned = cleaned.Replace("\n", " ").Replace("\t", " ");

            return cleaned.ToUpper().Trim();
        }



        static List<ColumnInfo> GetVariables(string input, string Db)
        {
            List<ColumnInfo> response = new List<ColumnInfo>();

            List<string> variableList = new List<string>();

            // Utilizamos una expresión regular para encontrar todas las palabras que comienzan con "@"
            MatchCollection matches = Regex.Matches(input, @"@\w+");

            foreach (Match match in matches)
            {
                // Agregamos la palabra encontrada a la lista de variables
                variableList.Add(match.Value);
            }

            variableList = variableList.Distinct().ToList();

            var tablas = GetTables(input);

            foreach (var variable in variableList)
            {
                var tipoDato = GetColumnTypeFromInformationSchema(tablas, variable.Replace("@", "").ToUpper(), Db.GetDB().GetConectionString());

                response.Add(new ColumnInfo() { ColumnName = variable.Replace("@", ""), Variable = variable, TipoDato = tipoDato.Split("|")[0], IsKey = Boolean.Parse(tipoDato.Split("|")[1]) });
            }



            return response;
        }


        static List<string> GetTables(string input)
        {
            List<string> tableList = new List<string>();

            // Utilizamos una expresión regular para encontrar todas las referencias a tablas en la consulta
            MatchCollection matches = Regex.Matches(input.ToUpper(), @"\b(?:FROM|JOIN|UPDATE|INTO)\s+(\[?\w+\]?)\s+");

            foreach (Match match in matches)
            {
                // Agregamos la referencia de tabla encontrada a la lista de tablas
                string tableName = match.Groups[1].Value.Trim(new[] { '[', ']' });
                if (!string.IsNullOrEmpty(tableName) && !tableList.Contains(tableName))
                {
                    tableList.Add(tableName);
                }
            }

            return tableList;
        }




        private static string GetColumnTypeFromInformationSchema(List<string> tablas, string columnName, string connectionString)
        {
            string datetype = "nvarchar|false";



            string whereTables = tablas!= null && tablas.Any()? " AND columnas.TABLE_NAME IN(" + string.Join(",", tablas.Select(s => "'" + s + "'").ToArray()) + ")" : String.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"SELECT case when keys.COLUMN_NAME is not null then cast(1 as bit) else cast(0 as bit) end IsKey, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS columnas left join [INFORMATION_SCHEMA].[KEY_COLUMN_USAGE] keys ON columnas.COLUMN_NAME = keys.COLUMN_NAME and columnas.TABLE_NAME = keys.TABLE_NAME WHERE columnas.COLUMN_NAME = '{columnName}' " + whereTables;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            datetype = !rdr.IsDBNull(0) && !rdr.IsDBNull(1) ? rdr.GetString(1) + "|" + rdr.GetBoolean(0).ToString() : "nvarchar|false";
                        }
                    }
                }
            }

            return datetype;
        }


        private static string GetQueryStoreProcedure(string storeprocedureName, string connectionString)
        {
            string _query = string.Empty;
            string query = $"SELECT top 1 ROUTINE_DEFINITION  FROM INFORMATION_SCHEMA.ROUTINES WHERE   ROUTINE_NAME= '" + storeprocedureName + "'";


            using (DAL dal = new DAL(connectionString))
            {
                using (var dr = dal.DataReader(query, CommandType.Text)) 
                {

                    while (dr.Read()) 
                    {
                        _query = !dr.IsDBNull(0) ? dr.GetValue(0).ToString() : String.Empty;
                    }
                }
            }

            if (!string.IsNullOrEmpty(_query) && _query.ToUpper().IndexOf("SET NOCOUNT ON;") != -1 && _query.LastIndexOf("END") != -1)
            {

                _query = _query.Substring(_query.IndexOf("SET NOCOUNT ON;") + 15, _query.LastIndexOf("END") - (_query.IndexOf("SET NOCOUNT ON;") + 15));
            }

            return _query;
        }


    }
}
