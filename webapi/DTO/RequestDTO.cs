namespace webapi.DTO
{
    public class RequestDTO
    {
        public int width { get; set; }
        public int height { get; set; }
        public int length { get; set; }
        public string Currency { get; set; }
        public int Weight { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DeliveryDay { get; set; }
        public bool DeliveryInWeekend { get; set; }
        public string prio { get; set; }
    }

    public class RequestKurierApi
    {
        public int width { get; set; }
        public int height { get; set; }
        public int length { get; set; }
        public string Currency { get; set; }
        public int Weight { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DeliveryDay { get; set; }
        public bool DeliveryInWeekend { get; set; }

        public string prio { get; set; }

    }
}
