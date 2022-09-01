using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public static class JsonFormatter
    {
        public static string Convert(object obj)
        {
            var properties = obj.GetType().GetProperties();
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            StringBuilder json = new StringBuilder("{");
            
            foreach (var property in properties)
            {

                var propertyType = property.PropertyType;
                var tempJson = string.Empty;

                if ((propertyType.IsPrimitive == true) || (propertyType == typeof(string)) || (propertyType == typeof(decimal)) || (propertyType == typeof(DateTime))) //For Properties that are of Premitive types, String, decimal and Datetime Properties

                {
                    tempJson = ObjectJsonSyntax(property, properties, obj); // Static method call for Json Syntax Format
                    json.Append(tempJson);
                }

                else if (property.ToString().StartsWith(assemblyName) && (property.PropertyType.IsArray != true)) // For properties that are a Single instance or Nested Objects of a particular class 
                {
                    var item = property.GetValue(obj);
                    var propName = "\"" + property.Name + "\"" + ":";
                    json.Append(propName);
                    json.Append(Convert(item)); // Recursion call
                    json.Append(",");
                }

                else // For properties that are List or Array of Objects and Premitive (including String, Decimal, DateTime) data types
                {

                    IEnumerable objectEnmrbl = property.GetValue(obj) as IEnumerable;
                    List<object> list = objectEnmrbl.Cast<object>().ToList();

                    var propName = "\"" + property.Name + "\"" + ":" + "[";
                    json.Append(propName);

                    if (list[0].GetType().IsPrimitive == true || list[0].GetType() == typeof(string))// For List or Array of Premitive (including String, Decimal, DateTime) data types
                    {
                        json.Append(NestedObjectJsonSyntax(list)); // Static method for Json Syntax Format
                        json.Append("],");
                    }

                    else // For List of Array of Instance Objects
                    {
                        foreach (var item in list)
                        {
                            json.Append(Convert(item));// Recursion call
                            json.Append("}");
                            if (item.Equals(list.Last()) != true)
                            {
                                json.Append(",");

                            }
                        }

                        json.Append("],");
                    }
                }
            }
            
            json.Append("}");
            json = json.Replace("}}", "}");
            json = json.Replace(",}", "}");
            return json.ToString();
        }

        public static string ObjectJsonSyntax(PropertyInfo property, PropertyInfo[] p, Object objct) //Json Syntax Formatter for Premitive, String, Decimal and DateTime type Properties 
        {
            var propertyType = property.PropertyType;
            StringBuilder tempString = new StringBuilder();

            if ((propertyType.IsPrimitive == true) || (propertyType == typeof(string)) || (propertyType == typeof(decimal))|| (propertyType == typeof(DateTime)))

            {
                var propName = property.Name;
                var propValue = property.GetValue(objct);
                var propType = propValue.GetType();
                if (propType == typeof(string) || propType == typeof(char))
                {
                    propValue = "\"" + propValue + "\"";
                }
                else if (propType == typeof(DateTime))
                {
                    var splitDateTime = propValue.ToString().Split(" ",2);
                    var date = DateTime.Parse(splitDateTime[0]).ToString("yyyy-MM-dd");
                    var time = DateTime.Parse(splitDateTime[1]).ToString("HH:mm:ss");
                    propValue = "\"" + date + "T" + time + "\"";
                }
                tempString.Append("\"" + propName + "\"" + ":" + propValue);
                if (property.Equals(p.Last()) != true)
                {
                    tempString.Append(",");
                }

            }
            return tempString.ToString();

        }

        public static string NestedObjectJsonSyntax(List<object> list)//Json Syntax Formatter for Nested Object, List and Array Properties
        {
            StringBuilder tempString = new StringBuilder();
            foreach (var item in list)
            {
                var value = item.ToString();
                if (item.GetType() == typeof(string) || item.GetType() == typeof(char))
                {
                    value = "\"" + item + "\"";
                }
                tempString.Append(value);
                if (item.Equals(list.Last()) != true)
                {
                    tempString.Append(",");

                }
            }
            return tempString.ToString();
        }

    }
}
