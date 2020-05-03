using AsmResolver.PE.DotNet.Cil;
using AsmResolver.PE.DotNet.Metadata.Tables.Rows;
using Echo.Platforms.AsmResolver.Emulation;
using Echo.Platforms.AsmResolver.Emulation.Values;
using Echo.Platforms.AsmResolver.Emulation.Values.Cli;
using Echo.Platforms.AsmResolver.Tests.Mock;
using Xunit;

namespace Echo.Platforms.AsmResolver.Tests.Emulation.Dispatch.Arrays
{
    public class NewArrTest : DispatcherTestBase
    {
        public NewArrTest(MockModuleProvider moduleProvider)
            : base(moduleProvider)
        {
        }

        [Theory]
        [InlineData(ElementType.I)]
        [InlineData(ElementType.U)]
        [InlineData(ElementType.I1)]
        [InlineData(ElementType.I2)]
        [InlineData(ElementType.I4)]
        [InlineData(ElementType.I8)]
        [InlineData(ElementType.U1)]
        [InlineData(ElementType.U2)]
        [InlineData(ElementType.U4)]
        [InlineData(ElementType.U8)]
        [InlineData(ElementType.R4)]
        [InlineData(ElementType.R8)]
        public void NewCorLibValueTypeArray(ElementType elementType)
        {
            const int length = 10;
            var environment = ExecutionContext.GetService<ICilRuntimeEnvironment>();
            var elementTypeSig = environment.Module.CorLibTypeFactory.FromElementType(elementType);
            
            var stack = ExecutionContext.ProgramState.Stack;
            stack.Push(new I4Value(length));

            var result = Dispatcher.Execute(ExecutionContext, new CilInstruction(CilOpCodes.Newarr, elementTypeSig.Type));
            
            Assert.True(result.IsSuccess);
            Assert.IsAssignableFrom<OValue>(stack.Top);
            var array = ((OValue) stack.Top).ObjectValue;
            Assert.IsAssignableFrom<IDotNetArrayValue>(array);
            var dotNetArray = (IDotNetArrayValue) array;
            
            Assert.Equal(elementTypeSig.FullName, dotNetArray.ElementType.FullName);
            Assert.Equal(length, dotNetArray.Length);
        }
        
    }
}