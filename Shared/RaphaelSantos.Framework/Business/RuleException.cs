using System;

namespace RaphaelSantos.Framework.Business
{
    public sealed class RuleException : Exception
    {
        public RuleException()
            : this(new BrokenRule())
        {
        }

        public RuleException(string message)
            : this(message, new BrokenRule())
        {
        }

        public RuleException(string message, Exception innerException)
            : this(message, innerException, new BrokenRule())
        {
        }

        public RuleException(BrokenRule rule)
            : base(rule.Message)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            this.Rule = rule;
        }

        public RuleException(string message, BrokenRule rule)
            : base(message)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            if (string.IsNullOrWhiteSpace(message))
                message = rule.Message;

            rule.Message = message;
            this.Rule = rule;
        }

        public RuleException(string message, Exception innerException, BrokenRule rule)
            : base(message, innerException)
        {
        }

        public BrokenRule Rule { get; private set; }
    }
}
