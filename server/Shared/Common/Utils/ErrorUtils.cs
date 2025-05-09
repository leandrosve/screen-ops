﻿using System.Reflection;

namespace Common.Utils
{
    public static class ErrorUtils
    {

        public static Dictionary<string, Dictionary<string, List<string>>> GetGroupedErrorConstants(params Type[] parentTypes)
        {
            var result = new Dictionary<string, Dictionary<string, List<string>>>();

            foreach (var type in parentTypes)
            {
                var typeName = type.Name;
                var nestedGroups = new Dictionary<string, List<string>>();

                var nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Static);
                foreach (var nested in nestedTypes)
                {
                    var nestedName = nested.Name;
                    List<string> constants = nested
                        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                        .Select(fi => fi.GetRawConstantValue()?.ToString())
                        .Where(v => v != null)
                        .ToList()!;

                    nestedGroups[nestedName] = constants;
                }

                result[typeName] = nestedGroups;
            }

            return result;
        }
    }
}
