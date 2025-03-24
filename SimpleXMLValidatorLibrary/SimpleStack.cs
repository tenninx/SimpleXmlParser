namespace SimpleXmlValidatorLibrary
{
    /// <summary>
    /// A simple implementation of stack with string values
    /// </summary>
    public class SimpleStack : List<string>
    {
        public void Push(string p_strItem)
        {
            this.Add(p_strItem);
        }

        public string Pop()
        {
            if (this.Count > 0)
            {
                string result = this[Count - 1];
                this.RemoveAt(this.Count - 1);

                return result;
            }

            return string.Empty;
        }
    }
}