using System;

namespace IS4
{
    public class CustomId
    {
        private Guid id;

        public CustomId()
        {
            this.id = Guid.NewGuid();
        }

        public CustomId(Guid guid)
        {
            this.id = guid;
        }

        public override string ToString()
        {
            return id.ToString();
        }
    }
}
