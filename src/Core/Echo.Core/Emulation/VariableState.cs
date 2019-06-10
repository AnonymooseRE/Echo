using System.Collections.Generic;
using Echo.Core.Code;
using Echo.Core.Values;

namespace Echo.Core.Emulation
{
    /// <summary>
    /// Provides a base implementation of a variable state, that initially assigns for every variable a default value.
    /// </summary>
    public class VariableState<TValue> : IVariableState<TValue>
        where TValue : IValue
    {
        private readonly IDictionary<IVariable, TValue> _variables = new Dictionary<IVariable, TValue>();
        private readonly TValue _defaultValue;

        /// <summary>
        /// Creates a new variable state snapshot, using the provided default value.
        /// </summary>
        /// <param name="defaultValue">The default value for all variables.</param>
        public VariableState(TValue defaultValue)
        {
            _defaultValue = defaultValue;
        }

        /// <inheritdoc />
        public TValue this[IVariable variable]
        {
            get => !_variables.TryGetValue(variable, out var value) ? _defaultValue : value;
            set => _variables[variable] = value;
        }

        /// <inheritdoc />
        IVariableState<TValue> IVariableState<TValue>.Copy()
        {
            return Copy();
        }
        
        /// <summary>
        /// Creates a copy of the snapshot.
        /// </summary>
        /// <returns>The copied variable state.</returns>
        public VariableState<TValue> Copy()
        {
            var result = new VariableState<TValue>(_defaultValue);

            foreach (var entry in _variables)
                result._variables.Add(entry);
            
            return result;
        }
    }
}