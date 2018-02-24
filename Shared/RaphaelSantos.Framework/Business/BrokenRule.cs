using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaphaelSantos.Framework.Business
{
    /// <summary>
    /// Invalid business rule
    /// </summary>
    public sealed class BrokenRule
    {

        public const string DefaultMessage = "One or more fields do not meet the specified criteria.";


        public BrokenRule()
            : this(DefaultMessage)
        {
        }

        public BrokenRule(string message)
        {
            this.Message = message;
            this.Fields = new List<BrokenField>();
        }

        public BrokenRule(string message, params BrokenField[] fields)
            : this(message)
        {
            this.Fields = fields != null ? new List<BrokenField>() : fields.ToList();
        }

        public BrokenRule(string message, Dictionary<string, string> fields)
            : this(message)
        {
            IEnumerable<BrokenField> result = new BrokenField[0];

            if (fields != null && fields.Any())
            {
                result = from i in fields
                         select new BrokenField(i.Key, i.Value);
            }

            this.Fields = result.ToList();
        }


        public string Message { get; set; }

        public List<BrokenField> Fields { get; set; }

        public bool IsBroken
        {
            get
            {
                return Fields != null && Fields.Any();
            }
        }
        
        public BrokenField Add(string field, string message, params string[] args)
        {
            var text = string.Format(message, args);
            var item = new BrokenField(field, text);
            this.Fields.Add(item);
            return item;
        }

        public int Append(BrokenRule rule)
        {
            if (rule == null || rule.Fields == null)
                return 0;

            // Seleciona campos nao existentes
            var fields = from i in rule.Fields
                         where this.Fields.Any(f => string.Equals(i.Name, f.Name, StringComparison.InvariantCultureIgnoreCase))
                         select i;

            this.Fields.AddRange(fields);

            return fields.Count();
        }

        /// <summary>
        /// Lança um erro independente de qualquer campo
        /// </summary>
        /// <param name="message">Mensagem a ser exibida</param>
        public void Throw(string message)
        {
            throw new RuleException(message, this);
        }

        public void Check(string message)
        {
            if (IsBroken)
                Throw(message);
        }

        public void Check()
        {
            Check(this.Message);
        }
    }
}
