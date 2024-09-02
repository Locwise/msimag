namespace MSIMAG.Api
{
    public class Mapping
    {
        public string SourceField { get; set; } =String.Empty;
        public string TargetField { get; set; } = String.Empty; 
        public int TargetObjectType { get; set; }
        public string TargetObjectName { get; set; }
        public string TargetFieldNameOnFileObject { get; set; }
        public string RelatedTo { get; set; }
        public bool Override { get; set; } = false;
    }
}
