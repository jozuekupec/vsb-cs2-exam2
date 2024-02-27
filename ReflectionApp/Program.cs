using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using MyLib;
using MyLib.Models;

namespace ReflectionApp
{
    public class Program
    {
        static string _path = Path.GetFullPath("../../../../MyLib/bin/Debug/net6.0/MyLib.dll");

        public static void Main(string[] args)
        {
            // ShowMethodCallResultWithReflection();

            // string url = "/Customer/List?limit=2";
            // ProcessUrl(url);


            // string url = "/Customer/Add?Name=Pepa&Age=30&IsActive=true";
            // ProcessUrl(url);


            // ProcessCustomer();


            // ProcessCustomerWithLibInReferences();


            // CustomerAttributes();
        }

        protected static void ShowMethodCallResultWithReflection()
        {
            Assembly assembly = Assembly.LoadFile(_path);

            // Console.WriteLine(assembly.FullName);
            // foreach (Type type in assembly.GetTypes())
            // {
            //     Console.WriteLine(type.FullName);
            // }

            Type customerControllerType = assembly.GetType("MyLib.Controllers.CustomerController");
            // object customerController = assembly.CreateInstance("MyLib.Controllers.CustomerController"); // Varianta A
            object customerController = Activator.CreateInstance(customerControllerType!); // Varianta B
            MethodInfo[] customerControllerMethods = customerControllerType!.GetMethods();
            // foreach (MethodInfo methodInfo in customerControllerMethods)
            // {
            //     Console.WriteLine($"{methodInfo.Name} {methodInfo.ReturnType}");
            // }

            MethodInfo listMethod = customerControllerType.GetMethod("List");
            // foreach (ParameterInfo parameterInfo in listMethod!.GetParameters())
            // {
            //     Console.WriteLine($"{parameterInfo.Name} - {parameterInfo.ParameterType.Name}");
            // }

            string result = (string)listMethod!.Invoke(customerController, new object[] { 2 });
            Console.WriteLine(result);
        }

        protected static void ProcessUrl(string url)
        {
            string[] pathParts, queryParameters;
            string[] mainParts = url.Split("?");

            pathParts = mainParts[0].Split("/");
            string controllerName = pathParts[1];
            string methodName = pathParts[2];
            queryParameters = mainParts[1].Split("&");

            Dictionary<string, string> parameters = queryParameters
                .Select(x => x.Split("="))
                .ToDictionary(
                    x => x[0],
                    y => y[1],
                    StringComparer.OrdinalIgnoreCase
                );

            Assembly assembly = Assembly.LoadFile(_path);
            string classFullname = $"MyLib.Controllers.{controllerName}Controller";
            Type classType = assembly.GetType(classFullname);
            if (classType == null)
            {
                Console.WriteLine("404");
                return;
            }

            MethodInfo method = classType!.GetMethod(methodName);
            if (method == null)
            {
                Console.WriteLine("404");
                return;
            }

            object controllerInstance = Activator.CreateInstance(classType);
            List<object> methodArgs = new List<object>();
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                if (!parameters.TryGetValue(parameter.Name!, out string val))
                {
                    Console.WriteLine("404");
                    return;
                }

                if (parameter.ParameterType == typeof(int))
                {
                    methodArgs.Add(int.Parse(val));
                }
                else if (parameter.ParameterType == typeof(bool))
                {
                    methodArgs.Add(bool.Parse(val));
                }
                else if (parameter.ParameterType == typeof(string))
                {
                    methodArgs.Add(val);
                }
            }

            string result = (string)method.Invoke(controllerInstance, methodArgs.ToArray());
            Console.WriteLine(result);
        }

        protected static object ProcessCustomer()
        {
            string modelName = "Customer";

            Assembly assembly = Assembly.LoadFile(_path);
            string classFullname = $"MyLib.Models.{modelName}";
            Type classType = assembly.GetType(classFullname);
            if (classType == null)
            {
                Console.WriteLine("404");
                return null;
            }

            object customerInstance = Activator.CreateInstance(classType);
            // foreach (PropertyInfo property in classType.GetProperties())
            // {
            //     Console.WriteLine($"{property.Name} {property.PropertyType}");
            // }

            PropertyInfo nameProperty = classType.GetProperty("Name");
            nameProperty!.SetValue(customerInstance, "Pepa");

            Console.WriteLine(nameProperty.GetValue(customerInstance));
            return customerInstance;
        }

        protected static object ProcessCustomerWithLibInReferences()
        {
            Assembly assembly = Assembly.LoadFile(_path);
            Type classType = typeof(Customer);
            Customer customerInstance = (Customer)Activator.CreateInstance(classType);

            PropertyInfo nameProperty = classType.GetProperty("Name");
            nameProperty!.SetValue(customerInstance, "Pepa");

            Console.WriteLine(nameProperty.GetValue(customerInstance));
            return customerInstance;
        }

        protected static void CustomerAttributes()
        {
            string modelName = "Customer";

            Assembly assembly = Assembly.LoadFile(_path);
            string classFullname = $"MyLib.Models.{modelName}";
            Type classType = assembly.GetType(classFullname);
            if (classType == null)
            {
                Console.WriteLine("404");
                return;
            }

            IList<CustomAttributeData> attributes = classType!.GetCustomAttributesData();
            foreach (CustomAttributeData attribute in attributes)
            {
                Console.WriteLine(
                    $"{attribute.AttributeType}: {string.Join(", ", attribute.ConstructorArguments.Select(x => $"{x.Value} - {x.ArgumentType}"))}");
            }
        }
    }
}