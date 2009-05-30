using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection.Emit;
using System.Reflection;

namespace TinyBDDTests.Learning
{
    [TestFixture]
    public class CreateCodeAtRuntimeTests
    {
        public void Haldis()
        {
            Console.WriteLine("Hello world!");
        }

        [Test]
        public void Create_class()
        {
            ILGenerator ilGenerator;
            AssemblyBuilder asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("Scenarios"), AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder moduleBuilder = asmBuilder.DefineDynamicModule("Scenarios", "Scenarios.dll");

            var typeBuilder = moduleBuilder.DefineType("Scenario_Checkout", TypeAttributes.Class | TypeAttributes.Public);
            var attrBuilder = new CustomAttributeBuilder(typeof(TestFixtureAttribute).GetConstructor(new Type[]{ }), new Object[]{ });
            typeBuilder.SetCustomAttribute(attrBuilder);

            var method = typeBuilder.DefineMethod("ShouldGetLatestChangeset", MethodAttributes.Public);
            attrBuilder = new CustomAttributeBuilder(typeof(TestAttribute).GetConstructor(new Type[]{ }), new Object[]{ });
            method.SetCustomAttribute(attrBuilder);
            ilGenerator = method.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldstr, "Hello world");
            ilGenerator.Emit(OpCodes.Call,
                typeof(Console).GetMethod("WriteLine", 
                    new Type[] { typeof(string)}));
            //ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ret);
            /*
            var thisType = typeof(CreateCodeAtRuntimeTests);
            var methodBody = thisType.GetMethod("Haldis").GetMethodBody().GetILAsByteArray();
            method.CreateMethodBody(methodBody, methodBody.Length);
            */
            typeBuilder.CreateType();

            asmBuilder.Save("Scenarios.dll");
        }
    }
}
