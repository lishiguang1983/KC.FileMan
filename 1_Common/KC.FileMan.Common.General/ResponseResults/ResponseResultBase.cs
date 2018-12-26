namespace KC.FileMan.Common.General
{
    public class ResponseResultBase
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public ResponseResultBase()
        {
            IsSuccess = true;
        }
    }
}
