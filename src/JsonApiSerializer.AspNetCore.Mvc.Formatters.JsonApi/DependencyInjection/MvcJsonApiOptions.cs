using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections;
using System.Collections.Generic;

namespace JsonApiSerializer.AspNetCore.Mvc.Formatters.JsonApi.DependencyInjection
{
    public class MvcJsonApiOptions : IEnumerable<ICompatibilitySwitch>
    {
        private readonly CompatibilitySwitch<bool> _allowInputFormatterExceptionMessages;
        private readonly ICompatibilitySwitch[] _switches;

        public MvcJsonApiOptions()
        {
            _allowInputFormatterExceptionMessages = new CompatibilitySwitch<bool>(nameof(AllowInputFormatterExceptionMessages));

            _switches = new ICompatibilitySwitch[]
            {
                _allowInputFormatterExceptionMessages,
            };
        }

        public bool AllowInputFormatterExceptionMessages
        {
            get => _allowInputFormatterExceptionMessages.Value;
            set => _allowInputFormatterExceptionMessages.Value = value;
        }

        public JsonApiSerializerSettings SerializerSettings { get; } = new JsonApiSerializerSettings();

        IEnumerator<ICompatibilitySwitch> IEnumerable<ICompatibilitySwitch>.GetEnumerator()
        {
            return ((IEnumerable<ICompatibilitySwitch>)_switches).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => _switches.GetEnumerator();
    }
}