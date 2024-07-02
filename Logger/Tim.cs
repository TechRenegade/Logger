namespace Logger
{
    public class Tim
    {
        public DateTime timing = DateTime.Now;
        public DateTime newTime = DateTime.Now.AddSeconds(1);

        public void updateTime()
        {
            timing = newTime;
            newTime = DateTime.Now.AddSeconds(1);
        }
    }
}
