namespace RSL
{
    public class ProcessOutput
    {
        public string Output;
        public string Error;

        public ProcessOutput(string output = "", string error = "")
        {
            Output = output;
            Error = error;
        }

        //Overrides the + operator for ProcessOutput so it works as expected
        public static ProcessOutput operator +(ProcessOutput a, ProcessOutput b)
        {
            return new ProcessOutput(a.Output + b.Output, a.Error + b.Error);
        }
    }
}