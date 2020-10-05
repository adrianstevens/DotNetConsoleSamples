using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;

namespace CecilTest
{
    class MainClass
    {
        static List<string> dependencyMap;
        static bool hasDependencyMap;

        static AssemblyDefinition GetAssemblyDefinition(string fileName)
        {
            fileName = Path.Combine("TheApp", fileName);

            if(Path.GetExtension(fileName) != ".exe")
            {
                fileName += ".dll";
            }

            return AssemblyDefinition.ReadAssembly(fileName);
        }

        public static List<string> GetDependencies(AssemblyDefinition assemblyDefinition, List<string> dependencyMap)
        {
            hasDependencyMap = true;

            foreach (var ar in assemblyDefinition.MainModule.AssemblyReferences)
            {
                if (ar.FullName == assemblyDefinition.FullName)
                {
                    continue;
                }

                foreach(var a in GetAssemblyDefinition(ar.Name).MainModule.AssemblyReferences)
                {
                    if (!dependencyMap.Contains(a.Name))
                    {
                        dependencyMap.Add(a.Name);
                        GetDependencies(GetAssemblyDefinition(a.Name), dependencyMap);
                    }
                }
            }
            return dependencyMap;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            dependencyMap = new List<string>();

            GetDependencies(GetAssemblyDefinition("App.exe"), dependencyMap);

            foreach(var dependency in dependencyMap)
            {
                Console.WriteLine(dependency + ".dll");
            }
            Console.WriteLine("Complete");
        }

        static void Other()
        {
            string fileName = Path.Combine("TheApp", "app.exe");

            var def = AssemblyDefinition.ReadAssembly(fileName);

            foreach (var ar in def.MainModule.AssemblyReferences)
            {
                var found = false;

                if (ar.FullName == def.FullName)
                {
                    continue;
                }

                Console.WriteLine($"Reference found: {ar.Name}");

                string refName = Path.Combine("TheApp", $"{ar.Name}.dll");

                var reference = AssemblyDefinition.ReadAssembly(refName);

                foreach (var r in reference.MainModule.AssemblyReferences)
                {
                    if(r.Name == ar.Name) { continue; }

                    Console.WriteLine($"Dependency found: {r.Name}");
                }
            }
        }
    }
}
