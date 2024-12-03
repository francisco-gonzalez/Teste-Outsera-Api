namespace OutseraApiTest.Models
{
    public class ProducerResponse
    {
        public string Producer { get; set; }

        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
        public ProducerResponse(string producer, int interval, 
            int previousWin, int followingWin)
        {
            Producer = producer;
            Interval = interval;
            PreviousWin = previousWin;
            FollowingWin = followingWin;
        }
    }
    public class MinMaxProducerResponse
    {
        public IEnumerable<ProducerResponse> Min { get; set; }
        public IEnumerable<ProducerResponse> Max { get; set; }
        public MinMaxProducerResponse()
        {
            Min = Enumerable.Empty<ProducerResponse>();
            Max = Enumerable.Empty<ProducerResponse>();
        }
    }
}
