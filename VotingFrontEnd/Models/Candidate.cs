namespace VotingFrontEnd.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }  // 用來存放候選人圖片的 URL
        public int VoteCount { get; set; }  // 投票數
    }
}
