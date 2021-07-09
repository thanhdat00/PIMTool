namespace PIMTool.Client
{
    public class GlobalSingleton
    {
        private static readonly GlobalSingleton _Instance = new GlobalSingleton();
        public static GlobalSingleton Instance
        {
            get
            {
                return _Instance;
            }
        }

        private GlobalSingleton()
        {
        }

        public bool ErrorOccured { get; set; }
    }
}
