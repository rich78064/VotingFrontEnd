using Microsoft.AspNetCore.Identity;

namespace VotingFrontEnd.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // 使用者的 ID
        public int CandidateId { get; set; }  // 所選候選人的 ID
        public IdentityUser User { get; set; }  // 關聯 IdentityUser
        public Candidate Candidate { get; set; }  // 關聯 Candidate
    }
}
