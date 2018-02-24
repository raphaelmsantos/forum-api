namespace RaphaelSantos.Framework.Business
{
    /// <summary>
    /// Field Business rule
    /// </summary>
    public class BrokenField
    {
        public BrokenField()
        {
        }

        public BrokenField(string name, string message)
        {
            this.Name = name;
            this.Message = message;
        }

        public string Name { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Name, Message);
        }
    }
}
