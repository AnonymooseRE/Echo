using System;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Signatures;
using AsmResolver.PE.DotNet.Cil;
using Echo.Concrete.Values.ReferenceType;
using Echo.Core.Code;
using Echo.Platforms.AsmResolver.Emulation.Values;

namespace Echo.Platforms.AsmResolver.Emulation
{
    /// <summary>
    /// Provides members for describing an environment that a .NET virtual machine runs in.
    /// </summary>
    public interface ICilRuntimeEnvironment : IDisposable
    {
        /// <summary>
        /// Gets the architecture description of the instructions to execute. 
        /// </summary>
        IInstructionSetArchitecture<CilInstruction> Architecture
        {
            get;
        }
        
        /// <summary>
        /// Gets a value indicating whether the virtual machine runs in 32-bit mode or in 64-bit mode.
        /// </summary>
        bool Is32Bit
        {
            get;
        }

        /// <summary>
        /// Gets the module that this execution context is part of. 
        /// </summary>
        ModuleDefinition Module
        {
            get;
        }

        /// <summary>
        /// Gets the object responsible for marshalling concrete values to values that can be put
        /// on the evaluation stack and back. 
        /// </summary>
        ICliMarshaller CliMarshaller
        {
            get;
        }

        /// <summary>
        /// Allocates a chunk of addressable memory on the virtual heap, and returns a pointer value to the start of
        /// the memory chunk.  
        /// </summary>
        /// <param name="size">The size of the region to allocate.</param>
        /// <param name="initializeWithZeroes">Indicates the memory region should be initialized with zeroes.</param>
        /// <returns>A pointer to the memory.</returns>
        MemoryPointerValue AllocateMemory(int size, bool initializeWithZeroes);

        /// <summary>
        /// Allocates an array on the virtual heap.
        /// </summary>
        /// <param name="elementType">The type of elements to store in the array.</param>
        /// <param name="length">The number of elements.</param>
        /// <returns>The array.</returns>
        IDotNetArrayValue AllocateArray(TypeSignature elementType, int length);

        /// <summary>
        /// Gets the string value for the fully known string literal.
        /// </summary>
        /// <param name="value">The string literal.</param>
        /// <returns>The string value.</returns>
        StringValue GetStringValue(string value);
    }
}