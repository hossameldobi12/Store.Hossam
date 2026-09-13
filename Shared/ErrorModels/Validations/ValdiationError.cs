namespace Domain.Exceptions.Validations
{
    public class ValdiationError
    {
        public string Field { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}