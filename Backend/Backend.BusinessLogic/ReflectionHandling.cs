using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Backend.Tools;

namespace Backend.BusinessLogic
{
    public static class ReflectionHandling
    {
        public static T CreateInstanceImplementing<T>(string dllName, string path) where T : class
        {
            Assembly dllAssembly = GetAssembly(dllName, path);
            Type implementation = GetTypesInAssembly<T>(dllAssembly);
            CheckImplementations(implementation);
            T createdObject = (T)Activator.CreateInstance(implementation);

            return createdObject;
        }

        public static List<T> CreateAllInstancesImplementing<T>(string path) where T : class
        {
            var listT = new List<T>();
            List<Assembly> dllAssemblys = GetAllAssemblys(path);
            List<Type> implementations = new List<Type>();

            dllAssemblys.ForEach(ass =>
            {
                Type implementation = GetTypesInAssembly<T>(ass);
                if (implementation != null)
                    implementations.Add(implementation);
            });
            implementations.ToList().ForEach(impl => listT.Add((T)Activator.CreateInstance(impl)));

            return listT;
        }

        private static Assembly GetAssembly(string dll, string path)
        {
            try
            {
                FileInfo dllFile = new FileInfo(path + dll);
                return Assembly.LoadFile(dllFile.FullName);
            }
            catch { throw new BackendException("ERR_DATAIMPORT_DLL_NOT_FOUND", dll); }
        }

        private static List<Assembly> GetAllAssemblys(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();
            String assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            DirectoryInfo di = new DirectoryInfo(assemblyPath);
            FileInfo[] fil = di.GetFiles(path + "*.dll");
            foreach (FileInfo finfo in fil)
            {
                assemblies.Add(Assembly.LoadFile(finfo.FullName));
            }

            return assemblies;
        }

        private static Type GetTypesInAssembly<Interface>(Assembly assembly)
        {
            List<Type> types = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(Interface).IsAssignableFrom(type))
                    types.Add(type);
            }
            return types.FirstOrDefault();
        }

        private static void CheckImplementations(Type implementations)
        {
            if (implementations == null)
                throw new BackendException("ERR_DATAIMPORT_DLL_INVALID");
        }

    }
}
