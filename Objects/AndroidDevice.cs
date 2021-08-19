namespace RSL
{
    public class AndroidDevice
    {
        public string ID;
        public status Status;

        public AndroidDevice(string id, status _status)
        {
            ID = id;
            Status = _status;
        }
        public enum status
        {
            device,
            offline
        }
    }
}